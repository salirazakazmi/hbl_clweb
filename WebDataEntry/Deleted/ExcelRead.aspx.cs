using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Configuration;

namespace WebDataEntry.Deleted
{
    public partial class ExcelRead : System.Web.UI.Page
    {
        DBClass dbclass = new DBClass();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {

            if (FileUpload1.HasFile)
            {
                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                string FilePath = Server.MapPath(FolderPath + FileName);
                FileUpload1.SaveAs(FilePath);

                lblFile.Text = FileUpload1.PostedFile.FileName;
                Import_To_Grid(FilePath, Extension, "Yes");
            }
        }
        private void Import_To_Grid(string FilePath, string Extension, string isHDR)
        {
            
            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePath, isHDR);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();
            
            //OleDbDataAdapter dataAdapter = new OleDbDataAdapter(cmdExcel);
            //DataSet dataSet = new DataSet();

            lblmsg.Text = dt.Rows.Count.ToString();


            //Bind Data to GridView
            GridView1.Caption = Path.GetFileName(FilePath);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
            string FileName = GridView1.Caption;
            string Extension = Path.GetExtension(FileName);
            string FilePath = Server.MapPath(FolderPath + FileName);

            Import_To_Grid(FilePath, Extension, "Yes");
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();

        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                string FilePath = Server.MapPath(FolderPath + FileName);
                FileUpload1.SaveAs(FilePath);

                lblFile.Text = FileUpload1.PostedFile.FileName;
                Save_To_DB(FilePath, Extension, "Yes");
            }
        }

        private void Save_To_DB(string FilePath, string Extension, string isHDR)
        {

            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePath, isHDR);
           
         
            string query = "Select [Request ID] as RequestID, [Region name] as RegionName, [Branch code] as BranchCode, ANCF03 as CustName, [Branch Name] as BranchName,";
            query = query + " [CIF Number] as CIF, [Status Desc] as StatusDesc, [F07 Date] as LastDate, [F07TM] as LastTime,";
            query = query + " Comments from [RN10698G$]";

            OleDbConnection conn = new OleDbConnection(conStr);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            OleDbCommand cmd = new OleDbCommand(query, conn);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dtView = ds.Tables[0];

            GridView1.DataSource = dtView;
            GridView1.DataBind();

            //OleDbDataAdapter dataAdapter = new OleDbDataAdapter(cmdExcel);
            //DataSet dataSet = new DataSet();

            lblmsg.Text =  "Total Records in File " + dtView.Rows.Count + Environment.NewLine;

            dbclass.OpenConection();

            int errorrow = 0;
            int rowno = 0;
            foreach (DataRow row in dtView.Rows)
            {
                
                    string sdt = null;
                    string inputString = row["LastDate"].ToString();
                    DateTime dDate;

                    if (DateTime.TryParse(inputString, out dDate))
                    {
                        String.Format("{0:dd/MM/yyyy}", dDate);
                        sdt = dDate.ToString("MM/dd/yyyy");
                    }

                    //RequestID Regionname  Branchcode CustName    BranchName ZNAstatus   CustOnboardingDate CIFNumber   PEP StatusDesc  LastDate LastTM  Comments User
                    String insertQuery = "Insert into [RN10698_2] (RequestID, ";
                    insertQuery = insertQuery + " Regionname,Branchcode,CustName,CIFNumber,StatusDesc,LastDate, LastTM) ";
                    insertQuery = insertQuery + "Values ('" + row["RequestID"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Regionname"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Branchcode"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["CustName"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["CIF"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["StatusDesc"].ToString() + "',";
                    insertQuery = insertQuery + "'" + sdt + "','" + row["LastTime"] + "')";

                    dbclass.ExecuteQueries(insertQuery);

                //GridView1.DeleteRow(1);
                lblmsg.Text = "Processsing .... " + rowno;
                
                rowno++;
            }

            dbclass.CloseConnection();


            //Bind Data to GridView
            //GridView1.Caption = Path.GetFileName(FilePath);
            //GridView1.DataSource = dt;
            //GridView1.DataBind();


        }
    }
}