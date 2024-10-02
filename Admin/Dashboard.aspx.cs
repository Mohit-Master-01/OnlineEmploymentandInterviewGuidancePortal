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
    public partial class Dashboard : System.Web.UI.Page
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
                Users();
                Jobs();
                AppliedJobs();
                ContactCount();
                fnGuide();
                fnConnectDB();
            }

            if (Session["s_admin"] == null)
            {
                //Response.Redirect("../User/Login.aspx");
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

        protected void Users()
        {
            try
            {
                fnConnectDB();
                string qry = "SELECT Count(*) FROM tblUser";
                cmd = new SqlCommand(qry, conn);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Session["user"] = dt.Rows[0][0];
                }
                else
                {
                    Session["user"] = 0;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }

        protected void Jobs()
        {
            try
            {
                fnConnectDB();
                string qry = "SELECT Count(*) FROM tblJob";
                cmd = new SqlCommand(qry, conn);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Session["Job"] = dt.Rows[0][0];
                }
                else
                {
                    Session["Job"] = 0;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }

        protected void AppliedJobs()
        {
            try
            {
                fnConnectDB();
                string qry = "SELECT Count(*) FROM tblAppliedJobs";
                cmd = new SqlCommand(qry, conn);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Session["AppliedJobs"] = dt.Rows[0][0];
                }
                else
                {
                    Session["AppliedJobs"] = 0;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }

        protected void ContactCount()
        {
            try
            {
                fnConnectDB();
                string qry = "SELECT Count(*) FROM tblContact";
                cmd = new SqlCommand(qry,conn);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    Session["Contact"] = dt.Rows[0][0];
                }
                else
                {
                    Session["Contact"] = 0;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }

        protected void fnGuide()
        {
            try
            {
                fnConnectDB();
                string qry = "SELECT Count(*) FROM tblGuide";
                cmd = new SqlCommand(qry, conn);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Session["Guide"] = dt.Rows[0][0];
                }
                else
                {
                    Session["Guide"] = 0;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }

    }
}