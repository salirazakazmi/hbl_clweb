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
    public partial class CLOtherCharts : System.Web.UI.Page
    {
        DBClass dbclass = new DBClass();

        public string chartseriesdata;
        public string chartdrilldowndata;



        // Public Variables for Dashboard Score
        public string ChartAlertLabel = null;
        public string ChartAlertTotal = null;
        public string ChartAlertClosed = null;
        public string ChartAlertRatio = null;
        public string ChartAlertScore = null;

        public string SankyChartMTDRegion = null;
        public string SankyChartMTDArea = null;

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
                dbclass.IP_LOG(Request.UserHostAddress, "CLOtherCharts.aspx");
                dropdownfill();
                getSegmentYTDScore();
                getSegmentMTDScore();
                getYTDAlerts();

                fillDataTableEachMonth();
                fillSankyChartMTD();
                fillSankyChartMTDRegion();
                //getRegionThresholdYTD();
                getRegionThresholdCommercial();
                getRegionThresholdCorporate();
                getBranchThreshold();
                getCommBranchThreshold();
                getCorpBranchThreshold();
                getRegionScore();
                getRegionScoreCommercial();
                getRegionScoreCorporate();
                getRegionScoreCorporate();
               
            }
        } // end page_load

        private void dropdownfill()
        {
            string query = "select Distinct Year  from CL_CALCULATED_DATA";

            dbclass.OpenConection();


            DataSet ds = dbclass.DataSet(query);

            DD_Year.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_Year.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_Year.DataSource = ds.Tables[0];
            DD_Year.DataBind();
            DD_Year.SelectedIndex = DD_Year.Items.Count - 1;
            
            query = "select Distinct Month  from CL_CALCULATED_DATA";

            ds = dbclass.DataSet(query);

            DD_Month.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_Month.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_Month.DataSource = ds.Tables[0];
            DD_Month.DataBind();
            DD_Month.SelectedIndex = DD_Month.Items.Count - 1;

            query = "select Segment  from CL_SEGMENT WHERE ACTIVE='Y' Order by SeqNo";

            ds = dbclass.DataSet(query);

            DD_Segment.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_Segment.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_Segment.DataSource = ds.Tables[0];
            DD_Segment.DataBind();
        }


        private void getSegmentYTDScore()
        {
            Double score = 0;
            string query2 = "";
            query2 = "Select avg(TotalScore) Total from CL_CALCULATED_DATA_AREA";
            query2 = query2 + " Where DataLevel='Region' and Year = '" + DD_Year.Text + "'";
            //query2 = query2 + " and Month = '" + DD_Month.Text + "'";
            if (DD_Segment.Text.ToUpper() == "DISTRIBUTION")
            {
                query2 = query2 + " AND Segment in ('Retail','Islamic')";
            }
            if (DD_Segment.Text.ToUpper() == "COMMERCIAL")
            {
                query2 = query2 + " AND Segment in ('Commercial')";
            }
            if (DD_Segment.Text.ToUpper() == "CORPORATE")
            {
                query2 = query2 + " AND Segment in ('Corporate')";
            }
            dbclass.OpenConection();
            SqlDataReader dr2 = dbclass.DataReader(query2);

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    if (!dr2.IsDBNull(0))
                    {
                        score = dr2.GetDouble(0);
                        pbardist.Style["width"] = String.Format("{0}%", (int)score);
                        //pbardist.Style["background-color"] = "darkcyan";
                        lblh5dist.Text = DD_Segment.Text + " " + String.Format("{0}%", (int)score);
                    }
                }
            }

            dbclass.CloseConnection();
        } // Get Segment Score

        private void getSegmentMTDScore()
        {
            Double score = 0;
            string query2 = "";
            query2 = "Select avg(TotalScore) Total from CL_CALCULATED_DATA_AREA";
            query2 = query2 + " Where DataLevel='Region' and Year = '" + DD_Year.Text + "'";
            query2 = query2 + " and Month = '" + DD_Month.Text + "'";
            if (DD_Segment.Text.ToUpper() == "DISTRIBUTION")
            {
                query2 = query2 + " AND Segment in ('Retail','Islamic')";
            }
            if (DD_Segment.Text.ToUpper() == "COMMERCIAL")
            {
                query2 = query2 + " AND Segment in ('Commercial')";
            }
            if (DD_Segment.Text.ToUpper() == "CORPORATE")
            {
                query2 = query2 + " AND Segment in ('Corporate')";
            }
            dbclass.OpenConection();
            SqlDataReader dr2 = dbclass.DataReader(query2);

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    if (!dr2.IsDBNull(0))
                    {
                        score = dr2.GetDouble(0);
                        pbarmtd.Style["width"] = String.Format("{0}%", (int)score);
                        lblh5mtd.Text = DD_Segment.Text + " " + String.Format("{0}%", (int)score);
                    }
                }
            }

            dbclass.CloseConnection();
        } // Get Segment Score

        private void fillDataTableEachMonth()
        {
            string query = "Select Year, Month, ";
            query = query + " Sum(AlertTotal) as Alerttotal, sum(AlertClosed) AlertClosed, Round(Avg(AlertRatio), 2) * 100 as AlertRatio,";
            query = query + " Round(Avg(AlertScore), 2) AlertScore,";
            query = query + " sum(StrRaised) as STRRaised, sum(STRConverted) ConvertedSTR,";
            query = query + " Round(Avg(STRRaisedScore), 2) STRScore,";
            query = query + " Sum(CLTrainingTotal) CLTotal, sum(CLTrainingClosed) CLClosed, Round(Avg(CLTrainingRatio), 2) * 100 as CLRatio,";
            query = query + " Round(Avg(CLTrainingScore), 2) CLScore,";
            query = query + " Sum(ELTrainingTotal) ELTotal, sum(ELTrainingClosed) ELClosed, Round(Avg(ELTrainingRatio), 2) * 100 as ELRatio,";
            query = query + " Round(Avg(ELTrainingScore), 2) ELScore,";
            query = query + " Sum(KYCtotal) KYCTotal, sum(KYCclosed) KYCClosed, Round(Avg(KYCRatio), 2) * 100 as KYCRatio,";
            query = query + " Round(Avg(KYCScore), 2) KYCScore,";
            query = query + " Sum(BranchReviewTotal) BRTotal, sum(BranchReviewClosed) BRClosed, Round(Avg(BranchReviewRatio), 2) * 100 as BRRatio,";
            query = query + " Round(Avg(BranchReviewScore), 2) BRScore,";
            query = query + " Sum(PEPTotal) PEPTotal, sum(PEPOutstanding) PEPOutstanding, Round(Avg(PEPRatio), 2) * 100 as PERatio, Avg(PEPScore) as PEPScore,";
            query = query + " round(Avg(Top100Ratio),2) * 100 as Top100Ratio, round(Avg(Top100Score),2) as Top100Score, round(Avg(totalScore),2) as TotalScore, round(Avg(TotalScoreWoBonus),2) as TotalScorewithoutBonus, Avg(RankOrder) as RankOder";

            query = query + " from CL_CALCULATED_DATA_AREA";

            query = query + " Where Year = '" + DD_Year.Text + "'";
            query = query + " and DataLevel = 'Region'";
            if (DD_Segment.Text.ToUpper() == "DISTRIBUTION")
            {
                query = query + " AND Segment in ('Retail','Islamic')";
            }
            if (DD_Segment.Text.ToUpper() == "COMMERCIAL")
            {
                query = query + " AND Segment in ('Commercial')";
            }
            if (DD_Segment.Text.ToUpper() == "CORPORATE")
            {
                query = query + " AND Segment in ('Corporate')";
            }
            query = query + " Group by Year, Month Order by Month";


           
            
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
                        TableCell cell13 = new TableCell();
                        TableCell cell14 = new TableCell();
                        TableCell cell15 = new TableCell();
                        TableCell cell16 = new TableCell();
                        TableCell cell17 = new TableCell();
                        TableCell cell18 = new TableCell();
                        TableCell cell19 = new TableCell();
                        TableCell cell20 = new TableCell();
                        TableCell cell21 = new TableCell();
                        TableCell cell22 = new TableCell();
                        TableCell cell23 = new TableCell();
                        TableCell cell24 = new TableCell();

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
                        cell13.HorizontalAlign = HorizontalAlign.Right;
                        cell14.HorizontalAlign = HorizontalAlign.Right;
                        cell15.HorizontalAlign = HorizontalAlign.Right;
                        cell16.HorizontalAlign = HorizontalAlign.Right;
                        cell17.HorizontalAlign = HorizontalAlign.Right;
                        cell18.HorizontalAlign = HorizontalAlign.Right;
                        cell19.HorizontalAlign = HorizontalAlign.Right;
                        cell20.HorizontalAlign = HorizontalAlign.Right;
                        cell21.HorizontalAlign = HorizontalAlign.Right;
                        cell22.HorizontalAlign = HorizontalAlign.Right;
                        cell23.HorizontalAlign = HorizontalAlign.Right;
                        cell24.HorizontalAlign = HorizontalAlign.Right;


                        cell1.Text = dr2["Month"].ToString();
                        row.Cells.Add(cell1);
                        cell2.Text = dr2["Alerttotal"].ToString();
                        row.Cells.Add(cell2);
                        cell3.Text = dr2["AlertRatio"].ToString();
                        row.Cells.Add(cell3);
                        cell4.Text = dr2["AlertScore"].ToString();
                        row.Cells.Add(cell4);
                        cell5.Text = dr2["STRRaised"].ToString();
                        row.Cells.Add(cell5);
                        cell6.Text = dr2["ConvertedSTR"].ToString();
                        row.Cells.Add(cell6);
                        cell7.Text = dr2["STRScore"].ToString();
                        row.Cells.Add(cell7);
                        cell8.Text = dr2["CLTotal"].ToString();
                        row.Cells.Add(cell8);
                        cell9.Text = dr2["CLRatio"].ToString();
                        row.Cells.Add(cell9);
                        cell10.Text = dr2["CLScore"].ToString();
                        row.Cells.Add(cell10);
                        cell11.Text = dr2["ELTotal"].ToString();
                        row.Cells.Add(cell11);
                        cell12.Text = dr2["ELRatio"].ToString();
                        row.Cells.Add(cell12);
                        cell13.Text = dr2["ELScore"].ToString();
                        row.Cells.Add(cell13);
                        cell14.Text = dr2["KYCTotal"].ToString();
                        row.Cells.Add(cell14);
                        cell15.Text = dr2["KYCRatio"].ToString();
                        row.Cells.Add(cell15);
                        cell16.Text = dr2["KYCScore"].ToString();
                        row.Cells.Add(cell16);
                        cell17.Text = dr2["BRTotal"].ToString();
                        row.Cells.Add(cell17);
                        cell18.Text = dr2["BRRatio"].ToString();
                        row.Cells.Add(cell18);
                        cell19.Text = dr2["BRScore"].ToString();
                        row.Cells.Add(cell19);
                        cell20.Text = dr2["PEPTotal"].ToString();
                        row.Cells.Add(cell20);
                        cell21.Text = dr2["PEPOutstanding"].ToString();
                        row.Cells.Add(cell21);
                        cell22.Text = dr2["Top100Ratio"].ToString();
                        row.Cells.Add(cell22);
                        cell23.Text = dr2["Top100Score"].ToString();
                        row.Cells.Add(cell23);
                        cell24.Text = dr2["TotalScore"].ToString();
                        row.Cells.Add(cell24);



                        myTable.Rows.Add(row);

                    }
                }
            }
            dbclass.CloseConnection();

            // Now Calculated Grand Total
            fillDataTableYTD();
        }

        private void fillDataTableYTD()
        {
            string query = "Select Year,  ";
            query = query + " Sum(AlertTotal) as Alerttotal, sum(AlertClosed) AlertClosed, Round(Avg(AlertRatio), 2) * 100 as AlertRatio,";
            query = query + " Round(Avg(AlertScore), 2) AlertScore,";
            query = query + " sum(StrRaised) as STRRaised, sum(STRConverted) ConvertedSTR,";
            query = query + " Round(Avg(STRRaisedScore), 2) STRScore,";
            query = query + " Sum(CLTrainingTotal) CLTotal, sum(CLTrainingClosed) CLClosed, Round(Avg(CLTrainingRatio), 2) * 100 as CLRatio,";
            query = query + " Round(Avg(CLTrainingScore), 2) CLScore,";
            query = query + " Sum(ELTrainingTotal) ELTotal, sum(ELTrainingClosed) ELClosed, Round(Avg(ELTrainingRatio), 2) * 100 as ELRatio,";
            query = query + " Round(Avg(ELTrainingScore), 2) ELScore,";
            query = query + " Sum(KYCtotal) KYCTotal, sum(KYCclosed) KYCClosed, Round(Avg(KYCRatio), 2) * 100 as KYCRatio,";
            query = query + " Round(Avg(KYCScore), 2) KYCScore,";
            query = query + " Sum(BranchReviewTotal) BRTotal, sum(BranchReviewClosed) BRClosed, Round(Avg(BranchReviewRatio), 2) * 100 as BRRatio,";
            query = query + " Round(Avg(BranchReviewScore), 2) BRScore,";
            query = query + " Sum(PEPTotal) PEPTotal, sum(PEPOutstanding) PEPOutstanding, Round(Avg(PEPRatio), 2) * 100 as PERatio, Avg(PEPScore) as PEPScore,";
            query = query + " round(Avg(Top100Ratio),2) * 100 as Top100Ratio, round(Avg(Top100Score),2) as Top100Score, round(Avg(totalScore),2) as TotalScore, round(Avg(TotalScoreWoBonus),2) as TotalScorewithoutBonus, Avg(RankOrder) as RankOder";

            query = query + " from CL_CALCULATED_DATA_AREA";

            query = query + " Where Year = '" + DD_Year.Text + "'";
            query = query + " and DataLevel = 'Region'";
            if (DD_Segment.Text.ToUpper() == "DISTRIBUTION")
            {
                query = query + " AND Segment in ('Retail','Islamic')";
            }
            if (DD_Segment.Text.ToUpper() == "COMMERCIAL")
            {
                query = query + " AND Segment in ('Commercial')";
            }
            if (DD_Segment.Text.ToUpper() == "CORPORATE")
            {
                query = query + " AND Segment in ('Corporate')";
            }
            query = query + " Group by Year Order by Year";




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
                        TableCell cell13 = new TableCell();
                        TableCell cell14 = new TableCell();
                        TableCell cell15 = new TableCell();
                        TableCell cell16 = new TableCell();
                        TableCell cell17 = new TableCell();
                        TableCell cell18 = new TableCell();
                        TableCell cell19 = new TableCell();
                        TableCell cell20 = new TableCell();
                        TableCell cell21 = new TableCell();
                        TableCell cell22 = new TableCell();
                        TableCell cell23 = new TableCell();
                        TableCell cell24 = new TableCell();

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
                        cell13.HorizontalAlign = HorizontalAlign.Right;
                        cell14.HorizontalAlign = HorizontalAlign.Right;
                        cell15.HorizontalAlign = HorizontalAlign.Right;
                        cell16.HorizontalAlign = HorizontalAlign.Right;
                        cell17.HorizontalAlign = HorizontalAlign.Right;
                        cell18.HorizontalAlign = HorizontalAlign.Right;
                        cell19.HorizontalAlign = HorizontalAlign.Right;
                        cell20.HorizontalAlign = HorizontalAlign.Right;
                        cell21.HorizontalAlign = HorizontalAlign.Right;
                        cell22.HorizontalAlign = HorizontalAlign.Right;
                        cell23.HorizontalAlign = HorizontalAlign.Right;
                        cell24.HorizontalAlign = HorizontalAlign.Right;


                        cell1.Text = "Total";
                        row.Cells.Add(cell1);
                        cell2.Text = dr2["Alerttotal"].ToString();
                        row.Cells.Add(cell2);
                        cell3.Text = dr2["AlertRatio"].ToString();
                        row.Cells.Add(cell3);
                        cell4.Text = dr2["AlertScore"].ToString();
                        row.Cells.Add(cell4);
                        cell5.Text = dr2["STRRaised"].ToString();
                        row.Cells.Add(cell5);
                        cell6.Text = dr2["ConvertedSTR"].ToString();
                        row.Cells.Add(cell6);
                        cell7.Text = dr2["STRScore"].ToString();
                        row.Cells.Add(cell7);
                        cell8.Text = dr2["CLTotal"].ToString();
                        row.Cells.Add(cell8);
                        cell9.Text = dr2["CLRatio"].ToString();
                        row.Cells.Add(cell9);
                        cell10.Text = dr2["CLScore"].ToString();
                        row.Cells.Add(cell10);
                        cell11.Text = dr2["ELTotal"].ToString();
                        row.Cells.Add(cell11);
                        cell12.Text = dr2["ELRatio"].ToString();
                        row.Cells.Add(cell12);
                        cell13.Text = dr2["ELScore"].ToString();
                        row.Cells.Add(cell13);
                        cell14.Text = dr2["KYCTotal"].ToString();
                        row.Cells.Add(cell14);
                        cell15.Text = dr2["KYCRatio"].ToString();
                        row.Cells.Add(cell15);
                        cell16.Text = dr2["KYCScore"].ToString();
                        row.Cells.Add(cell16);
                        cell17.Text = dr2["BRTotal"].ToString();
                        row.Cells.Add(cell17);
                        cell18.Text = dr2["BRRatio"].ToString();
                        row.Cells.Add(cell18);
                        cell19.Text = dr2["BRScore"].ToString();
                        row.Cells.Add(cell19);
                        cell20.Text = dr2["PEPTotal"].ToString();
                        row.Cells.Add(cell20);
                        cell21.Text = dr2["PEPOutstanding"].ToString();
                        row.Cells.Add(cell21);
                        cell22.Text = dr2["Top100Ratio"].ToString();
                        row.Cells.Add(cell22);
                        cell23.Text = dr2["Top100Score"].ToString();
                        row.Cells.Add(cell23);
                        cell24.Text = dr2["TotalScore"].ToString();
                        row.Cells.Add(cell24);



                        myTable.Rows.Add(row);

                    }
                }
            }
            dbclass.CloseConnection();

            
        }

        private void fillSankyChartMTD()
        {
            dbclass.OpenConection();

            string query = "Select Month, Region, ";
            query = query + " Round(Avg(TotalScore),2) as TotalScore";
            query = query + " from CL_CALCULATED_DATA_AREA";
            query = query + " Where Year = '" + DD_Year.Text + "'";
            query = query + "  ANd  Month = '" + DD_Month.Text + "'";
            query = query + " and DataLevel = 'Region'";
            if (DD_Segment.Text.ToUpper() == "DISTRIBUTION")
            {
                query = query + " AND Segment in ('Retail','Islamic')";
            }
            if (DD_Segment.Text.ToUpper() == "COMMERCIAL")
            {
                query = query + " AND Segment in ('Commercial')";
            }
            if (DD_Segment.Text.ToUpper() == "CORPORATE")
            {
                query = query + " AND Segment in ('Corporate')";
            }
            query = query + " Group by Month, Region Order by TotalScore Desc";
            
            SqlDataReader dr2 = dbclass.DataReader(query);

            SankyChartMTDRegion = "";

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {

                    SankyChartMTDRegion = SankyChartMTDRegion + "['" + dr2["Month"].ToString() + "',";
                    SankyChartMTDRegion = SankyChartMTDRegion + "'" + dr2["Region"].ToString() + "',";
                    SankyChartMTDRegion = SankyChartMTDRegion + "" + dr2["TotalScore"].ToString() + "],";

                }
            }

            SankyChartMTDRegion = SankyChartMTDRegion.Substring(0, SankyChartMTDRegion.Length - 1);

            dbclass.CloseConnection();


        }

        private void fillSankyChartMTDRegion()
        {
            dbclass.OpenConection();

            string query = "Select Month, Region, AreaName, ";
            query = query + " Round(Avg(TotalScore),2) as TotalScore";
            query = query + " from CL_CALCULATED_DATA_AREA";
            query = query + " Where Year = '" + DD_Year.Text + "'";
            query = query + "  ANd  Month = '" + DD_Month.Text + "'";
            query = query + " and DataLevel = 'Area'";
            if (DD_Segment.Text.ToUpper() == "DISTRIBUTION")
            {
                query = query + " AND Segment in ('Retail','Islamic')";
            }
            if (DD_Segment.Text.ToUpper() == "COMMERCIAL")
            {
                query = query + " AND Segment in ('Commercial')";
            }
            if (DD_Segment.Text.ToUpper() == "CORPORATE")
            {
                query = query + " AND Segment in ('Corporate')";
            }
            query = query + " Group by Month, Region, AreaName Order by TotalScore Desc";

            SqlDataReader dr2 = dbclass.DataReader(query);

            SankyChartMTDArea = "";

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {

                    SankyChartMTDArea = SankyChartMTDArea + "['" + dr2["Region"].ToString() + "',";
                    SankyChartMTDArea = SankyChartMTDArea + "'" + dr2["AreaName"].ToString() + "',";
                    SankyChartMTDArea = SankyChartMTDArea + "" + dr2["TotalScore"].ToString() + "],";

                }
            }

            SankyChartMTDArea = SankyChartMTDArea.Substring(0, SankyChartMTDArea.Length - 1);

            dbclass.CloseConnection();


        }
        private void getYTDAlerts()
        {

            string query = " Select ";
            query = query + " Sum(AlertTotal) as Alerttotal, ";
            query = query + " Sum(AlertClosed) AlertClosed, ";
            query = query + " Round(Avg(AlertRatio), 2) * 100 as AlertRatio,";
            query = query + " Round(Avg(AlertScore), 2) as AlertScore";
           
            query = query + " from CL_CALCULATED_DATA_AREA";

            query = query + " Where Year = '" + DD_Year.Text + "'";
            //query = query  + " and Month = '" + DD_Month.Text + "'";
            query = query + " and DataLevel = 'Region'";
            if (DD_Segment.Text.ToUpper() == "DISTRIBUTION")
            {
                query = query + " AND Segment in ('Retail','Islamic')";
            }
            if (DD_Segment.Text.ToUpper() == "COMMERCIAL")
            {
                query = query + " AND Segment in ('Commercial')";
            }
            if (DD_Segment.Text.ToUpper() == "CORPORATE")
            {
                query = query + " AND Segment in ('Corporate')";
            }
            //query = query + " Group by Region, Threshold Order by totalScore desc";


            dbclass.OpenConection();
            SqlDataReader dr2 = dbclass.DataReader(query);

            ChartAlertLabel = "['Total','Closed','Completion %', 'Score']";
           
            while (dr2.Read())
            {

                ChartAlertTotal = ChartAlertTotal + "[" + dr2["Alerttotal"].ToString() + "]";
                ChartAlertClosed = ChartAlertClosed + "[" + dr2["Alertclosed"].ToString() + "]";
                ChartAlertRatio = ChartAlertRatio + "["+ dr2["AlertRatio"].ToString() + "]";
                ChartAlertScore = ChartAlertScore + "["+ dr2["AlertScore"].ToString() + "]";
            }
           
            dbclass.CloseConnection();

        }   // end function


        private void getRegionThresholdYTD()
        {


            string query2 = "";
            query2 = "Select C.Threshold, Count(region) Total, T.seqno from CL_Threshold T LEFT Join CL_CALCULATED_DATA_AREA C on T.CL_Threshold = C.Threshold";
            query2 = query2 + " Where DataLevel='Region' and Year = '" + DD_Year.Text + "'";
            //query2= query2 +  " and Month = '" + DD_Month.Text + "'";
            if (DD_Segment.Text.ToUpper() == "DISTRIBUTION")
            {
                query2 = query2 + " AND Segment in ('Retail','Islamic')";
            }
            if (DD_Segment.Text.ToUpper() == "COMMERCIAL")
            {
                query2 = query2 + " AND Segment in ('Commercial')";
            }
            if (DD_Segment.Text.ToUpper() == "CORPORATE")
            {
                query2 = query2 + " AND Segment in ('Corporate')";
            }
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

                chartseriesdata = chartseriesdata + "{ name: '" + dr2["Threshold"].ToString() + "', y: " + dr2["Total"].ToString() + " , drilldown: '" + dr2["Threshold"].ToString() + "' },";

            }
            ChartLabelRegdist = ChartLabelRegdist.Substring(0, ChartLabelRegdist.Length - 1);
            ChartLabelRegdist = ChartLabelRegdist + "]";
            ChartDataRegdist = ChartDataRegdist.Substring(0, ChartDataRegdist.Length - 1);
            ChartDataRegdist = ChartDataRegdist + "]";

            chartseriesdata = chartseriesdata.Substring(0, chartseriesdata.Length - 1);


            //chartseriesdata = "{ name: 'Chrome', y: 62.74, drilldown: 'Chrome' },";
            //chartseriesdata = chartseriesdata + "{ name: 'Firefox', y: 10.57, drilldown: 'Firefox' }";


            //ChartDataRegdist = "[{ name: 'Partial' , y:10, drilldown: 'Partial'}, { name: 'Full', y:15, drilldown: 'Full' }]";
            //ChartDataRegdist = ChartDataRegdist + ", drilldown: { series: [{ name: 'Partial', id: 'Partial', data: [ ['Kar' , 5], ['Lhr', 6] ] }] }";
            dbclass.CloseConnection();

            // drill down data query
             query2 = "";
            query2 = "Select C.Threshold, Region, TotalScore from CL_Threshold T LEFT Join CL_CALCULATED_DATA_AREA C on T.CL_Threshold = C.Threshold";
            query2 = query2 + " Where DataLevel='Region' and Segment in ('Retail','Islamic') and Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";
            query2 = query2 + "  Order by T.Seqno";

            dbclass.OpenConection();
            dr2 = dbclass.DataReader(query2);
            while (dr2.Read())
            {

                chartdrilldowndata = chartdrilldowndata + "{ name: '" + dr2["Threshold"].ToString() + "',";
                chartdrilldowndata = chartdrilldowndata + " id: '" + dr2["Threshold"].ToString() + "',";
                chartdrilldowndata = chartdrilldowndata + " data: [";
                chartdrilldowndata = chartdrilldowndata + " [ '" + dr2["Region"].ToString() + "',";
                chartdrilldowndata = chartdrilldowndata +  dr2["TotalScore"].ToString() + "],";

            }

            chartdrilldowndata = chartdrilldowndata.Substring(0, chartdrilldowndata.Length - 1);

            chartdrilldowndata = chartdrilldowndata + " data: ]},";
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
            query2 = query2 + " Where  Segment in ('Retail','Islamic') and Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";
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
            //GridView1.PageIndex = e.NewPageIndex;

          
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

            query = query + " Where Segment in ('Retail','Islamic') ";

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

            getSegmentYTDScore();
            getSegmentMTDScore();
            fillDataTableEachMonth();
            fillSankyChartMTD();
            fillSankyChartMTDRegion();
            //getRegionThresholdYTD();
            getRegionThresholdCommercial();
            getRegionThresholdCorporate();
            getBranchThreshold();
            getCommBranchThreshold();
            getCorpBranchThreshold();
            getRegionScore();
            getRegionScoreCommercial();
            getRegionScoreCorporate();
            getRegionScoreCorporate();
         
        }

        private void downloadCSV()
        {

            string query = "Select Region, Threshold,";
            query = query + " Sum(TotalBranches) as TotalBranches, Sum(AlertTotal) as Alerttotal, sum(AlertClosed) AlertClosed, Round(Avg(AlertRatio), 2) * 100 as AlertRatio,";
            query = query + " sum(StrRaised) as STRRaised, sum(STRConverted) ConvertedSTR,";
            query = query + " Sum(CLTrainingTotal) CLTotal, sum(CLTrainingClosed) CLClosed, Round(Avg(CLTrainingRatio), 2) * 100 as CLRatio,";
            query = query + " Sum(ELTrainingTotal) ELTotal, sum(ELTrainingClosed) ELClosed, Round(Avg(ELTrainingRatio), 2) * 100 as ELRatio,";
            query = query + " Sum(KYCtotal) KYCTotal, sum(KYCclosed) KYCClosed, Round(Avg(KYCRatio), 2) * 100 as KYCRatio,";
            query = query + " Sum(BranchReviewTotal) BRTotal, sum(BranchReviewClosed) BRClosed, Round(Avg(BranchReviewRatio), 2) * 100 as BRRatio,";
            query = query + " Sum(PEPTotal) PEPTotal, sum(PEPOutstanding) PEPOutstanding, Round(Avg(PEPRatio), 2) * 100 as PERatio, Avg(PEPScore) as PEPScore,";
            query = query + " round(sum(Top100Ratio),2) as Top100Ratio, round(Avg(totalScore),2) as TotalScore, Avg(RankOrder) as RankOder";

            query = query + " from CL_CALCULATED_DATA_AREA";

            query = query + " Where Segment in ('Retail','Islamic') ";

            query = query + " AND Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";
            query = query + " and DataLevel = 'Region'";
            query = query + " Group by Region, Threshold Order by Region";


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