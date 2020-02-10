using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Web.Script.Services;

namespace WebDataEntry
{
    /// <summary>
    /// Summary description for BCUWebService
    /// </summary>
    [WebService(Namespace = "http://hblbcu.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]

    
    public class BCUWebService : System.Web.Services.WebService
    {
        DBClass dbclass = new DBClass();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]//Specify return format.
        public string getPEPStatusCount()
        {
            
            string query = "Select CaseStatus, Count(*) as Total from PEP_CASE Group by CaseStatus ";


            dbclass.OpenConection();
            DataSet ds = dbclass.DataSet(query);

            return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
            


        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void showJson()
        {
            Context.Response.Write(getPEPStatusCount());

        }


        [WebMethod]
        public DataSet getXMLPEPStatusCount()
        {

            string query = "Select CaseStatus, Count(*) as Total from PEP_CASE Group by CaseStatus ";


            dbclass.OpenConection();
            DataSet ds = dbclass.DataSet(query);

            return ds;


        }
    }
}
