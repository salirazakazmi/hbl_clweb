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
    public partial class HighchartDemo : System.Web.UI.Page
    {
        //public string category = "['Karachi', 'Lahore', 'Islambad']";
        //public string dd1 = "[200, 173, 500]";
        //public string dd2 = "[324, 124, 547, 221]";
        DBClass dbclass = new DBClass();

        // Public Variables for Dashboard Score
        public string ChartLabels = null;
        public string ChartData1 = null;
        public string ChartLabel2 = null;
        public string ChartData2 = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dropdownfill();
                getSegmentScore();
                getRegionThreshold();
                getBranchThreshold();

            }
        }


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
            dbclass.CloseConnection();
        }
        private void getSegmentScore()
        {
            Double score = 0;
            string query2 = "";
            query2 = "Select avg(TotalScore) Total from CL_CALCULATED_DATA";
            query2 = query2 + " Where Segment in ('Retail','Islamic') and Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";

            dbclass.OpenConection();
            SqlDataReader dr2 = dbclass.DataReader(query2);

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    score = dr2.GetDouble(0);
                    pbardist.Style["width"] = String.Format("{0}%", (int)score);
                    lblh5dist.Text = String.Format("{0}%", (int)score);
                }
            }


            query2 = "Select avg(TotalScore) Total from CL_CALCULATED_DATA";
            query2 = query2 + " Where Segment in ('Commercial') and Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";

            
            dr2 = dbclass.DataReader(query2);

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    score = dr2.GetDouble(0);
                    pbarcom.Style["width"] = String.Format("{0}%", (int)score);
                    lblh5com.Text = String.Format("{0}%", (int)score);
                }
            }

            query2 = "Select avg(TotalScore) Total from CL_CALCULATED_DATA";
            query2 = query2 + " Where Segment in ('Corporate') and Year = '" + DD_Year.Text + "' and Month = '" + DD_Month.Text + "'";


            dr2 = dbclass.DataReader(query2);

            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    score = dr2.GetDouble(0);
                    pbarcor.Style["width"] = String.Format("{0}%", (int)score);
                    lblh5cor.Text = String.Format("{0}%", (int)score);

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
        }
    }
}