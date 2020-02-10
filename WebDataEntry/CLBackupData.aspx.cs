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
    public partial class CLBackupData : System.Web.UI.Page
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
     
        protected void btnBackup_Click(object sender, EventArgs e)
        {
            Boolean noissue = true;
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
                backup_CL();
            }

        } // End of Click Backup


        private void backup_CL()
        {
            // Check spefic year and month CL is exists in CL_CALCULATED_DATA
            // if exists then check what CL Version will be assign
            // Then insert the record in CL_Version and get Version
            // Then Insert all the records in CL_CALCULATED_DATA_HST Table with 
            // Both C_CALCULATED_DATA and CL_CALCULATED_DATA_AREA will be backup

            dbclass.OpenConection();
            string query;
            SqlDataReader dr2;
            Int16 intmon;
            string Month;

            // Pad Leading Zeros in Month
            intmon =  Convert.ToInt16( txtMonth.Text.ToString());

            Month = intmon.ToString("00");
            query = "Select count(*) as TotalRec from CL_CALCULATED_DATA Where Year = '" + txtYear.Text + "' and Month = '" + Month + "'";
            dr2 = dbclass.DataReader(query);
            if (dr2.HasRows)
            {
                Int16 totalRec = 0;
                while (dr2.Read())
                {
                    // read data for each record here
                    string val = dr2[0].ToString();
                    totalRec = Convert.ToInt16(val);
                }

                if (totalRec < 1)
                {
                    lblmsg.Text = "Provided Year / Month data is never generated for CL, can't backup";
                    return;
                }
            }

            query = "Select isnull(Max(Version),0) as VersionNum from CL_Version Where Year='" + txtYear.Text + "' And Month ='" + Month + "'";
            int nextversion=0;

            dr2 = dbclass.DataReader(query);
            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    // read data for each record here
                    string val = dr2[0].ToString();
                    nextversion = Convert.ToInt16(val);
                }

                nextversion++;

            }
            else
            {

                nextversion = 1;
            }

            // Now Start Backup         
            query = "Insert into CL_VERSION (Year, Month, Version, Remarks, VersionDTTM, UserID) Values (";
            query = query + "'" + txtYear.Text + "',";
            query = query + "'" + Month + "',";
            query = query + "" + nextversion + ",";
            query = query + "'" + txtRemarks.Text + "',";
            query = query + "GetDate(),";
            query = query + "'" + userID + "')";

            dbclass.ExecuteQueries(query);

            query = "Insert into CL_CALCULATED_DATA_HST";
            query = query + " Select *,'" + nextversion + "',GETDATE() from CL_CALCULATED_DATA Where Year='" + txtYear.Text + "' And Month ='" + Month + "'";
            dbclass.ExecuteQueries(query);

            query = "Insert into CL_CALCULATED_DATA_AREA_HST";
            query = query + " Select *,'" + nextversion + "',GETDATE() from CL_CALCULATED_DATA_AREA Where Year='" + txtYear.Text + "' And Month ='" + Month + "'";
            dbclass.ExecuteQueries(query);

            query = "Insert into CL_CALCULATION_PARAMETER_HST";
            query = query + " Select *,'" + nextversion + "',GETDATE(),'" + txtYear.Text + "','" + Month  + "' from CL_CALCULATION_PARAMETER ";
            dbclass.ExecuteQueries(query);



            dbclass.CloseConnection();
            lblmsg.Text = "CL Data Successfully Backup.... ";
        }

    }
}