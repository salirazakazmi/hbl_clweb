using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Collections;


/* 

http://www.sharepointblue.com/2015/01/using-chartsjs-with-aspnet.html
https://codepen.io/jordanwillis/pen/bqaGRR

*/


namespace WebDataEntry
{
    public partial class TestDashboard : System.Web.UI.Page
    {
        DBClass dbclass = new DBClass();
        string userID;
        string pcondition = "";
        public string ChartLabels = null;
        public string ChartData1 = null;
        public string ChartData2 = null;

        public List<String> lstLabel = new List<String>();
        public List<String> lstdata = new List<String>();
        //ArrayList lstLabel = new ArrayList();
        //ArrayList lstdata = new ArrayList();
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["UserID"] == null)
            //{
            //    Response.Redirect("Login.aspx");
            //}
            //else
            //{
            //    userID = Session["UserID"].ToString();
            //}

            //if (Session["condition"] == null)
            //{
            //    // Nothing to to
            //}
            //else
            //{
            //    pcondition = Session["condition"].ToString();
            //}

            BCUWebService webser = new BCUWebService();
            webser.HelloWorld();
            //webser.showJson();

            if (!IsPostBack)
            {

                //showdata();
                //Response.Write(getJSON());

            }

        } // end page_load

        private void showdata()
        {

            DataTable tb = new DataTable();
            string query = "Select CaseStatus, Count(*) as Total from PEP_CASE Group by CaseStatus ";


            dbclass.OpenConection();
            SqlDataReader dr = dbclass.DataReader(query);

            tb.Load(dr, LoadOption.OverwriteChanges);


            String chart = "";
            chart = "<canvas id=\"line-chart\" width=\"100%\" height=\"40\"></canvas>";
            chart += "<script>";
            chart += "new Chart(document.getElementById(\"line-chart\"), { type: 'line', data: {labels: [";

            // more detais
            for (int i = 0; i < 50; i++)
                chart += i.ToString() + ",";
            chart = chart.Substring(0, chart.Length - 1);

            chart += "],datasets: [{ data: [";

            // get data from database and add to chart
            String value = "";
            for (int i = 0; i < tb.Rows.Count; i++)
                value += tb.Rows[i]["CaseStatus"].ToString() + ",";
            value = value.Substring(0, value.Length - 1);
            chart += value;

            chart += "],label: \"Air Temperature\",borderColor: \"#3e95cd\",fill: true}"; // Chart color
            chart += "]},options: { title: { display: true,text: 'Air Temperature (oC)'} }"; // Chart title
            chart += "});";
            chart += "</script>";

            //ltChart.Text = chart;
        }


        private string getJSON()
        {


            string query = "Select CaseStatus, Count(*) as Total from PEP_CASE Group by CaseStatus ";


            dbclass.OpenConection();
            SqlDataReader sdr = dbclass.DataReader(query);

            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            while (sdr.Read())
            {
                sb.Append("{");
                System.Threading.Thread.Sleep(50);
                string color = String.Format("#{0:X6}", new Random().Next(0x1000000));
                sb.Append(string.Format("text :'{0}', value:{1}, color: '{2}'", sdr[0], sdr[1], color));
                sb.Append("},");
            }
            sb = sb.Remove(sb.Length - 1, 1);
            sb.Append("]");

            dbclass.CloseConnection();

            return sb.ToString();



        }   // end getJSON

        protected void ShowGraph_Click(object sender, EventArgs e)
        {


            //this.ChartLabels = "['Approved', 'New']";
            //this.ChartData1 = "[4, 10]";
            //this.ChartData2 = "[28, 48, 40, 19, 86, 27, 90]";

            //Call the Javascript function from C#
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DrawChart()", true);

            string query = "Select CaseStatus, Count(*) as Total from PEP_CASE Group by CaseStatus ";
            dbclass.OpenConection();
            SqlDataReader sdr = dbclass.DataReader(query);

            //List<String> lstLabel = new List<String>();
            //List<Int16> lstdata = new List<Int16>();


            //this.ChartLabels = "['Approved', 'New']";
            //this.ChartData1 = "[4, 10]";

            ChartLabels = "[";
            ChartData1 = "[";

            while (sdr.Read())
            {
                //lstLabel.Add(sdr["CaseStatus"].ToString());
                //lstdata.Add(sdr["Total"].ToString());

                ChartLabels = ChartLabels + "'" + sdr["CaseStatus"].ToString() + "',";
                ChartData1 = ChartData1 + sdr["Total"].ToString() + ",";

                
            }
            ChartLabels = ChartLabels.Substring(0, ChartLabels.Length - 1);
            ChartLabels = ChartLabels + "]";
            ChartData1 = ChartData1.Substring(0, ChartData1.Length - 1);
            ChartData1 = ChartData1 + "]";


        }
           
        }
}