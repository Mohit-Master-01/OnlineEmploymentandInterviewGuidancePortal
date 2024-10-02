using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using OnlineJobPortal.Admin;


namespace OnlineJobPortal.User
{
    public partial class Job_details : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter sda;
        SqlDataReader dr;
        DataTable dt, dt1;
        public string jobtitle = string.Empty;
        int User = Login.key;
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                fnBindJobDetails();
                DataBind();
            }
            else
            {
                Response.Redirect("Job_listing.aspx");
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            fnConnectDB();
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

        protected void fnBindJobDetails()
        {
            DataSet ds = new DataSet();
            try
            {

                fnConnectDB();
                string qry = "SELECT * FROM tblJob WHERE Job_id = '" + Request.QueryString["id"] + "'";
                cmd = new SqlCommand(qry, conn);
                sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dlJobDetails.DataSource = dt;
                dlJobDetails.DataBind();
                jobtitle = dt.Rows[0]["Title"].ToString();//changes the title in UI accordingly
                conn.Close();
            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }

        protected string GetImageUrl(Object url)
        {
            string url1 = "";

            if (string.IsNullOrEmpty(url.ToString()) || url == DBNull.Value)
            {
                url1 = "~/Images/No_image.png";
            }
            else
            {
                url1 = string.Format("~/{0}", url);
            }
            return ResolveUrl(url1);
        }

        protected void dlJobDetails_ItemCommand(object source, DataListCommandEventArgs e)
        {
            fnConnectDB();

            if (e.CommandName == "ApplyJob")
            {
                if (Session["user"] != null)
                {
                    try
                    {
                        fnConnectDB();

                        // Step 1: Fetch the Last Date to Apply for this job
                        string qryLastDate = "SELECT LastDateToApply FROM tblJob WHERE Job_id = @Job_id";
                        cmd = new SqlCommand(qryLastDate, conn);
                        cmd.Parameters.AddWithValue("@Job_id", Request.QueryString["id"]);

                        object lastDateObj = cmd.ExecuteScalar(); // Get the last date

                        if (lastDateObj != null)
                        {
                            DateTime lastDateToApply = Convert.ToDateTime(lastDateObj);

                            // Step 2: Check if the current date has passed the last date
                            if (DateTime.Now > lastDateToApply)
                            {
                                // Job application deadline has passed, show a message
                                lblMsg.Visible = true;
                                lblMsg.Text = "The last date to apply for this job has passed. You cannot apply!";
                                lblMsg.CssClass = "alert alert-danger";
                                return; // Stop further processing
                            }
                        }
                        else
                        {
                            // If no last date is found, show an error
                            lblMsg.Visible = true;
                            lblMsg.Text = "Error: Unable to retrieve the job's last date to apply.";
                            lblMsg.CssClass = "alert alert-danger";
                            return;
                        }

                        // Step 3: Fetch the current user ID from the session
                        string qryGetUserId = "SELECT u_id FROM tblUser WHERE U_name = @U_name";
                        cmd = new SqlCommand(qryGetUserId, conn);
                        cmd.Parameters.AddWithValue("@U_name", Session["user"].ToString());
                        int u_id = Convert.ToInt32(cmd.ExecuteScalar());

                        // Step 4: Insert the applied job record
                        string qryApplyJob = "INSERT INTO tblAppliedJobs (u_id, Job_id) VALUES (@u_id, @Job_id)";
                        cmd = new SqlCommand(qryApplyJob, conn);
                        cmd.Parameters.AddWithValue("@u_id", u_id);
                        cmd.Parameters.AddWithValue("@Job_id", Request.QueryString["id"]);

                        int res = cmd.ExecuteNonQuery();

                        if (res > 0)
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Job applied successfully!";
                            lblMsg.CssClass = "alert alert-success";
                            fnBindJobDetails(); // Re-bind DataList to update the button to "Applied"
                        }
                        else
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Unable to apply for the job. Please try again later.";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else
                {
                    // Redirect to login if the user is not logged in
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void dlJobDetails_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            LinkButton btnApplyJob = e.Item.FindControl("lbApplyJob") as LinkButton;

            if (Session["user"] != null)
            {
                fnConnectDB();

                // Step 1: Fetch the current job's Last Date to Apply
                string qryLastDate = "SELECT LastDateToApply FROM tblJob WHERE Job_id = @Job_id";
                cmd = new SqlCommand(qryLastDate, conn);
                cmd.Parameters.AddWithValue("@Job_id", Request.QueryString["id"]);

                object lastDateObj = cmd.ExecuteScalar();

                if (lastDateObj != null)
                {
                    DateTime lastDateToApply = Convert.ToDateTime(lastDateObj);

                    // Step 2: Check if the current date is beyond the last date to apply
                    if (DateTime.Now > lastDateToApply)
                    {
                        // If the last date to apply has passed, disable the "Apply Now" button
                        btnApplyJob.Enabled = false;
                        btnApplyJob.Text = "Application Closed";
                        return;
                    }
                }

                // Step 3: Check if the user has already applied for this job
                if (isApplied())
                {
                    btnApplyJob.Enabled = false;
                    btnApplyJob.Text = "Applied";
                }
                else
                {
                    btnApplyJob.Enabled = true;
                    btnApplyJob.Text = "Apply Now";
                }

                conn.Close();
            }
        }

        bool isApplied()
        {

            fnConnectDB();
            string qry = "SELECT * FROM tblAppliedJobs WHERE u_id = @u_id AND Job_id = @Job_id";
            cmd = new SqlCommand(qry, conn);
            cmd.Parameters.AddWithValue("@u_id", User);
            cmd.Parameters.AddWithValue("@Job_id", Request.QueryString["id"]);

            sda = new SqlDataAdapter(cmd);
            dt1 = new DataTable();
            sda.Fill(dt1);
            if (dt1.Rows.Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



    }


}
