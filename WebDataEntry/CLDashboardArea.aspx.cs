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
    public partial class CLDashboardArea : System.Web.UI.Page
    {
        DBClass dbclass = new DBClass();
        public string TxtSegment = "";

        //Threshold chart
        public string ChartLabelThBranch = null;
        public string ChartDataThBranch = null;
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
                dbclass.IP_LOG(Request.UserHostAddress, "CLDashboardArea.aspx");
                dropdownfill();
                check_Segment();
                getSegmentScore();
                getBranchThreshold();
                getChartData();
                fillgrid();
                fillDataTableArea();
                fillDataTableBranch();

            }

            lblsegment.Text = DD_Segment.Text;
            lblArea.Text = DD_Area.Text;
        } // end page_load

        private void dropdownfill()
        {
            string query = "select Distinct Year  from CL_CALCULATED_DATA Order by Year Desc";

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

            query = "select Segment  from CL_SEGMENT WHERE ACTIVE='Y' Order by SeqNo";

            ds = dbclass.DataSet(query);

            DD_Segment.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_Segment.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_Segment.DataSource = ds.Tables[0];
            DD_Segment.DataBind();

            //query = "select Distinct Region  from CL_CALCULATED_DATA";

            //ds = dbclass.DataSet(query);

            //DD_Region.DataTextField = ds.Tables[0].Columns[0].ToString();
            //DD_Region.DataValueField = ds.Tables[0].Columns[0].ToString();
            //DD_Region.DataSource = ds.Tables[0];
            //DD_Region.DataBind();

            check_Segment();
            query = "select Distinct AreaName  from CL_CALCULATED_DATA ";
            query = query + " WHERE Segment in " + TxtSegment.ToString();

            //if (DD_Segment.Text.ToUpper() == "DISTRIBUTION" || DD_Segment.Text == "")
            //{
            //    query = query + " Where Segment in ('Retail','Islamic')";
            //}
            //if (DD_Segment.Text.ToUpper() == "COMMERCIAL")
            //{
            //    query = query + " Where Segment in ('Commercial')";
            //}
            //if (DD_Segment.Text.ToUpper() == "CORPORATE")
            //{
            //    query = query + " Where Segment in ('Corporate')";
            //}
            query = query + "   ORder by AreaName";
            //query = query + " AND Region = '" + DD_Region.Text + "'  ORder by AreaName";

            //query = "select Distinct AreaName  from CL_CALCULATED_DATA Where Segment='" + DD_Segment.Text + "' Order by AreaName";
            dbclass.OpenConection();
            ds = dbclass.DataSet(query);

            DD_Area.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_Area.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_Area.DataSource = ds.Tables[0];
            DD_Area.DataBind();
            dbclass.CloseConnection();


        }


        private void check_Segment()
        {
            string query = "Select SegmentString  from CL_Segment where Segment='" + DD_Segment.Text.ToString() + "'";

            dbclass.OpenConection();

            SqlDataReader dr2 = dbclass.DataReader(query);

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    TxtSegment = dr2["SegmentString"].ToString();
                }
            }

            dbclass.CloseConnection();
        } // Check_Segement


        private void fillDataTableArea()
        {
            string query = "Select BranchCodeText, BranchName,";
            query = query + " Round(Avg(TotalScore), 2)  as TotalScore,";
            query = query + " Round(Avg(AlertScore), 2)  as AlertRatio,";

            query = query + " Round(Avg(CLTrainingScore), 2)  as CLRatio,";
            query = query + " Round(Avg(ELTrainingScore), 2)  as ELRatio,";
            query = query + " Round(Avg(KYCscore), 2)  as KYCRatio,";
            query = query + " Round(Avg(BranchReviewScore), 2) as BRRatio,";
            query = query + " Round(Avg(PEPScore), 2) as PERatio, ";
            query = query + " round(Avg(Top100Score),2) as Top100Ratio,";
            query = query + " sum(STRRaisedScore) + sum(STRconvertedScore) as STRScore,";
            query = query + " Avg(RankOrder) as RegionalRank ";

            query = query + " from CL_CALCULATED_DATA";
            query = query + " Where Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";
            query = query + " AND Segment in " + TxtSegment.ToString();

            //if (DD_Segment.Text.ToUpper() == "DISTRIBUTION")
            //{
            //    query = query + " AND Segment in ('Retail','Islamic')";
            //}
            //if (DD_Segment.Text.ToUpper() == "COMMERCIAL")
            //{
            //    query = query + " AND Segment in ('Commercial')";
            //}
            //if (DD_Segment.Text.ToUpper() == "CORPORATE")
            //{
            //    query = query + " AND Segment in ('Corporate')";
            //}

            query = query + " And AreaName = '" + DD_Area.Text + "'";
            //query = query + " and DataLevel = 'Region'";
            query = query + " Group by BranchCodeText, BranchName Order by RegionalRank";

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

                        tblBranch.Rows.Add(row);

                    }
                }
            }



            //GridView1.ControlStyle.Font.Size = 7;
            dbclass.CloseConnection();
        }

        private void fillDataTableBranch()
        {
            string query = "Select AreaName,";
            query = query + " sum(TotalBranches) as TotalBranches, ";
            query = query + " Round(Avg(TotalScore), 2)  as TotalScore,";
            query = query + " Round(Avg(AlertScore), 2)  as AlertRatio,";
            query = query + " Round(Avg(CLTrainingScore), 2) as CLRatio,";
            query = query + " Round(Avg(ELTrainingScore), 2) as ELRatio,";
            query = query + " Round(Avg(KYCScore), 2)  as KYCRatio,";
            query = query + " Round(Avg(BranchReviewScore), 2) as BRRatio,";
            query = query + " Round(Avg(PEPScore), 2) as PERatio, ";
            query = query + " round(Avg(Top100Score),2)  as Top100Ratio, ";
            query = query + " sum(STRRaisedScore) + sum(STRconvertedScore) as STRScore";
            query = query + " from CL_CALCULATED_DATA_AREA";

            //query = query + " Where Segment in ('Retail','Islamic') ";

            query = query + " Where DataLevel='Area' AND Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";
            query = query + " AND Segment in " + TxtSegment.ToString();

            //if (DD_Segment.Text.ToUpper() == "DISTRIBUTION")
            //{
            //    query = query + " AND Segment in ('Retail','Islamic')";
            //}
            //if (DD_Segment.Text.ToUpper() == "COMMERCIAL")
            //{
            //    query = query + " AND Segment in ('Commercial')";
            //}
            //if (DD_Segment.Text.ToUpper() == "CORPORATE")
            //{
            //    query = query + " AND Segment in ('Corporate')";
            //}

            query = query + " And AreaName = '" + DD_Area.Text + "'";
            query = query + " Group by AreaName, TotalBranches Order by TotalScore Desc";

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
                        //cell12.Text = dr2[11].ToString();
                        //row.Cells.Add(cell12);


                        myTable.Rows.Add(row);

                    }
                }
            }
            dbclass.CloseConnection();
        }

        private void fillgrid()
        {
            string query = "Select AreaName, Threshold,";
            query = query + " Sum(TotalBranches) as TotalBranches, Sum(AlertTotal) as Alerttotal, sum(AlertClosed) AlertClosed, Round(Avg(AlertRatio), 2) * 100 as AlertRatio,";
            query = query + " sum(StrRaised) as STRRaised, sum(STRConverted) ConvertedSTR,";
            query = query + " Sum(CLTrainingTotal) CLTotal, sum(CLTrainingClosed) CLClosed, Round(Avg(CLTrainingRatio), 2) * 100 as CLRatio,";
            query = query + " Sum(ELTrainingTotal) ELTotal, sum(ELTrainingClosed) ELClosed, Round(Avg(ELTrainingRatio), 2) * 100 as ELRatio,";
            query = query + " Sum(KYCtotal) KYCTotal, sum(KYCclosed) KYCClosed, Round(Avg(KYCRatio), 2) * 100 as KYCRatio,";
            query = query + " Sum(BranchReviewTotal) BRTotal, sum(BranchReviewClosed) BRClosed, Round(Avg(BranchReviewRatio), 2) * 100 as BRRatio,";
            query = query + " Sum(PEPTotal) PEPTotal, sum(PEPClosed) PEPClosed, Round(Avg(PEPRatio), 2) * 100 as PERatio, Avg(PEPScore) as PEPScore,";
            query = query + " round(sum(Top100Ratio),2) as Top100Ratio, round(Avg(totalScore),2) as TotalScore, round(Avg(TotalScoreWoBonus),2) as TotalScoreWithoutBonus, Avg(RankOrder) as RankOder";

            query = query + " from CL_CALCULATED_DATA_AREA";
            //query = query + " Where Segment in ('Retail','Islamic') ";

            query = query + " Where Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";
            query = query + " AND Segment in " + TxtSegment.ToString();

            //query = query + " AND Segment = '" + DD_Segment.Text + "'";
            //query = query + " AND Region = '" + DD_Region.Text + "'";
            //if (DD_Segment.Text.ToUpper() == "DISTRIBUTION")
            //{
            //    query = query + " AND Segment in ('Retail','Islamic')";
            //}
            //if (DD_Segment.Text.ToUpper() == "COMMERCIAL")
            //{
            //    query = query + " AND Segment in ('Commercial')";
            //}
            //if (DD_Segment.Text.ToUpper() == "CORPORATE")
            //{
            //    query = query + " AND Segment in ('Corporate')";
            //}

            query = query + " and DataLevel = 'Area'";
            query = query + " Group by AreaName, Threshold Order by AreaName";

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
            Double scorewo = 0;
            string query2 = "";
            query2 = "Select avg(TotalScore) Total, avg(TotalScoreWOBonus) TotalWO from CL_CALCULATED_DATA_AREA";
            query2 = query2 + " Where DataLevel='Area' AND AreaName ='" + DD_Area.Text + "'";
            query2 = query2 + " and Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";
            query2 = query2 + " AND Segment in " + TxtSegment.ToString();

            //if (DD_Segment.Text.ToUpper() == "DISTRIBUTION")
            //{
            //    query2 = query2 + " AND Segment in ('Retail','Islamic')";
            //}
            //if (DD_Segment.Text.ToUpper() == "COMMERCIAL")
            //{
            //    query2 = query2 + " AND Segment in ('Commercial')";
            //}
            //if (DD_Segment.Text.ToUpper() == "CORPORATE")
            //{
            //    query2 = query2 + " AND Segment in ('Corporate')";
            //}

            dbclass.OpenConection();
            SqlDataReader dr2 = dbclass.DataReader(query2);

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    if (!dr2.IsDBNull(0))
                    {
                        score = Math.Round(dr2.GetDouble(0), 2);
                        scorewo = Math.Round(dr2.GetDouble(1), 2);
                        //pbardist.Style["width"] = String.Format("{0}%", (int)score);
                        //pbardist.Style["background-color"] = "darkcyan";
                        lblh5dist.Text = String.Format("{0}%", (double)score);
                        lblwbscore.Text = "Without Bonus " + String.Format("{0}%", (double)scorewo);
                    }
                }
            }


            query2 = "Select Threshold as Total, RankOrder Rank from CL_CALCULATED_DATA_AREA";
            query2 = query2 + " Where DataLevel='Area' AND AreaName ='" + DD_Area.Text + "'";
            query2 = query2 + " and Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";
            query2 = query2 + " AND Segment in " + TxtSegment.ToString();

            //if (DD_Segment.Text.ToUpper() == "DISTRIBUTION")
            //{
            //    query2 = query2 + " AND Segment in ('Retail','Islamic')";
            //}
            //if (DD_Segment.Text.ToUpper() == "COMMERCIAL")
            //{
            //    query2 = query2 + " AND Segment in ('Commercial')";
            //}
            //if (DD_Segment.Text.ToUpper() == "CORPORATE")
            //{
            //    query2 = query2 + " AND Segment in ('Corporate')";
            //}


            dr2 = dbclass.DataReader(query2);

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    if (!dr2.IsDBNull(0))
                    {
                        score = dr2.GetDouble(1);
                        //pbarcom.Style["width"] = String.Format("{0}%", (int)score);
                        //lblrank.Text = String.Format("{0}", (int)score);
                        lblh5com.Text = dr2.GetString(0);
                        lblRank.Text = "Rank " + dr2.GetDouble(1).ToString();
                        Label1.Text = ".";
                    }
                }
            }


            dbclass.CloseConnection();
        } // Get Segment Score

        private void getChartData()
        {
            dbclass.OpenConection();
            // Get Parameter Table value
            string obtquery = "Select Alert_Weightage, [CLTraining_Weightage], [ELTraining_Weightage], ";
            obtquery = obtquery + " [PSTR_Weightage], [PEP_Weightage], [100Depositor_Weightage], [KYCScore_Weightage], [BranchReview_Weightage]  ";
            obtquery = obtquery + " from CL_CALCULATION_PARAMETER_HST where CL_Level='Region'";
            obtquery = obtquery + " and Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";
            SqlDataReader drobt = dbclass.DataReader(obtquery);
            double alert, pstr, cltraining, eltraining, kyc, branchonsite, pep, top100deposit;
            alert = pstr = cltraining = eltraining = kyc = branchonsite = pep = top100deposit = 0;
            if (drobt.HasRows)
            {
                while (drobt.Read())
                {
                    alert = drobt.GetDouble(0);
                    pstr = drobt.GetDouble(3);
                    cltraining = drobt.GetDouble(1);
                    eltraining = drobt.GetDouble(2);
                    pep = drobt.GetDouble(4);
                    top100deposit = drobt.GetDouble(5);
                    kyc = drobt.GetDouble(6);
                    branchonsite = drobt.GetDouble(7);
                    lblmaxalert.Text = alert.ToString();
                    lblmaxpstr.Text = pstr.ToString();
                    lblmaxclscore.Text = cltraining.ToString();
                    lblelmaxscore.Text = eltraining.ToString();
                    lblmaxkycscore.Text = kyc.ToString();
                    lblmaxbrscore.Text = branchonsite.ToString();
                    lblmaxpepscore.Text = pep.ToString();
                    lblmaxtop100score.Text = top100deposit.ToString();
                }
            }
            else
            {
                lblmaxalert.Text = "";
                lblmaxpstr.Text = "";
                lblmaxclscore.Text = "";
                lblelmaxscore.Text = "";
                lblmaxkycscore.Text = "";
                lblmaxbrscore.Text = "";
                lblmaxpepscore.Text = "";
                lblmaxtop100score.Text = "";
            }

            string query = "Select AreaName, Threshold,";
            query = query + " Sum(TotalBranches) as TotalBranches, Sum(AlertTotal) as Alerttotal, sum(AlertClosed) AlertClosed, Round(Avg(AlertRatio), 2) * 100 as AlertRatio,";
            query = query + " sum(StrRaised) as STRRaised, sum(STRConverted) ConvertedSTR,";
            query = query + " Sum(CLTrainingTotal) CLTotal, sum(CLTrainingClosed) CLClosed, Round(Avg(CLTrainingRatio), 2) * 100 as CLRatio,";
            query = query + " Sum(ELTrainingTotal) ELTotal, sum(ELTrainingClosed) ELClosed, Round(Avg(ELTrainingRatio), 2) * 100 as ELRatio,";
            query = query + " Sum(KYCtotal) KYCTotal, sum(KYCclosed) KYCClosed, Round(Avg(KYCRatio), 2) * 100 as KYCRatio,";
            query = query + " Sum(KYC_High_Total) KYCHTotal, sum(KYC_High_Closed) KYCHClosed, Round(Avg(KYC_High_Ratio), 2) * 100 as KYCHRatio, Round(Avg(KYC_High_Score),2) as KYCHScore,";
            query = query + " Sum(KYC_Med_Total) KYCMTotal, sum(KYC_Med_Closed) KYCMClosed, Round(Avg(KYC_Med_Ratio), 2) * 100 as KYCMRatio, Round(Avg(KYC_Med_Score),2) as KYCMScore,";
            query = query + " Sum(KYC_Low_Total) KYCLTotal, sum(KYC_Low_Closed) KYCLClosed, Round(Avg(KYC_Low_Ratio), 2) * 100 as KYCLRatio, Round(Avg(KYC_Low_Score),2) as KYCLScore,";
            query = query + " Sum(BranchReviewTotal) BRTotal, sum(BranchReviewClosed) BRClosed, Round(Avg(BranchReviewRatio), 2) * 100 as BRRatio,";
            query = query + " Sum(PEPTotal) PEPTotal, sum(PEPClosed) PEPClosed, Round(Avg(PEPRatio), 2) * 100 as PEPRatio, Avg(PEPScore) as PEPScore,";
            query = query + " round(sum(Top100Ratio),2) *100 as Top100Ratio, round(Avg(totalScore),2) as TotalScore, Avg(RankOrder) as RankOder,";
            query = query + " Round(Avg(AlertScore),2) as AlertScore,";
            query = query + " Round(Avg(STRRaisedScore),2) as STRScore,";
            query = query + " Round(Avg(STRConvertedScore),2) as PSTRScore,";
            query = query + " Round(Avg(CLTrainingScore),2) as CLScore,";
            query = query + " Round(Avg(ELTrainingScore),2) as ELScore,";
            query = query + " Round(Avg(KYCScore),2) as KYCScore,";
            query = query + " Round(Avg(BranchReviewScore),2) as BRScore,";
            query = query + " Round(Avg(PEPScore),0) as PEPScore,";
            query = query + " Round(Avg(Top100_RMC_Score),2) as Top100RMCScore,";
            query = query + " Round(Avg(Top100_Self_Score),2) as Top100SelfScore,";
            query = query + " Round(Avg(Top100_KYC_Score),2) as Top100KYCScore,";
            query = query + " Round(Avg(Top100Score),2) as Top100Score";

            query = query + " from CL_CALCULATED_DATA_AREA";
            //query = query + " Where Segment in ('Retail','Islamic') ";

            query = query + " Where Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";
            query = query + " AND Segment in " + TxtSegment.ToString();

            //if (DD_Segment.Text.ToUpper() == "DISTRIBUTION")
            //{
            //    query = query + " AND Segment in ('Retail','Islamic')";
            //}
            //if (DD_Segment.Text.ToUpper() == "COMMERCIAL")
            //{
            //    query = query + " AND Segment in ('Commercial')";
            //}
            //if (DD_Segment.Text.ToUpper() == "CORPORATE")
            //{
            //    query = query + " AND Segment in ('Corporate')";
            //}

            query = query + " and AreaName = '" + DD_Area.Text + "'";
            query = query + " and DataLevel = 'Area'";
            query = query + " Group by AreaName, Threshold Order by TotalScore Desc";

            SqlDataReader dr2 = dbclass.DataReader(query);

            lbltotalalerts.Text = "";
            lblclosedalerts.Text = "";
            lblratioalerts.Text = "";
            lblscorealerts.Text = "";
            lbltotalstr.Text = "";
            lblconvertedstr.Text = "";
            lblscorestr.Text = "";
            lblscorepstr.Text = "";

            lblCLtotal.Text = "";
            lblCLClosed.Text = "";
            lblCLRatio.Text = "";
            lblCLScore.Text = "";

            lblELtotal.Text = "";
            lblELClosed.Text = "";
            lblELRatio.Text = "";
            lblELScore.Text = "";

            lblKYCtotal.Text = "";
            lblKYCclosed.Text = "";
            lblKYCratio.Text = "";
            lblKYCScore.Text = "";

            lblKYCHtotal.Text = "";
            lblKYCHclosed.Text = "";
            lblKYCHscore.Text = "";
            lblKYCMtotal.Text = "";
            lblKYCMclosed.Text = "";
            lblKYCMscore.Text = "";
            lblKYCLtotal.Text = "";
            lblKYCLclosed.Text = "";
            lblKYCLscore.Text = "";

            lblBRTotal.Text = "";
            lblBRclosed.Text = "";
            lblBRratio.Text = "";
            lblBRscore.Text = "";

            lblPEPtotal.Text = "";
            lblPEPClosed.Text = "";
            lblPEPratio.Text = "";
            lblPEPscore.Text = "";
            lbltop100ratio.Text = "";
            lbltop100score.Text = "";
            lbltop100KYCscore.Text = "";
            lbltop100RMCscore.Text = "";
            lbltop100Selfscore.Text = "";

            while (dr2.Read())
            {

                lbltotalalerts.Text = dr2["Alerttotal"].ToString();
                lblclosedalerts.Text = dr2["AlertClosed"].ToString();
                lblratioalerts.Text = dr2["AlertRatio"].ToString() + " %";
                lblscorealerts.Text = dr2["AlertScore"].ToString();

                lbltotalstr.Text = dr2["STRRaised"].ToString();
                lblconvertedstr.Text = dr2["ConvertedSTR"].ToString();
                lblscorestr.Text = dr2["STRScore"].ToString();
                lblscorepstr.Text = dr2["PSTRScore"].ToString();

                lblCLtotal.Text = dr2["CLTotal"].ToString();
                lblCLClosed.Text = dr2["CLClosed"].ToString();
                lblCLRatio.Text = dr2["CLRatio"].ToString() + " %";
                lblCLScore.Text = dr2["CLScore"].ToString();

                lblELtotal.Text = dr2["ELTotal"].ToString();
                lblELClosed.Text = dr2["ELClosed"].ToString();
                lblELRatio.Text = dr2["ELRatio"].ToString() + " %";
                lblELScore.Text = dr2["ELScore"].ToString();

                lblKYCtotal.Text = dr2["KYCTotal"].ToString();
                lblKYCclosed.Text = dr2["KYCClosed"].ToString();
                lblKYCratio.Text = dr2["KYCRatio"].ToString() + " %";
                lblKYCScore.Text = dr2["KYCScore"].ToString();

                // add by ark on Jan-15-2020
                lblKYCHtotal.Text = dr2["KYCHTotal"].ToString();
                lblKYCHclosed.Text = dr2["KYCHClosed"].ToString();
                lblKYCHscore.Text = dr2["KYCHScore"].ToString();

                lblKYCMtotal.Text = dr2["KYCMTotal"].ToString();
                lblKYCMclosed.Text = dr2["KYCMClosed"].ToString();
                lblKYCMscore.Text = dr2["KYCMScore"].ToString();

                lblKYCLtotal.Text = dr2["KYCLTotal"].ToString();
                lblKYCLclosed.Text = dr2["KYCLClosed"].ToString();
                lblKYCLscore.Text = dr2["KYCLScore"].ToString();

                lblBRTotal.Text = dr2["BRTotal"].ToString();
                lblBRclosed.Text = dr2["BRClosed"].ToString();
                lblBRratio.Text = dr2["BRRatio"].ToString() + " %";
                lblBRscore.Text = dr2["BRScore"].ToString();

                lblPEPtotal.Text = dr2["PEPTotal"].ToString();
                lblPEPClosed.Text = dr2["PEPClosed"].ToString();
                lblPEPratio.Text = dr2["PEPRatio"].ToString() + " %";
                lblPEPscore.Text = dr2["PEPScore"].ToString();

                lbltop100ratio.Text = dr2["Top100Ratio"].ToString() + " %";
                lbltop100score.Text = dr2["Top100Score"].ToString();

                lbltop100RMCscore.Text = dr2["Top100RMCScore"].ToString();
                lbltop100Selfscore.Text = dr2["Top100SelfScore"].ToString();
                lbltop100KYCscore.Text = dr2["Top100KYCScore"].ToString();

            }

            dbclass.CloseConnection();

        }



        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // increment PageIndex
            GridView1.PageIndex = e.NewPageIndex;

            // bind table again
            fillgrid();
        }



        protected void btnSearch_Click(object sender, EventArgs e)
        {
            check_Segment();
            getSegmentScore();
            getBranchThreshold();
            getChartData();
            fillgrid();
            fillDataTableArea();
            fillDataTableBranch();

        }

        private void downloadCSV()
        {

            string query = "Select Year, Month, AreaName, Threshold, Segment,";
            query = query + " Sum(TotalBranches) as TotalBranches, Sum(AlertTotal) as Alerttotal, sum(AlertClosed) AlertClosed, Round(Avg(AlertRatio), 2) * 100 as AlertRatio,";
            query = query + " sum(StrRaised) as STRRaised, sum(STRConverted) ConvertedSTR,";
            query = query + " Sum(CLTrainingTotal) CLTotal, sum(CLTrainingClosed) CLClosed, Round(Avg(CLTrainingRatio), 2) * 100 as CLRatio,";
            query = query + " Sum(ELTrainingTotal) ELTotal, sum(ELTrainingClosed) ELClosed, Round(Avg(ELTrainingRatio), 2) * 100 as ELRatio,";
            query = query + " Sum(KYCtotal) KYCTotal, sum(KYCclosed) KYCClosed, Round(Avg(KYCRatio), 2) * 100 as KYCRatio,";
            query = query + " Sum(BranchReviewTotal) BRTotal, sum(BranchReviewClosed) BRClosed, Round(Avg(BranchReviewRatio), 2) * 100 as BRRatio,";
            query = query + " Sum(PEPTotal) PEPTotal, sum(PEPClosed) PEPClosed, Round(Avg(PEPRatio), 2) * 100 as PERatio, Avg(PEPScore) as PEPScore,";
            query = query + " round(sum(Top100Ratio),2) as Top100Ratio, round(Avg(totalScore),2) as TotalScore, round(Avg(TotalScoreWoBonus),2) as TotalScorewithoutBonus, Avg(RankOrder) as RankOder";

            query = query + " from CL_CALCULATED_DATA_AREA";

            //query = query + " Where Segment = '" + DD_Segment.Text + "'";

            query = query + " Where Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";
            query = query + " AND Segment in " + TxtSegment.ToString();

            //if (DD_Segment.Text.ToUpper() == "DISTRIBUTION")
            //{
            //    query = query + " AND Segment in ('Retail','Islamic')";
            //}
            //if (DD_Segment.Text.ToUpper() == "COMMERCIAL")
            //{
            //    query = query + " AND Segment in ('Commercial')";
            //}
            //if (DD_Segment.Text.ToUpper() == "CORPORATE")
            //{
            //    query = query + " AND Segment in ('Corporate')";
            //}

            query = query + " and DataLevel = 'Area'";
            query = query + " Group by Year, Month, AreaName, Threshold, Segment Order by AreaName";


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
            Response.AddHeader("content-disposition", "attachment;filename=CLDashboardArea.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(csv);
            Response.Flush();
            Response.End();

        }

        protected void btndownload_Click(object sender, EventArgs e)
        {
            check_Segment();
            downloadCSV();
        }

        private void getBranchThreshold()
        {
            string query2 = "";

            query2 = "Select C.Threshold, Count(region) Total, T.seqno from CL_Threshold T LEFT Join CL_CALCULATED_DATA C on T.CL_Threshold = C.Threshold";
            query2 = query2 + " Where  AreaName ='" + DD_Area.Text + "' AND Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";
            query2 = query2 + " AND Segment in " + TxtSegment.ToString();

            //if (DD_Segment.Text.ToUpper() == "DISTRIBUTION")
            //{
            //    query2 = query2 + " AND Segment in ('Retail','Islamic')";
            //}
            //if (DD_Segment.Text.ToUpper() == "COMMERCIAL")
            //{
            //    query2 = query2 + " AND Segment in ('Commercial')";
            //}
            //if (DD_Segment.Text.ToUpper() == "CORPORATE")
            //{
            //    query2 = query2 + " AND Segment in ('Corporate')";
            //}

            query2 = query2 + " Group by C.Threshold, T.Seqno Order by T.Seqno";

            dbclass.OpenConection();
            SqlDataReader dr2 = dbclass.DataReader(query2);

            ChartLabelThBranch = "[";
            ChartDataThBranch = "[";

            while (dr2.Read())
            {
                ChartLabelThBranch = ChartLabelThBranch + "'" + dr2["Threshold"].ToString() + "',";
                ChartDataThBranch = ChartDataThBranch + dr2["Total"].ToString() + ",";
            }

            ChartLabelThBranch = ChartLabelThBranch.Substring(0, ChartLabelThBranch.Length - 1);
            ChartLabelThBranch = ChartLabelThBranch + "]";
            ChartDataThBranch = ChartDataThBranch.Substring(0, ChartDataThBranch.Length - 1);
            ChartDataThBranch = ChartDataThBranch + "]";

            dbclass.CloseConnection();
        } // end function

        protected void DD_Region_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dropdownfill();
        }
    }
}