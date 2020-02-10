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
using System.Data.SqlClient;

namespace WebDataEntry
{
    public partial class Upl_CLCompileFiles : System.Web.UI.Page
    {
        DBClass dbclass = new DBClass();
        string userID;
        string pcondition = "";
        public int pbarrec = 0;
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
                case ".xlsb": //Excel xlsb
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
            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "move()", true);
            Boolean noissue = true;
            Int16 intmon = 0;
            string Month;
            intmon = Convert.ToInt16(txtMonth.Text.ToString());
            Month = intmon.ToString("00");

            if (txtYear.Text == "")
            {
                noissue = false;
            }

            if (txtMonth.Text == "")
            {
                noissue = false;
            }

            if (txtRemarks.Text == "")
            {
                noissue = false;
            }
            if (noissue)
            {
                if (FileUpload1.HasFile)
                {
                    string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                    string FilePath = Server.MapPath(FolderPath + FileName);
                    FileUpload1.SaveAs(FilePath);

                    //string FileDate = Path.GetFileNameWithoutExtension(FileUpload1.PostedFile.FileName);
                    //FileDate = FileDate.Substring(FileDate.Length - 8);

                    //string sdt = null;
                    //string inputString = FileDate.ToString();
                    //DateTime dDate;

                    //if (DateTime.TryParse(inputString, out dDate))
                    //{
                    //    String.Format("{0:dd/MM/yyyy}", dDate);
                    //    sdt = dDate.ToString("MM/dd/yyyy");
                    //}

                    lblFile.Text = FileUpload1.PostedFile.FileName;
                    //Save_To_DB(FilePath, Extension, "Yes", sdt);
                    if (ddSource.Text == "1")
                    {
                        lblmsg.Text = "Branch Data";
                        Update_Branch(FilePath, Extension, "Yes");
                    }
                    else
                    {
                        lblmsg.Text = "Region data";
                        Update_Region(FilePath, Extension, "Yes");
                    }
                }
            }

        } // end of function Upload



        private void Update_Branch(string FilePath, string Extension, string isHDR)
        {
            // Upload CL-Branch File, input by user (Sheetname = CLBranch)
            // Upload CL-Area File, Input by user (Sheetname = CLAREA)
            // Ask from User which you want to upload?
            // base on selection then Update the records
            // For Branch -- Match Year, Month, Segment, Branch Code if exists then overwrite else insert
            // For Region -- MAtch Year, Month, DataLevel, Segment if Area=Area, if SAM=SAM and Region=Region

            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                    break;
                case ".xlsb": //Excel xlsb
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePath, isHDR);

            string query = "Select * from [CLBranch$]";

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

            lblmsg.Text = "Total Records in File " + dtView.Rows.Count + Environment.NewLine;

            dbclass.OpenConection();

            int rowno = 0;
            int insertrow = 0;
            int updaterow = 0;
            foreach (DataRow row in dtView.Rows)
            {

                string Year = row["Year"].ToString();
                string Month = row["Month"].ToString();
                string BranchCodeText = row["BranchCodeText"].ToString();
                string Segment = row["Segment"].ToString();
                string Comments = row["Comments"].ToString().Replace("\'", "");
                Comments = Comments.Replace("\"", "");

                string checkquery = "";
                string dbquery = "";

                Boolean recordExist = false;

                checkquery = "Select * from CL_CALCULATED_DATA Where Year ='" + Year + "' And Month='" + Month + "'";
                checkquery = checkquery + " And BranchCodeText = '" + BranchCodeText + "' And Segment ='" + Segment + "'";
                SqlDataReader dr = dbclass.DataReader(checkquery);

                if (dr.HasRows)
                {
                    recordExist = true;
                }

                if (recordExist)
                {
                    // update

                    dbquery = "Update CL_CALCULATED_DATA Set ";
                    dbquery = dbquery + "AlertTotal = '" + row["AlertTotal"].ToString() + "',";
                    dbquery = dbquery + "AlertClosed = '" + row["AlertClosed"].ToString() + "',";
                    dbquery = dbquery + "AlertOutStanding = '" + row["AlertOutStanding"].ToString() + "',";
                    dbquery = dbquery + "AlertRatio = '" + row["AlertRatio"].ToString() + "',";
                    dbquery = dbquery + "AlertScore = '" + row["AlertScore"].ToString() + "',";
                    dbquery = dbquery + "STRRaised = '" + row["STRRaised"].ToString() + "',";
                    dbquery = dbquery + "STRRaisedScore = '" + row["STRRaisedScore"].ToString() + "',";
                    dbquery = dbquery + "STRConverted = '" + row["STRConverted"].ToString() + "',";
                    dbquery = dbquery + "STRConvertedScore = '" + row["STRConvertedScore"].ToString() + "',";
                    dbquery = dbquery + "CLTrainingTotal = '" + row["CLTrainingTotal"].ToString() + "',";
                    dbquery = dbquery + "CLTrainingClosed = '" + row["CLTrainingClosed"].ToString() + "',";
                    dbquery = dbquery + "CLTrainingOutStanding = '" + row["CLTrainingOutStanding"].ToString() + "',";
                    dbquery = dbquery + "CLTrainingRatio = '" + row["CLTrainingRatio"].ToString() + "',";
                    dbquery = dbquery + "CLTrainingScore = '" + row["CLTrainingScore"].ToString() + "',";
                    dbquery = dbquery + "ELTrainingTotal = '" + row["ELTrainingTotal"].ToString() + "',";
                    dbquery = dbquery + "ELTrainingClosed = '" + row["ELTrainingClosed"].ToString() + "',";
                    dbquery = dbquery + "ELTrainingOutStanding = '" + row["ELTrainingOutStanding"].ToString() + "',";
                    dbquery = dbquery + "ELTrainingRatio = '" + row["ELTrainingRatio"].ToString() + "',";
                    dbquery = dbquery + "ELTrainingScore = '" + row["ELTrainingScore"].ToString() + "',";
                    dbquery = dbquery + "KYCTotal = '" + row["KYCTotal"].ToString() + "',";
                    dbquery = dbquery + "KYCClosed = '" + row["KYCClosed"].ToString() + "',";
                    dbquery = dbquery + "KYCOutStanding = '" + row["KYCOutStanding"].ToString() + "',";
                    dbquery = dbquery + "KYCRatio = '" + row["KYCRatio"].ToString() + "',";
                    dbquery = dbquery + "KYCScore = '" + row["KYCScore"].ToString() + "',";
                    dbquery = dbquery + "BranchReviewTotal = '" + row["BranchReviewTotal"].ToString() + "',";
                    dbquery = dbquery + "BranchReviewClose = '" + row["BranchReviewClose"].ToString() + "',";
                    dbquery = dbquery + "BranchReviewOutStanding = '" + row["BranchReviewOutStanding"].ToString() + "',";
                    dbquery = dbquery + "BranchReviewRatio = '" + row["BranchReviewRatio"].ToString() + "',";
                    dbquery = dbquery + "BranchReviewScore = '" + row["BranchReviewScore"].ToString() + "',";
                    dbquery = dbquery + "PEPTotal = '" + row["PEPTotal"].ToString() + "',";
                    dbquery = dbquery + "PEPClose = '" + row["PEPClose"].ToString() + "',";
                    dbquery = dbquery + "PEPOutStanding = '" + row["PEPOutStanding"].ToString() + "',";
                    dbquery = dbquery + "PEPRatio = '" + row["PEPRatio"].ToString() + "',";
                    dbquery = dbquery + "PEPScore = '" + row["PEPScore"].ToString() + "',";
                    dbquery = dbquery + "TOP100Ratio = '" + row["TOP100Ratio"].ToString() + "',";
                    dbquery = dbquery + "TOP100Score = '" + row["TOP100Score"].ToString() + "',";
                    dbquery = dbquery + "TotalScore = '" + row["TotalScore"].ToString() + "',";
                    dbquery = dbquery + "TotalScoreWOBonus = '" + row["TotalScoreWOBonus"].ToString() + "',";
                    dbquery = dbquery + "RankOrder = '" + row["RankOrder"].ToString() + "',";
                    dbquery = dbquery + "RegionRankOrder = '" + row["RegionRankOrder"].ToString() + "',";
                    dbquery = dbquery + "Threshold = '" + row["Threshold"].ToString() + "'";
                    dbquery = dbquery + " Where Year='" + Year + "' And Month='" + Month + "'";
                    dbquery = dbquery + " AND BranchCodeText='" + BranchCodeText + "' And Segment='" + Segment + "'";

                    //dbquery = dbquery + "Comments = '" + Comments + "'";

                    dbclass.ExecuteQueries(dbquery);

                    //dbquery = "Insert into CL_WebUpload_Log (Year, Month,BranchCodeText, DataLevel,";
                    //dbquery = dbquery + "Region, SAM, Area, Comments, UpdateDTTM, UserID, Action ) Values (";
                    dbquery = "Insert into CL_WebUpload_Log (Year, Month,Segment, BranchCodeText, DataLevel,";
                    dbquery = dbquery + "Comments, UpdateDTTM, UserID, Action ) Values (";
                    dbquery = dbquery + "'" + Year + "',";
                    dbquery = dbquery + "'" + Month + "',";
                    dbquery = dbquery + "'" + Segment + "',";
                    dbquery = dbquery + "'" + BranchCodeText + "',";
                    dbquery = dbquery + "'Branch',";
                    dbquery = dbquery + "'" + Comments + "',";
                    dbquery = dbquery + "GetDate(),";
                    dbquery = dbquery + "'" + userID + "',";
                    dbquery = dbquery + "'Edit')";

                    dbclass.ExecuteQueries(dbquery);

                    updaterow++;
                }
                else
                {
                    // Insert
                    dbquery = "INSERT INTO [dbo].[CL_CALCULATED_DATA]";
                    dbquery = dbquery + " ( [Year] ,[Month] ,[BranchCodeText] ,[BranchCode],[BranchName]";
                    dbquery = dbquery + " ,[AreaName] ,[SAM] ,[Region] ,[Segment] ";
                    dbquery = dbquery + " ,[AlertTotal] ,[AlertClosed] ,[AlertOutStanding] ,[AlertRatio] ,[AlertScore]";
                    dbquery = dbquery + " ,[STRRaised] ,[STRRaisedScore] ,[STRConverted] ,[STRConvertedScore]";
                    dbquery = dbquery + " ,[CLTrainingTotal] ,[CLTrainingClosed] ,[CLTrainingOutStanding] ,[CLTrainingRatio] ,[CLTrainingScore]";
                    dbquery = dbquery + " ,[ELTrainingTotal] ,[ELTrainingClosed] ,[ELTrainingOutStanding] ,[ELTrainingRatio] ,[ELTrainingScore]";
                    dbquery = dbquery + " ,[KYCTotal] ,[KYCclosed] ,[KYCOutStanding] ,[KYCRatio] ,[KYCScore]";
                    dbquery = dbquery + " ,[BranchReviewTotal] ,[BranchReviewClose] ,[BranchReviewOutStanding] ,[BranchReviewRatio],[BranchReviewScore]";
                    dbquery = dbquery + " ,[PEPTotal] ,[PEPClose] ,[PEPOutstanding] ,[PEPRatio] ,[PEPScore]";
                    dbquery = dbquery + " ,[TOP100Ratio] ,[TOP100Score] ,[TotalScore],[TotalScoreWOBonus],[RankOrder]";
                    dbquery = dbquery + "  ,[RegionRankOrder] ,[Threshold]) VALUES(";
                    dbquery = dbquery + "'" + Year + "',";
                    dbquery = dbquery + "'" + Month + "',";
                    dbquery = dbquery + "'" + BranchCodeText + "',";
                    dbquery = dbquery + "'" + row["BranchCode"].ToString() + "',";
                    dbquery = dbquery + "'" + row["BranchName"].ToString() + "',";
                    dbquery = dbquery + "'" + row["AreaName"].ToString() + "',";
                    dbquery = dbquery + "'" + row["SAM"].ToString() + "',";
                    dbquery = dbquery + "'" + row["Region"].ToString() + "',";
                    dbquery = dbquery + "'" + row["Segment"].ToString() + "',";
                    dbquery = dbquery + "'" + row["AlertTotal"].ToString() + "',";
                    dbquery = dbquery + "'" + row["AlertClosed"].ToString() + "',";
                    dbquery = dbquery + "'" + row["AlertOutStanding"].ToString() + "',";
                    dbquery = dbquery + "'" + row["AlertRatio"].ToString() + "',";
                    dbquery = dbquery + "'" + row["AlertScore"].ToString() + "',";
                    dbquery = dbquery + "'" + row["STRRaised"].ToString() + "',";
                    dbquery = dbquery + "'" + row["STRRaisedScore"].ToString() + "',";
                    dbquery = dbquery + "'" + row["STRConverted"].ToString() + "',";
                    dbquery = dbquery + "'" + row["STRConvertedScore"].ToString() + "',";
                    dbquery = dbquery + "'" + row["CLTrainingTotal"].ToString() + "',";
                    dbquery = dbquery + "'" + row["CLTrainingClosed"].ToString() + "',";
                    dbquery = dbquery + "'" + row["CLTrainingOutStanding"].ToString() + "',";
                    dbquery = dbquery + "'" + row["CLTrainingRatio"].ToString() + "',";
                    dbquery = dbquery + "'" + row["CLTrainingScore"].ToString() + "',";
                    dbquery = dbquery + "'" + row["ELTrainingTotal"].ToString() + "',";
                    dbquery = dbquery + "'" + row["ELTrainingClosed"].ToString() + "',";
                    dbquery = dbquery + "'" + row["ELTrainingOutStanding"].ToString() + "',";
                    dbquery = dbquery + "'" + row["ELTrainingRatio"].ToString() + "',";
                    dbquery = dbquery + "'" + row["ELTrainingScore"].ToString() + "',";
                    dbquery = dbquery + "'" + row["KYCTotal"].ToString() + "',";
                    dbquery = dbquery + "'" + row["KYCClosed"].ToString() + "',";
                    dbquery = dbquery + "'" + row["KYCOutStanding"].ToString() + "',";
                    dbquery = dbquery + "'" + row["KYCRatio"].ToString() + "',";
                    dbquery = dbquery + "'" + row["KYCScore"].ToString() + "',";
                    dbquery = dbquery + "'" + row["BranchReviewTotal"].ToString() + "',";
                    dbquery = dbquery + "'" + row["BranchReviewClose"].ToString() + "',";
                    dbquery = dbquery + "'" + row["BranchReviewOutStanding"].ToString() + "',";
                    dbquery = dbquery + "'" + row["BranchReviewRatio"].ToString() + "',";
                    dbquery = dbquery + "'" + row["BranchReviewScore"].ToString() + "',";
                    dbquery = dbquery + "'" + row["PEPTotal"].ToString() + "',";
                    dbquery = dbquery + "'" + row["PEPClose"].ToString() + "',";
                    dbquery = dbquery + "'" + row["PEPOutStanding"].ToString() + "',";
                    dbquery = dbquery + "'" + row["PEPRatio"].ToString() + "',";
                    dbquery = dbquery + "'" + row["PEPScore"].ToString() + "',";
                    dbquery = dbquery + "'" + row["TOP100Ratio"].ToString() + "',";
                    dbquery = dbquery + "'" + row["TOP100Score"].ToString() + "',";
                    dbquery = dbquery + "'" + row["TotalScore"].ToString() + "',";
                    dbquery = dbquery + "'" + row["TotalScoreWOBonus"].ToString() + "',";
                    dbquery = dbquery + "'" + row["RankOrder"].ToString() + "',";
                    dbquery = dbquery + "'" + row["RegionRankOrder"].ToString() + "',";
                    dbquery = dbquery + "'" + row["Threshold"].ToString() + "')";


                    dbclass.ExecuteQueries(dbquery);

                    //dbquery = "Insert into CL_WebUpload_Log (Year, Month,BranchCodeText, DataLevel,";
                    //dbquery = dbquery + "Region, SAM, Area, Comments, UpdateDTTM, UserID, Action ) Values (";
                    dbquery = "Insert into CL_WebUpload_Log (Year, Month,Segment, BranchCodeText, DataLevel,";
                    dbquery = dbquery + "Comments, UpdateDTTM, UserID, Action ) Values (";
                    dbquery = dbquery + "'" + Year + "',";
                    dbquery = dbquery + "'" + Month + "',";
                    dbquery = dbquery + "'" + Segment + "',";
                    dbquery = dbquery + "'" + BranchCodeText + "',";
                    dbquery = dbquery + "'Branch',";
                    dbquery = dbquery + "'" + Comments + "',";
                    dbquery = dbquery + "GetDate(),";
                    dbquery = dbquery + "'" + userID + "',";
                    dbquery = dbquery + "'Add')";

                    dbclass.ExecuteQueries(dbquery);

                    insertrow++;
                }


            }   // End For

            dbclass.CloseConnection();
            lblmsg.Text = "Total Records in File " + dtView.Rows.Count + " Edit Records = " + updaterow + ", Inserted Records =" + insertrow;


        } // End of Branch Update in DB

        private void Update_Region(string FilePath, string Extension, string isHDR)
        {
            // 
            // Upload CL-Area File, Input by user (Sheetname = CLAREA)
            // Ask from User which you want to upload?
            // base on selection then Update the records
            // For Region -- MAtch Year, Month, DataLevel, Segment if Area=Area, if SAM=SAM and Region=Region

            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                    break;
                case ".xlsb": //Excel xlsb
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePath, isHDR);

            string query = "Select * from [CLArea$]";

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

            lblmsg.Text = "Total Records in File " + dtView.Rows.Count + Environment.NewLine;

            dbclass.OpenConection();

            int rowno = 0;
            int insertrow = 0;
            int updaterow = 0;
            foreach (DataRow row in dtView.Rows)
            {

                string Year = row["Year"].ToString();
                string Month = row["Month"].ToString();
                string DataLevel = row["DataLevel"].ToString();
                string Segment = row["Segment"].ToString();
                string Comments = row["Comments"].ToString().Replace("\'", "");
                Comments = Comments.Replace("\"", "");

                string checkquery = "";
                string dbquery = "";

                Boolean recordExist = false;

                checkquery = "Select * from CL_CALCULATED_DATA_AREA Where Year ='" + Year + "' And Month='" + Month + "'";
                checkquery = checkquery + " And DataLevel = '" + DataLevel + "' And Segment ='" + Segment + "'";
                if (DataLevel == "Area")
                {
                    checkquery = checkquery + " And AreaName ='" + row["AreaName"].ToString() + "'";
                }
                if (DataLevel == "SAM")
                {
                    checkquery = checkquery + " And SAM ='" + row["SAM"].ToString() + "'";
                }
                if (DataLevel == "Region")
                {
                    checkquery = checkquery + " And Region ='" + row["Region"].ToString() + "'";
                }

                SqlDataReader dr = dbclass.DataReader(checkquery);

                if (dr.HasRows)
                {
                    recordExist = true;
                }

                if (recordExist)
                {
                    // update Area

                    dbquery = "Update CL_CALCULATED_DATA_AREA Set ";
                    dbquery = dbquery + "TotalBranches = '" + row["TotalBranches"].ToString() + "',";
                    dbquery = dbquery + "TotalAreas = '" + row["TotalAreas"].ToString() + "',";
                    dbquery = dbquery + "AlertTotal = '" + row["AlertTotal"].ToString() + "',";
                    dbquery = dbquery + "AlertClosed = '" + row["AlertClosed"].ToString() + "',";
                    dbquery = dbquery + "AlertOutStanding = '" + row["AlertOutStanding"].ToString() + "',";
                    dbquery = dbquery + "AlertRatio = '" + row["AlertRatio"].ToString() + "',";
                    dbquery = dbquery + "AlertScore = '" + row["AlertScore"].ToString() + "',";
                    dbquery = dbquery + "STRRaised = '" + row["STRRaised"].ToString() + "',";
                    dbquery = dbquery + "STRRaisedScore = '" + row["STRRaisedScore"].ToString() + "',";
                    dbquery = dbquery + "STRConverted = '" + row["STRConverted"].ToString() + "',";
                    dbquery = dbquery + "STRConvertedScore = '" + row["STRConvertedScore"].ToString() + "',";
                    dbquery = dbquery + "CLTrainingTotal = '" + row["CLTrainingTotal"].ToString() + "',";
                    dbquery = dbquery + "CLTrainingClosed = '" + row["CLTrainingClosed"].ToString() + "',";
                    dbquery = dbquery + "CLTrainingOutStanding = '" + row["CLTrainingOutStanding"].ToString() + "',";
                    dbquery = dbquery + "CLTrainingRatio = '" + row["CLTrainingRatio"].ToString() + "',";
                    dbquery = dbquery + "CLTrainingScore = '" + row["CLTrainingScore"].ToString() + "',";
                    dbquery = dbquery + "ELTrainingTotal = '" + row["ELTrainingTotal"].ToString() + "',";
                    dbquery = dbquery + "ELTrainingClosed = '" + row["ELTrainingClosed"].ToString() + "',";
                    dbquery = dbquery + "ELTrainingOutStanding = '" + row["ELTrainingOutStanding"].ToString() + "',";
                    dbquery = dbquery + "ELTrainingRatio = '" + row["ELTrainingRatio"].ToString() + "',";
                    dbquery = dbquery + "ELTrainingScore = '" + row["ELTrainingScore"].ToString() + "',";
                    dbquery = dbquery + "KYCTotal = '" + row["KYCTotal"].ToString() + "',";
                    dbquery = dbquery + "KYCClosed = '" + row["KYCClosed"].ToString() + "',";
                    dbquery = dbquery + "KYCOutStanding = '" + row["KYCOutStanding"].ToString() + "',";
                    dbquery = dbquery + "KYCRatio = '" + row["KYCRatio"].ToString() + "',";
                    dbquery = dbquery + "KYCScore = '" + row["KYCScore"].ToString() + "',";
                    dbquery = dbquery + "BranchReviewTotal = '" + row["BranchReviewTotal"].ToString() + "',";
                    dbquery = dbquery + "BranchReviewClosed = '" + row["BranchReviewClosed"].ToString() + "',";
                    dbquery = dbquery + "BranchReviewOutStanding = '" + row["BranchReviewOutStanding"].ToString() + "',";
                    dbquery = dbquery + "BranchReviewRatio = '" + row["BranchReviewRatio"].ToString() + "',";
                    dbquery = dbquery + "BranchReviewScore = '" + row["BranchReviewScore"].ToString() + "',";
                    dbquery = dbquery + "PEPTotal = '" + row["PEPTotal"].ToString() + "',";
                    dbquery = dbquery + "PEPClosed = '" + row["PEPClosed"].ToString() + "',";
                    dbquery = dbquery + "PEPOutStanding = '" + row["PEPOutStanding"].ToString() + "',";
                    dbquery = dbquery + "PEPRatio = '" + row["PEPRatio"].ToString() + "',";
                    dbquery = dbquery + "PEPScore = '" + row["PEPScore"].ToString() + "',";
                    dbquery = dbquery + "TOP100Ratio = '" + row["TOP100Ratio"].ToString() + "',";
                    dbquery = dbquery + "TOP100Score = '" + row["TOP100Score"].ToString() + "',";
                    dbquery = dbquery + "TotalScore = '" + row["TotalScore"].ToString() + "',";
                    dbquery = dbquery + "TotalScoreWOBonus = '" + row["TotalScoreWOBonus"].ToString() + "',";
                    dbquery = dbquery + "RankOrder = '" + row["RankOrder"].ToString() + "',";
                    dbquery = dbquery + "RegionRankOrder = '" + row["RegionRankOrder"].ToString() + "',";
                    dbquery = dbquery + "Threshold = '" + row["Threshold"].ToString() + "'";
                    dbquery = dbquery + " Where Year='" + Year + "' And Month='" + Month + "'";
                    dbquery = dbquery + " AND DataLevel='" + DataLevel + "' And Segment='" + Segment + "'";
                    if (DataLevel == "Area")
                    {
                        checkquery = checkquery + " And AreaName ='" + row["AreaName"].ToString() + "'";
                    }
                    if (DataLevel == "SAM")
                    {
                        checkquery = checkquery + " And SAM ='" + row["SAM"].ToString() + "'";
                    }
                    if (DataLevel == "Region")
                    {
                        checkquery = checkquery + " And Region ='" + row["Region"].ToString() + "'";
                    }

                    //dbquery = dbquery + "Comments = '" + Comments + "'";

                    dbclass.ExecuteQueries(dbquery);

                    dbquery = "Insert into CL_WebUpload_Log (Year, Month,Segment, BranchCodeText, DataLevel,";
                    dbquery = dbquery + "Region, SAM, Area, Comments, UpdateDTTM, UserID, Action ) Values (";
                    dbquery = dbquery + "'" + Year + "',";
                    dbquery = dbquery + "'" + Month + "',";
                    dbquery = dbquery + "'" + Segment + "',";
                    dbquery = dbquery + "'CL_AREA',";
                    dbquery = dbquery + "'" + DataLevel + "',";
                    dbquery = dbquery + "'" + row["Region"].ToString() + "',";
                    dbquery = dbquery + "'" + row["SAM"].ToString() + "',";
                    dbquery = dbquery + "'" + row["AreaName"].ToString() + "',";
                    dbquery = dbquery + "'" + Comments + "',";
                    dbquery = dbquery + "GetDate(),";
                    dbquery = dbquery + "'" + userID + "',";
                    dbquery = dbquery + "'Edit')";

                    dbclass.ExecuteQueries(dbquery);

                    updaterow++;
                }
                else
                {
                    // Insert Area

                    dbquery = "INSERT INTO [dbo].[CL_CALCULATED_DATA_AREA]";
                    dbquery = dbquery + " ( [Year] ,[Month] ";
                    dbquery = dbquery + " ,[AreaName] ,[SAM] ,[Region] ,[Segment] ";
                    dbquery = dbquery + " ,[DataLevel], [TotalBranches]  ,[TotalAreas]";
                    dbquery = dbquery + " ,[AlertTotal] ,[AlertClosed] ,[AlertOutStanding] ,[AlertRatio] ,[AlertScore]";
                    dbquery = dbquery + " ,[STRRaised] ,[STRRaisedScore] ,[STRConverted] ,[STRConvertedScore]";
                    dbquery = dbquery + " ,[CLTrainingTotal] ,[CLTrainingClosed] ,[CLTrainingOutStanding] ,[CLTrainingRatio] ,[CLTrainingScore]";
                    dbquery = dbquery + " ,[ELTrainingTotal] ,[ELTrainingClosed] ,[ELTrainingOutStanding] ,[ELTrainingRatio] ,[ELTrainingScore]";
                    dbquery = dbquery + " ,[KYCTotal] ,[KYCclosed] ,[KYCOutStanding] ,[KYCRatio] ,[KYCScore]";
                    dbquery = dbquery + " ,[BranchReviewTotal] ,[BranchReviewClosed] ,[BranchReviewOutStanding] ,[BranchReviewRatio],[BranchReviewScore]";
                    dbquery = dbquery + " ,[PEPTotal] ,[PEPClosed] ,[PEPOutstanding] ,[PEPRatio] ,[PEPScore]";
                    dbquery = dbquery + " ,[TOP100Ratio] ,[TOP100Score] ,[TotalScore],[TotalScoreWOBonus],[RankOrder]";
                    dbquery = dbquery + "  ,[RegionRankOrder] ,[Threshold]) VALUES(";
                    dbquery = dbquery + "'" + Year + "',";
                    dbquery = dbquery + "'" + Month + "',";
                    dbquery = dbquery + "'" + row["AreaName"].ToString() + "',";
                    dbquery = dbquery + "'" + row["SAM"].ToString() + "',";
                    dbquery = dbquery + "'" + row["Region"].ToString() + "',";
                    dbquery = dbquery + "'" + row["Segment"].ToString() + "',";
                    dbquery = dbquery + "'" + row["DataLevel"].ToString() + "',";
                    dbquery = dbquery + "'" + row["TotalBranches"].ToString() + "',";
                    dbquery = dbquery + "'" + row["TotalAreas"].ToString() + "',";
                    dbquery = dbquery + "'" + row["AlertTotal"].ToString() + "',";
                    dbquery = dbquery + "'" + row["AlertClosed"].ToString() + "',";
                    dbquery = dbquery + "'" + row["AlertOutStanding"].ToString() + "',";
                    dbquery = dbquery + "'" + row["AlertRatio"].ToString() + "',";
                    dbquery = dbquery + "'" + row["AlertScore"].ToString() + "',";
                    dbquery = dbquery + "'" + row["STRRaised"].ToString() + "',";
                    dbquery = dbquery + "'" + row["STRRaisedScore"].ToString() + "',";
                    dbquery = dbquery + "'" + row["STRConverted"].ToString() + "',";
                    dbquery = dbquery + "'" + row["STRConvertedScore"].ToString() + "',";
                    dbquery = dbquery + "'" + row["CLTrainingTotal"].ToString() + "',";
                    dbquery = dbquery + "'" + row["CLTrainingClosed"].ToString() + "',";
                    dbquery = dbquery + "'" + row["CLTrainingOutStanding"].ToString() + "',";
                    dbquery = dbquery + "'" + row["CLTrainingRatio"].ToString() + "',";
                    dbquery = dbquery + "'" + row["CLTrainingScore"].ToString() + "',";
                    dbquery = dbquery + "'" + row["ELTrainingTotal"].ToString() + "',";
                    dbquery = dbquery + "'" + row["ELTrainingClosed"].ToString() + "',";
                    dbquery = dbquery + "'" + row["ELTrainingOutStanding"].ToString() + "',";
                    dbquery = dbquery + "'" + row["ELTrainingRatio"].ToString() + "',";
                    dbquery = dbquery + "'" + row["ELTrainingScore"].ToString() + "',";
                    dbquery = dbquery + "'" + row["KYCTotal"].ToString() + "',";
                    dbquery = dbquery + "'" + row["KYCClosed"].ToString() + "',";
                    dbquery = dbquery + "'" + row["KYCOutStanding"].ToString() + "',";
                    dbquery = dbquery + "'" + row["KYCRatio"].ToString() + "',";
                    dbquery = dbquery + "'" + row["KYCScore"].ToString() + "',";
                    dbquery = dbquery + "'" + row["BranchReviewTotal"].ToString() + "',";
                    dbquery = dbquery + "'" + row["BranchReviewClosed"].ToString() + "',";
                    dbquery = dbquery + "'" + row["BranchReviewOutStanding"].ToString() + "',";
                    dbquery = dbquery + "'" + row["BranchReviewRatio"].ToString() + "',";
                    dbquery = dbquery + "'" + row["BranchReviewScore"].ToString() + "',";
                    dbquery = dbquery + "'" + row["PEPTotal"].ToString() + "',";
                    dbquery = dbquery + "'" + row["PEPClosed"].ToString() + "',";
                    dbquery = dbquery + "'" + row["PEPOutStanding"].ToString() + "',";
                    dbquery = dbquery + "'" + row["PEPRatio"].ToString() + "',";
                    dbquery = dbquery + "'" + row["PEPScore"].ToString() + "',";
                    dbquery = dbquery + "'" + row["TOP100Ratio"].ToString() + "',";
                    dbquery = dbquery + "'" + row["TOP100Score"].ToString() + "',";
                    dbquery = dbquery + "'" + row["TotalScore"].ToString() + "',";
                    dbquery = dbquery + "'" + row["TotalScoreWOBonus"].ToString() + "',";
                    dbquery = dbquery + "'" + row["RankOrder"].ToString() + "',";
                    dbquery = dbquery + "'" + row["RegionRankOrder"].ToString() + "',";
                    dbquery = dbquery + "'" + row["Threshold"].ToString() + "')";


                    dbclass.ExecuteQueries(dbquery);

                    dbquery = "Insert into CL_WebUpload_Log (Year, Month,Segment, BranchCodeText, DataLevel,";
                    dbquery = dbquery + "Region, SAM, Area, Comments, UpdateDTTM, UserID, Action ) Values (";
                    dbquery = dbquery + "'" + Year + "',";
                    dbquery = dbquery + "'" + Month + "',";
                    dbquery = dbquery + "'" + Segment + "',";
                    dbquery = dbquery + "'CL_AREA',";
                    dbquery = dbquery + "'" + DataLevel + "',";
                    dbquery = dbquery + "'" + row["Region"].ToString() + "',";
                    dbquery = dbquery + "'" + row["SAM"].ToString() + "',";
                    dbquery = dbquery + "'" + row["AreaName"].ToString() + "',";
                    dbquery = dbquery + "'" + Comments + "',";
                    dbquery = dbquery + "GetDate(),";
                    dbquery = dbquery + "'" + userID + "',";
                    dbquery = dbquery + "'Add')";

                    dbclass.ExecuteQueries(dbquery);

                    insertrow++;
                }


            }   // End For

            dbclass.CloseConnection();
            lblmsg.Text = "Total Records in File " + dtView.Rows.Count + " Edit Records = " + updaterow + ", Inserted Records =" + insertrow;


        } // End of Area / SAM / Region Update in DB

        protected void btnGrandTotal_Click(object sender, EventArgs e)
        {
            Boolean noissue = true;
            Int16 intmon = 0;
            string month;
            string year;
            intmon = Convert.ToInt16(txtMonth.Text.ToString());
            month = intmon.ToString("00");
            year = txtYear.Text.ToString();

            if (txtYear.Text == "")
            {
                noissue = false;
            }

            if (txtMonth.Text == "")
            {
                noissue = false;
            }

            if (txtRemarks.Text == "")
            {
                noissue = false;
            }
            if (noissue)
            {
                string query = "";
                dbclass.OpenConection();



                // Calculate Grand Total
                query = "";
                query = query + "Update[dbo].[CL_CALCULATED_DATA]  Set TotalScore = (isnull(AlertScore, 0) + isnull(STRRaisedScore, 0) + isnull(STRConvertedScore, 0)";
                query = query + " +isnull(CLTrainingScore, 0) + isnull(ELTrainingScore, 0) +";
                query = query + " +isnull(KYCScore, 0) + isnull(BranchReviewScore, 0) + isnull(PepScore, 0) + isnull(Top100Score, 0))";
                query = query + " Where Year = '" + year + "' and Month = '" + month + "'";


                dbclass.ExecuteQueries(query);

                query = "";
                query = query + "Update[dbo].[CL_CALCULATED_DATA_AREA]  Set TotalScore = (isnull(AlertScore, 0) + isnull(STRRaisedScore, 0) + isnull(STRConvertedScore, 0) +";
                query = query + " isnull(CLTrainingScore, 0) + isnull(ELTrainingScore, 0) +";
                query = query + " isnull(KYCScore, 0) + isnull(BranchReviewScore, 0) + isnull(PepScore, 0) + isnull(Top100Score, 0))";
                query = query + " Where Year = '" + year + "' and Month = '" + month + "'";

                dbclass.ExecuteQueries(query);

                // Calculate total Score without Bonus Point
                query = "";
                query = query + "Update[dbo].[CL_CALCULATED_DATA]  Set TotalScoreWOBonus = (isnull(AlertScore, 0) ";
                query = query + " +isnull(CLTrainingScore, 0) + isnull(ELTrainingScore, 0) +";
                query = query + " +isnull(KYCScore, 0) + isnull(BranchReviewScore, 0) + isnull(PepScore, 0) + isnull(Top100Score, 0))";
                query = query + " Where Year = '" + year + "' and Month = '" + month + "'";


                dbclass.ExecuteQueries(query);

                query = "";
                query = query + "Update[dbo].[CL_CALCULATED_DATA_AREA]  Set TotalScoreWOBonus = (isnull(AlertScore, 0)  +";
                query = query + " isnull(CLTrainingScore, 0) + isnull(ELTrainingScore, 0) +";
                query = query + " isnull(KYCScore, 0) + isnull(BranchReviewScore, 0) + isnull(PepScore, 0) + isnull(Top100Score, 0))";
                query = query + " Where Year = '" + year + "' and Month = '" + month + "'";

                dbclass.ExecuteQueries(query);

                //=IF(AK1691>=100,"Fully Compliant",IF(AK1691>=90,"Significantly Compliant",
                //IF(AK1691>=70,"Partially Compliant",IF(AK1691>=41,"Marginally Compliant",IF(AK1691<41,"Non-Compliant")))))
                // Apply Threshold

                query = "";
                query = query + "Update CL_CALCULATED_DATA  Set Threshold =";
                query = query + " IIF(TotalScore >= 100, 'Fully Compliant', IIF(TotalScore >= 90, 'Significantly Compliant', IIF(TotalScore >= 70, 'Partially Compliant', IIF(TotalScore >= 41, 'Marginally Compliant',";
                query = query + " IIF(TotalScore < 41, 'Non-Compliant', '0')))))";
                query = query + " Where Year = '" + year + "' and Month = '" + month + "'";

                dbclass.ExecuteQueries(query);

                // Now applying Ranking

                query = "";
                query = query + "Update x  Set x.RankOrder = x.SN From ";
                //query = query + " ( Select *, ROW_NUMBER() Over(partition by Segment Order by TotalScore Desc) As SN from CL_CALCULATED_DATA ";
                query = query + " ( Select *, ROW_NUMBER() Over(Order by TotalScore Desc) As SN from CL_CALCULATED_DATA ";
                query = query + " Where Year = '" + year + "' and Month = '" + month + "' and Segment in ('Retail','Islamic') ) x";

                dbclass.ExecuteQueries(query);

                query = "";
                query = query + "Update x  Set x.RankOrder = x.SN From ";
                query = query + " ( Select *, ROW_NUMBER() Over(Order by TotalScore Desc) As SN from CL_CALCULATED_DATA ";
                query = query + " Where Year = '" + year + "' and Month = '" + month + "' and Segment in ('Commercial') ) x";

                dbclass.ExecuteQueries(query);

                query = "";
                query = query + "Update x  Set x.RankOrder = x.SN From ";
                query = query + " ( Select *, ROW_NUMBER() Over( Order by TotalScore Desc) As SN from CL_CALCULATED_DATA ";
                query = query + " Where Year = '" + year + "' and Month = '" + month + "' and Segment in ('Corporate') ) x";

                dbclass.ExecuteQueries(query);

                query = "";
                query = query + " Update x Set x.RegionRankOrder = x.SN From (";
                query = query + " Select ROW_NUMBER() Over(partition by Region Order by TotalScore Desc) As SN, * from CL_CALCULATED_DATA";
                query = query + " Where Year = '" + year + "' and Month = '" + month + "' and Segment in ('Retail','Islamic') ) x";
                dbclass.ExecuteQueries(query);

                query = "";
                query = query + " Update x Set x.RegionRankOrder = x.SN From (";
                query = query + " Select ROW_NUMBER() Over(partition by Region Order by TotalScore Desc) As SN, * from CL_CALCULATED_DATA";
                query = query + " Where Year = '" + year + "' and Month = '" + month + "' and Segment in ('Commercial') ) x";
                dbclass.ExecuteQueries(query);

                query = "";
                query = query + " Update x Set x.RegionRankOrder = x.SN From (";
                query = query + " Select ROW_NUMBER() Over(partition by Region Order by TotalScore Desc) As SN, * from CL_CALCULATED_DATA";
                query = query + " Where Year = '" + year + "' and Month = '" + month + "' and Segment in ('Corporate') ) x";
                dbclass.ExecuteQueries(query);


                // Region, ARea, SAM Calculate Threshold

                query = "";
                query = query + "Update CL_CALCULATED_DATA_AREA  Set Threshold =";
                query = query + " IIF(TotalScore >= 100, 'Fully Compliant', IIF(TotalScore >= 90, 'Significantly Compliant', IIF(TotalScore >= 70, 'Partially Compliant', IIF(TotalScore >= 41, 'Marginally Compliant',";
                query = query + " IIF(TotalScore < 41, 'Non-Compliant', '0')))))";
                query = query + " Where Year = '" + year + "' and Month = '" + month + "'";

                dbclass.ExecuteQueries(query);

                query = "";
                query = query + "Update x  Set x.RankOrder = x.SN From ";
                query = query + " ( Select *, ROW_NUMBER() Over(partition by Segment Order by TotalScore Desc) As SN from CL_CALCULATED_DATA_AREA ";
                query = query + " Where DataLevel='Area' And Year = '" + year + "' and Month = '" + month + "' ) x";

                dbclass.ExecuteQueries(query);

                query = "";
                query = query + "Update x  Set x.RankOrder = x.SN From ";
                query = query + " ( Select *, ROW_NUMBER() Over(partition by Segment Order by TotalScore Desc) As SN from CL_CALCULATED_DATA_AREA ";
                query = query + " Where DataLevel='SAM' And Year = '" + year + "' and Month = '" + month + "' ) x";

                dbclass.ExecuteQueries(query);
                query = "";
                query = query + "Update x  Set x.RankOrder = x.SN From ";
                query = query + " ( Select *, ROW_NUMBER() Over(partition by Segment Order by TotalScore Desc) As SN from CL_CALCULATED_DATA_AREA ";
                query = query + " Where DataLevel='Region' AND Year = '" + year + "' and Month = '" + month + "' ) x";

                dbclass.ExecuteQueries(query);

                dbclass.CloseConnection();

                lblmsg.Text = "Grand Total Updated";
            }
            else
            {
                lblmsg.Text = "Must provide Year and Month";
            }
        }
    }
}