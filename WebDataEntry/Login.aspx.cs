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
   
    public partial class Login: System.Web.UI.Page
    {
        DBClass dbclass = new DBClass();
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnsave_Click(object sender, EventArgs e)
        {
            lblerr.Text = "";
            if (txtname.Text == "")
            {
                lblerr.Text = "Please enter Login ID";
            }

            if (lblerr.Text == "")
            {
                string query = "select * from Users where Active='Y' AND UserID='" + txtname.Text + "' AND Password='" + txtPass.Text + "'";

                dbclass.OpenConection();

                SqlDataReader dr = dbclass.DataReader(query);
                
                if (dr.Read())
                {
                    // do action
                    Session["UserID"] = txtname.Text;
                    Response.Redirect("UserManagement.aspx");
                    lblerr.Text = "Login succcesfully";
                }
                else
                {

                    lblerr.Text = "Invalid User ID and Password";

                }
                dbclass.CloseConnection();
            }
        }

        protected void btnreset_Click(object sender, EventArgs e)
        {

            lblerr.Text = "";
            txtname.Text = "";
            txtPass.Text = "";
            
        }
        
    }
}