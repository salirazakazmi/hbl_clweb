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
    public partial class CLDashboardbk : System.Web.UI.Page
    {
        DBClass dbclass = new DBClass();

        // Public Variables for Dashboard Score
        public string ChartLabels = null;
        public string ChartData1 = null;
        public string ChartLabel2 = null;
        public string ChartData2 = null;
        public string ChartLabel3 = null;
        public string ChartData3 = null;
        public string ChartLabel4 = null;
        public string ChartData4 = null;
        public string ChartData4s2 = null;
        public string ChartData4s3 = null;
        public string ChartData4s4 = null;
        public string ChartData4s5 = null;
        public string ChartData4s6 = null;
        public string ChartData4s7 = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dropdownfill();
                getSegmentScore();
                getRegionThreshold();
                getBranchThreshold();
                getCommBranchThreshold();
                getRegionScore();
                fillgrid();

            }
        } // end page_load

        private void dropdownfill()
        {
            string query = "select Distinct Year  from CL_CALCULATED_DATA";

            dbclass.OpenConection();

            dbclass.DataReader(query);
            DataSet ds = dbclass.DataSet(query);

            DD_Year.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_Year.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_Year.DataSource = ds.Tables[0];
            DD_Year.DataBind();


            query = "select Distinct Month  from CL_CALCULATED_DATA";
            dbclass.DataReader(query);
            ds = dbclass.DataSet(query);

            DD_Month.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_Month.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_Month.DataSource = ds.Tables[0];
            DD_Month.DataBind();

            query = "select Distinct Segment  from CL_CALCULATED_DATA order by Segment desc";
            dbclass.DataReader(query);
            ds = dbclass.DataSet(query);

            DD_Segment.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_Segment.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_Segment.DataSource = ds.Tables[0];
            DD_Segment.DataBind();
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
            query = query + " Sum(PEPTotal) PEPTotal, sum(PEPClosed) PEPClosed, Round(Avg(PEPRatio), 2) * 100 as PERatio, Avg(PEPScore) as PEPScore,";
            query = query + " round(sum(Top100Ratio),2) as Top100Ratio, round(Avg(totalScore),2) as TotalScore, Avg(RankOrder) as RankOder";

            query = query + " from CL_CALCULATED_DATA_AREA";
            if (DD_Segment.Text == "Retail")
            {
                query = query + " Where Segment in ('Retail','Islamic') ";
            }
            if (DD_Segment.Text == "Corporate")
            {
                query = query + " Where Segment in ('Corporate') ";
            }
            if (DD_Segment.Text == "Commercial")
            {
                query = query + " Where Segment in ('Commercial') ";
            }
            query = query + " AND Year = '" + DD_Year.Text +"' and Month = '" + DD_Month.Text + "'";
            query = query + " and DataLevel = 'Region'";  
            query = query + " Group by Region, Threshold";

            dbclass.OpenConection();

            dbclass.DataReader(query);
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
            query2 = query2 + " Where DataLevel='Region' and Segment in ('Retail','Islamic') and Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";

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
                        lblh5dist.Text = String.Format("{0}%", (int)score);
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
                        score = dr2.GetDouble(0);
                        pbarcom.Style["width"] = String.Format("{0}%", (int)score);
                        lblh5com.Text = String.Format("{0}%", (int)score);
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
                        score = dr2.GetDouble(0);
                        pbarcor.Style["width"] = String.Format("{0}%", (int)score);
                        lblh5cor.Text = String.Format("{0}%", (int)score);
                    }

                }
            }
            dbclass.CloseConnection();
        } // Get Segment Score

        private void getRegionThreshold()
        {
            string query2 = "";
            query2 = "Select Threshold, Count(region) Total from CL_CALCULATED_DATA_AREA";
            query2 = query2 + " Where DataLevel='Region' and Segment in ('Retail','Islamic') and Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";
            query2 = query2 + " Group by Threshold";

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
        }   // end function

        private void getBranchThreshold()
        {
            string query2 = "";

            query2 = "Select Threshold, Count(region) Total from CL_CALCULATED_DATA";
            query2 = query2 + " Where Segment in ('Retail','Islamic') and Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";
            query2 = query2 + " Group by Threshold";

            dbclass.OpenConection();
            SqlDataReader dr2 = dbclass.DataReader(query2);

            ChartLabel2 = "[";
            ChartData2 = "[";

            while (dr2.Read())
            {
                //lstLabel.Add(sdr["CaseStatus"].ToString());
                //lstdata.Add(sdr["Total"].ToString());

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

            query2 = "Select Threshold, Count(region) Total from CL_CALCULATED_DATA";
            query2 = query2 + " Where Segment in ('Commercial') and Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";
            query2 = query2 + " Group by Threshold";

            dbclass.OpenConection();
            SqlDataReader dr2 = dbclass.DataReader(query2);

            ChartLabel3 = "[";
            ChartData3 = "[";

            while (dr2.Read())
            {
                //lstLabel.Add(sdr["CaseStatus"].ToString());
                //lstdata.Add(sdr["Total"].ToString());

                ChartLabel3 = ChartLabel3 + "'" + dr2["Threshold"].ToString() + "',";
                ChartData3 = ChartData3 + dr2["Total"].ToString() + ",";


            }
            ChartLabel3 = ChartLabel3.Substring(0, ChartLabel3.Length - 1);
            ChartLabel3 = ChartLabel3 + "]";
            ChartData3 = ChartData3.Substring(0, ChartData3.Length - 1);
            ChartData3 = ChartData3 + "]";

            dbclass.CloseConnection();
        } // end function comm

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

            if (DD_Segment.Text == "Retail")
            {
                query = query + " Where Segment in ('Retail','Islamic') ";
            }
            if (DD_Segment.Text == "Corporate")
            {
                query = query + " Where Segment in ('Corporate') ";
            }
            if (DD_Segment.Text == "Commercial")
            {
                query = query + " Where Segment in ('Commercial') ";
            }

            query = query + " AND Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";
            query = query + " and DataLevel = 'Region'";
            query = query + " Group by Region, Threshold";


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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            getSegmentScore();
            getRegionThreshold();
            getBranchThreshold();
            getCommBranchThreshold();
            getRegionScore();
            fillgrid();
        }
    }
}