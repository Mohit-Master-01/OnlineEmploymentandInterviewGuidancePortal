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
    public partial class UserList : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter sda;
        SqlDataReader dr;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["s_admin"] == null)
            {
                Response.Redirect("../User/Login.aspx");
            }

            if (!IsPostBack)
            {
                fnConnectDB();
                fnBindUsers();
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

        protected void fnBindUsers()
        {
            DataSet ds = new DataSet();
            try
            {
                fnConnectDB();
                string qry = "SELECT Row_Number() over(Order by(Select 1)) as [Sr.No], u_id, Name, Email, Mobile, Country, State FROM tblUser";
                cmd = new SqlCommand(qry, conn);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                dtgUserList.DataSource = dt;
                dtgUserList.DataBind();
                conn.Close();
            }
            catch (Exception ex)
            {

                Response.Write(ex.ToString());
            }
        }

        protected void dtgUserList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dtgUserList.PageIndex = e.NewPageIndex;
            fnBindUsers();
        }

        protected void dtgUserList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                fnConnectDB();
                GridViewRow rw = dtgUserList.Rows[e.RowIndex];
                int u_id = Convert.ToInt32(dtgUserList.DataKeys[e.RowIndex].Values[0]);
                string qry = "DELETE FROM tblUser WHERE u_id = '" + u_id + "'";
                cmd = new SqlCommand(qry, conn);
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "User Deleted Successfully!";
                    lblMsg.CssClass = "alert alert-success";
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Cannot delete this record!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                conn.Close();
                dtgUserList.EditIndex = -1;
                fnBindUsers();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }
    }
}