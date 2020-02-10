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
    public partial class UserManagement : System.Web.UI.Page
    {
        DBClass dbclass = new DBClass();
        String lblUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (GridView1.HeaderRow != null)
            {
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                lblUser = Session["UserID"].ToString();
            }

            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        private void BindGridView()
        {

            dbclass.OpenConection();
            string query = "Select * from UserManagement A Left Join MenuOption M On A.MenuOption=M.MenuOption ";
            query = query + " Where Active='Y' and UserID='" + lblUser + "' Order by SortKey";
            //query = query + GetFilters();
            dbclass.DataReader(query);
            DataSet ds = dbclass.DataSet(query);

            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }


        }   // end of binggridview

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            // This link button get the value of role, and base on ID value
            // Pick value from DB, send this role to next entry form to control admin / supervisory
            // also get condition to view data restriction.

            GridViewRow grdRow = (GridViewRow)((LinkButton)sender).NamingContainer;
            Label role = grdRow.FindControl("lblRole") as Label;
            //string _id = GridView1.DataKeys[row.RowIndex].Values["ID"].ToString();
            Label id = grdRow.FindControl("lblID") as Label;
            Session["Role"] = role.Text;

            string query = "Select * from UserManagement A ";
            query = query + " Where ID='" + id.Text + "'";

            dbclass.OpenConection();

            SqlDataReader dr = dbclass.DataReader(query);

            if (dr.Read())
            {
                // do action
                string condition = dr["condition"].ToString();
                Session["condition"] = condition;
                string url = dr["RedirectPage"].ToString();
                Response.Redirect(url);
            }



        }

    }
}