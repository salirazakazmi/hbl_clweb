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
using Microsoft.Web.Administration;
using System.Web.Hosting;

namespace WebDataEntry
{
    public partial class Upl_RN10698 : System.Web.UI.Page
    {
        DBClass dbclass = new DBClass();
        string userID;
        string pcondition = "";
        public int pbarrec = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Timeout = 30;

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

            if (FileUpload1.HasFile)
            {
                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                string FilePath = Server.MapPath(FolderPath + FileName);
                if (!File.Exists(FilePath))
                {
                    // then upload the file
                    FileUpload1.SaveAs(FilePath);
                }
                //FileUpload1.SaveAs(FilePath);

                string FileDate = Path.GetFileNameWithoutExtension(FileUpload1.PostedFile.FileName);
                FileDate = FileDate.Substring(FileDate.Length - 8);

                string sdt = null;
                string inputString = FileDate.ToString();
                DateTime dDate;

                if (DateTime.TryParse(inputString, out dDate))
                {
                    String.Format("{0:dd/MM/yyyy}", dDate);
                    sdt = dDate.ToString("MM/dd/yyyy");
                }

                lblFile.Text = FileUpload1.PostedFile.FileName;
                Save_To_DB(FilePath, Extension, "Yes", sdt);
                Save_MIS(sdt);
            }
        }

