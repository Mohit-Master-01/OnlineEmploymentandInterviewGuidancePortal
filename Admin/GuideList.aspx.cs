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
    public partial class GuideList : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter sda;
        SqlDataReader dr;
        DataTable dt;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Session["s_admin"] == null)
            {
                //Response.Redirect("../User/Login.aspx");
            }
            if (!IsPostBack)
            {
                fnBindguide();
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

        protected void fnBindguide()
        {
            DataSet ds = new DataSet();
            try
            {
                fnConnectDB();
                string qry = "SELECT Row_Number() over(Order by(Select 1)) as [Sr.No], g_id, Title, Category,Level FROM tblGuide";
                cmd = new SqlCommand(qry, conn);
                sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
                dtgGuideList.DataSource = ds;
                dtgGuideList.DataBind();
                if (Request.QueryString["id"] != null)
                {
                    Linkback.Visible = true;
                }
                conn.Close();
            }
            catch (Exception ex)
            {

                Response.Write(ex.ToString());
            }

        }

        protected void dtgGuideList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dtgGuideList.PageIndex = e.NewPageIndex;
            fnBindguide();
        }

        protected void dtgGuideList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                fnConnectDB();
                GridViewRow rw = dtgGuideList.Rows[e.RowIndex];
                int G_id = Convert.ToInt32(dtgGuideList.DataKeys[e.RowIndex].Values[0]);
                string qry = "DELETE FROM tblGuide WHERE g_id = '" + G_id + "'";
                cmd = new SqlCommand(qry, conn);
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Guide Deleted Successfully!";
                    lblMsg.CssClass = "alert alert-success";
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Cannot delete this record!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                conn.Close();
                dtgGuideList.EditIndex = -1;
                fnBindguide();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }

        protected void dtgGuideList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.ID = e.Row.RowIndex.ToString();
                if (Request.QueryString["id"] != null)
                {
                    int g_id = Convert.ToInt32(dtgGuideList.DataKeys[e.Row.RowIndex].Values[0]);
                    if (g_id == Convert.ToInt32(Request.QueryString["id"]))
                    {
                        e.Row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    }
                }
            }
        }
    }
}