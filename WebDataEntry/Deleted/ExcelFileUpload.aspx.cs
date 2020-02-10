using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.Data.Common;
using System.IO;
//using Excel = Microsoft.Office.Interop.Excel;

namespace WebDataEntry.Deleted
{
    public partial class ExcelFileUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridview();
            }
        }

        private void BindGridview()
        {
            //string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            //using (SqlConnection con = new SqlConnection(CS))
            //{
            //    SqlCommand cmd = new SqlCommand("spGetAllEmployee", con);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    con.Open();
            //    GridView1.DataSource = cmd.ExecuteReader();
            //    GridView1.DataBind();
            //}
        }


        private void DirectoryBrowse()
        {
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Title = "Select Excel File";
            //openFileDialog.CheckFileExists = true;
            //openFileDialog.AddExtension = true;
            //openFileDialog.Multiselect = false;
            //openFileDialog.Filter = "Excel File|*.xlsx;*.xls";


            ///*openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";*/
            //if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    foreach (string fileName in openFileDialog.FileNames)
            //    {
            //        File.Copy(fileName, @"\\fileserver\IT\" + Path.GetFileName(fileName));
            //        txtFileName.Text = fileName;
            //    }
            //}
        } // end if DirectoryBrowse()


        private void ViewFile()
        {
            DBClass dbconn = new DBClass();

            string connString = "";


            //string strFileType = txtFileName.Text.ToLower();
            string strFileType = Path.GetExtension(FileUpload2.FileName).ToLower();

            //Connection String to Excel Workbook
            if (strFileType.Trim() == ".xls")
            {
                connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileUpload2.FileName + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }
            else if (strFileType.Trim() == ".xlsx")
            {
                connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileUpload2.FileName + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }

            // (Date,Segment,Region,SAM,Area,BranchCode,Closed,Outstanding,Total,CompletionPer,Score)";
            //Date	Segment	BranchCode	Outstanding	Close	Grand Total	Completion	Score	Region	SAM	Area

            string query = "SELECT * ";
            query = query + " FROM [CLData$]";
            OleDbConnection conn = new OleDbConnection(connString);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            OleDbCommand cmd = new OleDbCommand(query, conn);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            //grvExcelData.DataSource = ds.Tables[0];
            DataTable dtView = ds.Tables[0];

            //rtbValidate.Text = rtbValidate.Text + "Total Records in File " + dtView.Rows.Count + Environment.NewLine;

            dbconn.OpenConection();

            //grvExcelData.Rows.Clear();
            GridView1.DataSource = dtView;


            //GridView1.Refresh();
            da.Dispose();
            conn.Close();
            conn.Dispose();
            dbconn.CloseConnection();
        } // end of process

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload1.PostedFile != null)
            {
                try
                {
                    string path = string.Concat(Server.MapPath("~/UploadFile/" + FileUpload1.FileName));
                    FileUpload1.SaveAs(path);
                    // Connection String to Excel Workbook  
                    string excelCS = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", path);
                    using (OleDbConnection con = new OleDbConnection(excelCS))
                    {
                        OleDbCommand cmd = new OleDbCommand("select * from [CLData]", con);
                        con.Open();
                        // Create DbDataReader to Data Worksheet  
                        DbDataReader dr = cmd.ExecuteReader();
                        // SQL Server Connection String  
                        string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                        // Bulk Copy to SQL Server   
                        SqlBulkCopy bulkInsert = new SqlBulkCopy(CS);
                        bulkInsert.DestinationTableName = "Employee";
                        bulkInsert.WriteToServer(dr);
                        BindGridview();
                        lblMessage.Text = "Your file uploaded successfully";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                    }
                }
                catch (Exception)
                {
                    lblMessage.Text = "Your file not uploaded";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            //ViewFile();
            
        }
    }
}