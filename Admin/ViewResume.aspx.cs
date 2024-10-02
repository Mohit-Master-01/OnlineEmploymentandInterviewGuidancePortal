using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Drawing;


namespace OnlineJobPortal.Admin
{
    public partial class ViewResume : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter sda;
        SqlDataReader dr;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fnConnectDB();
                fnBindAppliedJob();
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

        protected void fnBindAppliedJob()
        {
            DataSet ds = new DataSet();
            try
            {
                fnConnectDB();
                string qry = "SELECT Row_Number() over(Order by(Select 1)) as [Sr.No], aj.AJ_id, j.CompanyName, aj.Job_id, j.Title, u.Mobile, u.Name, u.Email, u.Resume, u.u_id FROM tblAppliedJobs aj\r\nINNER JOIN tblUser u ON aj.u_id = u.u_id\r\nINNER JOIN tblJob j ON aj.Job_id = j.Job_id";
                cmd = new SqlCommand(qry, conn);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                dtgResume.DataSource = dt;
                dtgResume.DataBind();
                conn.Close();
            }
            catch (Exception ex)
            {

                Response.Write(ex.ToString());
            }

        }


        protected void dtgResume_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                fnConnectDB();
                GridViewRow rw = dtgResume.Rows[e.RowIndex];
                int aj_id = Convert.ToInt32(dtgResume.DataKeys[e.RowIndex].Values[0]);
                string qry = "DELETE FROM tblAppliedJobs WHERE AJ_id = '" + aj_id + "'";
                cmd = new SqlCommand(qry, conn);
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Resume Deleted Successfully!";
                    lblMsg.CssClass = "alert alert-success";
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Cannot delete this record!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                conn.Close();
                dtgResume.EditIndex = -1;
                fnBindAppliedJob();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }

        protected void dtgResume_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(dtgResume,"Select$" +e.Row.RowIndex);
            e.Row.ToolTip = "Click to view job details";
        }

        protected void dtgResume_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach(GridViewRow row in dtgResume.Rows)
            {
                if(row.RowIndex == dtgResume.SelectedIndex)
                {
                    HiddenField job_id = (HiddenField)row.FindControl("hdnJob_id");
                    Response.Redirect("JobList.aspx?id=" + job_id.Value);
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Click to select this row";
                }
            }
        }

        protected void dtgResume_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            dtgResume.PageIndex = e.NewPageIndex;
            fnBindAppliedJob();
        }

        protected void DownloadPDF_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the User ID from CommandArgument
                int userId = Convert.ToInt32((sender as Button).CommandArgument);

                fnConnectDB();  // Ensure the connection is established properly

                // Query to fetch the resume (PDF) from the database
                string query = "SELECT Resume FROM tblUser WHERE u_id = @UserId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    // Open the connection if it's not already open
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        byte[] fileData = reader["Resume"] as byte[];  // Get the binary PDF data

                        // If data exists, initiate the download
                        if (fileData != null && fileData.Length > 0)
                        {
                            Response.Clear();
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("Content-Disposition", "attachment;filename=Resume.pdf");
                            Response.BinaryWrite(fileData);
                            Response.End();
                        }
                        else
                        {
                            lblMsg.Text = "No resume found.";
                        }
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Error downloading the resume: " + ex.Message;
            }
            finally
            {
                // Ensure the connection is closed in the finally block
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

    }
} 