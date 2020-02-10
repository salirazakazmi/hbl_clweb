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
    public partial class PEPMIS_RN10698 : System.Web.UI.Page
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
            string query = "";
            dbclass.OpenConection();
            DataSet ds;
            
            query = "Select Distinct RegionName from [RN10698] ORder by RegionName";
         
            dbclass.DataReader(query);
            ds = dbclass.DataSet(query);
            DD_Region.Items.Add("");        // add blank item in list
            DD_Region.AppendDataBoundItems = true;
            DD_Region.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_Region.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_Region.DataSource = ds.Tables[0];
            DD_Region.DataBind();
        
        
            query = "Select Distinct BranchCode from [RN10698]  Order by BranchCode";
            dbclass.OpenConection();
            dbclass.DataReader(query); 
            ds = dbclass.DataSet(query);
            DD_Branch.Items.Add("");
            DD_Branch.AppendDataBoundItems = true;
            DD_Branch.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_Branch.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_Branch.DataSource = ds.Tables[0];
            DD_Branch.DataBind();


            query = "Select Distinct StatusDesc from [RN10698] Order by StatusDesc";
            dbclass.OpenConection();
            dbclass.DataReader(query);
            ds = dbclass.DataSet(query);
            DD_Status.Items.Add(""); 
            DD_Status.AppendDataBoundItems = true;
            DD_Status.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_Status.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_Status.DataSource = ds.Tables[0];
            DD_Status.DataBind();
            
        }

        // Manage Filters Condition
        private string GetFilters()
        {
            string condition;
            int more = 0;

            condition = " Where 1=1";
            if (DD_Region.SelectedValue != "")
            {
                more = 1;
                condition = condition + " AND RegionName = '" + DD_Region.SelectedValue + "'";
            }

           
           
            if (DD_Branch.SelectedValue != "")
            {
                condition = condition + " AND BranchCode = '" + DD_Branch.SelectedValue + "'";
                
            }

            if (DD_Status.SelectedValue != "")
            {
                condition = condition + " AND StatusDesc = '" + DD_Status.SelectedValue + "'";
                
            }

            if (txtAccount.Text != "")
            {
                condition = condition + " AND A.RequestID = '" + txtAccount.Text + "'";
                
            }

            if (txtCaseID.Text != "")
            {
                condition = condition + " AND A.CIFNumber = '" + txtCaseID.Text + "'";

               
            }


            return condition;
        }
        private void BindGridView()
        {

            dbclass.OpenConection();
            
            string query = "Select A.RequestID,A.CIFNumber, A.RegionName, A.BranchCode,  A.CustName, CustOnBoardingDate, A.StatusDesc, A.OBCode,";
            query = query + " A.LastDate, A.LastTM, BB.Bucket, A.Comments, ";
            query = query + " Datediff(day, (Select Min(LastDate) From RN10698 CF where A.CIFNumber = CF.CIFNumber),Getdate() - 1) Days, ";
            query = query + " (Select Min(LastDate) From RN10698 CF where A.CIFNumber = CF.CIFNumber) as FirstDate ,";
            query = query + " (SElect Count(*) from Holidays where Date between(Select Min(LastDate) From RN10698 CF where A.CIFNumber = CF.CIFNumber) and Getdate()) Holidays,";
            query = query + " (Datediff(day, (Select Min(LastDate) From RN10698 CF where A.CIFNumber = CF.CIFNumber),Getdate() - 1)  - ";
            query = query + " (SElect Count(*) from Holidays where Date between(Select Min(LastDate) From RN10698 CF where A.CIFNumber = CF.CIFNumber) and Getdate()) ) WorkingDays";
            query = query + " from[RN10698] A Inner Join(Select RequestID, max(Concat(LastDate, ' ', LastTM)) as LatestDTTM";
            query = query + " from[RN10698]  Group by RequestID) B On A.RequestID = B.RequestID and Concat(LastDate,' ', LastTM) = B.LatestDTTM";
            query = query + " Left Join[RN10698_Bucket] BB on A.statusDesc = BB.StatusDesc and A.OBCode = BB.OBCode";

            query = query + GetFilters() + " AND " + pcondition;
            query = query + " Order by LastDate Desc";

            dbclass.DataReader(query);
            DataSet ds = dbclass.DataSet(query);

            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }

            lblmsg.Text = "Total Records " + ds.Tables[0].Rows.Count + " Fetched";
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

        private void downloadCSV()
        {
            dbclass.OpenConection();
            string query = "Select A.RequestID,A.CIFNumber, A.RegionName, A.BranchCode,  A.CustName, CustOnBoardingDate, A.StatusDesc, A.OBCode,";
            query = query + " A.LastDate, A.LastTM, BB.Bucket, A.Comments, ";
            query = query + " Datediff(day, (Select Min(LastDate) From RN10698 CF where A.CIFNumber = CF.CIFNumber),Getdate() - 1) Days, ";
            query = query + " (Select Min(LastDate) From RN10698 CF where A.CIFNumber = CF.CIFNumber) as FirstDate ,";
            query = query + " (SElect Count(*) from Holidays where Date between(Select Min(LastDate) From RN10698 CF where A.CIFNumber = CF.CIFNumber) and Getdate()) Holidays,";
            query = query + " (Datediff(day, (Select Min(LastDate) From RN10698 CF where A.CIFNumber = CF.CIFNumber),Getdate() - 1)  - ";
            query = query + " (SElect Count(*) from Holidays where Date between(Select Min(LastDate) From RN10698 CF where A.CIFNumber = CF.CIFNumber) and Getdate()) ) WorkingDays";
            query = query + " from[RN10698] A Inner Join(Select RequestID, max(Concat(LastDate, ' ', LastTM)) as LatestDTTM";
            query = query + " from[RN10698]  Group by RequestID) B On A.RequestID = B.RequestID and Concat(LastDate,' ', LastTM) = B.LatestDTTM";
            query = query + " Left Join[RN10698_Bucket] BB on A.statusDesc = BB.StatusDesc and A.OBCode = BB.OBCode";

            query = query + GetFilters() + " AND " + pcondition;
            query = query + " Order by LastDate Desc";

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
            Response.AddHeader("content-disposition", "attachment;filename=PEPMISRN10698.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(csv);
            Response.Flush();
            Response.End();

        }

        protected void downloadCSV(object sender, EventArgs e)
        {
            downloadCSV();
        }
    }
}