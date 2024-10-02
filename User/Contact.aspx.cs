using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml.Linq;

namespace OnlineJobPortal.User
{
    public partial class Contact : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;


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
                if(conn.State != ConnectionState.Open)
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

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                fnConnectDB();
                string qry = "INSERT INTO tblContact(Name, Email, Subject, Message) VALUES('" + name.Value.Trim() + "', '"+ email.Value.Trim() + "', '"+ subject.Value.Trim() + "', '"+ message.Value.Trim() + "')";
                cmd = new SqlCommand(qry, conn);
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Thanks For Reaching Out Will Look Into Your Query!";
                    lblMsg.CssClass = "alert alert-success";
                    clear();
                    //Response.Write("Contact Added!");
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Cannot Save Record Right Now, Please Try After Sometime..!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                conn.Close();
            }
            catch (Exception ex)
            {

                Response.Write(ex.ToString());
            }
        }

        private void clear()
        {
            name.Value = string.Empty;
            email.Value = string.Empty;
            subject.Value = string.Empty;
            message.Value = string.Empty;
        }
    }
}