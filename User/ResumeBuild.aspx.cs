using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;


namespace OnlineJobPortal.User
{
    public partial class ResumeBuild : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter sda;
        SqlDataReader dr;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            fnConnectDB();
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (Page.IsPostBack == false)
            {
                fnBindCountry();
                showUserInfo();

            }
        }

        protected void fnConnectDB()
        {
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["Myconstr"].ConnectionString;
                conn = new SqlConnection(strcon);
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    //Response.Write("<script>alert('Connected Successfully')</script>");
                }
            }
            catch (Exception ex)
            {

                Response.Write(ex.ToString());
            }
        }

        protected void fnBindCountry()
        {
            DataSet ds = new DataSet();
            try
            {
                fnConnectDB();
                String qry = "SELECT Cou_id, C_name FROM tblCountry";
                cmd = new SqlCommand(qry, conn);
                sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
                ddlCountry.DataSource = ds;
                ddlCountry.DataTextField = "C_name";
                ddlCountry.DataValueField = "Cou_id";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("Select Country"));
                conn.Close();
            }
            catch (Exception ex)
            {

                Response.Write(ex.ToString());
            }
        }

        protected void showUserInfo()
        {
            if (Request.QueryString["id"] != null)
            {
                fnConnectDB();

                try
                {
                    string qry = "SELECT * FROM tblUser WHERE u_id = @u_id";
                    cmd = new SqlCommand(qry, conn);
                    cmd.Parameters.AddWithValue("@u_id", Request.QueryString["id"]);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        if (dr.Read())
                        {
                            txtUsername.Text = dr["U_name"].ToString();
                            txtFullName.Text = dr["Name"].ToString();
                            txtEmail.Text = dr["Email"].ToString();
                            txtMobile.Text = dr["Mobile"].ToString();
                            txtTenth.Text = dr["Tenth_grade"].ToString();
                            txtTwelfth.Text = dr["Tewelfth_grade"].ToString();
                            txtGraduation.Text = dr["Graduation_grade"].ToString();
                            txtPostGraduation.Text = dr["P_graduation_grade"].ToString();
                            txtPhd.Text = dr["Phd"].ToString();
                            txtWork.Text = dr["Workson"].ToString();
                            txtExperience.Text = dr["Experience"].ToString();
                            txtAddress.Text = dr["Address"].ToString();

                            string countryValue = dr["Country"].ToString();

                            if (ddlCountry.Items.FindByValue(countryValue) != null)
                            {
                                ddlCountry.SelectedValue = countryValue;
                            }
                            else
                            {
                                lblMsg.Text = "Country value not found in dropdown!";
                            }

                        }
                    }
                    else
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "User not found..!";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                    dr.Close();
                }
                catch (Exception ex)
                {

                    lblMsg.Text = "An error occurred: " + ex.Message;
                    lblMsg.CssClass = "alert alert-danger";
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            fnConnectDB();  // Ensure the connection to the database

            string filePath = "~/Upload/" + fuResume.FileName;
            string img = "~/Upload/" + fuProfile.FileName;

            string type;
            string fileExtension = System.IO.Path.GetExtension(fuResume.FileName).ToLower();
            string[] allowedExtensions = { ".doc", ".docx", ".pdf" };

            string fileExtension1 = System.IO.Path.GetExtension(fuProfile.FileName).ToLower();
            string[] allowedExtensions1 = { ".jpg", ".jpeg", ".png" };

            byte[] fileData = null;

            // Fetch the selected country name
            string qryCountry = "SELECT C_name FROM tblCountry WHERE Cou_id = @Cou_id";
            cmd = new SqlCommand(qryCountry, conn);
            cmd.Parameters.AddWithValue("@Cou_id", ddlCountry.SelectedValue);
            string countryName = cmd.ExecuteScalar()?.ToString();

            try
            {
                if (Request.QueryString["id"] != null)
                {
                    if (fuResume.HasFile)
                    {
                        if (allowedExtensions.Contains(fileExtension))
                        {
                            // Convert the uploaded file into a byte array
                            fileData = fuResume.FileBytes;

                            string qry = "Update tblUser set U_name=@U_name, Name=@Name, Email=@Email, Mobile=@Mobile, " +
                                         "Tenth_grade=@Tenth_grade, Tewelfth_grade=@Tewelfth_grade, Graduation_grade=@Graduation_grade, " +
                                         "P_graduation_grade=@P_graduation_grade, Phd=@Phd, Workson=@Workson, Experience=@Experience, " +
                                         "Resume=@Resume, Address=@Address, Country=@Country WHERE u_id = @u_id";
                            type = "Updated";

                            cmd = new SqlCommand(qry, conn);

                            // Bind parameters
                            cmd.Parameters.AddWithValue("@U_name", txtUsername.Text);
                            cmd.Parameters.AddWithValue("@Name", txtFullName.Text);
                            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                            cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
                            cmd.Parameters.AddWithValue("@Tenth_grade", txtTenth.Text);
                            cmd.Parameters.AddWithValue("@Tewelfth_grade", txtTwelfth.Text);
                            cmd.Parameters.AddWithValue("@Graduation_grade", txtGraduation.Text);
                            cmd.Parameters.AddWithValue("@P_graduation_grade", txtPostGraduation.Text);
                            cmd.Parameters.AddWithValue("@Phd", txtPhd.Text);
                            cmd.Parameters.AddWithValue("@Workson", txtWork.Text);
                            cmd.Parameters.AddWithValue("@Experience", txtExperience.Text);
                            cmd.Parameters.AddWithValue("@Resume", fileData);  // Insert the binary file data
                            cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                            cmd.Parameters.AddWithValue("@Country", countryName);
                            //cmd.Parameters.AddWithValue("@profile", img);
                            cmd.Parameters.AddWithValue("@u_id", Request.QueryString["id"].ToString());

                            int res = cmd.ExecuteNonQuery();

                            // Save the file on the server
                            fuResume.SaveAs(Server.MapPath(filePath));
                            //fuProfile.SaveAs(Server.MapPath(img));

                            if (res > 0)
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = "Resume Details " + type + " Successfully!";
                                lblMsg.CssClass = "alert alert-success";
                                clear();
                            }
                            else
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = "Cannot update the records, please try again later!";
                                lblMsg.CssClass = "alert alert-danger";
                            }
                        }
                        else
                        {
                            // Invalid file extension
                            lblMsg.Visible = true;
                            lblMsg.Text = "Invalid file type. Only .doc, .docx, and .pdf are allowed.";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                    }
                    else if (fuProfile.HasFiles)
                    {
                        if (allowedExtensions1.Contains(fileExtension1))
                        {
                            string qry = "Update tblUser set U_name=@U_name, Name=@Name, Email=@Email, Mobile=@Mobile, " +
                                         "Tenth_grade=@Tenth_grade, Tewelfth_grade=@Tewelfth_grade, Graduation_grade=@Graduation_grade, " +
                                         "P_graduation_grade=@P_graduation_grade, Phd=@Phd, Workson=@Workson, Experience=@Experience, " +
                                         " Address=@Address, Country=@Country, profile=@profile WHERE u_id = @u_id";
                            type = "Updated";

                            cmd = new SqlCommand(qry, conn);

                            // Bind parameters
                            cmd.Parameters.AddWithValue("@U_name", txtUsername.Text);
                            cmd.Parameters.AddWithValue("@Name", txtFullName.Text);
                            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                            cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
                            cmd.Parameters.AddWithValue("@Tenth_grade", txtTenth.Text);
                            cmd.Parameters.AddWithValue("@Tewelfth_grade", txtTwelfth.Text);
                            cmd.Parameters.AddWithValue("@Graduation_grade", txtGraduation.Text);
                            cmd.Parameters.AddWithValue("@P_graduation_grade", txtPostGraduation.Text);
                            cmd.Parameters.AddWithValue("@Phd", txtPhd.Text);
                            cmd.Parameters.AddWithValue("@Workson", txtWork.Text);
                            cmd.Parameters.AddWithValue("@Experience", txtExperience.Text);
                            cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                            cmd.Parameters.AddWithValue("@Country", countryName);
                            cmd.Parameters.AddWithValue("@profile", img);
                            cmd.Parameters.AddWithValue("@u_id", Request.QueryString["id"].ToString());

                            int res = cmd.ExecuteNonQuery();

                            // Save the file on the server
                            fuProfile.SaveAs(Server.MapPath(img));

                            if (res > 0)
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = "Resume Details " + type + " Successfully!";
                                lblMsg.CssClass = "alert alert-success";
                                clear();
                            }
                            else
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = "Cannot update the records, please try again later!";
                                lblMsg.CssClass = "alert alert-danger";
                            }
                        }
                        else
                        {
                            // Invalid file extension
                            lblMsg.Visible = true;
                            lblMsg.Text = "Invalid file type. Only .jpg, .jpeg, and .png are allowed.";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                    }
                    else
                    {
                        string qry = "Update tblUser set U_name=@U_name, Name=@Name, Email=@Email, Mobile=@Mobile, " +
                                     "Tenth_grade=@Tenth_grade, Tewelfth_grade=@Tewelfth_grade, Graduation_grade=@Graduation_grade, " +
                                     "P_graduation_grade=@P_graduation_grade, Phd=@Phd, Workson=@Workson, Experience=@Experience, " +
                                     " Address=@Address, Country=@Country WHERE u_id = @u_id";
                        type = "Updated";

                        cmd = new SqlCommand(qry, conn);

                        // Bind parameters
                        cmd.Parameters.AddWithValue("@U_name", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@Name", txtFullName.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
                        cmd.Parameters.AddWithValue("@Tenth_grade", txtTenth.Text);
                        cmd.Parameters.AddWithValue("@Tewelfth_grade", txtTwelfth.Text);
                        cmd.Parameters.AddWithValue("@Graduation_grade", txtGraduation.Text);
                        cmd.Parameters.AddWithValue("@P_graduation_grade", txtPostGraduation.Text);
                        cmd.Parameters.AddWithValue("@Phd", txtPhd.Text);
                        cmd.Parameters.AddWithValue("@Workson", txtWork.Text);
                        cmd.Parameters.AddWithValue("@Experience", txtExperience.Text);
                        //cmd.Parameters.AddWithValue("@Resume", fileData);  // Insert the binary file data
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@Country", countryName);
                        //cmd.Parameters.AddWithValue("@profile", img);
                        cmd.Parameters.AddWithValue("@u_id", Request.QueryString["id"].ToString());

                        int res = cmd.ExecuteNonQuery();

                        // Save the file on the server
                        //fuResume.SaveAs(Server.MapPath(filePath));
                        //fuProfile.SaveAs(Server.MapPath(img));

                        if (res > 0)
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Resume Details " + type + " Successfully!";
                            lblMsg.CssClass = "alert alert-success";
                            clear();
                        }
                        else
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Cannot update the records, please try again later!";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();  // Close the connection after the operation
                }
            }
        }

        private void clear()
        {
            txtUsername.Text = string.Empty;
            txtFullName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtTenth.Text = string.Empty;
            txtTwelfth.Text = string.Empty;
            txtGraduation.Text = string.Empty;
            txtPostGraduation.Text = string.Empty;
            txtPhd.Text = string.Empty;
            txtWork.Text = string.Empty;
            txtExperience.Text = string.Empty;
            txtAddress.Text = string.Empty;
            ddlCountry.ClearSelection();
        }
    }
}