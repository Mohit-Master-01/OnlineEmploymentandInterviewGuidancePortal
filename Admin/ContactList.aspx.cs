using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace OnlineJobPortal.Admin
{
    public partial class ContactList : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                //Response.Redirect("../User/Login.aspx");
            }

            if (!IsPostBack)
            {
                fnConnectDB();
                fnBindContact();
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

        protected void fnBindContact()
        {
            DataSet ds = new DataSet();
            try
            {
                fnConnectDB();
                string qry = "SELECT Row_Number() over(Order by(Select 1)) as [Sr.No], Con_id, Name, Email, Subject, Message FROM tblContact";
                cmd = new SqlCommand(qry, conn);
                sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
                dtgContactList.DataSource = ds;
                dtgContactList.DataBind();
                conn.Close();
            }
            catch (Exception ex)
            {

                Response.Write(ex.ToString());
            }
        }

        protected void dtgContactList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dtgContactList.PageIndex = e.NewPageIndex;
            fnBindContact();
        }

        protected void dtgContactList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                fnConnectDB();
                GridViewRow rw = dtgContactList.Rows[e.RowIndex];
                int Con_id = Convert.ToInt32(dtgContactList.DataKeys[e.RowIndex].Values[0]);
                string qry = "DELETE FROM tblContact WHERE Con_id = '" + Con_id + "'";
                cmd = new SqlCommand(qry, conn);
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Contact Deleted Successfully!";
                    lblMsg.CssClass = "alert alert-success";
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Cannot delete this record!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                conn.Close();
                dtgContactList.EditIndex = -1;
                fnBindContact();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }
    }
}