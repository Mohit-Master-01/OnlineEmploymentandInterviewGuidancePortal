using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace OnlineJobPortal.User
{
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataSet ds;
        DataTable dt = new DataTable();
        SqlDataReader dr;
        public static int key;


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

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            fnConnectDB();
            if (ddlType.SelectedValue == "Admin")
            {
                if (txtName.Text == "Admin" && txtPass.Text == "123")
                {
                    Session["s_admin"] = txtName.Text.Trim();
                    Response.Redirect("../Admin/Dashboard.aspx");
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text ="<b>"+ txtName.Text+ " Invalid Admin!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                conn.Close();

            }
            if(ddlType.SelectedValue == "User")
            {
                
                string qry = "SELECT u_id, U_name, Password FROM tblUser WHERE U_name = '"+txtName.Text+"' and Password = '"+txtPass.Text+"'";
                cmd = new SqlCommand(qry, conn);
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    Session["user"] = txtName.Text.Trim();
                    key = Convert.ToInt32(dt.Rows[0][0].ToString());
                    Response.Redirect("Default.aspx");

                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "<b>" + txtName.Text + " Invalid Credentials!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                conn.Close ();
            }
        }
    }
}