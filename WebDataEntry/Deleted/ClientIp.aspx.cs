using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebDataEntry.Deleted
{
    public partial class ClientIp : System.Web.UI.Page
    {
        DBClass dbclass = new DBClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            //string IP = Request.UserHostAddress;

            dbclass.IP_LOG(Request.UserHostAddress, "ClientIP.aspx");
            Response.Write("Thanks");
        }
    }
}