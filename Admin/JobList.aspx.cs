using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace OnlineJobPortal.Admin
{
    public partial class JobList : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        public static int id;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Session["s_admin"] == null)
            {
                //Response.Redirect("../User/Login.aspx");
            }
            if (!IsPostBack)
            {
                fnBindJob();
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                fnConnectDB();
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

        protected void fnBindJob()
        {
            DataSet ds = new DataSet();
            try
            {
                fnConnectDB();
                string qry = "SELECT Row_Number() over(Order by(Select 1)) as [Sr.No], Job_id, Title, NoOfPost, Qualification, Experience, LastDateToApply,  CompanyName, Country, State, CreateDate FROM tblJob";
                cmd = new SqlCommand(qry, conn);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                dtgJobList.DataSource = dt;
                dtgJobList.DataBind();
                if (Request.QueryString["id"] != null)
                {
                    linkback.Visible = true;
                }
                conn.Close();
            }
            catch (Exception ex)
            {

                Response.Write(ex.ToString());
            }

        }

        protected void dtgJobList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dtgJobList.PageIndex = e.NewPageIndex;
            fnBindJob();
        }

        protected void dtgJobList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                fnConnectDB();
                GridViewRow rw = dtgJobList.Rows[e.RowIndex];
                int Job_id = Convert.ToInt32(dtgJobList.DataKeys[e.RowIndex].Values[0]);
                string qry = "DELETE FROM tblJob WHERE Job_id = '" + Job_id + "'";
                cmd = new SqlCommand(qry, conn);
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Job Deleted Successfully!";
                    lblMsg.CssClass = "alert alert-success";
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Cannot delete this record!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                conn.Close();
                dtgJobList.EditIndex = -1;
                fnBindJob();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }

        protected void dtgJobList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditJob")
                {
                    // Use CommandArgument to get the Job_id
                    string jobId = e.CommandArgument.ToString();
                    Response.Redirect("NewJob.aspx?id=" + jobId);
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }

        protected void dtgJobList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.ID = e.Row.RowIndex.ToString();
                if (Request.QueryString["id"] != null)
                {
                    int Job_id = Convert.ToInt32(dtgJobList.DataKeys[e.Row.RowIndex].Values[0]);
                    if(Job_id == Convert.ToUInt32(Request.QueryString["id"]))
                    {
                        e.Row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    }

                }
            }
        }
    }
}