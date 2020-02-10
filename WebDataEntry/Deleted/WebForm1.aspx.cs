using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
/* ---- Add-in */
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace WebDataEntry
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        DBClass dbclass = new DBClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        private void BindGridView()
        {
            dbclass.OpenConection();
            string query = "Select * from vendors";
            dbclass.DataReader(query);
            DataSet ds = dbclass.DataSet(query);
         
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            //else
            //{
            //    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            //    GridView1.DataSource = ds;
            //    GridView1.DataBind();
            //    int columncount = GridView1.Rows[0].Cells.Count;
            //    GridView1.Rows[0].Cells.Clear();
            //    GridView1.Rows[0].Cells.Add(new TableCell());
            //    GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
            //    GridView1.Rows[0].Cells[0].Text = "No Records Found";
            //}

        }   // end of binggridview

      
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindGridView();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //int userid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            string _id = GridView1.DataKeys[e.RowIndex].Values["vendorid"].ToString();
            //string userid = GridView1.DataKeys[e.RowIndex].Value.ToString();
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            Label lblID = (Label)row.FindControl("lblidedit");
            TextBox txt1 = (TextBox)row.FindControl("TextBox1");
            TextBox txt2 = (TextBox)row.FindControl("TextBox2");
           
            GridView1.EditIndex = -1;
            dbclass.OpenConection();
            string query = "update vendors set vendor='" + txt1.Text + "',address='" + txt2.Text + "'where vendorid='" + _id + "'";
            dbclass.ExecuteQueries(query);
            dbclass.CloseConnection();
            BindGridView();
            
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView();
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindGridView();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string _id = GridView1.DataKeys[e.RowIndex].Values["vendorid"].ToString();
            Response.Write(_id);
            dbclass.OpenConection();
            string query = "delete from vendors where vendorid='" + _id + "'";
            dbclass.ExecuteQueries(query);
            dbclass.CloseConnection();

            BindGridView();
             
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            TextBox _id = GridView1.FooterRow.FindControl("TextBoxN1") as TextBox;
            TextBox txt1 = GridView1.FooterRow.FindControl("TextBoxN2") as TextBox;
            TextBox txt2 = GridView1.FooterRow.FindControl("TextBoxN3") as TextBox;
            
            dbclass.OpenConection();
            string query = "Insert Into vendors(vendorid, vendor, address) values('" + _id.Text + "','" + txt1.Text + "','" + txt2.Text + "' )";
            dbclass.ExecuteQueries(query);
            dbclass.CloseConnection();
            BindGridView();
        }
    }
}