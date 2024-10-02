using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services.Description;
using System.Xml.Linq;

namespace OnlineJobPortal.User
{
    public partial class Register : System.Web.UI.Page
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
                ddlCountry.Items.Insert(0, new ListItem("Select Country", "0"));
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
                ddlState.Items.Insert(0, new ListItem("Select State"));
                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        private void Clear()
        {
            txtUsername.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtFullName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtConfirmPass.Text = string.Empty;
            ddlCountry.ClearSelection();
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                fnConnectDB();

                // Fetch the selected country and state names
                string qryCountry = "SELECT C_name FROM tblCountry WHERE Cou_id = @Cou_id";
                cmd = new SqlCommand(qryCountry, conn);
                cmd.Parameters.AddWithValue("@Cou_id", ddlCountry.SelectedValue);
                string countryName = cmd.ExecuteScalar().ToString(); 

                string qryState = "SELECT S_name FROM tblState WHERE State_id = @State_id";
                cmd = new SqlCommand(qryState, conn);
                cmd.Parameters.AddWithValue("@State_id", ddlState.SelectedValue);
                string stateName = cmd.ExecuteScalar().ToString();

                string img = "~/Upload/" + fuProfile.FileName;
                string concatQuery = string.Empty;
                string fileExtension = System.IO.Path.GetExtension(fuProfile.FileName).ToLower();
                string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };

                if(fuProfile.HasFile)
                {
                    if(allowedExtensions.Contains(fileExtension))
                    {
                        String qry = "INSERT INTO tblUser(U_name,Password,Name,Address,Mobile,Country, State,Email,profile) VALUES('" + txtUsername.Text + "','" + txtPassword.Text + "','" + txtFullName.Text + "','" + txtAddress.Text + "','" + txtMobile.Text + "','" + countryName + "','" + stateName + "','" + txtEmail.Text + "','"+img+"')";
                        cmd = new SqlCommand(qry, conn);
                        int res = cmd.ExecuteNonQuery();
                        fuProfile.SaveAs(Server.MapPath(img));

                        if (res > 0)
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Registered Successfull!";
                            lblMsg.CssClass = "alert alert-success";
                            Clear();
                            Response.Redirect("Login.aspx");
                        }
                        else
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Cannot Save Record Right Now, Please Try After Sometime..!";
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
                    String qry = "INSERT INTO tblUser(U_name,Password,Name,Address,Mobile,Country, State,Email) VALUES('" + txtUsername.Text + "','" + txtPassword.Text + "','" + txtFullName.Text + "','" + txtAddress.Text + "','" + txtMobile.Text + "','" + countryName + "','" + stateName + "','" + txtEmail.Text + "')";
                    cmd = new SqlCommand(qry, conn);
                    int res = cmd.ExecuteNonQuery();
                    if (res > 0)
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "Registered Successfull!";
                        lblMsg.CssClass = "alert alert-success";
                        Clear();
                        Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "Cannot Save Record Right Now, Please Try After Sometime..!";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                }
                conn.Close();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = $"<b>{txtUsername.Text.Trim()}</b> username already exists, please try a different name!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                else
                {
                    Response.Write(ex.ToString());
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            fnBindStates();
        }
    }
}