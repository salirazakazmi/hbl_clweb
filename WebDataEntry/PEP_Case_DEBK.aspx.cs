using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Configuration;


namespace WebDataEntry
{
    public partial class PEP_Case_DEBK : System.Web.UI.Page
    {
        DBClass dbclass = new DBClass();
        String lblUser;
        protected void Page_Load(object sender, EventArgs e)
        {
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
                //btnCancel.Enabled = false;
                //btnSearch.Enabled = true;
                //btnSave.Enabled = false;
                //btnNew.Enabled = true;
                init_form();
                disable_input(false);
                workflow_button();
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            //btnCancel.Enabled = true;
            btnSearch.Enabled = false;
            btnSave.Enabled = true;
            btnNew.Enabled = false;

            init_form();
            disable_input(true);

            txtID.Text = "New";
            txtID.Enabled = false;
            txtCustID.Text = "";
            txtCustID.Focus();
            txtStatus.Text = "New";

            workflow_button();
        }

        private void init_form()
        {

            txtID.Text = "";
            txtID.Enabled = true;
            txtCustID.Text = "";
            txtname.Text = "";
            txtAML.Text = "";
            txtApproveBy.Text = "";
            txtApprovedDate.Text = "";
            txtCaseRaiseBy.Text = "";
            txtCaseRaiseDate.Text = "";

            txtEditBy.Text = "";
            txtEditDate.Text = "";
            txtForwardby.Text = "";
            txtForwardDate.Text = "";

            txtConnectedPEP.Text = "";
            txtCustomerType.Text = "";
            TxtEDD.Text = "";
            txtHR.Text = "";
            txtPosition.Text = "";
            txtProduct.Text = "";
            txtReason.Text = "";
            TxtRegion.Text = "";
            txtRemarks.Text = "";
            txtStatus.Text = "";
            DDActive.SelectedIndex = -1;
            DDBCUHead.SelectedIndex = -1;
            DDBusinessRecom.SelectedIndex = -1;
            DDHR.SelectedIndex = -1;
            lblErr.Text = "";

            btnSave.Visible = false;
            btnForward.Visible = false;
            btnApprovalReq.Visible = false;

        }

