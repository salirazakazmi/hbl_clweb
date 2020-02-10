<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_DE.Master" AutoEventWireup="true" CodeBehind="PEP_Case_DEBK.aspx.cs" Inherits="WebDataEntry.PEP_Case_DEBK" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">
    <%--  -- Recoomended Site for Bootstrap https://bootsnipp.com/tags/forms
    -- https://getbootstrap.com/docs/4.0/components/forms/#disabled-forms--%>
      
 
     <h1 style="align-content:center"> PEP Verification Form </h1>


<!-- --------------------------------------------------------- -->


<div class="container">
       <table class="table table-striped">
          <tbody>
             <tr>
                <td colspan="1" style="width:50%">
                   <div class="well form-horizontal">
                      <fieldset>
                          <div class="form-group">
                            <label >Enter Case ID*</label>
                            <div class="col-md-8 inputGroupContainer">
                               <div class="input-group">
                                   <asp:textbox runat="server" class="form-control" id="txtID" placeholder="Enter for Search"></asp:textbox>
                                   
                               </div>
                            </div>
                         </div>
                          <div class="form-group">
                            <label >Customer ID *</label>
                            <div class="col-md-8 inputGroupContainer">
                               <div class="input-group">
                                   <asp:textbox runat="server" class="form-control" id="txtCustID" placeholder="6 digit"></asp:textbox>
                                   
                               </div>
                            </div>
                         </div>
                          
                          <div class="form-group">
                            <label class="col-md-4 control-label"></label>
                            <div class="col-md-8 inputGroupContainer">
                               <div class="input-group">
                                   <asp:Button style="margin:5px;" ID="btnSearch"  class="btn btn-primary col-md-4 control-label" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                   <asp:Button  style="margin:5px;" ID="btnNew"  class="btn btn-primary" runat="server" Text="New" OnClick="btnNew_Click" />
                                   <asp:Button style="margin:5px;" ID="btnCancel"  class="btn btn-primary" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                             
                               </div>
                            </div>
                         </div>
                        
                          
                         <div class="form-group">
                            <label >Customer Full Name *</label>
                            <div class="col-md-8 inputGroupContainer">
                               <div class="input-group">
                                   <span class="input-group-addon" style="max-width: 100%;">
                                   <asp:textbox runat="server" class="form-control" id="txtname" placeholder="Full name"></asp:textbox>
                                       </span>
                               </div>
                            </div>
                         </div>
                          <div class="form-group">
                            <label >Product Applied *</label>
                            <div class="col-md-8 inputGroupContainer">
                               <div class="input-group">
                                   <span class="input-group-addon">
                                   <asp:textbox runat="server" class="form-control" id="txtProduct" placeholder="Product"></asp:textbox>
                                       </span>
                               </div>
                            </div>
                         </div>
                         <div class="form-group">
                            <label >Customer Type *</label>
                            <div class="col-md-8 inputGroupContainer">
                               <div class="input-group">
                                   <asp:textbox runat="server" class="form-control" id="txtCustomerType" placeholder="e.g ETB"></asp:textbox>
                               </div>
                            </div>
                         </div>
                         <div class="form-group">
                            <label >Region Name</label>
                            <div class="col-md-8 inputGroupContainer">
                               <div class="input-group">
                                   <asp:textbox runat="server" class="form-control" id="TxtRegion" placeholder="e.g Karachi"></asp:textbox>
                               </div>
                            </div>
                         </div>
                         <div class="form-group">
                            <label >EDD Performed</label>
                            <div class="col-md-8 inputGroupContainer">
                               <div class="input-group">
                                   <asp:textbox runat="server" class="form-control" id="TxtEDD" placeholder=""></asp:textbox>
                               </div>
                            </div>
                         </div>
                         <div class="form-group">
                            <label >High Risk Category</label>
                            <div class="col-md-8 inputGroupContainer">
                               <div class="input-group">
                                   <asp:textbox runat="server" class="form-control" id="txtHR" placeholder="PEP"></asp:textbox>
                               </div>
                            </div>
                         </div>
                         <div class="form-group">
                            <label >AML Approval in Palace</label>
                            <div class="col-md-8 inputGroupContainer">
                               <div class="input-group">
                                  <asp:textbox runat="server" class="form-control" id="txtAML" placeholder="Expected Date"></asp:textbox>
                               </div>
                            </div>
                         </div>
                         <div class="form-group">
                            <label >Reason for PEP *</label>
                            <div class="col-md-8 inputGroupContainer">
                               <div class="input-group">
                                   <asp:textbox runat="server" class="form-control" id="txtReason" placeholder=""></asp:textbox>

                               </div>
                            </div>
                         </div>
                         <div class="form-group">
                            <label >Position *</label>
                            <div class="col-md-8 inputGroupContainer">
                               <div class="input-group">
                                   <asp:textbox runat="server" class="form-control" id="txtPosition" placeholder=""></asp:textbox>

                               </div>
                            </div>
                         </div>
                      </fieldset>
                   </div>
                </td>


                <td colspan="1" style="width:50%">
                   <div class="well form-horizontal">
                      <fieldset>
                         <div class="form-group">
                            <label >Connected PEP, if Applicable </label>
                            <div class="col-md-8 inputGroupContainer">
                               <div class="input-group">
                                   <span class="input-group-addon">
                                   <asp:textbox runat="server" class="form-control" id="txtConnectedPEP" placeholder=""></asp:textbox>
                                       </span>
                               </div>
                            </div>
                         </div>
                         <div class="form-group">
                            <label>Business Recommendation Obtain</label>
                            <div class="col-md-8 inputGroupContainer">
                               <div class="input-group">
                                   <span class="input-group-addon">
                                   <asp:dropdownlist runat="server" id="DDBusinessRecom" class="custom-select mr-sm-2">
                                            <asp:ListItem>NA</asp:ListItem>
                                            <asp:ListItem>No</asp:ListItem>
                                            <asp:ListItem>Yes</asp:ListItem>
                                    </asp:dropdownlist>
                                       </span>
                               </div>
                            </div>
                         </div>
                       
                         <div class="form-group">
                            <label>BCU Head Approval if diverse Media Exists</label>
                            <div class="col-md-8 inputGroupContainer">
                               <div class="input-group">
                                   <span class="input-group-addon">
                                   <asp:dropdownlist runat="server" id="DDBCUHead" class="custom-select mr-sm-2">
                                            <asp:ListItem>NA</asp:ListItem>
                                            <asp:ListItem>No</asp:ListItem>
                                            <asp:ListItem>Yes</asp:ListItem>
                                    </asp:dropdownlist>
                                       </span>

                               </div>
                            </div>
                         </div>
                         <div class="form-group">
                            <label >Very High risk Profile Customer</label>
                            <div class="col-md-8 inputGroupContainer">
                               <div class="input-group">
                                   <span class="input-group-addon">
                                   <asp:dropdownlist runat="server" id="DDHR" class="custom-select mr-sm-2">
                                            <asp:ListItem>NA</asp:ListItem>
                                            <asp:ListItem>No</asp:ListItem>
                                            <asp:ListItem>Yes</asp:ListItem>
                                    </asp:dropdownlist>
                                       </span>
                               </div>
                            </div>
                         </div>
                         <div class="form-group">
                            <label >Active Case</label>
                            <div class="col-md-8 inputGroupContainer">
                               <div class="input-group">
                                   <span class="input-group-addon" style="max-width: 50%;">
                                       <asp:dropdownlist runat="server" id="DDActive" class="custom-select mr-sm-2">
                                            <asp:ListItem>Yes</asp:ListItem>
                                            <asp:ListItem>No</asp:ListItem>
                                    </asp:dropdownlist>
                                       </span>
                               </div>
                            </div>
                         </div>
                         <div class="form-group">
                            <label>Additonal Remarks</label>
                            <div class="col-md-8 inputGroupContainer">
                               <div class="input-group">
                                   <asp:textbox runat="server" class="form-control" id="txtRemarks" placeholder=""></asp:textbox>
                               </div>
                            </div>
                         </div>
                         <div class="form-group">
                            <label >Status</label>
                            <div class="col-md-8 inputGroupContainer">
                               <div class="input-group">
                                  <span class="input-group-addon">
                                  <asp:textbox runat="server" class="form-control" id="txtStatus" placeholder="" Enabled="False"  ></asp:textbox>
                                   </span>
                               </div>
                            </div>
                         </div>
                         <div class="form-group">
                            <label >Case Raised By</label>
                            <div class="col-md-8 inputGroupContainer">
                               <div class="input-group">
                                   <span class="input-group-addon">
                                    <asp:textbox runat="server" class="form-control" id="txtCaseRaiseBy" placeholder="" Enabled="False"  ></asp:textbox>
                                    <asp:textbox runat="server" class="form-control" id="txtCaseRaiseDate" placeholder="" Enabled="False"  ></asp:textbox>

                                   </span>
                               </div>
                            </div>
                         </div>
                         <div class="form-group">
                            <label >Case Edit By</label>
                            <div class="col-md-8 inputGroupContainer">
                               <div class="input-group">
                                   <span class="input-group-addon">
                                    <asp:textbox runat="server" class="form-control" id="txtEditBy" placeholder="" Enabled="False"  ></asp:textbox>
                                    <asp:textbox runat="server" class="form-control" id="txtEditDate" placeholder="" Enabled="False"  ></asp:textbox>

                                   </span>
                               </div>
                            </div>
                         </div>
                          <div class="form-group">
                            <label >Case Forward By</label>
                            <div class="col-md-8 inputGroupContainer">
                               <div class="input-group">
                                   <span class="input-group-addon">
                                    <asp:textbox runat="server" class="form-control" id="txtForwardby" placeholder="" Enabled="False"  ></asp:textbox>
                                    <asp:textbox runat="server" class="form-control" id="txtForwardDate" placeholder="" Enabled="False"  ></asp:textbox>
                                   </span>
                               </div>
                            </div>
                         </div>
                          <div class="form-group">
                            <label >Case Approved By</label>
                            <div class="col-md-8 inputGroupContainer">
                               <div class="input-group">
                                   <span class="input-group-addon">
                                    <asp:textbox runat="server" class="form-control" id="txtApproveBy" placeholder="" Enabled="False"  ></asp:textbox>
                                    <asp:textbox runat="server" class="form-control" id="txtApprovedDate" placeholder="" Enabled="False"  ></asp:textbox>

                                   </span>
                               </div>
                            </div>
                         </div>
                          <div class="form-group">
                            
                            <div class="col-md-8 inputGroupContainer">
                                <asp:label id="lblErr" text="" runat="server" class="col-md-4 control-label"></asp:label>
                               <div class="input-group"> 
                                   <asp:Button ID="btnSave"  class="btn btn-primary" runat="server" Text="Save" OnClick="btnSave_Click" />
                                   <asp:Button ID="btnForward" style="margin:5px;"  class="btn btn-primary" runat="server" Text="Forward to HOK" OnClick="btnForward_Click" />
                                   <asp:Button ID="btnApprovalReq" style="margin:5px;" class="btn btn-primary" runat="server" Text="Send for Approval" OnClick="btnApprovalReq_Click" />
                                   <asp:Button ID="btnReOpen" style="margin:5px;" class="btn btn-primary" runat="server" Text="Re-Open" OnClick="btnReOpen_Click" />
                                   <span class="input-group-addon">
                                   
                                   </span>
                               </div>
                            </div>
                         </div>
                      </fieldset>
                   </div>
                </td>
             </tr>
          </tbody>
       </table>
    </div>
</asp:Content>
