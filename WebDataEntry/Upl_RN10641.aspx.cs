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
    public partial class Upl_RN10641 : System.Web.UI.Page
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
            cmdExcel.CommandText = "SELECT * From [" + txtSheet.Text + "$]";
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
                //string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                //string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                //string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                //string FilePath = Server.MapPath(FolderPath + FileName);
                //FileUpload1.SaveAs(FilePath);

                //lblFile.Text = FileUpload1.PostedFile.FileName;
                //Save_To_DB(FilePath, Extension, "Yes");

                Bulk_Insert();
            }
        }

        private void Bulk_Insert()
        {
            string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

            //Upload and save the file
            string FilePath = Server.MapPath("~/" + FolderPath) + Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(FilePath);
            string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
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
            conStr = String.Format(conStr, FilePath, "Yes");
            DateTime start = DateTime.Now;
            string query = " Select *  ";
            query = query + " FROM [" + txtSheet.Text + "$]";

            OleDbConnection conn = new OleDbConnection(conStr);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            OleDbCommand cmd = new OleDbCommand(query, conn);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dtView = ds.Tables[0];
            DateTime end = DateTime.Now;

            dbclass.OpenConection();
            string delquery = "Truncate table RN10641_STG";
            dbclass.ExecuteQueries(delquery);
            dbclass.CloseConnection();

            string consString = ConfigurationManager.ConnectionStrings["DBC"].ConnectionString;
            using (SqlConnection con = new SqlConnection(consString))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    //Set the database table name
                    sqlBulkCopy.DestinationTableName = "[RN10641_STG]";

                    SqlBulkCopyColumnMapping External = new SqlBulkCopyColumnMapping("External #", "ExternalNo");
                    sqlBulkCopy.ColumnMappings.Add(External);
                    SqlBulkCopyColumnMapping FileRegion = new SqlBulkCopyColumnMapping("Region code description", "FileRegion");
                    sqlBulkCopy.ColumnMappings.Add(FileRegion);
                    SqlBulkCopyColumnMapping Accountbranch = new SqlBulkCopyColumnMapping("Account branch", "Accountbranch");
                    sqlBulkCopy.ColumnMappings.Add(Accountbranch);
                    SqlBulkCopyColumnMapping Branchname = new SqlBulkCopyColumnMapping("Branch name", "Branchname");
                    sqlBulkCopy.ColumnMappings.Add(Branchname);
                    SqlBulkCopyColumnMapping BasicpartAccount = new SqlBulkCopyColumnMapping("Basic part of account number", "BasicpartAccount");
                    sqlBulkCopy.ColumnMappings.Add(BasicpartAccount);
                    SqlBulkCopyColumnMapping Accountsuffix = new SqlBulkCopyColumnMapping("Account suffix", "Accountsuffix");
                    sqlBulkCopy.ColumnMappings.Add(Accountsuffix);
                    SqlBulkCopyColumnMapping Customerfullname = new SqlBulkCopyColumnMapping("Customer full name", "Customerfullname");
                    sqlBulkCopy.ColumnMappings.Add(Customerfullname);
                    SqlBulkCopyColumnMapping ACOpenDate = new SqlBulkCopyColumnMapping("A/c Open Date", "ACOpenDate");
                    sqlBulkCopy.ColumnMappings.Add(ACOpenDate);
                    SqlBulkCopyColumnMapping KYCUpdateDate = new SqlBulkCopyColumnMapping("KYC Update Date", "KYCUpdateDate");
                    sqlBulkCopy.ColumnMappings.Add(KYCUpdateDate);
                    SqlBulkCopyColumnMapping Customertype = new SqlBulkCopyColumnMapping("Customer type", "Customertype");
                    sqlBulkCopy.ColumnMappings.Add(Customertype);
                    SqlBulkCopyColumnMapping Customertypedescription = new SqlBulkCopyColumnMapping("Customer type description", "Customertypedescription");
                    sqlBulkCopy.ColumnMappings.Add(Customertypedescription);
                    SqlBulkCopyColumnMapping CNICNumber = new SqlBulkCopyColumnMapping("CNIC Number", "CNICNumber");
                    sqlBulkCopy.ColumnMappings.Add(CNICNumber);
                    SqlBulkCopyColumnMapping Accountstatus = new SqlBulkCopyColumnMapping("Account status - inactive?", "Accountstatus");
                    sqlBulkCopy.ColumnMappings.Add(Accountstatus);
                    SqlBulkCopyColumnMapping Accountclosing = new SqlBulkCopyColumnMapping("Account closing?", "Accountclosing");
                    sqlBulkCopy.ColumnMappings.Add(Accountclosing);
                    SqlBulkCopyColumnMapping Accounttype = new SqlBulkCopyColumnMapping("Account type", "Accounttype");
                    sqlBulkCopy.ColumnMappings.Add(Accounttype);
                    SqlBulkCopyColumnMapping Accounttypedescription = new SqlBulkCopyColumnMapping("Account type description", "Accounttypedescription");
                    sqlBulkCopy.ColumnMappings.Add(Accounttypedescription);
                    SqlBulkCopyColumnMapping Accountstatusblocked = new SqlBulkCopyColumnMapping("Account status - blocked?", "Accountstatusblocked");
                    sqlBulkCopy.ColumnMappings.Add(Accountstatusblocked);
                    SqlBulkCopyColumnMapping Deceasedorliquidated = new SqlBulkCopyColumnMapping("Deceased or liquidated?", "Deceasedorliquidated");
                    sqlBulkCopy.ColumnMappings.Add(Deceasedorliquidated);
                    SqlBulkCopyColumnMapping Sundryanalysisname = new SqlBulkCopyColumnMapping("Sundry analysis name", "Sundryanalysisname");
                    sqlBulkCopy.ColumnMappings.Add(Sundryanalysisname);
                    SqlBulkCopyColumnMapping PEPZNA = new SqlBulkCopyColumnMapping("PEP Y/N - ZNA", "PEPZNA");
                    sqlBulkCopy.ColumnMappings.Add(PEPZNA);
                    SqlBulkCopyColumnMapping PEPZKD = new SqlBulkCopyColumnMapping("PEP Y/N - ZKD", "PEPZKD");
                    sqlBulkCopy.ColumnMappings.Add(PEPZKD);
                    SqlBulkCopyColumnMapping PurposeofAccountZKD = new SqlBulkCopyColumnMapping("Purpose of Account Description - ZKD", "PurposeofAccountZKD");
                    SqlBulkCopyColumnMapping PurposeofAccountZNA = new SqlBulkCopyColumnMapping("Purpose of Account Description - ZNA", "PurposeofAccountZNA");
                    SqlBulkCopyColumnMapping ProfessionDescription = new SqlBulkCopyColumnMapping("Profession Description", "ProfessionDescription");
                    SqlBulkCopyColumnMapping JobPosition = new SqlBulkCopyColumnMapping("Job Position / Rank description", "JobPosition");
                    SqlBulkCopyColumnMapping Institute = new SqlBulkCopyColumnMapping("Institute / Organization", "Institute");
                    SqlBulkCopyColumnMapping ResidentCountry = new SqlBulkCopyColumnMapping("Resident Country Description", "ResidentCountry");
                    SqlBulkCopyColumnMapping ParentCountry = new SqlBulkCopyColumnMapping("Parent Country Description", "ParentCountry");
                    SqlBulkCopyColumnMapping RiskCountry = new SqlBulkCopyColumnMapping("Risk Country Description", "RiskCountry");
                    SqlBulkCopyColumnMapping ExpectedMonthlyCreditturnoverZKD = new SqlBulkCopyColumnMapping("Expected Monthly Credit turnover - ZKD", "ExpectedMonthlyCreditturnoverZKD");
                    SqlBulkCopyColumnMapping ExpectedMonthlyDebitturnoverZKD = new SqlBulkCopyColumnMapping("Expected Monthly Debit turnover - ZKD", "ExpectedMonthlyDebitturnoverZKD");
                    SqlBulkCopyColumnMapping ActualMonthlyCreditturnover = new SqlBulkCopyColumnMapping("Actual Monthly Credit turnover", "ActualMonthlyCreditturnover");
                    SqlBulkCopyColumnMapping ActualMonthlydebitturnover = new SqlBulkCopyColumnMapping("Actual Monthly debit turnover", "ActualMonthlydebitturnover");
                    SqlBulkCopyColumnMapping Typeofcompany = new SqlBulkCopyColumnMapping("Type of company Description", "Typeofcompany");
                    SqlBulkCopyColumnMapping IndustrytypeDescription = new SqlBulkCopyColumnMapping("Industry type Description", "IndustrytypeDescription");
                    SqlBulkCopyColumnMapping RealBalance = new SqlBulkCopyColumnMapping("Real Balance             ", "RealBalance");
                    SqlBulkCopyColumnMapping Currencymnemonic = new SqlBulkCopyColumnMapping("Currency mnemonic     ", "Currencymnemonic");
                    SqlBulkCopyColumnMapping PKRBalance = new SqlBulkCopyColumnMapping("PKR Balance         ", "PKRBalance");
                    SqlBulkCopyColumnMapping ZNACIF = new SqlBulkCopyColumnMapping("ZNA CIF?", "ZNACIF");
                    SqlBulkCopyColumnMapping ZNAAccount = new SqlBulkCopyColumnMapping("ZNA Account?", "ZNAAccount");
                    SqlBulkCopyColumnMapping LastKYCUpdateDate = new SqlBulkCopyColumnMapping("KYC Update Date A/c", "LastKYCUpdateDate");
                    SqlBulkCopyColumnMapping BranchCode = new SqlBulkCopyColumnMapping("BranchCode", "BranchCode");
                    SqlBulkCopyColumnMapping Region = new SqlBulkCopyColumnMapping("Region", "Region");
                    SqlBulkCopyColumnMapping Segment = new SqlBulkCopyColumnMapping("Segment", "Segment");
                    SqlBulkCopyColumnMapping Area = new SqlBulkCopyColumnMapping("Area", "Area");
                    SqlBulkCopyColumnMapping SAM = new SqlBulkCopyColumnMapping("SAM", "SAM");
                    sqlBulkCopy.ColumnMappings.Add(PurposeofAccountZKD);
                    sqlBulkCopy.ColumnMappings.Add(PurposeofAccountZNA);
                    sqlBulkCopy.ColumnMappings.Add(ProfessionDescription);
                    sqlBulkCopy.ColumnMappings.Add(JobPosition);
                    sqlBulkCopy.ColumnMappings.Add(Institute);
                    sqlBulkCopy.ColumnMappings.Add(ResidentCountry);
                    sqlBulkCopy.ColumnMappings.Add(ParentCountry);
                    sqlBulkCopy.ColumnMappings.Add(RiskCountry);
                    sqlBulkCopy.ColumnMappings.Add(ExpectedMonthlyCreditturnoverZKD);
                    sqlBulkCopy.ColumnMappings.Add(ExpectedMonthlyDebitturnoverZKD);
                    sqlBulkCopy.ColumnMappings.Add(ActualMonthlyCreditturnover);
                    sqlBulkCopy.ColumnMappings.Add(ActualMonthlydebitturnover);
                    sqlBulkCopy.ColumnMappings.Add(Typeofcompany);
                    sqlBulkCopy.ColumnMappings.Add(IndustrytypeDescription);
                    sqlBulkCopy.ColumnMappings.Add(RealBalance);
                    sqlBulkCopy.ColumnMappings.Add(Currencymnemonic);
                    sqlBulkCopy.ColumnMappings.Add(PKRBalance);
                    sqlBulkCopy.ColumnMappings.Add(ZNACIF);
                    sqlBulkCopy.ColumnMappings.Add(ZNAAccount);
                    sqlBulkCopy.ColumnMappings.Add(LastKYCUpdateDate);
                    sqlBulkCopy.ColumnMappings.Add(BranchCode);
                    sqlBulkCopy.ColumnMappings.Add(Region);
                    sqlBulkCopy.ColumnMappings.Add(Segment);
                    sqlBulkCopy.ColumnMappings.Add(Area);
                    sqlBulkCopy.ColumnMappings.Add(SAM);


                    //[OPTIONAL]: Map the Excel columns with that of the database table
                    //sqlBulkCopy.ColumnMappings.Add("External #", "ExternalNo");
                    //sqlBulkCopy.ColumnMappings.Add("Region code description", "FileRegion");
                    //sqlBulkCopy.ColumnMappings.Add("KYC Update Date A/c", "LastKYCUpdateDate");
                    con.Open();
                    sqlBulkCopy.WriteToServer(dtView);
                    con.Close();
                }
            }


        }

        private void Save_To_DB(string FilePath, string Extension, string isHDR)
        {
            DateTime starttime = DateTime.Now;
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

            string query = " Select *  ";
            query = query + "  from [Sheet2$]";

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
            
            TimeSpan timeDiff = DateTime.Now - starttime;
            
            lblmsg.Text = "Total Records in File " + dtView.Rows.Count + Environment.NewLine + "Time Taken (Min)" + timeDiff.Minutes;

            dbclass.OpenConection();
            query = "Truncate table [RN10698]";
            dbclass.ExecuteQueries(query);
            int ratio = dtView.Rows.Count / 100;
            int rationcount = 0;
            int rowno = 0;
            foreach (DataRow row in dtView.Rows)
            {

                string sdt = null;
                string kycdt = null;
                string kycupdt = null;

                string inputString = row["A/c Open Date"].ToString();
                DateTime dDate;

                if (DateTime.TryParse(inputString, out dDate))
                {
                    String.Format("{0:dd/MM/yyyy}", dDate);
                    sdt = dDate.ToString("MM/dd/yyyy");
                }

                inputString = row["KYC Update Date"].ToString();
                if (DateTime.TryParse(inputString, out dDate))
                {
                    String.Format("{0:dd/MM/yyyy}", dDate);
                    kycdt = dDate.ToString("MM/dd/yyyy");
                }

                inputString = row["KYC Update Date A/c"].ToString();
                if (DateTime.TryParse(inputString, out dDate))
                {
                    String.Format("{0:dd/MM/yyyy}", dDate);
                    kycupdt = dDate.ToString("MM/dd/yyyy");
                }

                String id = row["External #"].ToString();
                string querycheck = "Select [ExternalNo] from [RN10641_STG] where ExternalNo='" + id + "'";

                SqlDataReader drdata = dbclass.DataReader(querycheck);
                int recfound = 0;

                if (drdata.HasRows)
                {
                    recfound = 1;
                }

                //String Comments = row["Comments"].ToString().Replace("\'", "");
                //Comments = Comments.Replace("\"", "");

                if (recfound == 1)
                {

                }
                else
                {

                    String insertQuery = "Insert into [RN10641_STG] (ExternalNo, ";
                    insertQuery = insertQuery + "  FileRegion, Accountbranch, Branchname,";
                    insertQuery = insertQuery + " BasicpartAccount, Accountsuffix, Customerfullname, ACOpenDate, ";
                    insertQuery = insertQuery + " KYCUpdateDate, Customertype, Customertypedescription, CNICNumber, ";
                    insertQuery = insertQuery + " Accountstatus, Accountclosing, Accounttype, Accounttypedescription, ";
                    insertQuery = insertQuery + " Accountstatusblocked, Deceasedorliquidated,";
                    insertQuery = insertQuery + " Sundryanalysisname, RiskRating, PEPZNA, PEPZKD,";
                    insertQuery = insertQuery + " PurposeofAccountZKD, PurposeofAccountZNA, ProfessionDescription,";
                    insertQuery = insertQuery + " JobPosition, Institute, ResidentCountry, ParentCountry, RiskCountry,";
                    insertQuery = insertQuery + " ExpectedMonthlyCreditturnoverZKD, ExpectedMonthlyDebitturnoverZKD,";
                    insertQuery = insertQuery + " ActualMonthlyCreditturnover, ActualMonthlydebitturnover,";
                    insertQuery = insertQuery + " Typeofcompany, IndustrytypeDescription, RealBalance, Currencymnemonic,";
                    insertQuery = insertQuery + " PKRBalance, ZNACIF, ZNAAccount,";
                    insertQuery = insertQuery + " LastKYCUpdateDate, BranchCode, Region, Segment, Area, SAM) Values (";

                    insertQuery = insertQuery + "'" + row["External #"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Region code description"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Account branch"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Branch name"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Basic part of account number"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Account suffix"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Customer full name"].ToString() + "',";
                    insertQuery = insertQuery + "'" + sdt + "',";
                    insertQuery = insertQuery + "'" + kycdt + "',";
                    insertQuery = insertQuery + "'" + row["Customer type"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Customer type description"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["CNIC Number"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Account status - inactive?"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Account closing?"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Account type"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Account type description"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Account status - blocked?"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Deceased or liquidated?"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Sundry analysis name"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Risk Rating"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["PEP Y/N - ZNA"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["PEP Y/N - ZKD"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Purpose of Account Description - ZKD"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Purpose of Account Description - ZNA"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Profession Description"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Job Position / Rank description"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Institute / Organization"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Resident Country Description"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Parent Country Description"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Risk Country Description"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Expected Monthly Credit turnover - ZKD"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Expected Monthly Debit turnover - ZKD"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Actual Monthly Credit turnover"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Actual Monthly debit turnover"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Type of company Description"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Industry type Description"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Real Balance             "].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Currency mnemonic     "].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["PKR Balance         "].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["ZNA CIF?"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["ZNA Account?"].ToString() + "',";
                    insertQuery = insertQuery + "'" + kycupdt + "',";
                    insertQuery = insertQuery + "'" + row["BranchCode"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Region"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Segment"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["Area"].ToString() + "',";
                    insertQuery = insertQuery + "'" + row["SAM"].ToString() + "')";
                    dbclass.ExecuteQueries(insertQuery);


                }

                rowno++;
            }

            dbclass.CloseConnection();

            TimeSpan timeDiff2 = DateTime.Now - starttime;

            lblmsg.Text = "Total Records Processed " + dtView.Rows.Count + Environment.NewLine + "Time Taken (Min)" + timeDiff.Minutes;

        }


    }
}