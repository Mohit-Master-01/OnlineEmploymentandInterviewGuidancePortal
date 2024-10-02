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
    public partial class Job_listing : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter sda;
        SqlDataReader dr;
        DataTable dt;
        public int jobcount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                fnConnectDB();
                fnBindJobList();
                fnBindCountry();
                RBSelectedColorChange();
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

        protected void fnBindCountry()
        {
            DataSet ds = new DataSet();
            try
            {
                fnConnectDB();
                String qry = "SELECT Cou_id, C_name FROM tblCountry";
                cmd = new SqlCommand(qry, conn);
                sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
                ddlCountry.DataSource = ds;
                ddlCountry.DataTextField = "C_name";
                ddlCountry.DataValueField = "Cou_id";
                ddlCountry.DataBind();
                conn.Close();
            }
            catch (Exception ex)
            {

                Response.Write(ex.ToString());
            }
        }

        protected void fnBindJobList()
        {
            DataSet ds = new DataSet();
            try
            {
                fnConnectDB();
                string qry = "SELECT Job_id, Title, Salary, JobType, CompanyName, CompanyImage, Country, State, CreateDate FROM tblJob";
                cmd = new SqlCommand(qry, conn);
                sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                dlJobList.DataSource = dt;
                dlJobList.DataBind();
                lbljobCount.Text = JobCount(dt.Rows.Count);
                conn.Close();
            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }

        string JobCount(int count)
        {
            if (count > 1)
            {
                return "Total <b>" + count + "</b> jobs found";
            }
            else if (count == 1)
            {
                return "Total <b>" + count + "</b> jobs found";
            }
            else
            {
                return "No job found";
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            fnConnectDB();

            string qryCountry = "SELECT C_name FROM tblCountry WHERE Cou_id = @Cou_id";
            cmd = new SqlCommand(qryCountry, conn);
            cmd.Parameters.AddWithValue("@Cou_id", ddlCountry.SelectedValue);
            string countryName = cmd.ExecuteScalar().ToString();
            try
            {
                if (ddlCountry.SelectedValue != "0")
                {
                    fnConnectDB();
                    string qry = "SELECT Job_id, Title, Salary, JobType, CompanyName, CompanyImage, Country, State, CreateDate FROM tblJob WHERE Country = '" + countryName + "'";
                    cmd = new SqlCommand(qry, conn);
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    sda.Fill(dt);
                    dlJobList.DataSource = dt;
                    dlJobList.DataBind();
                    lbljobCount.Text = JobCount(dt.Rows.Count);
                    RBSelectedColorChange();
                }
                else
                {
                    fnBindJobList();
                    RBSelectedColorChange();
                }
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


        // Getting RelativeDate for given date like -- '1 month ago'

        public static string RelativeDate(DateTime theDate)

        {

            Dictionary<long, string> thresholds = new Dictionary<long, string>();

            int minute = 60;

            int hour = 60 * minute;

            int day = 24 * hour;

            thresholds.Add(60, "{0} seconds ago");

            thresholds.Add(minute * 2, "a minute ago");

            thresholds.Add(45 * minute, "{0} minutes ago");

            thresholds.Add(120 * minute, "an hour ago");

            thresholds.Add(day, "{0} hours ago");

            thresholds.Add(day * 2, "yesterday");

            thresholds.Add(day * 30, "{0} days ago");

            thresholds.Add(day * 365, "{0} months ago");

            thresholds.Add(long.MaxValue, "{0} years ago");

            long since = (DateTime.Now.Ticks - theDate.Ticks) / 10000000;

            foreach (long threshold in thresholds.Keys)

            {

                if (since < threshold)

                {

                    TimeSpan t = new TimeSpan((DateTime.Now.Ticks - theDate.Ticks));

                    return string.Format(thresholds[threshold], (t.Days > 365 ? t.Days / 365 : (t.Days > 0 ? t.Days : (t.Hours > 0 ? t.Hours : (t.Minutes > 0 ? t.Minutes : (t.Seconds > 0 ? t.Seconds : 0))))).ToString());

                }

            }

            return "";

        }



        protected void cblType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fnConnectDB();
                string jobtype = string.Empty;
                jobtype = selectedCheckBox();
                if (jobtype != "")
                {
                    string qry = "SELECT Job_id, Title, Salary, JobType, CompanyName, CompanyImage, Country, State, CreateDate FROM tblJob WHERE JobType IN (" + jobtype + ")";
                    cmd = new SqlCommand(qry, conn);
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    sda.Fill(dt);
                    dlJobList.DataSource = dt;
                    dlJobList.DataBind();
                    lbljobCount.Text = JobCount(dt.Rows.Count);
                    RBSelectedColorChange();
                }
                else
                {
                    fnBindJobList();
                }
            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }

        string selectedCheckBox()
        {
            string jobtype = string.Empty;

            try
            {
                for (int i = 0; i < cblType.Items.Count; i++)
                {
                    if (cblType.Items[i].Selected)
                    {
                        jobtype += "'" + cblType.Items[i].Text + "',";
                    }
                }
            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
            return jobtype = jobtype.TrimEnd(',');

        }


        protected void rblPostedDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fnConnectDB();
                string postedDate = string.Empty;
                postedDate = selectedRadioButton();
                if (rblPostedDay.SelectedValue != "0")
                {
                    string qry = "SELECT Job_id, Title, Salary, JobType, CompanyName, CompanyImage, Country, State, CreateDate FROM tblJob WHERE Convert(DATE, CreateDate) " + postedDate + " ";
                    cmd = new SqlCommand(qry, conn);
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    sda.Fill(dt);
                    dlJobList.DataSource = dt;
                    dlJobList.DataBind();
                    lbljobCount.Text = JobCount(dt.Rows.Count);
                    RBSelectedColorChange();
                }
                else
                {
                    fnBindJobList();
                    RBSelectedColorChange();
                }
            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }

        string selectedRadioButton()
        {
            string postedDate = string.Empty;
            DateTime date = DateTime.Today;

            try
            {
                if (rblPostedDay.SelectedValue == "1")
                {
                    postedDate = "= Convert(DATE, '" + date.ToString("yyyy/MM/dd") + "')";
                }
                else if (rblPostedDay.SelectedValue == "2")
                {
                    postedDate = "between Convert(DATE, '" + DateTime.Now.AddDays(-2).ToString("yyyy/MM/dd") + "') and Convert(DATE, '" + date.ToString("yyyy/MM/dd") + "')";

                }
                else if (rblPostedDay.SelectedValue == "3")
                {
                    postedDate = "between Convert(DATE, '" + DateTime.Now.AddDays(-3).ToString("yyyy/MM/dd") + "') and Convert(DATE, '" + date.ToString("yyyy/MM/dd") + "')";

                }
                else if (rblPostedDay.SelectedValue == "4")
                {
                    postedDate = "between Convert(DATE, '" + DateTime.Now.AddDays(-5).ToString("yyyy/MM/dd") + "') and Convert(DATE, '" + date.ToString("yyyy/MM/dd") + "')";

                }
                else
                {
                    postedDate = "between Convert(DATE, '" + DateTime.Now.AddDays(-10).ToString("yyyy/MM/dd") + "') and Convert(DATE, '" + date.ToString("yyyy/MM/dd") + "')";
                }

            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }



            return postedDate;
        }

        protected void lbReset_Click(object sender, EventArgs e)
        {
            ddlCountry.ClearSelection();
            cblType.ClearSelection();
            rblPostedDay.SelectedValue = "0";
            RBSelectedColorChange();
            fnBindJobList();
        }

        void RBSelectedColorChange()
        {
            if (rblPostedDay.SelectedItem.Selected == true)
            {
                rblPostedDay.SelectedItem.Attributes.Add("class", "selectedradio");
            }
        }
    }
}