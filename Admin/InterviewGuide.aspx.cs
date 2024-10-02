using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineJobPortal.Admin
{
    public partial class InterviewGuide : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter sda;
        SqlDataReader dr;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            fnConnectDB();
            if (Session["s_admin"] == null)
            {
                Response.Redirect("~/User/Login.aspx");
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            fnConnectDB();  // Ensure the connection to the database

            string filePath = "~/Guide/" + fuGuide.FileName;
            string type;
            string fileExtension = System.IO.Path.GetExtension(fuGuide.FileName).ToLower();
            string[] allowedExtensions = { ".doc", ".docx", ".pdf" };
            byte[] fileData = null;


            try
            {
                if (ddlLevel.SelectedValue != "0" && ddlType.SelectedValue != "0")
                {
                    if (fuGuide.HasFile)
                    {
                        if (allowedExtensions.Contains(fileExtension))
                        {
                            // Convert the uploaded file into a byte array
                            fileData = fuGuide.FileBytes;

                            string qry = "INSERT INTO tblGuide VALUES(@Title, @Description, @Category, @Level, @guide)";
                            type = "Added";

                            cmd = new SqlCommand(qry, conn);

                            // Bind parameters
                            cmd.Parameters.AddWithValue("@Title", txtGuideTitle.Text);
                            cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                            cmd.Parameters.AddWithValue("@Category", ddlType.SelectedValue);
                            cmd.Parameters.AddWithValue("@Level", ddlLevel.SelectedValue);
                            cmd.Parameters.AddWithValue("@guide", fileData);  // Insert the binary file data

                            int res = cmd.ExecuteNonQuery();

                            // Save the file on the server
                            fuGuide.SaveAs(Server.MapPath(filePath));

                            if (res > 0)
                            {
                                lblMsg.Visible = true;
                                lblMsg.Text = "Guide " + type + " Successfully!";
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
                    else
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "Upload file!";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Please fill the values";
                    lblMsg.CssClass = "alert alert-danger";
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
            txtGuideTitle.Text = string.Empty;
            txtDescription.Text = string.Empty;
            ddlType.ClearSelection();
            ddlLevel.ClearSelection();
        }

    }
}