using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace OnlineJobPortal
{
    public partial class test : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Page.IsPostBack == false)
            {
                //   fnConnectDB();
                fnBindCountry();
            }

            //  ddlCountry_SelectedIndexChanged(sender, e);
            //fnBindStates();
            //    ddlState.Items.Insert(0, new ListItem("Select State"));
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

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Write("h");
        }

        protected void fnBindCountry()
        {
            DataSet ds = new DataSet();
            try
            {
                fnConnectDB();
                string qry = "SELECT * FROM tblCountry";
                cmd = new SqlCommand(qry, conn);
                sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);

                DropDownList1.DataSource = ds;
                DropDownList1.DataTextField = "C_name";
                DropDownList1.DataValueField = "Cou_id";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("--Select Country--"));
                sda.Dispose();
                conn.Close();
            }
            catch (Exception ex)
            {

                Response.Write(ex.ToString());
            }
        }
    }
}