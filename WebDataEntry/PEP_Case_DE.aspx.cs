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
using System.IO;




namespace WebDataEntry
{
    public partial class PEP_Case_DE : System.Web.UI.Page
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
                Populate_DropDown();
                if (HttpContext.Current.Request.QueryString["caseid"] != null)
                {
                    txtID.Text = HttpContext.Current.Request.QueryString["caseid"].ToString();
                    btnSearchClick();
                }
                disable_input(false);
                workflow_button(0);
            }
        }

        private void Populate_DropDown()
        {
            string query = "";

            dbclass.OpenConection();

            DataSet ds;


            query = " Select LOVVAlue from LOVs where LOVTable='PEP_CASE' And LOVField='ProductApplied'";

            dbclass.DataReader(query);
            ds = dbclass.DataSet(query);
            DD_Product.Items.Add("");
            DD_Product.AppendDataBoundItems = true;
            DD_Product.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_Product.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_Product.DataSource = ds.Tables[0];
            DD_Product.DataBind();


            query = " Select LOVVAlue from LOVs where LOVTable='PEP_CASE' And LOVField='CustomerType'";

            dbclass.DataReader(query);
            ds = dbclass.DataSet(query);
            DD_CustomerType.Items.Add("");
            DD_CustomerType.AppendDataBoundItems = true;
            DD_CustomerType.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_CustomerType.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_CustomerType.DataSource = ds.Tables[0];
            DD_CustomerType.DataBind();

            query = " Select LOVVAlue from LOVs where LOVTable='PEP_CASE' And LOVField='CustomerNature'";

            dbclass.DataReader(query);
            ds = dbclass.DataSet(query);
            DD_CustomerNature.Items.Add("");
            DD_CustomerNature.AppendDataBoundItems = true;
            DD_CustomerNature.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_CustomerNature.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_CustomerNature.DataSource = ds.Tables[0];
            DD_CustomerNature.DataBind();

            query = " Select LOVVAlue from LOVs where LOVTable='PEP_CASE' And LOVField='RegionName' Order by LOVVALUE";

            dbclass.DataReader(query);
            ds = dbclass.DataSet(query);
            DD_Region.Items.Add("");
            DD_Region.AppendDataBoundItems = true;
            DD_Region.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_Region.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_Region.DataSource = ds.Tables[0];
            DD_Region.DataBind();

            query = " Select LOVVAlue from LOVs where LOVTable='PEP_CASE' And LOVField='ReasonforPEP' Order by LOVVALUE";

            dbclass.DataReader(query);
            ds = dbclass.DataSet(query);
            DD_Reason.Items.Add("");
            DD_Reason.AppendDataBoundItems = true;
            DD_Reason.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_Reason.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_Reason.DataSource = ds.Tables[0];
            DD_Reason.DataBind();

            query = " Select LOVVAlue from LOVs where LOVTable='PEP_CASE' And LOVField='BucketStatus' Order by LOVVALUE";

            dbclass.DataReader(query);
            ds = dbclass.DataSet(query);
            DD_CIFStatus.Items.Add("");
            DD_CIFStatus.AppendDataBoundItems = true;
            DD_CIFStatus.DataTextField = ds.Tables[0].Columns[0].ToString();
            DD_CIFStatus.DataValueField = ds.Tables[0].Columns[0].ToString();
            DD_CIFStatus.DataSource = ds.Tables[0];
            DD_CIFStatus.DataBind();

            dbclass.CloseConnection();

        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            // Check user is authorize for this new or level 1 
            string query2 = "";
            query2 = "Select * from Workflow_User where OptionName='PEP_Case' And Active='Y'";
            query2 = query2 + " AND UserID='" + lblUser + "' AND WorkflowLevel=1 ";
            dbclass.OpenConection();
            SqlDataReader dr2 = dbclass.DataReader(query2);

            if (dr2.HasRows)
            {


                btnSearch.Enabled = false;
                btnNew.Enabled = false;

                init_form();
                disable_input(true);

                txtID.Text = "New";
                txtID.Enabled = false;
                txtCustID.Text = "";
                txtCustID.Focus();
                txtStatus.Text = "New";

                workflow_button(1); // level 1

            }
            else
            {
                lblErr.Text = "Not have an Access";
            }

            dbclass.CloseConnection();
        }

        private void init_form()
        {

            txtID.Text = "";
            txtID.Enabled = true;
            txtCustID.Text = "";
            txtname.Text = "";
            txtApproveBy.Text = "";
            txtApprovedDate.Text = "";
            txtCaseRaiseBy.Text = "";
            txtCaseRaiseDate.Text = "";

            txtEditBy.Text = "";
            txtEditDate.Text = "";
            txtForwardby.Text = "";
            txtForwardDate.Text = "";

            txtPosition.Text = "";

            txtRemarks.Text = "";
            txtStatus.Text = "";

            lblErr.Text = "";

            txtBranchCode.Text = "";
            txtAMLAppDate.Text = "";
            txtPEPRelation.Text = "";
            txtBCUHeadAppDate.Text = "";
            txtHBBAppDate.Text = "";

            DD_CIFStatus.SelectedIndex = -1;
            DD_HBBApp.SelectedIndex = -1;
            DD_AdrMedia.SelectedIndex = -1;
            DD_NAB.SelectedIndex = -1;
            DD_BCUAppr.SelectedIndex = -1;
            DD_PEPPolitical.SelectedIndex = -1;
            DD_Business.SelectedIndex = -1;
            DD_AML.SelectedIndex = -1;
            DD_ConnectedPEP.SelectedIndex = -1;
            DD_CustomerType.SelectedIndex = -1;
            DD_EDD.SelectedIndex = -1;
            DD_HRCat.SelectedIndex = -1;
            DDActive.SelectedIndex = -1;
            DDBCUHead.SelectedIndex = -1;
            DDBusinessRecom.SelectedIndex = -1;
            DDHR.SelectedIndex = -1;
            DD_Product.SelectedIndex = -1;
            DD_CustomerNature.SelectedIndex = -1;
            DD_Reason.SelectedIndex = -1;
            DD_Region.SelectedIndex = -1;
        }

        private void workflow_button(int level)
        {
            if (level == 0)
            {
                Level1.Visible = false;
                Level2.Visible = false;
                Level3.Visible = false;
                disable_input(false);
            }

            if (level == 1)
            {
                Level1.Visible = true;
                Level2.Visible = false;
                Level3.Visible = false;
                disable_input(true);
            }

            if (level == 2)
            {
                Level1.Visible = false;
                Level2.Visible = true;
                Level3.Visible = false;
                disable_input(false);
                txtRemarks.Enabled = true;
            }

            if (level == 3)
            {
                Level1.Visible = false;
                Level2.Visible = false;
                Level3.Visible = true;
                disable_input(false);
                txtRemarks.Enabled = true;
            }

        }   // end of workflow button
        private void disable_input(Boolean bl)
        {

            //txtID.Text = "";
            //txtCustID.Text = "";
            DD_AML.Enabled = bl;
            txtApproveBy.Enabled = false;
            txtApprovedDate.Enabled = false;
            txtCaseRaiseBy.Enabled = false;
            txtCaseRaiseDate.Enabled = false;
            txtEditDate.Enabled = false;
            txtEditBy.Enabled = false;
            txtForwardDate.Enabled = false;
            txtForwardby.Enabled = false;

            DD_ConnectedPEP.Enabled = bl;
            DD_CustomerType.Enabled = bl;
            DD_EDD.Enabled = bl;
            DD_HRCat.Enabled = bl;
            txtname.Enabled = bl;
            txtPosition.Enabled = bl;
            DD_Product.Enabled = bl;
            DD_CustomerNature.Enabled = bl;
            DD_Region.Enabled = bl;
            DD_Reason.Enabled = bl;
            txtRemarks.Enabled = bl;
            txtStatus.Enabled = false;
            DDActive.Enabled = bl;
            DDBCUHead.Enabled = bl;
            DDBusinessRecom.Enabled = bl;
            DDHR.Enabled = bl;
            txtBranchCode.Enabled = bl;
            DD_Business.Enabled = bl;
            txtAMLAppDate.Enabled = bl;
            DD_PEPPolitical.Enabled = bl;
            txtPEPRelation.Enabled = bl;
            DD_BCUAppr.Enabled = bl;
            DD_NAB.Enabled = bl;
            DD_AdrMedia.Enabled = bl;
            txtBCUHeadAppDate.Enabled = bl;
            DD_HBBApp.Enabled = bl;
            txtHBBAppDate.Enabled = bl;
            DD_CIFStatus.Enabled = bl;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            btnSearchClick();
        }

        private void btnSearchClick()
        {
            int level = 0;
            Boolean recfound = false;
            lblErr.Text = "";
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
                    level = int.Parse(dr["WorkflowLevel"].ToString());

                    // Check user is authorize for this level, it will come later base on record current level then decide and get condition
                    string query2 = "";
                    query2 = "Select * from Workflow_User where OptionName='PEP_Case' And Active='Y'";
                    query2 = query2 + " AND UserID='" + lblUser + "' AND WorkflowLevel=" + level;
                    SqlDataReader dr2 = dbclass.DataReader(query2);

                    if (dr2.HasRows)
                    {
                        while (dr2.Read())
                        {
                            condition = condition + " AND " + dr2["condition"];
                            recfound = true;
                        }

                    } // if Workflow_user has record
                }

                if (recfound)
                {
                    query = "Select * from PEP_CASE ";
                    query = query + " Where " + condition;
                    dbclass.CloseConnection();

                    dbclass.OpenConection();
                    dr = dbclass.DataReader(query);

                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            // Change code here to set correct fields - Done
                            txtID.Text = dr["ID"].ToString();
                            if (!dr.IsDBNull(dr.GetOrdinal("CustomerID")))
                            {
                                txtCustID.Text = dr["CustomerID"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("CustomerName")))
                            {
                                txtname.Text = dr["CustomerName"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("ProductApplied")))
                            {
                                DD_Product.Text = dr["ProductApplied"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("CustomerType")))
                            {
                                DD_CustomerType.Text = dr["CustomerType"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("RegionName")))
                            {
                                DD_Region.Text = dr["RegionName"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("BranchCode")))
                            {
                                txtBranchCode.Text = dr["BranchCode"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("Business")))
                            {
                                DD_Business.Text = dr["Business"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("EDD")))
                            {
                                DD_EDD.Text = dr["EDD"].ToString();
                            }

                            if (!dr.IsDBNull(dr.GetOrdinal("HighRiskCategory")))
                            {
                                DD_HRCat.Text = dr["HighRiskCategory"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("AMLapproval")))
                            {
                                DD_AML.Text = dr["AMLapproval"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("AMLapprovalDate")))
                            {
                                txtAMLAppDate.Text = dr["AMLapprovalDate"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("reasonforPEP")))
                            {
                                DD_Reason.Text = dr["reasonforPEP"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("PEPPolitical")))
                            {
                                DD_PEPPolitical.Text = dr["PEPPolitical"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("position")))
                            {
                                txtPosition.Text = dr["position"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("ConnectedPEP")))
                            {
                                DD_ConnectedPEP.Text = dr["ConnectedPEP"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("BusinessRecommendation")))
                            {
                                DDBusinessRecom.Text = dr["BusinessRecommendation"].ToString();
                            }

                            if (!dr.IsDBNull(dr.GetOrdinal("AdverseMedia")))
                            {
                                DD_AdrMedia.Text = dr["AdverseMedia"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("BCUHeadApproval")))
                            {
                                DDBCUHead.Text = dr["BCUHeadApproval"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("BCUHeadApprovalDate")))
                            {
                                txtBCUHeadAppDate.Text = dr["BCUHeadApprovalDate"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("VeryHighProfileCustomer")))
                            {
                                DDHR.Text = dr["VeryHighProfileCustomer"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("NAB_LEAProceeding")))
                            {
                                DD_NAB.Text = dr["NAB_LEAProceeding"].ToString();
                            }

                            if (!dr.IsDBNull(dr.GetOrdinal("HBBapproval")))
                            {
                                DD_HBBApp.Text = dr["HBBapproval"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("HBBapprovalDate")))
                            {
                                txtHBBAppDate.Text = dr["HBBapprovalDate"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("active")))
                            {
                                DDActive.Text = dr["active"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("CaseRaiseDate")))
                            {
                                txtCaseRaiseDate.Text = dr["CaseRaiseDate"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("CaseRaiseBy")))
                            {
                                txtCaseRaiseBy.Text = dr["CaseRaiseBy"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("BucketStatus")))
                            {
                                DD_CIFStatus.Text = dr["BucketStatus"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("CaseStatus")))
                            {
                                txtStatus.Text = dr["CaseStatus"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("Remarks")))
                            {
                                txtRemarks.Text = dr["Remarks"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("CaseEditDate")))
                            {
                                txtEditDate.Text = dr["CaseEditDate"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("CaseEditBy")))
                            {
                                txtEditBy.Text = dr["CaseEditBy"].ToString();
                            }

                            if (!dr.IsDBNull(dr.GetOrdinal("CaseForwardDate")))
                            {
                                txtForwardDate.Text = dr["CaseForwardDate"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("CaseForwardBy")))
                            {
                                txtForwardby.Text = dr["CaseForwardBy"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("CaseApproveDate")))
                            {
                                txtApprovedDate.Text = dr["CaseApproveDate"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("CaseApproveBy")))
                            {
                                txtApproveBy.Text = dr["CaseApproveBy"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("BCUApproval")))
                            {
                                DD_BCUAppr.Text = dr["BCUApproval"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("PEPRelationship")))
                            {
                                txtPEPRelation.Text = dr["PEPRelationship"].ToString();
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("CustomerNature")))
                            {
                                DD_CustomerNature.Text = dr["CustomerNature"].ToString();
                            }
                            workflow_button(level);

                        }

                    }
                }
                else
                {
                    lblErr.Text = "You don't have access on this Record";
                    workflow_button(0);
                }
                dbclass.CloseConnection();
            }
            catch
            {
                workflow_button(0);
            }


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            btnCancel.Enabled = true;
            btnSearch.Enabled = true;
            //btnSave.Enabled = false;
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
                if (DD_Product.Text == "")
                {
                    lblErr.Text = "Please must Select Product";
                    noerror = 1;
                }
                if (DD_CustomerType.Text == "")
                {
                    lblErr.Text = "Please must Select Customer Type";
                    noerror = 1;
                }
                if (DD_Reason.Text == "")
                {
                    lblErr.Text = "Please must enter Reason for PEP";
                    noerror = 1;
                }
                if (txtPosition.Text == "")
                {
                    lblErr.Text = "Please must enter Position";
                    noerror = 1;
                }

                if (DD_Region.Text == "")
                {
                    lblErr.Text = "Please must Select Region Name";
                    noerror = 1;
                }

                if (DD_CustomerNature.Text == "")
                {
                    lblErr.Text = "Please must Select Customer Nature for PEP";
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

                    // Need to correct and add fields
                    if (newrec == true)
                    {


                        query = "INSERT INTO[PEP_Case] ([CustomerID], [CustomerName] ,[ProductApplied] ,[CustomerType]";
                        query = query + " ,[RegionName] ,[EDD] ,[HighRiskCategory] ,[AMLapproval]  ,[AMLapprovalDate]";
                        query = query + " ,[ReasonforPEP] ,[PEPPolitical]   ,[Position] ,[ConnectedPEP] ,[BusinessRecommendation]";
                        query = query + " ,[AdverseMedia] ,[BCUHeadApproval] ,[BCUHeadApprovalDate] ,[VeryHighProfileCustomer]";
                        query = query + " ,[NAB_LEAProceeding] ,[HBBapproval]  ,[HBBapprovalDate]  ,[Active]  ,[CaseRaiseDate]";
                        query = query + " ,[CaseRaiseBy]  ,[CaseStatus] ,[Remarks]";
                        query = query + " ,[WorkflowLevel]  ,[CustomerNature] ,[BranchCode] ,[Business]";
                        query = query + " ,[BucketStatus] ,[PEPRelationship],[BCUApproval]";
                        query = query + " ) Values (";

                        query = query + "'" + txtCustID.Text + "',";
                        query = query + "'" + txtname.Text + "',";
                        query = query + "'" + DD_Product.Text + "',";
                        query = query + "'" + DD_CustomerType.Text + "',";
                        query = query + "'" + DD_Region.Text + "',";
                        query = query + "'" + DD_EDD.Text + "',";
                        query = query + "'" + DD_HRCat.Text + "',";
                        query = query + "'" + DD_AML.Text + "',";
                        query = query + "'" + txtAMLAppDate.Text + "',";
                        query = query + "'" + DD_Reason.Text + "',";
                        query = query + "'" + DD_PEPPolitical.Text + "',";
                        query = query + "'" + txtPosition.Text + "',";
                        query = query + "'" + DD_ConnectedPEP.Text + "',";
                        query = query + "'" + DDBusinessRecom.Text + "',";
                        query = query + "'" + DD_AdrMedia.Text + "',";
                        query = query + "'" + DDBCUHead.Text + "',";
                        query = query + "'" + txtBCUHeadAppDate.Text + "',";
                        query = query + "'" + DDHR.Text + "',";
                        query = query + "'" + DD_NAB.Text + "',";
                        query = query + "'" + DD_HBBApp.Text + "',";
                        query = query + "'" + txtHBBAppDate.Text + "',";
                        query = query + "'" + DDActive.Text + "',";
                        query = query + "GetDate(),";
                        query = query + "'" + lblUser + "',";
                        query = query + "'New',";
                        query = query + "'" + txtRemarks.Text + "','1',";
                        query = query + "'" + DD_CustomerNature.Text + "',";
                        query = query + "'" + txtBranchCode.Text + "',";
                        query = query + "'" + DD_Business.Text + "',";
                        query = query + "'" + DD_CIFStatus.Text + "',";
                        query = query + "'" + txtPEPRelation.Text + "',";
                        query = query + "'" + DD_BCUAppr.Text + "')";

                        dbclass.OpenConection();
                        dbclass.ExecuteQueries(query);

                        // display last number to track
                        query = "";
                        query = "Select Max(ID) from PEP_CASE";
                        SqlDataReader dr2 = dbclass.DataReader(query);


                        while (dr2.Read())
                        {
                            lblErr.Text = "New Case ID = " + dr2[0];
                            txtID.Text = dr2[0].ToString();
                        }

                        dbclass.CloseConnection();
                    }
                    // need to correct and add fields
                    if (newrec == false)
                    {
                        query = " Update PEP_CASE ";
                        query = query + "Set CustomerID = '" + txtCustID.Text + "',";
                        query = query + "CustomerName = '" + txtname.Text + "',";
                        query = query + "ProductApplied= '" + DD_Product.Text + "',";
                        query = query + "CustomerType ='" + DD_CustomerType.Text + "',";
                        query = query + "RegionName='" + DD_Region.Text + "',";
                        query = query + "EDD ='" + DD_EDD.Text + "',";
                        query = query + "HighRiskCategory='" + DD_HRCat.Text + "',";
                        query = query + "AMLapproval='" + DD_AML.Text + "',";
                        query = query + "ReasonforPEP='" + DD_Reason.Text + "',";
                        query = query + "Position='" + txtPosition.Text + "',";
                        query = query + "ConnectedPEP='" + DD_ConnectedPEP.Text + "',";
                        query = query + "BusinessRecommendation='" + DDBusinessRecom.Text + "',";
                        query = query + "BCUHeadApproval='" + DDBCUHead.Text + "',";
                        query = query + "VeryHighProfileCustomer='" + DDHR.Text + "',";
                        query = query + "Active='" + DDActive.Text + "',";
                        query = query + "CaseEditDate=  GetDate() ,";
                        query = query + "CaseEditby='" + lblUser + "',";
                        query = query + "CaseStatus='" + txtStatus.Text + "',";
                        query = query + "Remarks='" + txtRemarks.Text + "',";
                        query = query + "CustomerNature='" + DD_CustomerNature.Text + "',";
                        query = query + "Workflowlevel=1 ";

                        query = query + "Where ID='" + txtID.Text + "'";

                        dbclass.OpenConection();
                        dbclass.ExecuteQueries(query);
                        dbclass.CloseConnection();
                        lblErr.Text = "Saved ";
                    }

                    workflow_button(1);
                } // if no error

            }
            catch
            {
                int a = 0;
                lblErr.Text = "Issue in Saving...";
            }

            //init_form();
        }


        protected void btnForward_Click(object sender, EventArgs e)
        {

            try
            {
                int noerror = 0;
                string query = "";

                if (txtID.Text == "New")
                {
                    lblErr.Text = " Please must save before Forward";
                    noerror = 1;
                }

                if (txtCustID.Text == "" && noerror == 0)
                {
                    lblErr.Text = "Please must enter Customer ID";
                    noerror = 1;
                }

                if (txtname.Text == "" && noerror == 0)
                {
                    lblErr.Text = "Please must enter Customer Full Name";
                    noerror = 1;
                }
                if (DD_Product.Text == "" && noerror == 0)
                {
                    lblErr.Text = "Please must enter Product";
                    noerror = 1;
                }
                if (DD_CustomerType.Text == "" && noerror == 0)
                {
                    lblErr.Text = "Please must enter Customer Type";
                    noerror = 1;
                }
                if (DD_Reason.Text == "" && noerror == 0)
                {
                    lblErr.Text = "Please must enter Reason for PEP";
                    noerror = 1;
                }
                if (txtPosition.Text == "" && noerror == 0)
                {
                    lblErr.Text = "Please must enter Position";
                    noerror = 1;
                }

                if (DDActive.Text == "No" && noerror == 0)
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
                    query = query + "ProductApplied= '" + DD_Product.Text + "',";
                    query = query + "CustomerType ='" + DD_CustomerType.Text + "',";
                    query = query + "RegionName='" + DD_Region.Text + "',";
                    query = query + "BranchCode='" + txtBranchCode.Text + "',";
                    query = query + "Business='" + DD_Business.Text + "',";
                    query = query + "EDD ='" + DD_EDD.Text + "',";
                    query = query + "HighRiskCategory='" + DD_HRCat.Text + "',";
                    query = query + "AMLapproval='" + DD_AML.Text + "',";
                    query = query + "AMLapprovalDate='" + txtAMLAppDate.Text + "',";
                    query = query + "ReasonforPEP='" + DD_Reason.Text + "',";
                    query = query + "PEPPolitical='" + DD_PEPPolitical.Text + "',";
                    query = query + "Position='" + txtPosition.Text + "',";
                    query = query + "ConnectedPEP='" + DD_ConnectedPEP.Text + "',";
                    query = query + "BusinessRecommendation='" + DDBusinessRecom.Text + "',";
                    query = query + "AdverseMedia='" + DD_AdrMedia.Text + "',";
                    query = query + "BCUHeadApproval='" + DDBCUHead.Text + "',";
                    query = query + "BCUHeadApprovalDate='" + txtBCUHeadAppDate.Text + "',";
                    query = query + "VeryHighProfileCustomer='" + DDHR.Text + "',";
                    query = query + "NAB_LEAProceeding='" + DD_NAB.Text + "',";
                    query = query + "HBBapproval='" + DD_HBBApp.Text + "',";
                    query = query + "HBBapprovalDate='" + txtHBBAppDate.Text + "',";
                    query = query + "Active='" + DDActive.Text + "',";
                    query = query + "CaseForwardDate=  GetDate() ,";
                    query = query + "CaseForwardby='" + lblUser + "',";
                    query = query + "CaseStatus='" + txtStatus.Text + "',";
                    query = query + "Remarks='" + txtRemarks.Text + "',";
                    query = query + "CustomerNature='" + DD_CustomerNature.Text + "',";
                    query = query + "BucketStatus='" + DD_CIFStatus.Text + "',";
                    query = query + "workflowlevel=2 ";

                    query = query + "Where ID='" + txtID.Text + "'";

                    dbclass.OpenConection();
                    dbclass.ExecuteQueries(query);
                    dbclass.CloseConnection();

                    sendEmail(1, 2, txtID.Text, txtStatus.Text);
                    //sendEmail(1, 2, txtID.Text);
                    init_form();
                    disable_input(false);
                    workflow_button(0);
                    lblErr.Text = "Forwarded to HOK Successfully";
                } // if no error

            }
            catch
            {
                int a = 0;
                lblErr.Text = "Issue in Data entry can't forward...";
            }

            //init_form();

        } // end of btnForward

        protected void btnApprovalReq_Click(object sender, EventArgs e)
        {
            String query = "";
            if (txtStatus.Text == "InProcess")
            {
                txtStatus.Text = "ApprovalReq";

                query = " Update PEP_CASE ";

                query = query + "Set CaseForwardDate=  GetDate() ,";
                query = query + "CaseForwardby='" + lblUser + "',";
                query = query + "CaseStatus='" + txtStatus.Text + "',";
                query = query + "Remarks='" + txtRemarks.Text + "',";
                query = query + "workflowlevel=3 ";

                query = query + "Where ID='" + txtID.Text + "'";

                dbclass.OpenConection();
                dbclass.ExecuteQueries(query);
                dbclass.CloseConnection();
                workflow_button(0);     // Set to level 3
                //sendEmail(2, 3, txtID.Text);

                sendEmail(2, 3, txtID.Text, txtStatus.Text);

                init_form();
                disable_input(false);
                lblErr.Text = "Sended for Approval Successfully";
                //sendEmail("New","InProcess", txtID.Text );

            } // Status is InProcess

        }


        private void sendEmail(int currentworkflowlevel, int nextworkflowlevel, string PEPID, string currentstatus)
        {

            string strFrom, strTo, strSubject, strBody, strcc, emailfile;
            strFrom = "";
            strTo = "";
            strcc = "";
            strSubject = "";
            strBody = "";
            emailfile = "";

            dbclass.OpenConection();

            string query = "Select EmailFrom from Workflow_user where Active='Y' and UserID= '" + lblUser + "'";
            query = query + " and workflowlevel = " + currentworkflowlevel;

            SqlDataReader dr = dbclass.DataReader(query);

            while (dr.Read())
            {
                strFrom = dr["EmailFrom"].ToString();
            }

            dr = null;



            string query2 = "";
            int workflow_rule = 0;
            query = "Select * from Workflow_Rule where TableName='PEP_CASE' and Status='" + currentstatus + "'";

            dr = dbclass.DataReader(query);
            workflow_rule = 0;
            while (dr.Read())
            {

                query2 = "Select * from PEP_CAse Where " + dr["Rule1"].ToString();
                SqlDataReader dr2 = dbclass.DataReader(query2);

                if (dr2.HasRows)
                {
                    while (dr2.Read())
                    {
                        workflow_rule = 1;
                        strTo = dr["MailTo"].ToString();
                        strcc = dr["Mailcc"].ToString();
                        emailfile = dr["MailFile"].ToString();
                    }
                }
                dr2 = null;
            }

            //query = "Select EmailTo from Workflow_user where Active='Y' ";
            //query = query + " and workflowlevel = " + nextworkflowlevel;

            //dr = dbclass.DataReader(query);

            //while (dr.Read())
            //{
            //    strTo = strTo + dr["EmailTo"].ToString();
            //}

            if (workflow_rule == 1) // if rule found then try to email, else not do email
            {

                query = "";
                query = "Select * from PEP_Case Where ID = '" + PEPID + "'";

                dr = dbclass.DataReader(query);

                while (dr.Read())
                {
                    //if (dr["CaseStatus"].ToString() == "InProcess")
                    //{

                    string body = string.Empty;

                    using (StreamReader reader = new StreamReader(Server.MapPath(emailfile)))
                    //using (StreamReader reader = new StreamReader(Server.MapPath("~/Email/EmailTemplate.htm")))
                    {
                        body = reader.ReadToEnd();
                    }
                    body = body.Replace("[CustomerName]", dr["CustomerName"].ToString());
                    body = body.Replace("[ReasonforPEP]", dr["ReasonforPEP"].ToString());
                    body = body.Replace("[CustomerID]", dr["CustomerID"].ToString());
                    body = body.Replace("[ProductApplied]", dr["ProductApplied"].ToString());
                    body = body.Replace("[CustomerType]", dr["CustomerType"].ToString());
                    body = body.Replace("[Business]", dr["Business"].ToString());
                    body = body.Replace("[RegionName]", dr["RegionName"].ToString());
                    body = body.Replace("[EDD]", dr["EDD"].ToString());
                    body = body.Replace("[HighRiskCategory]", dr["HighRiskCategory"].ToString());
                    body = body.Replace("[AMLapproval]", dr["AMLapproval"].ToString());
                    body = body.Replace("[Position]", dr["Position"].ToString());
                    body = body.Replace("[ConnectedPEP]", dr["ConnectedPEP"].ToString());
                    body = body.Replace("[BusinessRecommendation]", dr["BusinessRecommendation"].ToString());
                    body = body.Replace("[AdverseMedia]", dr["AdverseMedia"].ToString());
                    body = body.Replace("[PEPRelationship]", dr["PEPRelationship"].ToString());

                    strBody = body;


                    strSubject = "PEP Case # - " + PEPID + " - " + dr["CustomerID"].ToString() + " - " + dr["CustomerName"].ToString() + " - " + dr["CaseStatus"].ToString();

                    //}

                    //strBody = "Please arrange Head Branch Banking approval for ";
                    //strBody = strBody + dr["CustomerName"] + ". He/she is categorized as PEP by virtue of his/her current position";
                    //strBody = strBody + " i.e. " + dr["ReasonforPEP"] + ". Case will be forwarded to AMLD after Head BB approval. ";
                    //strBody = strBody + "<br><br>";

                    //strBody = strBody + "Customer Name: " + dr["CustomerName"] + "<br>";
                    //strBody = strBody + "Product Applied: " + dr["ProductApplied"] + "<br>";
                    //strBody = strBody + "Customer: " + dr["CustomerType"] + "<br>";
                    //strBody = strBody + "Region Name: " + dr["RegionName"] + "<br>";
                    //strBody = strBody + "EDD Performed: " + dr["ProductApplied"] + "<br><br>";
                    //strBody = strBody + "High Risk Category: " + dr["ProductApplied"] + "<br>";
                    //strBody = strBody + "AML Approval in Palace: " + dr["ProductApplied"] + "<br><br>";
                    //strBody = strBody + "Reason for PEP: " + dr["ProductApplied"] + "<br>";
                    //strBody = strBody + "Position: " + dr["ProductApplied"] + "<br>";
                    //strBody = strBody + "Connected PEP, if applicable: " + dr["ProductApplied"] + "<br>";
                    //strBody = strBody + "Business recommendation obtained: " + dr["ProductApplied"] + "<br>";
                    //strBody = strBody + "BCU Head Approval if adverse media exists: " + dr["ProductApplied"] + "<br>";
                    //strBody = strBody + "Very High Profile/ Risk Customer: " + dr["ProductApplied"] + "<br>";

                }


                //dbclass.CloseConnection();

                string smtp_ip = ConfigurationManager.AppSettings.Get("smtp_ip");
                string smtp_port = ConfigurationManager.AppSettings.Get("smtp_port");


                MailMessage mail = new MailMessage();
                //SmtpClient SmtpServer = new SmtpClient("10.200.48.76");
                SmtpClient SmtpServer = new SmtpClient(smtp_ip);
                SmtpServer.Port = 25;//int.Parse(smtp_port);


                mail.IsBodyHtml = true;
                mail.From = new MailAddress(strFrom);
                mail.To.Add(strTo);
                mail.CC.Add(strcc);
                mail.Subject = strSubject;
                mail.Body = strBody;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;     // This method not ask password

                // old mathod not using this
                //SmtpServer.Credentials = new System.Net.NetworkCredential("mis.admin@domestic.hbl.com", "hbl@1234");
                //SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            } // end if workflow_rule 
        } // end sendemail

        // old email logic
        //private void sendEmail(int currentworkflowlevel, int nextworkflowlevel, string PEPID)
        //{

        //    string strFrom, strTo, strSubject, strBody;
        //    strFrom = "";
        //    strTo = "";
        //    strSubject = "";
        //    strBody = "";
        //    string query = "Select ID, EmailTo from workflow_stage ";
        //    query = query + " Where OptionName='PEP_Case'  And Active='Y' and CurrentStatus='" + OldStatus + "' and NewStatus='" + NewStatus + "'";

        //    dbclass.OpenConection();

        //    string query = "Select EmailFrom from Workflow_user where Active='Y' and UserID= '" + lblUser + "'";
        //    query = query + " and workflowlevel = " + currentworkflowlevel;

        //    SqlDataReader dr = dbclass.DataReader(query);

        //    while (dr.Read())
        //    {
        //        strFrom = dr["EmailFrom"].ToString();
        //    }

        //    dr = null;

        //    query = "Select EmailTo from Workflow_user where Active='Y' ";
        //    query = query + " and workflowlevel = " + nextworkflowlevel;

        //    dr = dbclass.DataReader(query);

        //    while (dr.Read())
        //    {
        //        strTo = strTo + dr["EmailTo"].ToString();
        //    }


        //    query = "";
        //    query = "Select * from PEP_Case Where ID = '" + PEPID + "'";

        //    dr = dbclass.DataReader(query);

        //    while (dr.Read())
        //    {
        //        if (dr["CaseStatus"].ToString() == "InProcess")
        //        {
        //            strSubject = "PEP Case # - " + PEPID + " - " + dr["CustomerID"].ToString() + " - " + dr["CustomerName"].ToString() + " - " + dr["CaseStatus"].ToString();

        //        }

        //        strBody = "Please arrange Head Branch Banking approval for ";
        //        strBody = strBody + dr["CustomerName"] + ". He/she is categorized as PEP by virtue of his/her current position";
        //        strBody = strBody + " i.e. " + dr["ReasonforPEP"] + ". Case will be forwarded to AMLD after Head BB approval. ";
        //        strBody = strBody + "<br><br>";

        //        strBody = strBody + "Customer Name: " + dr["CustomerName"] + "<br>";
        //        strBody = strBody + "Product Applied: " + dr["ProductApplied"] + "<br>";
        //        strBody = strBody + "Customer: " + dr["CustomerType"] + "<br>";
        //        strBody = strBody + "Region Name: " + dr["RegionName"] + "<br>";
        //        strBody = strBody + "EDD Performed: " + dr["ProductApplied"] + "<br><br>";
        //        strBody = strBody + "High Risk Category: " + dr["ProductApplied"] + "<br>";
        //        strBody = strBody + "AML Approval in Palace: " + dr["ProductApplied"] + "<br><br>";
        //        strBody = strBody + "Reason for PEP: " + dr["ProductApplied"] + "<br>";
        //        strBody = strBody + "Position: " + dr["ProductApplied"] + "<br>";
        //        strBody = strBody + "Connected PEP, if applicable: " + dr["ProductApplied"] + "<br>";
        //        strBody = strBody + "Business recommendation obtained: " + dr["ProductApplied"] + "<br>";
        //        strBody = strBody + "BCU Head Approval if adverse media exists: " + dr["ProductApplied"] + "<br>";
        //        strBody = strBody + "Very High Profile/ Risk Customer: " + dr["ProductApplied"] + "<br>";

        //    }


        //    dbclass.CloseConnection();

        //    string smtp_ip = ConfigurationManager.AppSettings.Get("smtp_ip");
        //    string smtp_port = ConfigurationManager.AppSettings.Get("smtp_port");


        //    MailMessage mail = new MailMessage();
        //    SmtpClient SmtpServer = new SmtpClient("10.200.48.76");
        //    SmtpClient SmtpServer = new SmtpClient(smtp_ip);
        //    SmtpServer.Port = 25;//int.Parse(smtp_port);


        //    mail.IsBodyHtml = true;
        //    mail.From = new MailAddress(strFrom);
        //    mail.To.Add(strTo);
        //    mail.CC.Add(strFrom);
        //    mail.Subject = strSubject;
        //    mail.Body = strBody;
        //    SmtpServer.UseDefaultCredentials = false;
        //    SmtpServer.Port = 25;
        //    SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;     // This method not ask password

        //    SmtpServer.Credentials = new System.Net.NetworkCredential("mis.admin@domestic.hbl.com", "hbl@1234");
        //    SmtpServer.EnableSsl = true;

        //    SmtpServer.Send(mail);

        //} // end sendemail

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
                query = query + "Remarks='" + txtRemarks.Text + "',";
                query = query + "workflowlevel=1 ";

                query = query + "Where ID='" + txtID.Text + "'";

                dbclass.OpenConection();
                dbclass.ExecuteQueries(query);
                dbclass.CloseConnection();
                sendEmail(2, 1, txtID.Text, txtStatus.Text);
                //sendEmail(2, 1, txtID.Text);
                workflow_button(1);     // Set to level 1
                init_form();
                disable_input(false);
                lblErr.Text = "Cased Re-Opened and send Back to Initiator";
                //sendEmail("New","InProcess", txtID.Text );

            } // Status is InProcess

        } // end of reopen

        protected void btnReject_Click(object sender, EventArgs e)
        {
            String query = "";
            if (txtStatus.Text == "ApprovalReq")
            {
                txtStatus.Text = "Reject";

                query = " Update PEP_CASE ";

                query = query + "Set CaseApproveDate=  GetDate() ,";
                query = query + "CaseApproveby='" + lblUser + "',";
                query = query + "CaseStatus='" + txtStatus.Text + "',";
                query = query + "Remarks='" + txtRemarks.Text + "',";
                query = query + "workflowlevel=1 ";

                query = query + "Where ID='" + txtID.Text + "'";

                dbclass.OpenConection();
                dbclass.ExecuteQueries(query);
                dbclass.CloseConnection();
                sendEmail(3, 3, txtID.Text, txtStatus.Text);
                //sendEmail(3, 3, txtID.Text);
                workflow_button(0);     // Set to level 0
                init_form();
                disable_input(false);
                lblErr.Text = "Case Rejected";
                //sendEmail("New","InProcess", txtID.Text );

            } // Status is InProcess

        }

        protected void btnApproved_Click(object sender, EventArgs e)
        {

            String query = "";
            if (txtStatus.Text == "ApprovalReq")
            {
                txtStatus.Text = "Approved";

                query = " Update PEP_CASE ";

                query = query + "Set CaseApproveDate=  GetDate() ,";
                query = query + "CaseApproveby='" + lblUser + "',";
                query = query + "CaseStatus='" + txtStatus.Text + "',";
                query = query + "Remarks='" + txtRemarks.Text + "',";
                query = query + "workflowlevel=3 ";

                query = query + "Where ID='" + txtID.Text + "'";

                dbclass.OpenConection();
                dbclass.ExecuteQueries(query);
                dbclass.CloseConnection();
                workflow_button(0);     // Set to level 0
                sendEmail(3, 3, txtID.Text, txtStatus.Text);
                //sendEmail(3, 3, txtID.Text);
                init_form();
                disable_input(false);
                lblErr.Text = "Case Approved Successfully";
                //sendEmail("New","InProcess", txtID.Text );

            } // Status is InProcess
            if (txtStatus.Text == "Approved")
            {
                lblErr.Text = "Case is Already Approved";
            }

        } // end approved
    }
}