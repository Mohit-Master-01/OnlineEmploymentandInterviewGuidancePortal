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
    public partial class Profile : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {



                fnConnectDB();
                fnBindUserProfile();
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

        protected void fnBindUserProfile()
        {
            DataSet ds = new DataSet();
            try
            {
                fnConnectDB();
                string qry = "SELECT u_id, U_name, Name, Address, Mobile, Email, Country, Resume, profile FROM tblUser WHERE U_name = '" + Session["user"] +"'";
                cmd = new SqlCommand(qry, conn);
                sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
                dlProfile.DataSource = ds;
                dlProfile.DataBind();
                conn.Close();
            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }

        protected void dlProfile_ItemCommand(object source, DataListCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditUserProfile")
                {
                    // Use CommandArgument to get the u_id
                    string u_id = e.CommandArgument.ToString();
                    Response.Redirect("ResumeBuild.aspx?id=" + u_id);
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }
    }
}