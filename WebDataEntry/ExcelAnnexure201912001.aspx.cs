using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data;


namespace WebDataEntry
{
    public partial class ExcelAnnexure201912001 : System.Web.UI.Page
    {
        DBClass dbclass = new DBClass();
        string userID;
        string role;
        string pcondition = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");

            }
            else
            {
                userID = Session["UserID"].ToString();
                role = Session["Role"].ToString();
            }

            if (Session["condition"] == null)
            {
                // Nothing to to
            }
            else
            {
                pcondition = Session["condition"].ToString();
            }


            if (!IsPostBack)
            {
                Populate_DropDown();
                BindGridView();
                CheckRole();
                
            }

        }

        private void CheckRole()
        {
                // Checking role to dispaly / hide specific row if table which is visible to specific role
            RowAdmin.Visible = false;
            RowSupervisor.Visible = false;
            
            if (role == "Supervisor")
            {
                RowSupervisor.Visible = true;
            }
            if (role == "Admin")
            {
                RowAdmin.Visible = true;
            }

        }

        protected string AllowEdit()
        {
            // Checking role to dispaly / hide specific row if table which is visible to specific role
            RowAdmin.Visible = false;
            RowSupervisor.Visible = false;

            if (role == "Supervisor")
            {
                return("Y");
            }
            if (role == "Admin")
            {
                return("Y");
            }

            return("N");
            
        }

        private void Populate_DropDown()
        {
            string query = "";
            
            dbclass.OpenConection();
          
            DataSet ds;


            query = " select Distinct AssignTo from ExcelAnnexure201912001";
            
            
            dbclass.DataReader(query);
            ds = dbclass.DataSet(query);
            DD_Segment.Items.Add("");
            DD_Segment.AppendDataBoundItems = true;
            DD_Segment.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_Segment.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_Segment.DataSource = ds.Tables[0];
            DD_Segment.DataBind();
            
            
            query = "Select Distinct Status from ExcelAnnexure201912001";
            dbclass.DataReader(query);
            ds = dbclass.DataSet(query);
            DD_Status.Items.Add("");
            DD_Status.AppendDataBoundItems = true;
            DD_Status.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_Status.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_Status.DataSource = ds.Tables[0];
            DD_Status.DataBind();


            query = "Select Distinct C2 from ExcelAnnexure201912001";
            dbclass.DataReader(query);
            ds = dbclass.DataSet(query);
            DD_Annexure.Items.Add("");
            DD_Annexure.AppendDataBoundItems = true;
            DD_Annexure.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_Annexure.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_Annexure.DataSource = ds.Tables[0];
            DD_Annexure.DataBind();

        }

        // Manage Filters Condition
        private string GetFilters()
        {
            string condition;

            condition = " Where 1 = 1";

            if (DD_Segment.SelectedValue != "")
            {
                condition = condition + " AND AssignTo = '" + DD_Segment.SelectedValue + "'";
            }

            if (DD_Annexure.SelectedValue != "")
            {
                condition = condition + " AND C2 = '" + DD_Annexure.SelectedValue + "'";
            }

            if (DD_Status.SelectedValue != "")
            {
                condition = condition + " AND Status = '" + DD_Status.SelectedValue + "'";
            }

            if (txtAccount.Text != "")
            {
                condition = condition + " AND C8 = '" + txtAccount.Text + "'";
            }

            if (txtSComment.Text != "")
            {
                condition = condition + " AND Comments Like '%" + txtSComment.Text + "%'";
            }
           
            return condition;
        }
        private void BindGridView()
        {

            dbclass.OpenConection();
            string query = "Select *";
            query = query + " from ExcelAnnexure201912001 ";
            query = query + GetFilters() + " AND " + pcondition;
            query = query + " Order by id";
            dbclass.DataReader(query);
            DataSet ds = dbclass.DataSet(query);

            //if (ds.Tables[0].Rows.Count > 0) { }
            
                lblMsg.Text = "Total Records Found: " + ds.Tables[0].Rows.Count;
                GridView1.DataSource = ds;
                GridView1.DataBind();
           

        }   // end of binggridview

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindGridView();
        }

      
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindGridView();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //int userid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            string _id = GridView1.DataKeys[e.RowIndex].Values["id"].ToString();
            //string userid = GridView1.DataKeys[e.RowIndex].Value.ToString();
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            //TextBox txt1 = (TextBox)row.FindControl("DDTarget");
            DropDownList txt1 = GridView1.Rows[e.RowIndex].FindControl("DDStatus") as DropDownList;
            TextBox txtcom = GridView1.Rows[e.RowIndex].FindControl("txtcomments") as TextBox;
            string txt = txt1.SelectedItem.ToString();

            GridView1.EditIndex = -1;
            dbclass.OpenConection();
            string query = "Update ExcelAnnexure201912001 set Status='" + txt1.Text + "', ";
            query = query + "Comments ='" + txtcom.Text + "',";
            query = query + "LastEditby ='" + userID + "',";
            query = query + "LastEditDate = GetDate()";
            query = query + " where id='" + _id + "'";
            dbclass.ExecuteQueries(query);
            dbclass.CloseConnection();
            BindGridView();
        }

        protected void cmdFilter_Click(object sender, EventArgs e)
        {
            BindGridView();
        }

        protected void cmdCheckerReview_Click(object sender, EventArgs e)
        {
           Lock();
        }

        private void Lock()
        {
            // If click on Review button then lock the table
            foreach (GridViewRow row in GridView1.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string _id = GridView1.DataKeys[row.RowIndex].Values["id"].ToString();
                    //int Emp_ID = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value);
                    dbclass.OpenConection();
                    string query = "Update ExcelAnnexure201912001 set Lock='Y',";
                    query = query + "LastReviewBy ='" + userID + "',";
                    query = query + "LastReviewDate = GetDate()";
                    query = query + " where id='" + _id + "'";
                    dbclass.ExecuteQueries(query);
                    dbclass.CloseConnection();
                }
            }
            BindGridView();
        }

        private void UnLock()
        {
            // If click on Review button then lock the table
            foreach (GridViewRow row in GridView1.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string _id = GridView1.DataKeys[row.RowIndex].Values["id"].ToString();
                    //int Emp_ID = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value);
                    dbclass.OpenConection();
                    string query = "Update ExcelAnnexure201912001 set Lock='N' where id='" + _id + "'";
                    dbclass.ExecuteQueries(query);
                    dbclass.CloseConnection();
                }
            }
            BindGridView();
        }


        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // increment PageIndex
            GridView1.PageIndex = e.NewPageIndex;

            // bind table again
            BindGridView();
        }

        protected void chkHeader_CheckedChanged(object sender, EventArgs e) 
        {
            // Select All Check box, autoPostBack = true when define check box control in header
            bool a = ((CheckBox)GridView1.HeaderRow.FindControl("chkHeader")).Checked;

            foreach (GridViewRow row in GridView1.Rows)
            {
                if (a == true)
                {
                    (row.FindControl("chkSelect") as CheckBox).Checked = true;

                }
                else
                {
                    (row.FindControl("chkSelect") as CheckBox).Checked = false;

                }

            }


        }

        protected void cmdGotoPage_Click(object sender, EventArgs e)
        {
            Int16 pg = Convert.ToInt16(txtPage.Text);
            if (pg > 0)
            {
                GridView1.PageIndex = pg; //since PageIndex starts from 0 by default.

                // bind table again
                BindGridView();
            }
        }

        protected void cmdLock_Click(object sender, EventArgs e)
        {
           Lock();
        }

        protected void cmdUnLock_Click(object sender, EventArgs e)
        {
            UnLock();
        }

        private void downloadCSV()
        {

            string query = "Select * ";
            query = query + " from ExcelAnnexure201912001 ";
            query = query + GetFilters() + " AND " + pcondition;
           

            dbclass.OpenConection();


            DataSet ds = dbclass.DataSet(query);


            string csv = string.Empty;

            foreach (DataColumn column in ds.Tables[0].Columns)
            {
                //Add the Header row for CSV file.
                csv += column.ColumnName + ',';
            }

            //Add new line.
            csv += "\r\n";

            foreach (DataRow row in ds.Tables[0].Rows)
            {

                foreach (DataColumn column in ds.Tables[0].Columns)
                {
                    //Add the Data rows.
                    csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                }

                //Add new line.
                csv += "\r\n";
            }

            //Download the CSV file.
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=SBPAnnexure.csv");
            Response.Charset = "";
            Response.ContentType = "application/html";
            Response.Output.Write(csv);
            Response.Flush();
            Response.End();

        }

        protected void cmdDownload_Click(object sender, EventArgs e)
        {
            downloadCSV();
        }
    }
}