        private void Save_MIS(string MISDate)
        {
            dbclass.OpenConection();
            string query = "Delete [RN10698_MIS] where MISDate ='" + MISDate + "'";
            dbclass.ExecuteQueries(query);

            query = "";
            //query = query + " Insert into [dbo].[RN10698_MIS]";
            //query = query + " ([MISDATE] ,[RequestDate] ,[RequestID] ,[CIFNumber] ,[RegionName] ,[BranchCode] ,[CustName] ,[CustOnBoardingDate] ";
            //query = query + " ,[StatusDesc] ,[OBCode] ,[LastDate] ,[LastTM] , [Bucket] ,[Comments],[Days] ,[FirstDate] ,[Holidays] ,[WorkingDays]) ";
            //query = query + " Select A.MISDATE, A.RequestDate, A.RequestID,A.CIFNumber, A.RegionName, A.BranchCode,  A.CustName, CustOnBoardingDate, A.StatusDesc, A.OBCode, A.LastDate, A.LastTM, BB.Bucket, A.Comments,  ";
            //query = query + " Datediff(day, (Select Min(LastDate) From RN10698 CF where A.CIFNumber = CF.CIFNumber and A.MISDate=CF.MISDATE),Getdate() - 1) Days,  (Select Min(LastDate)";
            //query = query + " From RN10698 CF where A.CIFNumber = CF.CIFNumber and A.MISDate=CF.MISDATE) as FirstDate , ";
            //query = query + " (SElect Count(*) from Holidays where Date between(Select Min(LastDate) From RN10698 CF where A.CIFNumber = CF.CIFNumber and A.MISDate=CF.MISDATE) and Getdate()) Holidays, ";
            //query = query + " (Datediff(day, (Select Min(LastDate) From RN10698 CF where A.CIFNumber = CF.CIFNumber and A.MISDate=CF.MISDATE),Getdate() - 1)  - ";
            //query = query + " (SElect Count(*) from Holidays where Date between(Select Min(LastDate) From RN10698 CF where A.CIFNumber = CF.CIFNumber and A.MISDate=CF.MISDATE) and Getdate()) ) WorkingDays";
            //query = query + " from[RN10698] A Inner Join(Select MISDate, RequestID, max(Concat(LastDate, ' ', LastTM)) as LatestDTTM from[RN10698]";
            //query = query + " Group by MISDate, RequestID) B On A.RequestID = B.RequestID and A.MISDATE = B.MISDATE and Concat(LastDate,' ', LastTM) = B.LatestDTTM";
            //query = query + " Left Join[RN10698_Bucket] BB on A.statusDesc = BB.StatusDesc and A.OBCode = BB.OBCode Where 1 = 1 AND 1 = 1";
            //query = query + " AND A.MISDATE='" + MISDate + "'";


            query = query + " Insert into [dbo].[RN10698_MIS]";
            query = query + " ([MISDATE] ,[RequestDate] ,[RequestID] ,[CIFNumber] ,[RegionName] ,[BranchCode] ,[CustName] ,[CustOnBoardingDate] ";
            query = query + " ,[StatusDesc] ,[OBCode] ,[LastDate] ,[LastTM] , [Bucket] ,[Days] ,[FirstDate] ,[Holidays] ,[WorkingDays], CIFPurpose, CIFLastStatus , CIFType, CIFActive) ";

            query = query + " Select A.MISDate, A.RequestDate, A.RequestID, A.CIFNumber, A.Regionname, A.Branchcode,";
            query = query + " A.CustName, A.CustOnboardingDate, A.StatusDesc, A.OBCode, A.LastDate, A.LastTM,";
            query = query + " B.Bucket,";
            query = query + "  DATEDIFF(day, A.RequestDate, MISDate) Days,";
            query = query + "  A.RequestDate as FirstDate,";
            query = query + "  (Select count(*) from Holidays where Date between A.RequestDate AND A.MISDate) as Holidays,";
            query = query + "   DATEDIFF(day, A.RequestDate, MISDate) - (Select count(*) from Holidays where Date between A.RequestDate AND A.MISDate) WorkingDays,";
            query = query + "   A.CIFPurpose, A.CIFLastStatus , A.CIFTYPE, A.CIFActive";
            query = query + "  from RN10698 A Left Join [RN10698_BucketLast] B on A.CIFLastStatus = B.Status";
            query = query + " where MISDate='" + MISDate + "' and Sequence = (SElect Max(Sequence) from RN10698 R where";
            query = query + " A.MISDate=R.MISDate AND A.RequestID=R.RequestID)";



            dbclass.ExecuteQueries(query);

            query = "Delete From RN10698_MIS_UNIT where MISDATE = '" + MISDate + "'";
            dbclass.ExecuteQueries(query);

            // Insert data for Unit Aging
            query = "Insert into RN10698_MIS_UNIT (MISDATE, CIFNumber, Bucket, ActivityDate, PreviousActivityDate, ActivityTime, SeqNo,";
            query = query + " TotalDays, WorkingDays, LastUpdateDTTM) ";
            query = query + " Select s.MISDate, s.CIFNumber, b.Bucket, LastDate, ";
            query = query + " LAG(LastDate, 1) OVER(PARTITION by CIFNumber ORDER BY CIFNUMBER) as PrevDT,";
            query = query + " Max(LastTM) LastTM,";
            query = query + " ROW_NUMBER()  OVER(PARTITION BY CIFNumber ORDER BY LastDate, Max(LastTM)) RecNum,";
            query = query + " DATEDIFF(day, LAG(LastDate, 1) OVER(PARTITION by CIFNumber ORDER BY CIFNUMBER), LastDate) TotaDays,0 as 'WD',GetDate()";
            query = query + " From[RN10698] s";
            query = query + " LEft Join[RN10698_Bucket] B on s.StatusDesc = B.StatusDesc And s.OBCode = B.OBcode";
            query = query + " Where";
            //query = query + " S.OBCode = 'A0001'";
            query = query + " s.MISDate = '" + MISDate + "'";
            query = query + " Group by S.MISDAte, CIFNumber,b.Bucket, LastDate";
            query = query + " Order by s.MISDate, CIFNumber, LastDate, Max(LastTM)";
            dbclass.ExecuteQueries(query);



            // Calcluale Previous Date
            query = "Update RN10698_MIS_UNIT set PreviousActivityDate = I.DT";
            query = query + " From (";
            query = query + " Select *,";
            query = query + " LAG(ActivityDate, 1) OVER(PARTITION by CIFNumber Order by CIFNumber, SeqNo) as DT";
            query = query + " from RN10698_MIS_UNIT A";
            query = query + " Where MISDATE = '" + MISDate + "' ) as i";
            query = query + " Where RN10698_MIS_UNIT.MISDate = i.MISDATE and RN10698_MIS_UNIT.CIFNumber = i.CIFNumber";
            query = query + " and RN10698_MIS_UNIT.SeqNo = i.Seqno";

            dbclass.ExecuteQueries(query);

            // Update Total Days of Given MIS
            query = "Update [RN10698_MIS_UNIT] Set TotalDays = isnull(DATEDIFF(day, PreviousActivityDate, ActivityDate),0)";
            query = query + " where MISDATE = '" + MISDate + "'";
            dbclass.ExecuteQueries(query);


            query = "Update[RN10698_MIS_UNIT] Set WorkingDays = TotalDays - (SElect Count(*) from Holidays where Date between a.PreviousActivityDate And A.ActivityDate )";
            query = query + " from[dbo].RN10698_MIS_UNIT A";
            query = query + " where A.MISDATE = '" + MISDate + "'";
            dbclass.ExecuteQueries(query);



            query = "Update [RN10698_MIS_UNIT] Set CustomerOnBoardingDate = B.CustOnBoardingDate,";
            query = query + " Branch = B.BranchCode";
            query = query + " from [RN10698_MIS_UNIT] A Left Join[RN10698_MIS] B";
            query = query + " On A.CIFNumber = B.CIFNumber";
            query = query + " and A.MISDATE = B.MISDate";
            query = query + " Where A.[MISDATE] = '" + MISDate + "'";

            dbclass.ExecuteQueries(query);

            query = " Update [RN10698_MIS_UNIT] Set Region = B.Region";
            query = query + " from [RN10698_MIS_UNIT] A Left Join CL_Branch B";
            query = query + " On A.Branch = B.BranchCodeText";
            query = query + " Where A.[MISDATE] = '" + MISDate + "'";

            dbclass.ExecuteQueries(query);

            query = " UPDATE [RN10698_MIS_UNIT] SET LastRec = A.SeqNo";
            query = query + " from[RN10698_MIS_UNIT] A";
            query = query + " where MISDATE = '" + MISDate + "'";
            query = query + " and Seqno = (Select Max(SeqNo) from[RN10698_MIS_UNIT] B where A.MISDATE = B.MISDATE AND A.CIFNumber = B.CIFNumber  Group by B.MISDATE, B.CIFNumber)";

            dbclass.ExecuteQueries(query);





            dbclass.CloseConnection();
        }
        private void Save_To_DB(string FilePath, string Extension, string isHDR, String FileDate)
        {
            try
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

                //string query = "Select [Request ID] as RequestID, [Region name] as RegionName, [Branch code] as BranchCode, ANCF03 as CustName, [Branch Name] as BranchName,";
                //query = query + " [CIF Number] as CIF, [Status Desc] as StatusDesc, [F07 Date] as LastDate, [F07TM] as LastTime,";
                //query = query + " Comments, [Cust Onboarding Date] As CustOnBoardDate, [Rejection Remarks] as Rejection,";
                //query = query + " [Ob code] as OBCode,  [Date Maker] as DateMaker, [Time Maker] as TimeMaker, [Date Checker] as DateChecker, [Time Checker] as TimeChecker, ";
                //query = query + " [Date of Approval BCU] as DateBCUApproval, [Date of Approval AML] as DateAMLApproval,";// SUBSTITUTE([C.O.P.C Checker 1],'.','') as COPCChecker1, SUBSTITUTE([C.O.P.C Checker 2],'.','') as COPCChecker2, ";
                //query = query + " [Date Checker 1] as DateChecker1, [Date Checker 2] as DateChecker2, [Time Checker 1] as TimeChecker1, [Time Checker 2] as TimeChecker2 "; //, [New CIF and  CIF for Maintenance] NewOrMaintenace ";
                //query = query + " from [RN10698G$]";
                string query = "Select * from [RN10698X13$]";


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

                lblmsg.Text = "Date: " + FileDate + "Total Records in File " + dtView.Rows.Count + Environment.NewLine;


                dbclass.OpenConection();
                query = "Delete [RN10698] where MISDate ='" + FileDate + "'";
                dbclass.ExecuteQueries(query);

                int ratio = dtView.Rows.Count / 100;
                int rationcount = 0;
                int rowno = 0;
                foreach (DataRow row in dtView.Rows)
                {

                    string sdt = null;
                    string onboarddt = null;
                    string datechecker1 = null;
                    string datechecker2 = null;
                    string datemaker = null;
                    string datechecker = null;
                    string requestdate = null;


                    //string inputString = row["LastDate"].ToString();
                    string inputString = row["F07 Date"].ToString();
                    DateTime dDate;

                    if (DateTime.TryParse(inputString, out dDate))
                    {
                        String.Format("{0:dd/MM/yyyy}", dDate);
                        sdt = dDate.ToString("MM/dd/yyyy");
                    }

                    //inputString = row["CustOnBoardDate"].ToString();
                    inputString = row["Cust Onboarding Date"].ToString();
                    if (DateTime.TryParse(inputString, out dDate))
                    {
                        String.Format("{0:dd/MM/yyyy}", dDate);
                        onboarddt = dDate.ToString("MM/dd/yyyy");
                    }

                    //inputString = row["Datechecker"].ToString();
                    inputString = row["Date Checker               "].ToString();
                    if (DateTime.TryParse(inputString, out dDate))
                    {
                        String.Format("{0:dd/MM/yyyy}", dDate);
                        datechecker = dDate.ToString("MM/dd/yyyy");
                    }

                    //inputString = row["DateMaker"].ToString();
                    inputString = row["Date Maker"].ToString();
                    if (DateTime.TryParse(inputString, out dDate))
                    {
                        String.Format("{0:dd/MM/yyyy}", dDate);
                        datemaker = dDate.ToString("MM/dd/yyyy");
                    }

                    //inputString = row["DateChecker1"].ToString();
                    inputString = row["Date Checker 1"].ToString();
                    if (DateTime.TryParse(inputString, out dDate))
                    {
                        String.Format("{0:dd/MM/yyyy}", dDate);
                        datechecker1 = dDate.ToString("MM/dd/yyyy");
                    }

                    //inputString = row["DateChecker2"].ToString();
                    inputString = row["Date Checker 2"].ToString();
                    if (DateTime.TryParse(inputString, out dDate))
                    {
                        String.Format("{0:dd/MM/yyyy}", dDate);
                        datechecker2 = dDate.ToString("MM/dd/yyyy");
                    }

                    inputString = row["Request Date"].ToString();
                    if (DateTime.TryParse(inputString, out dDate))
                    {
                        String.Format("{0:dd/MM/yyyy}", dDate);
                        requestdate = dDate.ToString("MM/dd/yyyy");
                    }


                    String Comments = row["Comments"].ToString().Replace("\'", "");
                    Comments = Comments.Replace("\"", "");


                    //RequestID Regionname  Branchcode CustName    BranchName ZNAstatus   CustOnboardingDate CIFNumber   PEP StatusDesc  LastDate LastTM  Comments User
                    String insertQuery = "Insert into [RN10698] (RequestID, ";
                    insertQuery = insertQuery + " Regionname,Branchcode,CustName,CIFNumber,StatusDesc,Comments, CustonBoardingDate,RejectionRemarks, ";
                    insertQuery = insertQuery + " OBCode, DateMaker, TimeMaker, DateChecker, TimeChecker, DateBCUApproval, DateAMLApproval, DateChecker1, DateChecker2, TimeChecker1,TimeChecker2,";
                    insertQuery = insertQuery + " LastDate, LastTM,RequestDate, CIFPurpose, CIFLastStatus, Sequence, CIFType, CIFActive, MISDate, LastUpdateDTTM) ";
                    insertQuery = insertQuery + "Values ('" + row["Request ID"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Region name"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Branch code"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["ANCF03"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["CIF Number"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row[27].ToString() + "',";    //StatusDesc.
                    insertQuery = insertQuery + "'" + Comments + "',";
                    insertQuery = insertQuery + "'" + onboarddt + "',";
                    insertQuery = insertQuery + "'" + row["Rejection Remarks"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Ob code"].ToString() + "',";
                    insertQuery = insertQuery + "'" + datemaker + "',";
                    insertQuery = insertQuery + "'" + row["Time Maker"].ToString() + "',";
                    insertQuery = insertQuery + "'" + datechecker + "',";
                    insertQuery = insertQuery + "'" + row["Time Checker"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Date of Approval BCU"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Date of Approval AML"].ToString() + "',";
                    insertQuery = insertQuery + "'" + datechecker1 + "',";
                    insertQuery = insertQuery + "'" + datechecker2 + "',";
                    insertQuery = insertQuery + "'" + row["Time Checker 1"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Time Checker 2"].ToString() + "',";
                    insertQuery = insertQuery + "'" + sdt + "','" + row["F07TM"] + "','" + requestdate + "',";
                    insertQuery = insertQuery + "'" + row["Purpose of CIF Desc"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["CIF Status Desc"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Sequence"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row[39].ToString() + "',";
                    insertQuery = insertQuery + "'" + row[40].ToString() + "',";
                    insertQuery = insertQuery + "'" + FileDate + "',GetDate() )";

                    dbclass.ExecuteQueries(insertQuery);

                    //GridView1.DeleteRow(1);
                    rationcount++;
                    if (rationcount > 11)
                    {
                        rationcount = 0;
                        //pbarrec++;
                        pbarrec = 50;

                    }

                    rowno++;
                }

                dbclass.CloseConnection();
                conn.Close();
            }
            catch
            {
                lblmsg.Text = "Issue in File, it can't open";
            }

        }

        protected void btnCalculateMIS_Click(object sender, EventArgs e)
        {
            if (txtMISDate.Text != "")
            {
                Save_MIS(txtMISDate.Text);
                lblmsg.Text = "MIS Data Generated";
            }
            else
            {
                lblmsg.Text = "To Run the MIS Please provide correct Date";
            }
        }

        protected void btnAdjustBucket_Click(object sender, EventArgs e)
        {


            dbclass.OpenConection();
            string query;
            // Backup Current Bucket value
            query = " Update[RN10698_MIS_UNIT] Set OldBucket = Bucket";
            query = query + " Where MISDATE = '" + txtMISDate.Text + "'";

            dbclass.ExecuteQueries(query);

            query = "Update[RN10698_MIS] Set OldBucket = Bucket";
            query = query + " Where MISDATE = '" + txtMISDate.Text + "'";

            dbclass.ExecuteQueries(query);

            // Now Udpate MoveintoBucket with provided data

            query = "Update [RN10698_MIS] Set Bucket = A.MoveIntoBucket";
            query = query + " From[dbo].[RN10698_CIF_Movement] A";
            query = query + " Left Join[dbo].[RN10698_MIS] B On A.MISDATE = B.MISDATE AND A.CIFNumber = B.CIFNumber";
            query = query + " Where A.MISDATE = '" + txtMISDate.Text + "'";

            dbclass.ExecuteQueries(query);

            query = "Update [RN10698_MIS_UNIT] Set Bucket = A.MoveIntoBucket";
            query = query + " From[dbo].[RN10698_CIF_Movement] A";
            query = query + " Left Join[dbo].[RN10698_MIS_UNIT] B On A.MISDATE = B.MISDATE AND A.CIFNumber = B.CIFNumber";
            query = query + " Where A.MISDATE = '" + txtMISDate.Text + "' and B.LastRec Is not NULL";

            dbclass.ExecuteQueries(query);

            lblmsg.Text = "Bucket Updated";

        }

        protected void btnCIFMovement_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                string FilePath = Server.MapPath(FolderPath + FileName);
                if (!File.Exists(FilePath))
                {
                    // then upload the file
                    FileUpload1.SaveAs(FilePath);
                }
                //FileUpload1.SaveAs(FilePath);

                string FileDate = Path.GetFileNameWithoutExtension(FileUpload1.PostedFile.FileName);
                FileDate = FileDate.Substring(FileDate.Length - 8);


                lblFile.Text = FileUpload1.PostedFile.FileName;
                Upload_Bucket_Move_File(FilePath, Extension, "Yes");

            }
            else
            {
                lblmsg.Text = "Please select valid format file";
            }
        }   // Btn Movement Call

        private void Upload_Bucket_Move_File(string FilePath, string Extension, string isHDR)
        {
            try
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


                string query = "Select * from [Bucket$]";

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

                lblmsg.Text = "Total Records in File " + dtView.Rows.Count + Environment.NewLine;

                string inputString = txtMISDate.Text;
                DateTime dDate;
                string sdt = null;

                if (DateTime.TryParse(inputString, out dDate))
                {
                    String.Format("{0:dd/MM/yyyy}", dDate);
                    sdt = dDate.ToString("MM/dd/yyyy");
                }

                dbclass.OpenConection();
                query = "Delete [RN10698_CIF_Movement] where MISDate ='" + sdt + "'";
                dbclass.ExecuteQueries(query);


                int rowno = 0;
                foreach (DataRow row in dtView.Rows)
                {
                    string misdate = null;
                    inputString = row["MISDATE"].ToString();
                    if (DateTime.TryParse(inputString, out dDate))
                    {
                        String.Format("{0:dd/MM/yyyy}", dDate);
                        misdate = dDate.ToString("MM/dd/yyyy");
                    }

                    // File sheet name should be "Bucket" and column name same as DB Column in Excel
                    //RequestID Regionname  Branchcode CustName    BranchName ZNAstatus   CustOnboardingDate CIFNumber   PEP StatusDesc  LastDate LastTM  Comments User
                    String insertQuery = "Insert into [RN10698_CIF_Movement] (MISDate, CIFNumber, CurrentBucket, MoveIntoBucket ";
                    insertQuery = insertQuery + " , UserID, UpdateDTTM ) ";
                    insertQuery = insertQuery + "Values ('" + misdate + "',";
                    insertQuery = insertQuery + "'" + row["CIFNumber"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["CurrentBucket"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["MoveIntoBucket"].ToString() + "',";
                    insertQuery = insertQuery + "'" + userID + "',GetDate() )";

                    dbclass.ExecuteQueries(insertQuery);


                    rowno++;
                }

                dbclass.CloseConnection();
                conn.Close();
                lblmsg.Text = " Bucket Movement File Uploaded";
            }
            catch
            {
                lblmsg.Text = "Issue in File, can't open";

            }

        }   // Upload movement File

        protected void btnRestart_Click(object sender, EventArgs e)
        {

            System.Web.HttpRuntime.UnloadAppDomain();

            //using (ServerManager iisManager = new ServerManager())
            //{
            //    SiteCollection sites = iisManager.Sites;
            //    foreach (Site site in sites)
            //    {
            //        if (site.Name == HostingEnvironment.ApplicationHost.GetSiteName())
            //        {
            //            iisManager.ApplicationPools[site.Applications["/"].ApplicationPoolName].Recycle();
            //            break;
            //        }
            //    }
            //}

        } // 
    }
}