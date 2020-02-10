using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data;
using System.Text;


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
    public partial class PEP_Annexure23Summary : System.Web.UI.Page
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
               
                
            }

        }

       
        private void Populate_DropDown()
        {
            string query = "select Distinct Year(TargetDate) from PEP_Annexure23";
            
            dbclass.OpenConection();
            
            dbclass.DataReader(query);
            DataSet ds = dbclass.DataSet(query);
            
            DD_Year.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_Year.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_Year.DataSource = ds.Tables[0];
            DD_Year.DataBind();
            dbclass.CloseConnection();


            query = "select Distinct Month(TargetDate) from PEP_Annexure23";
            
            dbclass.OpenConection();

            dbclass.DataReader(query);
            ds = dbclass.DataSet(query);

            DD_Month.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_Month.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_Month.DataSource = ds.Tables[0];
            DD_Month.DataBind();
            dbclass.CloseConnection();

            query = "Select Distinct Region from PEP_Annexure23 Where " + pcondition + " ORder by Region";
         

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

            query = "Select Distinct Cluster from PEP_Annexure23 Where " + pcondition + "  ORder by Cluster";
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

            query = "Select Distinct BranchCode from PEP_Annexure23 Where " + pcondition + "  Order by BranchCode";
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

            query = "Select Distinct FinalStatus from PEP_Annexure23 Where " + pcondition + " ";
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

            condition = " Where Year(TargetDate) = '" + DD_Year.SelectedValue + "' And Month(TargetDate) = '" + DD_Month.SelectedValue + "'";
            
            if (DD_Region.SelectedValue != "")
            {
                condition = condition + " AND Region = '" + DD_Region.SelectedValue + "'";
            }

           
            if (DD_Area.SelectedValue != "")
            {
                condition = condition + " AND Cluster = '" + DD_Area.SelectedValue + "'";
            }

            if (DD_Branch.SelectedValue != "")
            {
                condition = condition + " AND BranchCode = '" + DD_Branch.SelectedValue + "'";
            }

            return condition;
        }
        private void BindGridView()
        {


            string query = "Select TargetDate, Cluster, Region,";
                query = query + " Sum(Case FinalStatus When 'RTD' Then 1 ELSE 0 End) as RTD,";
            query = query + " Sum(Case FinalStatus When 'AC Closed' Then 1 ELSE 0 End) as 'AC Closed',";
            query = query + " Sum(Case FinalStatus When 'Account not yet opened remediation required' Then 1 ELSE 0 End) as 'Account not yet opened',";
            query = query + " Sum(Case FinalStatus When 'Corporate Relationship' Then 1 ELSE 0 End) as 'Corporate Relationship',";
            query = query + " Sum(Case FinalStatus When 'Debit block' Then 1 ELSE 0 End) as 'Debit block',";
            query = query + " Sum(Case FinalStatus When 'Deceased account' Then 1 ELSE 0 End) as 'Deceased account',";
            query = query + " Sum(Case FinalStatus When 'NA - Not pertained to BB' Then 1 ELSE 0 End) as 'NA - Not pertained to BB',";
            query = query + " Sum(Case FinalStatus When 'Not PEP' Then 1 ELSE 0 End) as 'Not PEP',";
            query = query + " Sum(Case FinalStatus When 'Remediated ' Then 1 ELSE 0 End) as 'Remediated',";
            query = query + " Count(FinalStatus) as Total";
            query = query + " from PEP_Annexure23";
            query = query + GetFilters() + " AND " + pcondition;
            query = query + " Group by TargetDate, Cluster, Region";

            dbclass.OpenConection();
            //string query = "Select * from PEP_Annexure_View1";
            //query = query + GetFilters() + " AND " + pcondition;
            dbclass.DataReader(query);
            DataSet ds = dbclass.DataSet(query);

            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            int col1 = 0;
            int col2 = 0;
            int col3 = 0;
            int col4 = 0;
            int col5 = 0;
            int col6 = 0;
            int col7 = 0;
            int col8 = 0;
            int col9 = 0;
            int col10 = 0;


            foreach (DataRow row in ds.Tables[0].Rows)
            {

                //foreach (DataColumn column in ds.Tables[0].Columns)
                //{
                col1 = col1 + int.Parse (row[3].ToString());
                col2 = col2 + int.Parse(row[4].ToString());
                col3 = col3 + int.Parse(row[5].ToString());
                col4 = col4 + int.Parse(row[6].ToString());
                col5 = col5 + int.Parse(row[7].ToString());
                col6 = col6 + int.Parse(row[8].ToString());
                col7 = col7 + int.Parse(row[9].ToString());
                col8 = col8 + int.Parse(row[10].ToString());
                col9 = col9 + int.Parse(row[11].ToString());
                col10 = col10 + int.Parse(row[12].ToString());

                // read column and item
                //}
            }
            //for (int k = 1; k < ds.Tables[0].Rows.Count - 1; k++)
            //{
            //    total = ds.Tables[0].Rows[k].c;

            //    total = ds.AsEnumerable().S.um(row => row.Field<Int32>(dt.Columns[k].ToString()));
            //    GridView1.FooterRow.Cells[k].Text = total.ToString();
            //    GridView1.FooterRow.Cells[k].Font.Bold = true;
            //    GridView1.FooterRow.BackColor = System.Drawing.Color.Beige;
            //}
            GridView1.FooterRow.Cells[3].Text = Convert.ToString(col1);
            GridView1.FooterRow.Cells[4].Text = Convert.ToString(col2);
            GridView1.FooterRow.Cells[5].Text = Convert.ToString(col3);
            GridView1.FooterRow.Cells[6].Text = Convert.ToString(col4);
            GridView1.FooterRow.Cells[7].Text = Convert.ToString(col5);
            GridView1.FooterRow.Cells[8].Text = Convert.ToString(col6);
            GridView1.FooterRow.Cells[9].Text = Convert.ToString(col7);
            GridView1.FooterRow.Cells[10].Text = Convert.ToString(col8);
            GridView1.FooterRow.Cells[11].Text = Convert.ToString(col9);
            GridView1.FooterRow.Cells[12].Text = Convert.ToString(col10);

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
            ////int userid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            //string _id = GridView1.DataKeys[e.RowIndex].Values["ID"].ToString();
            ////string userid = GridView1.DataKeys[e.RowIndex].Value.ToString();
            //GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            ////TextBox txt1 = (TextBox)row.FindControl("DDTarget");
            //DropDownList txt1 = GridView1.Rows[e.RowIndex].FindControl("DDfld1") as DropDownList;
            //TextBox txt2 = GridView1.Rows[e.RowIndex].FindControl("txtfld2") as TextBox;
            //TextBox txt3 = GridView1.Rows[e.RowIndex].FindControl("txtfld4") as TextBox;
            //DropDownList txt4 = GridView1.Rows[e.RowIndex].FindControl("DDfld3") as DropDownList;
            //TextBox txt5 = GridView1.Rows[e.RowIndex].FindControl("txtfld5") as TextBox;
            //TextBox txt6 = GridView1.Rows[e.RowIndex].FindControl("txtfld6") as TextBox;
            //TextBox txt7 = GridView1.Rows[e.RowIndex].FindControl("txtfld7") as TextBox;
            //string txt = txt1.SelectedItem.ToString();
            //GridView1.EditIndex = -1;
            //dbclass.OpenConection();
            //string query = "Update PEP_Annexure23 set FinalStatus='" + txt1.Text + "', ";
            //query = query + "Pep_Approval_GH ='" + txt2.Text + "',";
            //query = query + "ZNA_CIF ='" + txt3.Text + "',";
            //query = query + "Pep_MarkedinSystem ='" + txt4.Text + "',";
            //query = query + "ZNA_ActivationDate ='" + txt5.Text + "',";
            //query = query + "ZNA_Account ='" + txt6.Text + "',";
            //query = query + "EntryRemarks ='" + txt7.Text + "',";
            //query = query + "LastEditby ='" + userID + "',";
            //query = query + "LastEditDate = GetDate()";
            //query = query + " where id='" + _id + "'";
            //dbclass.ExecuteQueries(query);
            //dbclass.CloseConnection();
            //BindGridView();
        }

        protected void cmdFilter_Click(object sender, EventArgs e)
        {
            BindGridView();
        }

            


        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // increment PageIndex
            GridView1.PageIndex = e.NewPageIndex;

            // bind table again
            BindGridView();
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

       
       
        protected void cmdExcel_Click(object sender, EventArgs e)
        {
            //string strPath = HttpContext.Current.Server.MapPath("~/bin");
            //string phyPath = Request.PhysicalApplicationPath;
            //Response.Write(strPath + " <Br>" );
            //Response.Write(phyPath);
            //Response.Redirect("~/test.xlsx");
            string delimiter = ",";
            dbclass.OpenConection();
            string query = "Select * from PEP_Annexure_View1";
            query = query + GetFilters() + " AND " + pcondition;
            dbclass.DataReader(query);
            DataTable dt = dbclass.DataTable(query);

            //prepare the output stream
            Response.Clear();
            Response.ContentType = "text/csv";
            Response.AppendHeader("Content-Disposition",
                string.Format("attachment; filename={0}", "downloadfile.csv"));

            //write the csv column headers
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                Response.Write(dt.Columns[i].ColumnName);
                Response.Write((i < dt.Columns.Count - 1) ? delimiter : Environment.NewLine);
            }

            //write the data
            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(row[i].ToString());
                    Response.Write((i < dt.Columns.Count - 1) ? delimiter : Environment.NewLine);
                }
            }

            Response.End();

        }
    }
}