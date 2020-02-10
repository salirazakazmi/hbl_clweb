using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace WebDataEntry.Deleted
{
    public partial class multiselect : System.Web.UI.Page
    {
        DBClass dbclass = new DBClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dropdownfill();
            }
        }

        protected void Submit(object sender, EventArgs e)
        {
            string message = "";
            foreach (ListItem item in lstHead.Items)
            {
                if (item.Selected)
                {
                    message += item.Text + " " + item.Value + "\\n";
                }
            }
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('" + message + "');", true);
        }


        protected void lstHead_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dropdownfill()
        {
            string query = "select Distinct Year  from CL_CALCULATED_DATA";

            dbclass.OpenConection();


            DataSet ds = dbclass.DataSet(query);

            lstHead.DataTextField = ds.Tables[0].Columns[0].ToString();
            lstHead.DataValueField = ds.Tables[0].Columns[0].ToString();
            lstHead.DataSource = ds.Tables[0];
            lstHead.DataBind();
            //DD_Year.SelectedIndex = DD_Year.Items.Count - 1;



            
        }

    }
}