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
using OnlineJobPortal.Admin;

namespace OnlineJobPortal.User
{
    public partial class InterviewTactics : System.Web.UI.Page
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
                fnConnectDB();
                fnBindFields();
                //fnBindType();
                fnBindGuide();
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



        protected void fnBindFields()
        {
            try
            {
                fnConnectDB();
                string qry = "SELECT g_id, Title FROM tblGuide";
                cmd = new SqlCommand(qry, conn);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                ddlField.DataSource = dt;
                ddlField.DataTextField = "Title";
                ddlField.DataValueField = "g_id";
                ddlField.DataBind();
                conn.Close();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }

        protected void fnBindType()
        {
            try
            {
                fnConnectDB();
                string qry = "SELECT g_id, Category FROM tblGuide";
                cmd = new SqlCommand(qry, conn);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                ddlType.DataSource = dt;
                ddlType.DataTextField = "Category";
                ddlType.DataValueField = "g_id";
                ddlType.DataBind();
                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }
        
        string Count(int count)
        {
            if (count > 1)
            {
                return "Total <b>" + count + "</b> fields found";
            }
            else if (count == 1)
            {
                return "Total <b>" + count + "</b> fields found";
            }
            else
            {
                return "No fields found";
            }
        }

        protected void fnBindGuide()
        {
            try
            {
                fnConnectDB();
                string qry = "SELECT Row_Number() over(Order by(Select 1)) as [Sr.No], g_id, Title, Description, Category, Level, guide FROM tblGuide";
                cmd = new SqlCommand(qry, conn);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                dtgInterview.DataSource = dt;
                dtgInterview.DataBind();
                lblCount.Text = Count(dt.Rows.Count);
                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }

        protected void ddlField_SelectedIndexChanged(object sender, EventArgs e)
        {
            fnConnectDB();

            string qryField = "SELECT Title FROM tblGuide WHERE g_id = @g_id";
            cmd = new SqlCommand(qryField, conn);
            cmd.Parameters.AddWithValue("@g_id", ddlField.SelectedValue);
            string field = cmd.ExecuteScalar().ToString();

            try
            {
                if (ddlField.SelectedValue != "0")
                {
                    fnConnectDB();
                    string qry = "SELECT Row_Number() OVER(ORDER BY (SELECT 1)) AS [Sr.No], g_id, Title, Description, Category, Level, guide " +
                                 "FROM tblGuide WHERE Title = @Title";

                    cmd = new SqlCommand(qry, conn);
                    cmd.Parameters.AddWithValue("@Title", field);

                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    sda.Fill(dt);
                    dtgInterview.DataSource = dt;
                    dtgInterview.DataBind();
                    lblCount.Text = Count(dt.Rows.Count);
                    RBSelectedColorChange();
                }
                else
                {
                    fnBindGuide();
                    RBSelectedColorChange();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            fnConnectDB();

            string qryType = "SELECT Category FROM tblGuide WHERE g_id = @g_id";
            cmd = new SqlCommand(qryType, conn);
            cmd.Parameters.AddWithValue("@g_id", ddlType.SelectedValue);
            object result = cmd.ExecuteScalar();
            string type = result != null ? result.ToString() : string.Empty;

            try
            {
                if (ddlType.SelectedValue != "0")
                {
                    if (ddlType.SelectedValue == "1")
                    {
                        fnConnectDB();
                        string qry = "SELECT Row_Number() OVER(ORDER BY (SELECT 1)) AS [Sr.No], g_id, Title, Description, Category, Level, guide " +
                                     "FROM tblGuide WHERE Category = 'Placement'";

                        cmd = new SqlCommand(qry, conn);

                        sda = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        sda.Fill(dt);
                        dtgInterview.DataSource = dt;
                        dtgInterview.DataBind();
                        lblCount.Text = Count(dt.Rows.Count);
                        RBSelectedColorChange();
                    }
                    else if (ddlType.SelectedValue == "2")
                    {
                        fnConnectDB();
                        string qry = "SELECT Row_Number() OVER(ORDER BY (SELECT 1)) AS [Sr.No], g_id, Title, Description, Category, Level, guide " +
                                     "FROM tblGuide WHERE Category = 'Internship'";

                        cmd = new SqlCommand(qry, conn);

                        sda = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        sda.Fill(dt);
                        dtgInterview.DataSource = dt;
                        dtgInterview.DataBind();
                        lblCount.Text = Count(dt.Rows.Count);
                        RBSelectedColorChange();
                    }
                }
                else
                {
                    fnBindGuide();
                    RBSelectedColorChange();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }

        //protected void cblType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        fnConnectDB();
        //        string type = string.Empty;
        //        type = selectedCheckBox();
        //        if (type != "")
        //        {
        //            string qry = "SELECT Row_Number() over(Order by(Select 1)) as [Sr.No], g_id, Title, Description, Category, Level, guide FROM tblGuide WHERE Category IN (" + type + ")";
        //            cmd = new SqlCommand(qry, conn);
        //            sda = new SqlDataAdapter(cmd);
        //            dt = new DataTable();
        //            sda.Fill(dt);
        //            dtgInterview.DataSource = dt;
        //            dtgInterview.DataBind();
        //            lblCount.Text = Count(dt.Rows.Count);
        //            RBSelectedColorChange();
        //        }
        //        else
        //        {
        //            fnBindGuide();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
        //    }
        //}

        //string selectedCheckBox()
        //{
        //    string type = string.Empty;

        //    try
        //    {
        //        for (int i = 0; i < cblType.Items.Count; i++)
        //        {
        //            if (cblType.Items[i].Selected)
        //            {
        //                type += "'" + cblType.Items[i].Text + "',";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
        //    }
        //    return type = type.TrimEnd(',');
        //}

        protected void lbReset_Click(object sender, EventArgs e)
        {
            ddlField.ClearSelection();
            ddlType.ClearSelection();
            rblLevel.SelectedValue = "0";
            RBSelectedColorChange();
            fnBindGuide();
        }

        protected void dtgInterview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dtgInterview.PageIndex = e.NewPageIndex;
            fnBindGuide();
        }



        protected void rblLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fnConnectDB();
                if (rblLevel.SelectedValue == "1")
                {
                    string qry = "SELECT Row_Number() over(Order by(Select 1)) as [Sr.No], g_id, Title, Description, Category, Level, guide FROM tblGuide WHERE Level = 'Basic'";
                    cmd = new SqlCommand(qry, conn);
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    sda.Fill(dt);
                    dtgInterview.DataSource = dt;
                    dtgInterview.DataBind();
                    lblCount.Text = Count(dt.Rows.Count);
                    RBSelectedColorChange();
                }
                else if (rblLevel.SelectedValue == "2")
                {
                    string qry = "SELECT Row_Number() over(Order by(Select 1)) as [Sr.No], g_id, Title, Description, Category, Level, guide FROM tblGuide WHERE Level = 'Moderate'";
                    cmd = new SqlCommand(qry, conn);
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    sda.Fill(dt);
                    dtgInterview.DataSource = dt;
                    dtgInterview.DataBind();
                    lblCount.Text = Count(dt.Rows.Count);
                    RBSelectedColorChange();
                }
                else if (rblLevel.SelectedValue == "3")
                {
                    string qry = "SELECT Row_Number() over(Order by(Select 1)) as [Sr.No], g_id, Title, Description, Category, Level, guide FROM tblGuide WHERE Level = 'Advance'";
                    cmd = new SqlCommand(qry, conn);
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    sda.Fill(dt);
                    dtgInterview.DataSource = dt;
                    dtgInterview.DataBind();
                    lblCount.Text = Count(dt.Rows.Count);
                    RBSelectedColorChange();
                }
                else
                {
                    fnBindGuide();
                    RBSelectedColorChange();
                }
                conn.Close();
            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }

        void RBSelectedColorChange()
        {
            if (rblLevel.SelectedItem.Selected == true)
            {
                rblLevel.SelectedItem.Attributes.Add("class", "selectedradio");
            }
        }

        protected void DownloadPDF_Click(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                try
                {
                    // Get the User ID from CommandArgument
                    int guideId = Convert.ToInt32((sender as Button).CommandArgument);

                    fnConnectDB();  // Ensure the connection is established properly

                    // Query to fetch the resume (PDF) from the database
                    string query = "SELECT guide FROM tblGuide WHERE g_id = @GuideId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@GuideId", guideId);

                        // Open the connection if it's not already open
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            byte[] fileData = reader["guide"] as byte[];  // Get the binary PDF data

                            // If data exists, initiate the download
                            if (fileData != null && fileData.Length > 0)
                            {
                                Response.Clear();
                                Response.ContentType = "application/pdf";
                                Response.AddHeader("Content-Disposition", "attachment;filename=Guide.pdf");
                                Response.BinaryWrite(fileData);
                                Response.End();
                            }
                            else
                            {
                                lblCount.Text = "No guide found.";
                            }
                        }

                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
                }
                finally
                {
                    // Ensure the connection is closed in the finally block
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}