        private void workflow_button()
        {
            if (txtStatus.Text == "")
            {
                btnSave.Visible = true;
                btnSave.Enabled = true;
                btnApprovalReq.Visible = false;
                btnForward.Visible = false;
                btnReOpen.Visible = false;

            }

            if (txtStatus.Text == "New")
            {
                btnSave.Visible = true;
                btnSave.Enabled = true;
                btnApprovalReq.Visible = false;
                btnForward.Visible = true;
                btnReOpen.Visible = false;
                disable_input(true);
            }

            if (txtStatus.Text == "InProcess")
            {
                btnSave.Visible = false;
                btnSave.Enabled = false;
                btnSave.Enabled = false;
                btnApprovalReq.Visible = true;
                btnForward.Visible = false;
                btnReOpen.Visible = true;
                disable_input(false);
                txtRemarks.Enabled = true;
            }


        }
        private void disable_input(Boolean bl)
        {

            //txtID.Text = "";
            //txtCustID.Text = "";
            txtAML.Enabled = bl;
            txtApproveBy.Enabled = false;
            txtApprovedDate.Enabled = false;
            txtCaseRaiseBy.Enabled = false;
            txtCaseRaiseDate.Enabled = false;
            txtEditDate.Enabled = false;
            txtEditBy.Enabled = false;
            txtForwardDate.Enabled = false;
            txtForwardby.Enabled = false;

            txtConnectedPEP.Enabled = bl;
            txtCustomerType.Enabled = bl;
            TxtEDD.Enabled = bl;
            txtHR.Enabled = bl;
            txtname.Enabled = bl;
            txtPosition.Enabled = bl;
            txtProduct.Enabled = bl;
            txtReason.Enabled = bl;
            TxtRegion.Enabled = bl;
            txtRemarks.Enabled = bl;
            txtStatus.Enabled = false;
            DDActive.Enabled = bl;
            DDBCUHead.Enabled = bl;
            DDBusinessRecom.Enabled = bl;
            DDHR.Enabled = bl;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string condition = "";
                int more = 0;
                if (txtID.Text != "")
                {
                    condition = " ID = " + txtID.Text;
                    more = 1;
                }

                if (txtCustID.Text != "")
                {
                    if (more == 1)
                    {
                        condition = condition + " AND CUSTOMERID = '" + txtCustID.Text + "'";
                    }
                    else
                    {
                        condition = " CUSTOMERID = '" + txtCustID.Text + "'";
                    }
                    more = 1;
                }

                string query = "Select * from PEP_CASE ";
                query = query + " Where " + condition;

                dbclass.OpenConection();
                SqlDataReader dr = dbclass.DataReader(query);

                while (dr.Read())
                {
                    txtID.Text = dr[0].ToString();
                    txtCustID.Text = dr[1].ToString();
                    txtname.Text = dr[2].ToString();
                    txtProduct.Text = dr[3].ToString();
                    txtCustomerType.Text = dr[4].ToString();
                    TxtRegion.Text = dr[5].ToString();

                    TxtEDD.Text = dr[6].ToString();
                    txtHR.Text = dr[7].ToString();
                    txtAML.Text = dr[8].ToString();
                    txtReason.Text = dr[9].ToString();
                    txtPosition.Text = dr[10].ToString();
                    txtConnectedPEP.Text = dr[11].ToString();

                    if (!dr.IsDBNull(12))
                    {
                        DDBusinessRecom.Text = dr[12].ToString();
                    }

                    if (!dr.IsDBNull(13))
                    {
                        DDBCUHead.Text = dr[13].ToString();
                    }
                    if (!dr.IsDBNull(14))
                    {
                        DDHR.Text = dr[14].ToString();
                    }
                    if (!dr.IsDBNull(15))
                    {
                        DDActive.Text = dr[15].ToString();
                    }
                    txtCaseRaiseDate.Text = dr[16].ToString();
                    txtCaseRaiseBy.Text = dr[17].ToString();
                    txtStatus.Text = dr[18].ToString();
                    txtRemarks.Text = dr[19].ToString();
                    txtEditDate.Text = dr[20].ToString();
                    txtEditBy.Text = dr[21].ToString();
                    txtForwardDate.Text = dr[22].ToString();
                    txtForwardby.Text = dr[23].ToString();
                    txtApprovedDate.Text = dr[24].ToString();
                    txtApproveBy.Text = dr[25].ToString();

                }


                dbclass.CloseConnection();
            }
            catch
            {
                int a = 0;
            }
            workflow_button();

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            btnCancel.Enabled = true;
            btnSearch.Enabled = true;
            btnSave.Enabled = false;
            btnNew.Enabled = true;
            init_form();
            disable_input(false);
            //sendEmail();
        }


        

        protected void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                int noerror = 0;
                string query = "";

                if (txtCustID.Text == "")
                {
                    lblErr.Text = "Please must enter Customer ID";
                    noerror = 1;
                }
                if (txtname.Text == "")
                {
                    lblErr.Text = "Please must enter Customer Full Name";
                    noerror = 1;
                }
                if (txtProduct.Text == "")
                {
                    lblErr.Text = "Please must enter Product";
                    noerror = 1;
                }
                if (txtCustomerType.Text == "")
                {
                    lblErr.Text = "Please must enter Customer Type";
                    noerror = 1;
                }
                if (txtReason.Text == "")
                {
                    lblErr.Text = "Please must enter Reason for PEP";
                    noerror = 1;
                }
                if (txtPosition.Text == "")
                {
                    lblErr.Text = "Please must enter Position";
                    noerror = 1;
                }



