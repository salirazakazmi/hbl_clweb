using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data;


namespace WebDataEntry
{
    public partial class CL_100Depositor : System.Web.UI.Page
    {
        DBClass dbclass = new DBClass();
            protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
                Populate_DropDown();
            }

        }

        private void Populate_DropDown()
        {
            string query = "select Distinct Year(date) from Top100Depositor";

            dbclass.OpenConection();
            
            dbclass.DataReader(query);
            DataSet ds = dbclass.DataSet(query);
            
            DD_Year.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_Year.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_Year.DataSource = ds.Tables[0];
            DD_Year.DataBind();
            dbclass.CloseConnection();


            query = "select Distinct Month(date) from Top100Depositor";

            dbclass.OpenConection();

            dbclass.DataReader(query);
            ds = dbclass.DataSet(query);

            DD_Month.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_Month.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_Month.DataSource = ds.Tables[0];
            DD_Month.DataBind();
            dbclass.CloseConnection();


            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    GridView1.DataSource = ds;
            //    GridView1.DataBind();
            //}

        }

        private void BindGridView()
        {
            dbclass.OpenConection();
            string query = "Select * from Top100Depositor";
            dbclass.DataReader(query);
            DataSet ds = dbclass.DataSet(query);

            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
           

        }   // end of binggridview

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindGridView();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindGridView();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //int userid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            string _id = GridView1.DataKeys[e.RowIndex].Values["ID"].ToString();
            //string userid = GridView1.DataKeys[e.RowIndex].Value.ToString();
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            //TextBox txt1 = (TextBox)row.FindControl("DDTarget");
            DropDownList txt1 = GridView1.Rows[e.RowIndex].FindControl("DDTarget") as DropDownList;
            string txt = txt1.SelectedItem.ToString();
            GridView1.EditIndex = -1;
            dbclass.OpenConection();
            string query = "update Top100Depositor set Target='" + txt1.Text + "' where id='" + _id + "'";
            dbclass.ExecuteQueries(query);
            dbclass.CloseConnection();
            BindGridView();
        }
    }
}