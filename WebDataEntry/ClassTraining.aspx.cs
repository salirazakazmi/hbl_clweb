using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data;


// When copy form following changes are required
// .aspx file change Inherits="WebDataEntry.ClassTraining" As file name
// .aspx.cs change public partial class classtraining : System.Web.UI.Page
// Change Gridview Header Label as per form
// Change the Eval("Field") with new fields when applied, make sure each DB has Status, Lock, Comments, LastEditDate to save time
// for Example Domain is replace with Region
// Change status options value e.g. Untrained
// In CodeBehind Replace the Table name with New Destination Table
// Add the table entry to view option in User Management

namespace WebDataEntry
{
    public partial class ClassTraining : System.Web.UI.Page
    {
        DBClass dbclass = new DBClass();
        string userID;
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
            string role = Session["Role"].ToString();
            if (role == "Supervisor")
            {
                RowSupervisor.Visible = true;
            }
            if (role == "Admin")
            {
                RowAdmin.Visible = true;
            }

        }
        private void Populate_DropDown()
        {
            string query = "select Distinct Year(MonthYear) from Training_ClassRoom";
            
            dbclass.OpenConection();
            
            dbclass.DataReader(query);
            DataSet ds = dbclass.DataSet(query);
            
            DD_Year.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_Year.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_Year.DataSource = ds.Tables[0];
            DD_Year.DataBind();
            dbclass.CloseConnection();


            query = "select Distinct Month(MonthYear) from Training_ClassRoom";
            
            dbclass.OpenConection();

            dbclass.DataReader(query);
            ds = dbclass.DataSet(query);

            DD_Month.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_Month.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_Month.DataSource = ds.Tables[0];
            DD_Month.DataBind();
            dbclass.CloseConnection();

            query = "Select Distinct Region from Training_ClassRoom Where " + pcondition + " ORder by Region";
         

            dbclass.OpenConection();
            dbclass.DataReader(query);
            ds = dbclass.DataSet(query);
            DD_Region.Items.Add("");        // add blank item in list
            DD_Region.AppendDataBoundItems = true;
            DD_Region.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_Region.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_Region.DataSource = ds.Tables[0];
            DD_Region.DataBind();
            dbclass.CloseConnection();

            query = "Select Distinct AreaName from Training_ClassRoom Where " + pcondition + "  ORder by AreaName";
            dbclass.OpenConection();
            dbclass.DataReader(query);
            ds = dbclass.DataSet(query);
            DD_Area.Items.Add("");
            DD_Area.AppendDataBoundItems = true;
            DD_Area.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_Area.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_Area.DataSource = ds.Tables[0];
            DD_Area.DataBind();
            dbclass.CloseConnection();

            query = "Select Distinct BranchCode from Training_ClassRoom Where " + pcondition + "  Order by BranchCode";
            dbclass.OpenConection();
            dbclass.DataReader(query); 
            ds = dbclass.DataSet(query);
            DD_Branch.Items.Add("");
            DD_Branch.AppendDataBoundItems = true;
            DD_Branch.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_Branch.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_Branch.DataSource = ds.Tables[0];
            DD_Branch.DataBind();
            dbclass.CloseConnection();

            query = "Select Distinct Status from Training_ClassRoom Where " + pcondition + " ";
            dbclass.OpenConection();
            dbclass.DataReader(query);
            ds = dbclass.DataSet(query);
            DD_Status.Items.Add(""); 
            DD_Status.AppendDataBoundItems = true;
            DD_Status.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_Status.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_Status.DataSource = ds.Tables[0];
            DD_Status.DataBind();
            dbclass.CloseConnection();

        }

        // Manage Filters Condition
        private string GetFilters()
        {
            string condition;

            condition = " Where Year(MonthYear) = '" + DD_Year.SelectedValue + "' And Month(MonthYear) = '" + DD_Month.SelectedValue + "'";
            
            if (DD_Region.SelectedValue != "")
            {
                condition = condition + " AND Region = '" + DD_Region.SelectedValue + "'";
            }

           
            if (DD_Area.SelectedValue != "")
            {
                condition = condition + " AND AreaName = '" + DD_Area.SelectedValue + "'";
            }

            if (DD_Branch.SelectedValue != "")
            {
                condition = condition + " AND BranchCode = '" + DD_Branch.SelectedValue + "'";
            }

            return condition;
        }
        private void BindGridView()
        {

            dbclass.OpenConection();
            string query = "Select * from Training_ClassRoom";
            query = query + GetFilters() + " AND " + pcondition;
            dbclass.DataReader(query);
            DataSet ds = dbclass.DataSet(query);

            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
           

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
            string _id = GridView1.DataKeys[e.RowIndex].Values["ID"].ToString();
            //string userid = GridView1.DataKeys[e.RowIndex].Value.ToString();
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            //TextBox txt1 = (TextBox)row.FindControl("DDTarget");
            DropDownList txt1 = GridView1.Rows[e.RowIndex].FindControl("DDTarget") as DropDownList;
            TextBox txtcom = GridView1.Rows[e.RowIndex].FindControl("txtcomments") as TextBox;
            string txt = txt1.SelectedItem.ToString();
            GridView1.EditIndex = -1;
            dbclass.OpenConection();
            string query = "Update Training_ClassRoom set Status='" + txt1.Text + "', ";
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
                    string _id = GridView1.DataKeys[row.RowIndex].Values["ID"].ToString();
                    //int Emp_ID = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value);
                    dbclass.OpenConection();
                    string query = "Update Training_ClassRoom set Lock='Y',";
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
                    string _id = GridView1.DataKeys[row.RowIndex].Values["ID"].ToString();
                    //int Emp_ID = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value);
                    dbclass.OpenConection();
                    string query = "Update Training_ClassRoom set Lock='N' where id='" + _id + "'";
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
    }
}