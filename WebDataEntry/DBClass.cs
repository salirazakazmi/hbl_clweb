using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/* ---- Add-in */
using System.Data;
using System.Data.SqlClient;
using System.Configuration;



namespace WebDataEntry
{
    public class DBClass
    {

        string ConnectionString = "";
        SqlConnection con;

        public void OpenConection()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DBC"].ConnectionString.ToString();
            con = new SqlConnection(ConnectionString);
            con.Open();
        }


        public void CloseConnection()
        {
            con.Close();
        }


        public void ExecuteQueries(string Query_)
        {
            SqlCommand cmd = new SqlCommand(Query_, con);
            cmd.CommandTimeout = 1000;
            cmd.ExecuteNonQuery();
        }


        public SqlDataReader DataReader(string Query_)
        {
            SqlCommand cmd = new SqlCommand(Query_, con);
            cmd.CommandTimeout = 0;
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }


        public object ShowDataInGridView(string Query_)
        {
            SqlDataAdapter dr = new SqlDataAdapter(Query_, ConnectionString);
            DataSet ds = new DataSet();
            dr.Fill(ds);
            object dataum = ds.Tables[0];
            return dataum;
        }

        public DataSet DataSet(string Query_)
        {
            SqlDataAdapter dr = new SqlDataAdapter(Query_, ConnectionString);
            DataSet ds = new DataSet();
            dr.Fill(ds);
            return ds;
        }

        public void BeginTran()
        {
            SqlTransaction transaction = con.BeginTransaction();
        }
        public DataTable DataTable(string Query_)
        {
            SqlDataAdapter dr = new SqlDataAdapter(Query_, ConnectionString);
            DataTable dt = new DataTable();
            dr.Fill(dt);
            return dt;
        }

        public void IP_LOG (String IP, String PageName)
        {
            String query = "";
            query = "Insert into IP_LOG (IP,Date,PageName) Values ('" + IP + "',GetDate(),'" + PageName + "')";
            OpenConection();
            ExecuteQueries(query);
            CloseConnection(); 
            
        }

    }
}