using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Xml.Linq;
using System.Net.NetworkInformation;

namespace OnlineJobPortal.Admin
{
    public partial class NewJob : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                fnConnectDB();
                fnBindCountry();
                ddlState.Items.Insert(0, new ListItem("Select State"));
                fillData();

            }

            if (Session["s_admin"] == null)
            {
                Response.Redirect("../User/Login.aspx");
            }


            if (Request.QueryString["id"] != null)
            {
                Session["title"] = "Edit Job";
            }
            else
            {
                Session["title"] = "Add Job";
            }

            txtLastDate.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");

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
                String qry = "SELECT * FROM tblCountry";
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

        protected void fnBindStates()
        {
            DataSet ds = new DataSet();
            try
            {
                fnConnectDB(); // Ensure this method properly connects to the database
                string qry = "SELECT State_id, S_name FROM tblState WHERE Cou_id =@st";
                cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("st", ddlCountry.SelectedValue);
                sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
                ddlState.DataSource = ds;
                ddlState.DataTextField = "S_name";
                ddlState.DataValueField = "State_id";
                ddlState.DataBind();
                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        protected void fillData()
        {
            if (Request.QueryString["id"] != null)
            {
                fnConnectDB();
                try
                {
                    string qry = "SELECT * FROM tblJob WHERE Job_id = @JobId";
                    cmd = new SqlCommand(qry, conn);
                    cmd.Parameters.AddWithValue("@JobId", Request.QueryString["id"]);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        if (dr.Read())
                        {

                            LinkBack.Visible = true;

                            txtJobTitle.Text = dr["Title"].ToString();
                            txtNoOfPost.Text = dr["NoOfPost"].ToString();
                            txtDescription.Text = dr["Description"].ToString();
                            txtQualification.Text = dr["Qualification"].ToString();
                            txtExperience.Text = dr["Experience"].ToString();
                            txtSpecialization.Text = dr["Specialization"].ToString();
                            txtLastDate.Text = Convert.ToDateTime(dr["LastDateToApply"]).ToString("yyyy-MM-dd");
                            txtSalary.Text = dr["Salary"].ToString();
                            ddlJobType.SelectedValue = dr["JobType"].ToString();
                            txtCompany.Text = dr["CompanyName"].ToString();
                            txtWebsite.Text = dr["Website"].ToString();
                            txtEmail.Text = dr["Email"].ToString();
                            txtAddress.Text = dr["Address"].ToString();
                            ddlCountry.SelectedValue = dr["Country"].ToString();

                            btnAdd.Text = "Update";
                        }

                    }
                    else
                    {
                        lblMsg.Text = "Job not found..!";
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

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            fnBindStates();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            fnConnectDB();


            DateTime lastDateToApply = Convert.ToDateTime(txtLastDate.Text);

            // Step 2: Get the current date
            DateTime currentDate = DateTime.Now;

            // Step 3: Compare if the last date is a past date
            if (lastDateToApply < currentDate.Date)
            {
                // If the LastDateToApply is in the past, show an error message
                lblMsg.Visible = true;
                lblMsg.Text = "The 'Last Date to Apply' cannot be a past date!";
                lblMsg.CssClass = "alert alert-danger";
                return;  // Stop further processing
            }

            // Fetch the selected country and state names
            string qryCountry = "SELECT C_name FROM tblCountry WHERE Cou_id = @Cou_id";
            cmd = new SqlCommand(qryCountry, conn);
            cmd.Parameters.AddWithValue("@Cou_id", ddlCountry.SelectedValue);
            string countryName = cmd.ExecuteScalar().ToString();

            string qryState = "SELECT S_name FROM tblState WHERE State_id = @State_id";
            cmd = new SqlCommand(qryState, conn);
            cmd.Parameters.AddWithValue("@State_id", ddlState.SelectedValue);
            string stateName = cmd.ExecuteScalar().ToString();

            string img = "~/Upload/" + fuCompanyLogo.FileName;
            string type, concatQuery = string.Empty;
            string fileExtension = System.IO.Path.GetExtension(fuCompanyLogo.FileName).ToLower();
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };



            try
            {
                if (Request.QueryString["id"] != null)
                {
                    if (fuCompanyLogo.HasFile)
                    {
                        if (allowedExtensions.Contains(fileExtension))
                        {

                            concatQuery = "CompanyImage = @CompanyImage,";// if ni under if km lagavyu 
                            string qry = "Update tblJob set Title= @Title, NoOfPost=@NoOfPost, Description=@Description, Qualification=@Qualification, Experience=@Experience, Specialization=@Specialization, LastDateToApply=@LastDateToApply, Salary=@Salary, JobType=@JobType, CompanyName=@CompanyName," + concatQuery + @" Website=@Website, Email=@Email, Address=@Address, Country=@Country, State=@State WHERE Job_id = @id";
                            type = "Updated";
                            cmd = new SqlCommand(qry, conn);
                            cmd.Parameters.AddWithValue("@Title", txtJobTitle.Text);
                            cmd.Parameters.AddWithValue("@NoOfPost", txtNoOfPost.Text);
                            cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                            cmd.Parameters.AddWithValue("@Qualification", txtQualification.Text);
                            cmd.Parameters.AddWithValue("@Experience", txtExperience.Text);
                            cmd.Parameters.AddWithValue("@Specialization", txtSpecialization.Text);
                            cmd.Parameters.AddWithValue("@LastDateToApply", txtLastDate.Text);
                            cmd.Parameters.AddWithValue("@Salary", txtSalary.Text);
                            cmd.Parameters.AddWithValue("@JobType", ddlJobType.SelectedValue);
                            cmd.Parameters.AddWithValue("@CompanyName", txtCompany.Text);
                            cmd.Parameters.AddWithValue("@CompanyImage", img);
                            cmd.Parameters.AddWithValue("@Website", txtWebsite.Text);
                            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                            cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                            cmd.Parameters.AddWithValue("@Country", countryName);
                            cmd.Parameters.AddWithValue("@State", stateName);
                            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"].ToString());
                            
                            int res = cmd.ExecuteNonQuery();
                            // Save the file to the server
                            fuCompanyLogo.SaveAs(Server.MapPath(img));

                            if (res > 0)
                            {

                                lblMsg.Visible = true;
                                lblMsg.Text = "Job " + type + " !";
                                lblMsg.CssClass = "alert alert-success";
                                clear();
                            }
                            else
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = "Cannot " + type + " the records, please try again later!";
                                lblMsg.CssClass = "alert alert-danger";
                            }
                        }
                        else
                        {
                            concatQuery = string.Empty;
                            // Invalid file extension
                            lblMsg.Visible = true;
                            lblMsg.Text = "Invalid file type.Only JPG, JPEG, PNG are allowed.";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                    }
                    else
                    {
                        concatQuery = string.Empty;
                    }



                }
                else
                {
                    if (fuCompanyLogo.HasFile)
                    {
                        // Validate the file extension
                        //string fileExtension = System.IO.Path.GetExtension(fuCompanyLogo.FileName).ToLower();
                        //string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };

                        if (allowedExtensions.Contains(fileExtension))
                        {
                            // Proceed with database insertion
                            fnConnectDB();
                            string qry = "INSERT INTO tblJob VALUES(@Title, @NoOfPost, @Description, @Qualification, @Experience, @Specialization, @LastDateToApply, @Salary, @JobType, @CompanyName, @CompanyImage, @Website, @Email, @Address, @Country, @State, @CreateDate)";
                            type = " Added";
                            DateTime time = DateTime.Now;
                            cmd = new SqlCommand(qry, conn);
                            cmd.Parameters.AddWithValue("@Title", txtJobTitle.Text);
                            cmd.Parameters.AddWithValue("@NoOfPost", txtNoOfPost.Text);
                            cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                            cmd.Parameters.AddWithValue("@Qualification", txtQualification.Text);
                            cmd.Parameters.AddWithValue("@Experience", txtExperience.Text);
                            cmd.Parameters.AddWithValue("@Specialization", txtSpecialization.Text);
                            cmd.Parameters.AddWithValue("@LastDateToApply", txtLastDate.Text);
                            cmd.Parameters.AddWithValue("@Salary", txtSalary.Text);
                            cmd.Parameters.AddWithValue("@JobType", ddlJobType.SelectedValue);
                            cmd.Parameters.AddWithValue("@CompanyName", txtCompany.Text);
                            cmd.Parameters.AddWithValue("@CompanyImage", img);
                            cmd.Parameters.AddWithValue("@Website", txtWebsite.Text);
                            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                            cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                            cmd.Parameters.AddWithValue("@Country", countryName);
                            cmd.Parameters.AddWithValue("@State", stateName);
                            cmd.Parameters.AddWithValue("@CreateDate", time.ToString("yyyy-MM-dd HH:mm:ss"));


                            int res = cmd.ExecuteNonQuery();
                            fuCompanyLogo.SaveAs(Server.MapPath(img));

                            if (res > 0)
                            {
                                // Save the file to the server                               
                                lblMsg.Visible = true;
                                lblMsg.Text = "Job" + type + "!";
                                lblMsg.CssClass = "alert alert-success";
                                clear();
                            }
                            else
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = "Cannot " + type + " the records, please try again later!";
                                lblMsg.CssClass = "alert alert-danger";
                            }
                        }
                        else
                        {
                            // Invalid file extension
                            lblMsg.Visible = true;
                            lblMsg.Text = "Invalid file type.Only JPG, JPEG, PNG are allowed.";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                    }
                    else
                    {
                        // No file uploaded
                        lblMsg.Visible = true;
                        lblMsg.Text = "No File!";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }

        private void clear()
        {
            txtJobTitle.Text = string.Empty;
            txtNoOfPost.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtQualification.Text = string.Empty;
            txtExperience.Text = string.Empty;
            txtSpecialization.Text = string.Empty;
            txtLastDate.Text = string.Empty;
            txtSalary.Text = string.Empty;
            txtCompany.Text = string.Empty;
            txtWebsite.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAddress.Text = string.Empty;
            ddlState.ClearSelection();
            ddlJobType.ClearSelection();
            ddlCountry.ClearSelection();
        }


    }

}
