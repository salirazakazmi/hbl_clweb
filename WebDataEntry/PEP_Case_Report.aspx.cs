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
    public partial class PEP_Case_Report : System.Web.UI.Page
    {
        DBClass dbclass = new DBClass();
        string userID;
        string pcondition = "";
        string workflowStatus = "";
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

            }

        }

        private void CheckRole()
        {
            string query2 = "";
            query2 = "Select * from Workflow_User where OptionName='PEP_Case' And Active='Y'";
            query2 = query2 + " AND UserID='" + userID + "'";
            dbclass.OpenConection();
            SqlDataReader dr2 = dbclass.DataReader(query2);
            string workflowrole = "";
            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    workflowrole = dr2["WorkflowLevel"].ToString();


                    if (workflowrole == "1")
                    {
                        workflowStatus = "('New','Reject')";
                    }
                    if (workflowrole == "2")
                    {
                        workflowStatus = "('InProcess')";
                    }
                    if (workflowrole == "3")
                    {
                        workflowStatus = "('ApprovalReq')";
                    }
                }
            }

            dbclass.CloseConnection();

        }
        private void Populate_DropDown()
        {
            string query = "select Distinct Year(CaseRaisedate) from PEP_CASE";

            dbclass.OpenConection();

            dbclass.DataReader(query);
            DataSet ds = dbclass.DataSet(query);

            DD_Year.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_Year.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_Year.DataSource = ds.Tables[0];
            DD_Year.DataBind();
            dbclass.CloseConnection();


            query = "select Distinct Month(CaseRaisedate) from PEP_CASE";

            dbclass.OpenConection();

            dbclass.DataReader(query);
            ds = dbclass.DataSet(query);

            DD_Month.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_Month.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_Month.DataSource = ds.Tables[0];
            DD_Month.DataBind();
            dbclass.CloseConnection();

            query = "Select Distinct RegionName from PEP_CASE Where " + pcondition + " ORder by RegionNAme";


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


            query = "Select Distinct CaseStatus from PEP_CASE Where " + pcondition + " ";
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
            CheckRole();
            if (DD_Status.SelectedValue != "")
            {
                condition = " Where CaseStatus = '" + DD_Status.SelectedValue + "'";
            }
            else
            {
                condition = " Where CaseStatus in " + workflowStatus;
            }

            //condition = " Where "
            //condition = " Where Year(CaseRaiseDate) = '" + DD_Year.SelectedValue + "' And Month(CaseRaiseDate) = '" + DD_Month.SelectedValue + "'";

            if (DD_Region.SelectedValue != "")
            {
                condition = condition + " AND RegionName = '" + DD_Region.SelectedValue + "'";
            }


            if (txtCaseID.Text != "")
            {
                condition = condition + " AND ID = '" + txtCaseID.Text + "'";
            }

            if (txtAccount.Text != "")
            {
                condition = condition + " AND CustomerID = '" + txtAccount.Text + "'";
            }

            return condition;
        }
        private void BindGridView()
        {


            string query = "Select * from PEP_CASE";
            query = query + GetFilters() + " AND " + pcondition;
            dbclass.OpenConection();
            dbclass.DataReader(query);
            DataSet ds = dbclass.DataSet(query);

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            GridView1.DataSource = ds;
            GridView1.DataBind();
            //}


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
            //////int userid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            ////string _id = GridView1.DataKeys[e.RowIndex].Values["ID"].ToString();
            //////string userid = GridView1.DataKeys[e.RowIndex].Value.ToString();
            ////GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            //////TextBox txt1 = (TextBox)row.FindControl("DDTarget");
            ////DropDownList txt1 = GridView1.Rows[e.RowIndex].FindControl("DDTarget") as DropDownList;
            ////TextBox txtcom = GridView1.Rows[e.RowIndex].FindControl("txtcomments") as TextBox;
            ////string txt = txt1.SelectedItem.ToString();
            ////GridView1.EditIndex = -1;
            ////dbclass.OpenConection();
            ////string query = "Update AlertSend set Status='" + txt1.Text + "', ";
            ////query = query + "Comments ='" + txtcom.Text + "',";
            ////query = query + "LastEditby ='" + userID + "',";
            ////query = query + "LastEditDate = GetDate()";
            ////query = query + " where id='" + _id + "'";
            ////dbclass.ExecuteQueries(query);
            ////dbclass.CloseConnection();
            ////BindGridView();
        }

        protected void cmdFilter_Click(object sender, EventArgs e)
        {
            BindGridView();
        }

        //protected void cmdCheckerReview_Click(object sender, EventArgs e)
        //{
        //   Lock();
        //}

        ////private void Lock()
        ////{
        ////    // If click on Review button then lock the table
        ////    foreach (GridViewRow row in GridView1.Rows)
        ////    {
        ////        if ((row.FindControl("chkSelect") as CheckBox).Checked)
        ////        {
        ////            string _id = GridView1.DataKeys[row.RowIndex].Values["ID"].ToString();
        ////            //int Emp_ID = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value);
        ////            dbclass.OpenConection();
        ////            string query = "Update AlertSend set Lock='Y',";
        ////            query = query + "LastReviewBy ='" + userID + "',";
        ////            query = query + "LastReviewDate = GetDate()";
        ////            query = query + " where id='" + _id + "'";
        ////            dbclass.ExecuteQueries(query);
        ////            dbclass.CloseConnection();
        ////        }
        ////    }
        ////    BindGridView();
        ////}

        ////private void UnLock()
        ////{
        ////    // If click on Review button then lock the table
        ////    foreach (GridViewRow row in GridView1.Rows)
        ////    {
        ////        if ((row.FindControl("chkSelect") as CheckBox).Checked)
        ////        {
        ////            string _id = GridView1.DataKeys[row.RowIndex].Values["ID"].ToString();
        ////            //int Emp_ID = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value);
        ////            dbclass.OpenConection();
        ////            string query = "Update AlertSend set Lock='N' where id='" + _id + "'";
        ////            dbclass.ExecuteQueries(query);
        ////            dbclass.CloseConnection();
        ////        }
        ////    }
        ////    BindGridView();
        ////}


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

        protected void OpenForm_Click(object sender, EventArgs e)
        {
            // This event open the PEP form with passing url value 
            // and in PEP form use this code to capture the url parameter HttpContext.Current.Request.QueryString["caseid"] != null 
            LinkButton btn = (LinkButton)(sender);
            string yourValue = btn.CommandArgument;
            string url = "~/PEP_Case_DE.aspx?caseid=" + yourValue.Trim();
            Response.Redirect(url);

            //LinkButton lnk = (LinkButton)sender;
            //int index = ((GridViewRow)lnk.NamingContainer).RowIndex;
        }



        ////protected void cmdLock_Click(object sender, EventArgs e)
        ////{
        ////   Lock();
        ////}

        ////protected void cmdUnLock_Click(object sender, EventArgs e)
        ////{
        ////    UnLock();
        ////}
    }
}