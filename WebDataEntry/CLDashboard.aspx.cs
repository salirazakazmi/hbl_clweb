using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace WebDataEntry
{
    public partial class CLDashboard : System.Web.UI.Page
    {
        DBClass dbclass = new DBClass();

        // Public Variables for Dashboard Score
        public string ChartLabels = null;
        public string ChartData1 = null;
        public string ChartLabel2 = null;
        public string ChartData2 = null;
        public string ChartLabel3 = null;
        public string ChartData3 = null;
        public string ChartLabelRegdist = null;
        public string ChartDataRegdist = null;
        public string ChartLabelRegComm = null;
        public string ChartDataRegComm = null;
        public string ChartLabelRegCorp = null;
        public string ChartDataRegCorp = null;
        // Distrubution chart data
        public string ChartLabel4 = null;
        public string ChartData4 = null;
        public string ChartData4s2 = null;
        public string ChartData4s3 = null;
        public string ChartData4s4 = null;
        public string ChartData4s5 = null;
        public string ChartData4s6 = null;
        public string ChartData4s7 = null;
        // Commercial Chart Data
        public string ChartLabel5 = null;
        public string ChartData5 = null;
        public string ChartData5s2 = null;
        public string ChartData5s3 = null;
        public string ChartData5s4 = null;
        public string ChartData5s5 = null;
        public string ChartData5s6 = null;
        public string ChartData5s7 = null;
        // Coporate Chart Data
        public string ChartLabel6 = null;
        public string ChartData6 = null;
        public string ChartData6s2 = null;
        public string ChartData6s3 = null;
        public string ChartData6s4 = null;
        public string ChartData6s5 = null;
        public string ChartData6s6 = null;
        public string ChartData6s7 = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["UserID"] == null)
            //{
            //    Response.Redirect("Login.aspx");
            //}
            //else
            //{
            //    lblUser.Text = Session["UserID"].ToString();
            //}


            if (!IsPostBack)
            {
                dbclass.IP_LOG(Request.UserHostAddress, "CLDashboard.aspx");
                dropdownfill();
                getSegmentScore();
                getRegionThresholdDist();
                getRegionThresholdCommercial();
                getRegionThresholdCorporate();
                getBranchThreshold();
                getCommBranchThreshold();
                getCorpBranchThreshold();
                getRegionScore();
                getRegionScoreCommercial();
                getRegionScoreCorporate();
                getRegionScoreCorporate();
                fillgrid();
                fillDataTable();
                fillDataTableComm();
                fillDataTableCorp();

            }
        } // end page_load

        private void dropdownfill()
        {
            string query = "Select Distinct Year from CL_CALCULATED_DATA Order by Year Desc";

            dbclass.OpenConection();

            DataSet ds = dbclass.DataSet(query);

            DD_Year.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_Year.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_Year.DataSource = ds.Tables[0];
            DD_Year.DataBind();

            query = "select Month from CL_Month order by seqno desc";

            ds = dbclass.DataSet(query);

            DD_Month.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_Month.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_Month.DataSource = ds.Tables[0];
            DD_Month.DataBind();

        }

        private void fillDataTable()
        {
            string query = "Select Region, Threshold,";
            query = query + " Sum(TotalBranches) as TotalBranches, ";
            query = query + " Round(Avg(TotalScore), 2) as TotalScore,";
            query = query + " Round(Avg(AlertRatio), 2) * 100 as AlertRatio,";
            //query = query + " sum(StrRaised) as STRRaised, sum(STRConverted) ConvertedSTR,";
            query = query + " Round(Avg(CLTrainingRatio), 2) * 100 as CLRatio,";
            query = query + " Round(Avg(ELTrainingRatio), 2) * 100 as ELRatio,";
            query = query + " Round(Avg(KYCRatio), 2) * 100 as KYCRatio,";
            query = query + " Round(Avg(BranchReviewRatio), 2) * 100 as BRRatio,";
            query = query + " Round(Avg(PEPRatio), 2) * 100 as PERatio, ";
            query = query + " round(sum(Top100Score),2)  as Top100Ratio, Avg(RankOrder) as RankOder";

            query = query + " from CL_CALCULATED_DATA_AREA";
            query = query + " Where Segment in ('Retail') ";

            query = query + " AND Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";
            query = query + " and DataLevel = 'Region'";
            query = query + " Group by Region, Threshold Order by TotalScore desc";

            dbclass.OpenConection();

            SqlDataReader dr2 = dbclass.DataReader(query);

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    if (!dr2.IsDBNull(0))
                    {
                        TableRow row = new TableRow();
                        TableCell cell1 = new TableCell();
                        TableCell cell2 = new TableCell();
                        TableCell cell3 = new TableCell();
                        TableCell cell4 = new TableCell();
                        TableCell cell5 = new TableCell();
                        TableCell cell6 = new TableCell();
                        TableCell cell7 = new TableCell();
                        TableCell cell8 = new TableCell();
                        TableCell cell9 = new TableCell();
                        TableCell cell10 = new TableCell();
                        TableCell cell11 = new TableCell();
                        TableCell cell12 = new TableCell();
                        cell3.HorizontalAlign = HorizontalAlign.Right;
                        cell4.HorizontalAlign = HorizontalAlign.Right;
                        cell5.HorizontalAlign = HorizontalAlign.Right;
                        cell6.HorizontalAlign = HorizontalAlign.Right;
                        cell7.HorizontalAlign = HorizontalAlign.Right;
                        cell8.HorizontalAlign = HorizontalAlign.Right;
                        cell9.HorizontalAlign = HorizontalAlign.Right;
                        cell10.HorizontalAlign = HorizontalAlign.Right;
                        cell11.HorizontalAlign = HorizontalAlign.Right;
                        cell12.HorizontalAlign = HorizontalAlign.Right;


                        cell1.Text = dr2[0].ToString();

                        row.Cells.Add(cell1);
                        cell2.Text = dr2[1].ToString();
                        row.Cells.Add(cell2);
                        cell3.Text = dr2[2].ToString();
                        row.Cells.Add(cell3);
                        cell4.Text = dr2[3].ToString();
                        row.Cells.Add(cell4);
                        cell5.Text = dr2[4].ToString();

                        row.Cells.Add(cell5);
                        cell6.Text = dr2[5].ToString();
                        row.Cells.Add(cell6);
                        cell7.Text = dr2[6].ToString();
                        row.Cells.Add(cell7);
                        cell8.Text = dr2[7].ToString();
                        row.Cells.Add(cell8);
                        cell9.Text = dr2[8].ToString();
                        row.Cells.Add(cell9);
                        cell10.Text = dr2[9].ToString();
                        row.Cells.Add(cell10);
                        cell11.Text = dr2[10].ToString();
                        row.Cells.Add(cell11);
                        cell12.Text = dr2[11].ToString();
                        row.Cells.Add(cell12);


                        myTable.Rows.Add(row);

                    }
                }
            }



            //GridView1.ControlStyle.Font.Size = 7;
            dbclass.CloseConnection();
        }

        private void fillDataTableComm()
        {
            string query = "Select Region, Threshold,";
            query = query + " Sum(TotalBranches) as TotalBranches, ";
            query = query + " Round(Avg(TotalScore), 2)  as TotalScore,";
            query = query + " Round(Avg(AlertRatio), 2) * 100 as AlertRatio,";
            //query = query + " sum(StrRaised) as STRRaised, sum(STRConverted) ConvertedSTR,";
            query = query + " Round(Avg(CLTrainingRatio), 2) * 100 as CLRatio,";
            query = query + " Round(Avg(ELTrainingRatio), 2) * 100 as ELRatio,";
            query = query + " Round(Avg(KYCRatio), 2) * 100 as KYCRatio,";
            query = query + " Round(Avg(BranchReviewRatio), 2) * 100 as BRRatio,";
            query = query + " Round(Avg(PEPRatio), 2) * 100 as PERatio, ";
            query = query + " Round(Avg(Top100Score),2) as Top100Ratio, Avg(RankOrder) as RankOder";

            query = query + " from CL_CALCULATED_DATA_AREA";
            query = query + " Where Segment in ('Commercial') ";

            query = query + " AND Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";
            query = query + " and DataLevel = 'Region'";
            query = query + " Group by Region, Threshold Order by TotalScore desc";

            dbclass.OpenConection();

            SqlDataReader dr2 = dbclass.DataReader(query);

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    if (!dr2.IsDBNull(0))
                    {
                        TableRow row = new TableRow();
                        TableCell cell1 = new TableCell();
                        TableCell cell2 = new TableCell();
                        TableCell cell3 = new TableCell();
                        TableCell cell4 = new TableCell();
                        TableCell cell5 = new TableCell();
                        TableCell cell6 = new TableCell();
                        TableCell cell7 = new TableCell();
                        TableCell cell8 = new TableCell();
                        TableCell cell9 = new TableCell();
                        TableCell cell10 = new TableCell();
                        TableCell cell11 = new TableCell();
                        TableCell cell12 = new TableCell();
                        cell3.HorizontalAlign = HorizontalAlign.Right;
                        cell4.HorizontalAlign = HorizontalAlign.Right;
                        cell5.HorizontalAlign = HorizontalAlign.Right;
                        cell6.HorizontalAlign = HorizontalAlign.Right;
                        cell7.HorizontalAlign = HorizontalAlign.Right;
                        cell8.HorizontalAlign = HorizontalAlign.Right;
                        cell9.HorizontalAlign = HorizontalAlign.Right;
                        cell10.HorizontalAlign = HorizontalAlign.Right;
                        cell11.HorizontalAlign = HorizontalAlign.Right;
                        cell12.HorizontalAlign = HorizontalAlign.Right;

                        cell1.Text = dr2[0].ToString();

                        row.Cells.Add(cell1);
                        cell2.Text = dr2[1].ToString();
                        row.Cells.Add(cell2);
                        cell3.Text = dr2[2].ToString();
                        row.Cells.Add(cell3);
                        cell4.Text = dr2[3].ToString();
                        row.Cells.Add(cell4);
                        cell5.Text = dr2[4].ToString();

                        row.Cells.Add(cell5);
                        cell6.Text = dr2[5].ToString();
                        row.Cells.Add(cell6);
                        cell7.Text = dr2[6].ToString();
                        row.Cells.Add(cell7);
                        cell8.Text = dr2[7].ToString();
                        row.Cells.Add(cell8);
                        cell9.Text = dr2[8].ToString();
                        row.Cells.Add(cell9);
                        cell10.Text = dr2[9].ToString();
                        row.Cells.Add(cell10);
                        cell11.Text = dr2[10].ToString();
                        row.Cells.Add(cell11);
                        cell12.Text = dr2[11].ToString();
                        row.Cells.Add(cell12);


                        mytable2.Rows.Add(row);

                    }
                }
            }
            dbclass.CloseConnection();
        }

        private void fillDataTableCorp()
        {
            string query = "Select Region, Threshold,";
            query = query + " Sum(TotalBranches) as TotalBranches, ";
            query = query + " Round(Avg(TotalScore), 2)  as TotalScore,";
            query = query + " Round(Avg(AlertRatio), 2) * 100 as AlertRatio,";
            //query = query + " sum(StrRaised) as STRRaised, sum(STRConverted) ConvertedSTR,";
            query = query + " Round(Avg(CLTrainingRatio), 2) * 100 as CLRatio,";
            query = query + " Round(Avg(ELTrainingRatio), 2) * 100 as ELRatio,";
            query = query + " Round(Avg(KYCRatio), 2) * 100 as KYCRatio,";
            query = query + " Round(Avg(BranchReviewRatio), 2) * 100 as BRRatio,";
            query = query + " Round(Avg(PEPRatio), 2) * 100 as PERatio, ";
            query = query + " round(sum(Top100Score),2) as Top100Ratio, Avg(RankOrder) as RankOder";

            query = query + " from CL_CALCULATED_DATA_AREA";
            query = query + " Where Segment in ('Corporate') ";

            query = query + " AND Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";
            query = query + " and DataLevel = 'Region'";
            query = query + " Group by Region, Threshold Order by TotalScore desc";

            dbclass.OpenConection();

            SqlDataReader dr2 = dbclass.DataReader(query);

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    if (!dr2.IsDBNull(0))
                    {
                        TableRow row = new TableRow();
                        TableCell cell1 = new TableCell();
                        TableCell cell2 = new TableCell();
                        TableCell cell3 = new TableCell();
                        TableCell cell4 = new TableCell();
                        TableCell cell5 = new TableCell();
                        TableCell cell6 = new TableCell();
                        TableCell cell7 = new TableCell();
                        TableCell cell8 = new TableCell();
                        TableCell cell9 = new TableCell();
                        TableCell cell10 = new TableCell();
                        TableCell cell11 = new TableCell();
                        TableCell cell12 = new TableCell();
                        cell3.HorizontalAlign = HorizontalAlign.Right;
                        cell4.HorizontalAlign = HorizontalAlign.Right;
                        cell5.HorizontalAlign = HorizontalAlign.Right;
                        cell6.HorizontalAlign = HorizontalAlign.Right;
                        cell7.HorizontalAlign = HorizontalAlign.Right;
                        cell8.HorizontalAlign = HorizontalAlign.Right;
                        cell9.HorizontalAlign = HorizontalAlign.Right;
                        cell10.HorizontalAlign = HorizontalAlign.Right;
                        cell11.HorizontalAlign = HorizontalAlign.Right;
                        cell12.HorizontalAlign = HorizontalAlign.Right;
                        cell1.Text = dr2[0].ToString();
                        row.Cells.Add(cell1);
                        cell2.Text = dr2[1].ToString();
                        row.Cells.Add(cell2);
                        cell3.Text = dr2[2].ToString();
                        row.Cells.Add(cell3);
                        cell4.Text = dr2[3].ToString();
                        row.Cells.Add(cell4);
                        cell5.Text = dr2[4].ToString();
                        row.Cells.Add(cell5);
                        cell6.Text = dr2[5].ToString();
                        row.Cells.Add(cell6);
                        cell7.Text = dr2[6].ToString();
                        row.Cells.Add(cell7);
                        cell8.Text = dr2[7].ToString();
                        row.Cells.Add(cell8);
                        cell9.Text = dr2[8].ToString();
                        row.Cells.Add(cell9);
                        cell10.Text = dr2[9].ToString();
                        row.Cells.Add(cell10);
                        cell11.Text = dr2[10].ToString();
                        row.Cells.Add(cell11);
                        cell12.Text = dr2[11].ToString();
                        row.Cells.Add(cell12);


                        myTable3.Rows.Add(row);

                    }
                }
            }



            //GridView1.ControlStyle.Font.Size = 7;
            dbclass.CloseConnection();
        }

        private void fillgrid()
        {
            string query = "Select Region, Threshold,";
            query = query + " Sum(TotalBranches) as TotalBranches, Sum(AlertTotal) as Alerttotal, sum(AlertClosed) AlertClosed, Round(Avg(AlertRatio), 2) * 100 as AlertRatio,";
            query = query + " sum(StrRaised) as STRRaised, sum(STRConverted) ConvertedSTR,";
            query = query + " Sum(CLTrainingTotal) CLTotal, sum(CLTrainingClosed) CLClosed, Round(Avg(CLTrainingRatio), 2) * 100 as CLRatio,";
            query = query + " Sum(ELTrainingTotal) ELTotal, sum(ELTrainingClosed) ELClosed, Round(Avg(ELTrainingRatio), 2) * 100 as ELRatio,";
            query = query + " Sum(KYCtotal) KYCTotal, sum(KYCclosed) KYCClosed, Round(Avg(KYCRatio), 2) * 100 as KYCRatio,";
            query = query + " Sum(BranchReviewTotal) BRTotal, sum(BranchReviewClosed) BRClosed, Round(Avg(BranchReviewRatio), 2) * 100 as BRRatio,";
            query = query + " Sum(PEPTotal) PEPTotal, sum(PEPOutstanding) PEPOutStanding, Round(Avg(PEPRatio), 2) * 100 as PERatio, Avg(PEPScore) as PEPScore,";
            query = query + " round(sum(Top100Ratio),2) as Top100Ratio, round(Avg(totalScore),2) as TotalScore, round(Avg(TotalScoreWoBonus),2) as TotalScoreWithoutBonus, Avg(RankOrder) as RankOder";

            query = query + " from CL_CALCULATED_DATA_AREA";
            query = query + " Where Segment in ('Retail') ";

            query = query + " AND Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";
            query = query + " and DataLevel = 'Region'";
            query = query + " Group by Region, Threshold Order by Region";

            dbclass.OpenConection();


            DataSet ds = dbclass.DataSet(query);

            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }

            //GridView1.ControlStyle.Font.Size = 7;
            dbclass.CloseConnection();
        }
        private void getSegmentScore()
        {
            Double score = 0;
            string query2 = "";
            query2 = "Select avg(TotalScore) Total from CL_CALCULATED_DATA_AREA";
            query2 = query2 + " Where DataLevel='Region' and Segment in ('Retail') and Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";

            dbclass.OpenConection();
            SqlDataReader dr2 = dbclass.DataReader(query2);

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    if (!dr2.IsDBNull(0))
                    {
                        score = Math.Round(dr2.GetDouble(0), 2);
                        pbardist.Style["width"] = String.Format("{0}%", (double)score);
                        //pbardist.Style["background-color"] = "darkcyan";
                        lblh5dist.Text = String.Format("{0}%", (double)score);
                    }
                }
            }


            query2 = "Select avg(TotalScore) Total from CL_CALCULATED_DATA_AREA";
            query2 = query2 + " Where DataLevel='Region' And Segment in ('Commercial') and Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";


            dr2 = dbclass.DataReader(query2);

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    if (!dr2.IsDBNull(0))
                    {
                        score = Math.Round(dr2.GetDouble(0), 2);
                        pbarcom.Style["width"] = String.Format("{0}%", (double)score);
                        lblh5com.Text = String.Format("{0}%", (double)score);
                    }
                }
            }

            query2 = "Select avg(TotalScore) Total from CL_CALCULATED_DATA_AREA";
            query2 = query2 + " Where DataLevel='Region' and Segment in ('Corporate') and Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";


            dr2 = dbclass.DataReader(query2);

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    if (!dr2.IsDBNull(0))
                    {
                        score = Math.Round(dr2.GetDouble(0), 2);
                        pbarcor.Style["width"] = String.Format("{0}%", (double)score);
                        lblh5cor.Text = String.Format("{0}%", (double)score);
                    }

                }
            }
            dbclass.CloseConnection();
        } // Get Segment Score

        private void getRegionThresholdDist()
        {


            string query2 = "";
            query2 = "Select C.Threshold, Count(region) Total, T.seqno from CL_Threshold T LEFT Join CL_CALCULATED_DATA_AREA C on T.CL_Threshold = C.Threshold";
            query2 = query2 + " Where DataLevel='Region' and Segment in ('Retail') and Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";
            query2 = query2 + " Group by C.Threshold, T.Seqno Order by T.Seqno";

            dbclass.OpenConection();
            SqlDataReader dr2 = dbclass.DataReader(query2);

            ChartLabelRegdist = "[";
            ChartDataRegdist = "[";

            while (dr2.Read())
            {
                //lstLabel.Add(sdr["CaseStatus"].ToString());
                //lstdata.Add(sdr["Total"].ToString());

                ChartLabelRegdist = ChartLabelRegdist + "'" + dr2["Threshold"].ToString() + "',";
                ChartDataRegdist = ChartDataRegdist + dr2["Total"].ToString() + ",";


            }
            ChartLabelRegdist = ChartLabelRegdist.Substring(0, ChartLabelRegdist.Length - 1);
            ChartLabelRegdist = ChartLabelRegdist + "]";
            ChartDataRegdist = ChartDataRegdist.Substring(0, ChartDataRegdist.Length - 1);
            ChartDataRegdist = ChartDataRegdist + "]";

            dbclass.CloseConnection();
        }   // end function

        private void getRegionThresholdCommercial()
        {
            string query2 = "";
            query2 = "Select C.Threshold, Count(region) Total, T.seqno from CL_Threshold T LEFT Join CL_CALCULATED_DATA_AREA C on T.CL_Threshold = C.Threshold";
            query2 = query2 + " Where DataLevel='Region' and Segment in ('Commercial') and Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";
            query2 = query2 + " Group by C.Threshold, T.Seqno Order by T.Seqno";

            dbclass.OpenConection();
            SqlDataReader dr2 = dbclass.DataReader(query2);

            ChartLabelRegComm = "[";
            ChartDataRegComm = "[";

            while (dr2.Read())
            {

                ChartLabelRegComm = ChartLabelRegComm + "'" + dr2["Threshold"].ToString() + "',";
                ChartDataRegComm = ChartDataRegComm + dr2["Total"].ToString() + ",";

            }
            ChartLabelRegComm = ChartLabelRegComm.Substring(0, ChartLabelRegComm.Length - 1);
            ChartLabelRegComm = ChartLabelRegComm + "]";
            ChartDataRegComm = ChartDataRegComm.Substring(0, ChartDataRegComm.Length - 1);
            ChartDataRegComm = ChartDataRegComm + "]";

            dbclass.CloseConnection();
        }   // end function

        private void getRegionThresholdCorporate()
        {
            string query2 = "";
            query2 = "Select C.Threshold, Count(region) Total, T.seqno from CL_Threshold T LEFT Join CL_CALCULATED_DATA_AREA C on T.CL_Threshold = C.Threshold";
            query2 = query2 + " Where DataLevel='Region' and Segment in ('Corporate') and Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";
            query2 = query2 + " Group by C.Threshold, T.Seqno Order by T.Seqno";

            dbclass.OpenConection();
            SqlDataReader dr2 = dbclass.DataReader(query2);

            ChartLabelRegCorp = "[";
            ChartDataRegCorp = "[";

            while (dr2.Read())
            {

                ChartLabelRegCorp = ChartLabelRegCorp + "'" + dr2["Threshold"].ToString() + "',";
                ChartDataRegCorp = ChartDataRegCorp + dr2["Total"].ToString() + ",";

            }
            ChartLabelRegCorp = ChartLabelRegCorp.Substring(0, ChartLabelRegCorp.Length - 1);
            ChartLabelRegCorp = ChartLabelRegCorp + "]";
            ChartDataRegCorp = ChartDataRegCorp.Substring(0, ChartDataRegCorp.Length - 1);
            ChartDataRegCorp = ChartDataRegCorp + "]";

            dbclass.CloseConnection();
        }   // end function

        private void getBranchThreshold()
        {
            string query2 = "";

            query2 = "Select C.Threshold, Count(region) Total, T.seqno from CL_Threshold T LEFT Join CL_CALCULATED_DATA C on T.CL_Threshold = C.Threshold";
            query2 = query2 + " Where  Segment in ('Retail') and Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";
            query2 = query2 + " Group by C.Threshold, T.Seqno Order by T.Seqno";

            dbclass.OpenConection();
            SqlDataReader dr2 = dbclass.DataReader(query2);

            ChartLabel2 = "[";
            ChartData2 = "[";

            while (dr2.Read())
            {
                ChartLabel2 = ChartLabel2 + "'" + dr2["Threshold"].ToString() + "',";
                ChartData2 = ChartData2 + dr2["Total"].ToString() + ",";
            }
            ChartLabel2 = ChartLabel2.Substring(0, ChartLabel2.Length - 1);
            ChartLabel2 = ChartLabel2 + "]";
            ChartData2 = ChartData2.Substring(0, ChartData2.Length - 1);
            ChartData2 = ChartData2 + "]";

            dbclass.CloseConnection();
        } // end function

        private void getCommBranchThreshold()
        {
            string query2 = "";

            query2 = "Select C.Threshold, Count(region) Total, T.seqno from CL_Threshold T LEFT Join CL_CALCULATED_DATA C on T.CL_Threshold = C.Threshold";
            query2 = query2 + " Where  Segment in ('Commercial') and Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";
            query2 = query2 + " Group by C.Threshold, T.Seqno Order by T.Seqno";


            dbclass.OpenConection();
            SqlDataReader dr2 = dbclass.DataReader(query2);

            ChartLabel3 = "[";
            ChartData3 = "[";

            while (dr2.Read())
            {

                ChartLabel3 = ChartLabel3 + "'" + dr2["Threshold"].ToString() + "',";
                ChartData3 = ChartData3 + dr2["Total"].ToString() + ",";

            }
            ChartLabel3 = ChartLabel3.Substring(0, ChartLabel3.Length - 1);
            ChartLabel3 = ChartLabel3 + "]";
            ChartData3 = ChartData3.Substring(0, ChartData3.Length - 1);
            ChartData3 = ChartData3 + "]";

            dbclass.CloseConnection();
        } // end function comm

        private void getCorpBranchThreshold()
        {
            string query2 = "";

            query2 = "Select C.Threshold, Count(region) Total, T.seqno from CL_Threshold T LEFT Join CL_CALCULATED_DATA C on T.CL_Threshold = C.Threshold";
            query2 = query2 + " Where  Segment in ('Corporate') and Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";
            query2 = query2 + " Group by C.Threshold, T.Seqno Order by T.Seqno";

            dbclass.OpenConection();
            SqlDataReader dr2 = dbclass.DataReader(query2);

            ChartLabels = "[";
            ChartData1 = "[";

            while (dr2.Read())
            {
                //lstLabel.Add(sdr["CaseStatus"].ToString());
                //lstdata.Add(sdr["Total"].ToString());

                ChartLabels = ChartLabels + "'" + dr2["Threshold"].ToString() + "',";
                ChartData1 = ChartData1 + dr2["Total"].ToString() + ",";


            }
            ChartLabels = ChartLabels.Substring(0, ChartLabels.Length - 1);
            ChartLabels = ChartLabels + "]";
            ChartData1 = ChartData1.Substring(0, ChartData1.Length - 1);
            ChartData1 = ChartData1 + "]";

            dbclass.CloseConnection();
        } // end function corporate

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // increment PageIndex
            GridView1.PageIndex = e.NewPageIndex;

            // bind table again
            fillgrid();
        }

        private void getRegionScore()
        {

            string query = "Select Region, Threshold,";
            query = query + " Sum(TotalBranches) as TotalBranches, Sum(AlertTotal) as Alerttotal, sum(AlertClosed) AlertClosed, Round(Avg(AlertRatio), 2) * 100 as AlertRatio,";
            query = query + " sum(StrRaised) as STRRaised, sum(STRConverted) ConvertedSTR,";
            query = query + " Sum(CLTrainingTotal) CLTotal, sum(CLTrainingClosed) CLClosed, Round(Avg(CLTrainingRatio), 2) * 100 as CLRatio,";
            query = query + " Sum(ELTrainingTotal) ELTotal, sum(ELTrainingClosed) ELClosed, Round(Avg(ELTrainingRatio), 2) * 100 as ELRatio,";
            query = query + " Sum(KYCtotal) KYCTotal, sum(KYCclosed) KYCClosed, Round(Avg(KYCRatio), 2) * 100 as KYCRatio,";
            query = query + " Sum(BranchReviewTotal) BRTotal, sum(BranchReviewClosed) BRClosed, Round(Avg(BranchReviewRatio), 2) * 100 as BRRatio,";
            query = query + " Sum(PEPTotal) PEPTotal, sum(PEPClosed) PEPClosed, Round(Avg(PEPRatio), 2) * 100 as PERatio, Avg(PEPScore) as PEPScore,";
            query = query + " round(sum(Top100Ratio),2) as Top100Ratio, round(Avg(totalScore),2) as TotalScore, Avg(RankOrder) as RankOder";

            query = query + " from CL_CALCULATED_DATA_AREA";

            query = query + " Where Segment in ('Retail') ";

            query = query + " AND Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";
            query = query + " and DataLevel = 'Region'";
            query = query + " Group by Region, Threshold Order by totalScore desc";


            dbclass.OpenConection();
            SqlDataReader dr2 = dbclass.DataReader(query);

            ChartLabel4 = "[";
            ChartData4 = "[";
            ChartData4s2 = "[";
            ChartData4s3 = "[";
            ChartData4s4 = "[";
            ChartData4s5 = "[";
            ChartData4s6 = "[";
            ChartData4s7 = "[";

            while (dr2.Read())
            {
                //lstLabel.Add(sdr["CaseStatus"].ToString());
                //lstdata.Add(sdr["Total"].ToString());

                ChartLabel4 = ChartLabel4 + "'" + dr2["Region"].ToString() + "',";
                ChartData4 = ChartData4 + dr2["TotalScore"].ToString() + ",";
                ChartData4s2 = ChartData4s2 + dr2["AlertRatio"].ToString() + ",";
                ChartData4s3 = ChartData4s3 + dr2["CLRatio"].ToString() + ",";
                ChartData4s4 = ChartData4s4 + dr2["ELRatio"].ToString() + ",";
                ChartData4s5 = ChartData4s5 + dr2["KYCRatio"].ToString() + ",";
                ChartData4s6 = ChartData4s6 + dr2["BRRatio"].ToString() + ",";
                ChartData4s7 = ChartData4s7 + dr2["Top100Ratio"].ToString() + ",";

            }
            ChartLabel4 = ChartLabel4.Substring(0, ChartLabel4.Length - 1);
            ChartLabel4 = ChartLabel4 + "]";
            ChartData4 = ChartData4.Substring(0, ChartData4.Length - 1);
            ChartData4 = ChartData4 + "]";
            ChartData4s2 = ChartData4s2.Substring(0, ChartData4s2.Length - 1);
            ChartData4s2 = ChartData4s2 + "]";
            ChartData4s3 = ChartData4s3.Substring(0, ChartData4s3.Length - 1);
            ChartData4s3 = ChartData4s3 + "]";
            ChartData4s4 = ChartData4s4.Substring(0, ChartData4s4.Length - 1);
            ChartData4s4 = ChartData4s4 + "]";
            ChartData4s5 = ChartData4s5.Substring(0, ChartData4s5.Length - 1);
            ChartData4s5 = ChartData4s5 + "]";
            ChartData4s6 = ChartData4s6.Substring(0, ChartData4s6.Length - 1);
            ChartData4s6 = ChartData4s6 + "]";
            ChartData4s7 = ChartData4s7.Substring(0, ChartData4s7.Length - 1);
            ChartData4s7 = ChartData4s7 + "]";

            dbclass.CloseConnection();
        }   // end function

        private void getRegionScoreCommercial()
        {

            string query = "Select Region, Threshold,";
            query = query + " Sum(TotalBranches) as TotalBranches, Sum(AlertTotal) as Alerttotal, sum(AlertClosed) AlertClosed, Round(Avg(AlertRatio), 2) * 100 as AlertRatio,";
            query = query + " sum(StrRaised) as STRRaised, sum(STRConverted) ConvertedSTR,";
            query = query + " Sum(CLTrainingTotal) CLTotal, sum(CLTrainingClosed) CLClosed, Round(Avg(CLTrainingRatio), 2) * 100 as CLRatio,";
            query = query + " Sum(ELTrainingTotal) ELTotal, sum(ELTrainingClosed) ELClosed, Round(Avg(ELTrainingRatio), 2) * 100 as ELRatio,";
            query = query + " Sum(KYCtotal) KYCTotal, sum(KYCclosed) KYCClosed, Round(Avg(KYCRatio), 2) * 100 as KYCRatio,";
            query = query + " Sum(BranchReviewTotal) BRTotal, sum(BranchReviewClosed) BRClosed, Round(Avg(BranchReviewRatio), 2) * 100 as BRRatio,";
            query = query + " Sum(PEPTotal) PEPTotal, sum(PEPClosed) PEPClosed, Round(Avg(PEPRatio), 2) * 100 as PERatio, Avg(PEPScore) as PEPScore,";
            query = query + " round(sum(Top100Ratio),2) as Top100Ratio, round(Avg(totalScore),2) as TotalScore, Avg(RankOrder) as RankOder";

            query = query + " from CL_CALCULATED_DATA_AREA";

            query = query + " Where Segment in ('Commercial') ";

            query = query + " AND Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";
            query = query + " and DataLevel = 'Region'";
            query = query + " Group by Region, Threshold Order by totalScore desc";


            dbclass.OpenConection();
            SqlDataReader dr2 = dbclass.DataReader(query);

            ChartLabel5 = "[";
            ChartData5 = "[";
            ChartData5s2 = "[";
            ChartData5s3 = "[";
            ChartData5s4 = "[";
            ChartData5s5 = "[";
            ChartData5s6 = "[";
            ChartData5s7 = "[";

            while (dr2.Read())
            {
                //lstLabel.Add(sdr["CaseStatus"].ToString());
                //lstdata.Add(sdr["Total"].ToString());

                ChartLabel5 = ChartLabel5 + "'" + dr2["Region"].ToString() + "',";
                ChartData5 = ChartData5 + dr2["TotalScore"].ToString() + ",";
                ChartData5s2 = ChartData5s2 + dr2["AlertRatio"].ToString() + ",";
                ChartData5s3 = ChartData5s3 + dr2["CLRatio"].ToString() + ",";
                ChartData5s4 = ChartData5s4 + dr2["ELRatio"].ToString() + ",";
                ChartData5s5 = ChartData5s5 + dr2["KYCRatio"].ToString() + ",";
                ChartData5s6 = ChartData5s6 + dr2["BRRatio"].ToString() + ",";
                ChartData5s7 = ChartData5s7 + dr2["Top100Ratio"].ToString() + ",";

            }
            ChartLabel5 = ChartLabel5.Substring(0, ChartLabel5.Length - 1);
            ChartLabel5 = ChartLabel5 + "]";
            ChartData5 = ChartData5.Substring(0, ChartData5.Length - 1);
            ChartData5 = ChartData5 + "]";
            ChartData5s2 = ChartData5s2.Substring(0, ChartData5s2.Length - 1);
            ChartData5s2 = ChartData5s2 + "]";
            ChartData5s3 = ChartData5s3.Substring(0, ChartData5s3.Length - 1);
            ChartData5s3 = ChartData5s3 + "]";
            ChartData5s4 = ChartData5s4.Substring(0, ChartData5s4.Length - 1);
            ChartData5s4 = ChartData5s4 + "]";
            ChartData5s5 = ChartData5s5.Substring(0, ChartData5s5.Length - 1);
            ChartData5s5 = ChartData5s5 + "]";
            ChartData5s6 = ChartData5s6.Substring(0, ChartData5s6.Length - 1);
            ChartData5s6 = ChartData5s6 + "]";
            ChartData5s7 = ChartData5s7.Substring(0, ChartData5s7.Length - 1);
            ChartData5s7 = ChartData5s7 + "]";

            dbclass.CloseConnection();
        }   // end function

        private void getRegionScoreCorporate()
        {

            string query = "Select Region, Threshold,";
            query = query + " Sum(TotalBranches) as TotalBranches, Sum(AlertTotal) as Alerttotal, sum(AlertClosed) AlertClosed, Round(Avg(AlertRatio), 2) * 100 as AlertRatio,";
            query = query + " sum(StrRaised) as STRRaised, sum(STRConverted) ConvertedSTR,";
            query = query + " Sum(CLTrainingTotal) CLTotal, sum(CLTrainingClosed) CLClosed, Round(Avg(CLTrainingRatio), 2) * 100 as CLRatio,";
            query = query + " Sum(ELTrainingTotal) ELTotal, sum(ELTrainingClosed) ELClosed, Round(Avg(ELTrainingRatio), 2) * 100 as ELRatio,";
            query = query + " Sum(KYCtotal) KYCTotal, sum(KYCclosed) KYCClosed, Round(Avg(KYCRatio), 2) * 100 as KYCRatio,";
            query = query + " Sum(BranchReviewTotal) BRTotal, sum(BranchReviewClosed) BRClosed, Round(Avg(BranchReviewRatio), 2) * 100 as BRRatio,";
            query = query + " Sum(PEPTotal) PEPTotal, sum(PEPClosed) PEPClosed, Round(Avg(PEPRatio), 2) * 100 as PERatio, Avg(PEPScore) as PEPScore,";
            query = query + " round(sum(Top100Ratio),2) as Top100Ratio, round(Avg(totalScore),2) as TotalScore, Avg(RankOrder) as RankOder";

            query = query + " from CL_CALCULATED_DATA_AREA";

            query = query + " Where Segment in ('Corporate') ";

            query = query + " AND Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";
            query = query + " and DataLevel = 'Region'";
            query = query + " Group by Region, Threshold Order by totalScore desc";


            dbclass.OpenConection();
            SqlDataReader dr2 = dbclass.DataReader(query);

            ChartLabel6 = "[";
            ChartData6 = "[";
            ChartData6s2 = "[";
            ChartData6s3 = "[";
            ChartData6s4 = "[";
            ChartData6s5 = "[";
            ChartData6s6 = "[";
            ChartData6s7 = "[";

            while (dr2.Read())
            {
                //lstLabel.Add(sdr["CaseStatus"].ToString());
                //lstdata.Add(sdr["Total"].ToString());

                ChartLabel6 = ChartLabel6 + "'" + dr2["Region"].ToString() + "',";
                ChartData6 = ChartData6 + dr2["TotalScore"].ToString() + ",";
                ChartData6s2 = ChartData6s2 + dr2["AlertRatio"].ToString() + ",";
                ChartData6s3 = ChartData6s3 + dr2["CLRatio"].ToString() + ",";
                ChartData6s4 = ChartData6s4 + dr2["ELRatio"].ToString() + ",";
                ChartData6s5 = ChartData6s5 + dr2["KYCRatio"].ToString() + ",";
                ChartData6s6 = ChartData6s6 + dr2["BRRatio"].ToString() + ",";
                ChartData6s7 = ChartData6s7 + dr2["Top100Ratio"].ToString() + ",";

            }
            ChartLabel6 = ChartLabel6.Substring(0, ChartLabel6.Length - 1);
            ChartLabel6 = ChartLabel6 + "]";
            ChartData6 = ChartData6.Substring(0, ChartData6.Length - 1);
            ChartData6 = ChartData6 + "]";
            ChartData6s2 = ChartData6s2.Substring(0, ChartData6s2.Length - 1);
            ChartData6s2 = ChartData6s2 + "]";
            ChartData6s3 = ChartData6s3.Substring(0, ChartData6s3.Length - 1);
            ChartData6s3 = ChartData6s3 + "]";
            ChartData6s4 = ChartData6s4.Substring(0, ChartData6s4.Length - 1);
            ChartData6s4 = ChartData6s4 + "]";
            ChartData6s5 = ChartData6s5.Substring(0, ChartData6s5.Length - 1);
            ChartData6s5 = ChartData6s5 + "]";
            ChartData6s6 = ChartData6s6.Substring(0, ChartData6s6.Length - 1);
            ChartData6s6 = ChartData6s6 + "]";
            ChartData6s7 = ChartData6s7.Substring(0, ChartData6s7.Length - 1);
            ChartData6s7 = ChartData6s7 + "]";

            dbclass.CloseConnection();
        }   // end function

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            getSegmentScore();
            getRegionThresholdDist();
            getRegionThresholdCommercial();
            getRegionThresholdCorporate();
            getBranchThreshold();
            getCommBranchThreshold();
            getCorpBranchThreshold();
            getRegionScore();
            getRegionScoreCommercial();
            getRegionScoreCorporate();
            getRegionScoreCorporate();
            fillgrid();
            fillDataTable();
            fillDataTableComm();
            fillDataTableCorp();
        }

        private void downloadCSV()
        {

            string query = "Select Year, Month, Region, Threshold,";
            query = query + " Sum(TotalBranches) as TotalBranches, Sum(AlertTotal) as Alerttotal, sum(AlertClosed) AlertClosed, Round(Avg(AlertRatio), 2) * 100 as AlertRatio,";
            query = query + " sum(StrRaised) as STRRaised, sum(STRConverted) ConvertedSTR,";
            query = query + " Sum(CLTrainingTotal) CLTotal, sum(CLTrainingClosed) CLClosed, Round(Avg(CLTrainingRatio), 2) * 100 as CLRatio,";
            query = query + " Sum(ELTrainingTotal) ELTotal, sum(ELTrainingClosed) ELClosed, Round(Avg(ELTrainingRatio), 2) * 100 as ELRatio,";
            query = query + " Sum(KYCtotal) KYCTotal, sum(KYCclosed) KYCClosed, Round(Avg(KYCRatio), 2) * 100 as KYCRatio,";
            query = query + " Sum(BranchReviewTotal) BRTotal, sum(BranchReviewClosed) BRClosed, Round(Avg(BranchReviewRatio), 2) * 100 as BRRatio,";
            query = query + " Sum(PEPTotal) PEPTotal, sum(PEPOutstanding) PEPOutstanding, Round(Avg(PEPRatio), 2) * 100 as PERatio, Avg(PEPScore) as PEPScore,";
            query = query + " round(sum(Top100Ratio),2) as Top100Ratio, round(Avg(totalScore),2) as TotalScore, round(Avg(TotalScoreWoBonus),2) as TotalScorewithoutBonus, Avg(RankOrder) as RankOder";

            query = query + " from CL_CALCULATED_DATA_AREA";

            query = query + " Where Segment in ('Retail') ";

            query = query + " AND Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";
            query = query + " and DataLevel = 'Region'";
            query = query + " Group by Year, Month, Region, Threshold Order by Region";


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
            Response.AddHeader("content-disposition", "attachment;filename=CLSegmentDashboard.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(csv);
            Response.Flush();
            Response.End();

        }

        protected void btndownload_Click(object sender, EventArgs e)
        {
            downloadCSV();
        }
    }
}