                if (noerror == 0)
                {
                    Boolean newrec = false;
                    query = "Select * from PEP_CAse Where ID='" + txtID.Text + "'";
                    dbclass.OpenConection();
                    SqlDataReader dr = dbclass.DataReader(query);

                    if (dr.HasRows)
                    {
                        newrec = false;
                    }
                    else
                    {
                        newrec = true;
                    }


                    if (newrec == true)
                    {
                        query = " Insert Into PEP_CASE ([CustomerID], [CustomerName] ,[ProductApplied] ,[CustomerType] ";
                        query = query + ",[RegionName],[EDD],[HighRiskCategory],[AMLapproval],[ReasonforPEP],[Position] ,[ConnectedPEP] ";
                        query = query + ",[BusinessRecommendation],[BCUHeadApproval],[VeryHighProfileCustomer],[Active],[CaseRaiseDate]";
                        query = query + ",[CaseRaiseBy] ,[CaseStatus],[Remarks]";
                        query = query + " ) Values (";

                        query = query + "'" + txtCustID.Text + "',";
                        query = query + "'" + txtname.Text + "',";
                        query = query + "'" + txtProduct.Text + "',";
                        query = query + "'" + txtCustomerType.Text + "',";
                        query = query + "'" + TxtRegion.Text + "',";
                        query = query + "'" + TxtEDD.Text + "',";
                        query = query + "'" + txtHR.Text + "',";
                        query = query + "'" + txtAML.Text + "',";
                        query = query + "'" + txtReason.Text + "',";
                        query = query + "'" + txtPosition.Text + "',";
                        query = query + "'" + txtConnectedPEP.Text + "',";
                        query = query + "'" + DDBusinessRecom.Text + "',";
                        query = query + "'" + DDBCUHead.Text + "',";
                        query = query + "'" + DDHR.Text + "',";
                        query = query + "'" + DDActive.Text + "',";
                        query = query + "GetDate(),";
                        query = query + "'" + lblUser + "',";
                        query = query + "'New',";
                        query = query + "'" + txtRemarks.Text + "')";

                        dbclass.OpenConection();
                        dbclass.ExecuteQueries(query);

                        // display last number to track
                        query = "";
                        query = "Select Max(ID) from PEP_CASE";
                        SqlDataReader dr2 = dbclass.DataReader(query);


                        while (dr2.Read())
                        {
                            lblErr.Text = "Recrod Insert Successfully PEP Case ID = '" + dr2[0];
                        }

                        dbclass.CloseConnection();
                    }

                    if (newrec == false)
                    {
                        query = " Update PEP_CASE ";
                        query = query + "Set CustomerID = '" + txtCustID.Text + "',";
                        query = query + "CustomerName = '" + txtname.Text + "',";
                        query = query + "ProductApplied= '" + txtProduct.Text + "',";
                        query = query + "CustomerType ='" + txtCustomerType.Text + "',";
                        query = query + "RegionName='" + TxtRegion.Text + "',";
                        query = query + "EDD ='" + TxtEDD.Text + "',";
                        query = query + "HighRiskCategory='" + txtHR.Text + "',";
                        query = query + "AMLapproval='" + txtAML.Text + "',";
                        query = query + "ReasonforPEP='" + txtReason.Text + "',";
                        query = query + "Position='" + txtPosition.Text + "',";
                        query = query + "ConnectedPEP='" + txtConnectedPEP.Text + "',";
                        query = query + "BusinessRecommendation='" + DDBusinessRecom.Text + "',";
                        query = query + "BCUHeadApproval='" + DDBCUHead.Text + "',";
                        query = query + "VeryHighProfileCustomer='" + DDHR.Text + "',";
                        query = query + "Active='" + DDActive.Text + "',";
                        query = query + "CaseEditDate=  GetDate() ,";
                        query = query + "CaseEditby='" + lblUser + "',";
                        query = query + "CaseStatus='" + txtStatus.Text + "',";
                        query = query + "Remarks='" + txtRemarks.Text + "'";

                        query = query + "Where ID='" + txtID.Text + "'";

                        dbclass.OpenConection();
                        dbclass.ExecuteQueries(query);
                        dbclass.CloseConnection();
                    }


                } // if no error

            }
            catch
            {
                int a = 0;
            }

            init_form();
        }


        protected void btnForward_Click(object sender, EventArgs e)
        {

            try
            {
                int noerror = 0;
                string query = "";

                if (txtCustID.Text == "")
                {
                    lblErr.Text = "Please must enter Customer ID";
                    noerror = 1;
                }
                if (txtname.Text == "")
                {
                    lblErr.Text = "Please must enter Customer Full Name";
                    noerror = 1;
                }
                if (txtProduct.Text == "")
                {
                    lblErr.Text = "Please must enter Product";
                    noerror = 1;
                }
                if (txtCustomerType.Text == "")
                {
                    lblErr.Text = "Please must enter Customer Type";
                    noerror = 1;
                }
                if (txtReason.Text == "")
                {
                    lblErr.Text = "Please must enter Reason for PEP";
                    noerror = 1;
                }
                if (txtPosition.Text == "")
                {
                    lblErr.Text = "Please must enter Position";
                    noerror = 1;
                }

                if (DDActive.Text == "No")
                {
                    lblErr.Text = "This Case in In-Active so it can't Forward";
                    noerror = 1;
                }

                if (noerror == 0)
                {

                    txtStatus.Text = "InProcess";

                    query = " Update PEP_CASE ";
                    query = query + "Set CustomerID = '" + txtCustID.Text + "',";
                    query = query + "CustomerName = '" + txtname.Text + "',";
                    query = query + "ProductApplied= '" + txtProduct.Text + "',";
                    query = query + "CustomerType ='" + txtCustomerType.Text + "',";
                    query = query + "RegionName='" + TxtRegion.Text + "',";
                    query = query + "EDD ='" + TxtEDD.Text + "',";
                    query = query + "HighRiskCategory='" + txtHR.Text + "',";
                    query = query + "AMLapproval='" + txtAML.Text + "',";
                    query = query + "ReasonforPEP='" + txtReason.Text + "',";
                    query = query + "Position='" + txtPosition.Text + "',";
                    query = query + "ConnectedPEP='" + txtConnectedPEP.Text + "',";
                    query = query + "BusinessRecommendation='" + DDBusinessRecom.Text + "',";
                    query = query + "BCUHeadApproval='" + DDBCUHead.Text + "',";
                    query = query + "VeryHighProfileCustomer='" + DDHR.Text + "',";
                    query = query + "Active='" + DDActive.Text + "',";
                    query = query + "CaseForwardDate=  GetDate() ,";
                    query = query + "CaseForwardby='" + lblUser + "',";
                    query = query + "CaseStatus='" + txtStatus.Text + "',";
                    query = query + "Remarks='" + txtRemarks.Text + "'";

                    query = query + "Where ID='" + txtID.Text + "'";

                    dbclass.OpenConection();
                    dbclass.ExecuteQueries(query);
                    dbclass.CloseConnection();

                    //sendEmail("New","InProcess", txtID.Text );

                } // if no error

            }
            catch
            {
                int a = 0;
            }

            init_form();

        } // end of btnForward

        protected void btnApprovalReq_Click(object sender, EventArgs e)
        {

        }


        
        private void sendEmail(String OldStatus, String NewStatus,string PEPID)
        {

            string strFrom, strTo, strSubject, strBody;

            strTo = "";
            strSubject = "";
            strBody = "";
            string query = "Select ID, EmailTo from workflow_stage ";
            query = query + " Where OptionName='PEP_Case'  And Active='Y' and CurrentStatus='" + OldStatus + "' and NewStatus='" + NewStatus + "'";

            dbclass.OpenConection();
            SqlDataReader dr = dbclass.DataReader(query);

            while (dr.Read())
            {
                txtID.Text = dr["ID"].ToString();
                strTo = dr["EmailTo"].ToString();
                
            }

            query = "";
            query = "Select * from PEP_Case Where ID = '" + PEPID + "'";

            dr = dbclass.DataReader(query);

            while (dr.Read())
            {
                if (dr["CaseStatus"].ToString() == "InProcess")
                {
                    strSubject = "PEP Case # - " + PEPID + " - " + dr["CustomerID"] + " - " + dr["CustomerName"] + " - ss inProcess";

                }

                strBody = "Please arrange Head Branch Banking approval for ";
                strBody = strBody + dr["CustomerName"] + "He/she is categorized as PEP by virtue of his/her current position";
                strBody = strBody + "i.e. " + dr["ReasonforPEP"] +". Case will be forwarded to AMLD after Head BB approval. ";
                strBody = strBody + "<br><br>";

                strBody = strBody + "Customer Name: " + dr["CustomerName"] + "<br>";
                strBody = strBody + "Product Applied: " + dr["ProductApplied"] + "<br>";
                strBody = strBody + "Customer: " + dr["CustomerType"] + "<br>";
                strBody = strBody + "Region Name: " + dr["RegionName"] + "<br>";
                strBody = strBody + "EDD Performed: " + dr["ProductApplied"] + "<br><br>";
                strBody = strBody + "High Risk Category: " + dr["ProductApplied"] + "<br>";
                strBody = strBody + "AML Approval in Palace: " + dr["ProductApplied"] + "<br><br>";
                strBody = strBody + "Reason for PEP: " + dr["ProductApplied"] + "<br>";
                strBody = strBody + "Position: " + dr["ProductApplied"] + "<br>";
                strBody = strBody + "Connected PEP, if applicable: " + dr["ProductApplied"] + "<br>";
                strBody = strBody + "Business recommendation obtained: " + dr["ProductApplied"] + "<br>";
                strBody = strBody + "BCU Head Approval if adverse media exists: " + dr["ProductApplied"] + "<br>";
                strBody = strBody + "Very High Profile/ Risk Customer: " + dr["ProductApplied"] + "<br>";





            }


            //dbclass.CloseConnection();

            string smtp_ip = ConfigurationManager.AppSettings.Get("smtp_ip");
            string smtp_port = ConfigurationManager.AppSettings.Get("smtp_port");


            MailMessage mail = new MailMessage();
            //SmtpClient SmtpServer = new SmtpClient("10.200.48.76");
            SmtpClient SmtpServer = new SmtpClient(smtp_ip);
            SmtpServer.Port = 25;//int.Parse(smtp_port);

            
            strFrom = lblUser + "@hbl.com";


            mail.From = new MailAddress(strFrom);
            mail.To.Add(strTo);
            mail.Subject = strSubject;
            mail.Body = strBody;
            //mail.To.Add("ali.raza12@hbl.com");
            //mail.Subject = "Test Mail";
            //mail.Body = "This is for testing SMTP mail from GMAIL";
            SmtpServer.UseDefaultCredentials = false;
            //SmtpServer.Port = 25;
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;     // This method not ask password

            //SmtpServer.Credentials = new System.Net.NetworkCredential("mis.admin@domestic.hbl.com", "hbl@1234");
            //SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        } // end sendemail

        protected void btnReOpen_Click(object sender, EventArgs e)
        {
            String query = "";
            if (txtStatus.Text == "InProcess")
            {
                txtStatus.Text = "New";

                query = " Update PEP_CASE ";
                
                query = query + "Set CaseEditDate=  GetDate() ,";
                query = query + "CaseEditby='" + lblUser + "',";
                query = query + "CaseStatus='" + txtStatus.Text + "',";
                query = query + "Remarks='" + txtRemarks.Text + "'";

                query = query + "Where ID='" + txtID.Text + "'";

                dbclass.OpenConection();
                dbclass.ExecuteQueries(query);
                dbclass.CloseConnection();

                //sendEmail("New","InProcess", txtID.Text );

            } // Status is InProcess

        }
    }
}