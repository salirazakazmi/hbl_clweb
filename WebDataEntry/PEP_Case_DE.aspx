<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_DE.Master" AutoEventWireup="true" CodeBehind="PEP_Case_DE.aspx.cs" Inherits="WebDataEntry.PEP_Case_DE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">
    <%--  -- Recoomended Site for Bootstrap https://bootsnipp.com/tags/forms
    -- https://getbootstrap.com/docs/4.0/components/forms/#disabled-forms--%>


    <h1 style="align-content: center">PEP Verification Form </h1>


    <!-- --------------------------------------------------------- -->


    <div class="container">
        <table class="table table-striped">
            <tbody>
                <tr>
                    <td colspan="1" style="width: 50%">
                        <div class="well form-horizontal">
                            <fieldset>
                                <div class="form-group">
                                    <label class="col-md-4 control-label">
                                        <asp:Label ID="lblErr" runat="server" Style="width: 100%" ForeColor="Red"></asp:Label>
                                    </label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <asp:Button Style="margin: 5px;" ID="btnSearch" class="btn btn-primary col-md-4 control-label" runat="server" Text="Load" OnClick="btnSearch_Click" />
                                            <asp:Button Style="margin: 5px;" ID="btnNew" class="btn btn-primary" runat="server" Text="New" OnClick="btnNew_Click" />
                                            <asp:Button Style="margin: 5px;" ID="btnCancel" class="btn btn-primary" runat="server" Text="Cancel" OnClick="btnCancel_Click" />

                                        </div>
                                    </div>
                                </div>


                                <div class="form-group">
                                    <label>Enter Case ID*</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <asp:TextBox runat="server" class="form-control" ID="txtID" placeholder="Enter for Search"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label>Customer / CIF ID *</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <asp:TextBox runat="server" class="form-control" ID="txtCustID" placeholder="6 digit"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label>Customer Full Name *</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <span class="input-group-addon" style="max-width: 100%;">
                                                <asp:TextBox runat="server" class="form-control" ID="txtname" placeholder="Full name"></asp:TextBox>
                                            </span>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label>Product Applied *</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <span class="input-group-addon">

                                                <asp:DropDownList ID="DD_Product" runat="server" class="form-control">
                                                </asp:DropDownList>

                                            </span>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label>Customer Type *</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">

                                            <asp:DropDownList ID="DD_CustomerType" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label>Customer Nature *</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">

                                            <asp:DropDownList ID="DD_CustomerNature" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label>Region Name* </label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <asp:DropDownList ID="DD_Region" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label>Branch Code</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <asp:TextBox runat="server" class="form-control" ID="txtBranchCode" placeholder=""></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label>Business </label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <asp:DropDownList ID="DD_Business" runat="server" class="form-control">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem>New</asp:ListItem>
                                                <asp:ListItem>Existing</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label>EDD Performed</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <asp:DropDownList ID="DD_EDD" runat="server" class="form-control">
                                                <asp:ListItem>Yes</asp:ListItem>
                                                <asp:ListItem>No</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label>High Risk Category</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <asp:DropDownList ID="DD_HRCat" runat="server" class="form-control">
                                                <asp:ListItem>Yes</asp:ListItem>
                                                <asp:ListItem>No</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label>AML Approval</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <asp:DropDownList ID="DD_AML" runat="server" class="form-control">
                                                <asp:ListItem>Yes</asp:ListItem>
                                                <asp:ListItem>No</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label>AML Approved Date</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <asp:TextBox runat="server" class="form-control" ID="txtAMLAppDate" placeholder="DD/MM/YYYY"></asp:TextBox>
                                           <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                ControlToValidate="txtAMLAppDate" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$" Display="Dynamic" SetFocusOnError="true" ErrorMessage="invalid date"> Invalid </asp:RegularExpressionValidator>
                                           --%>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label>Reason for PEP *</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <asp:DropDownList ID="DD_Reason" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label>Polical PEP</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <asp:DropDownList ID="DD_PEPPolitical" runat="server" class="form-control">
                                                <asp:ListItem>Yes</asp:ListItem>
                                                <asp:ListItem>No</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label>Position *</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <asp:TextBox runat="server" class="form-control" ID="txtPosition" placeholder=""></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </td>


                    <td colspan="1" style="width: 50%">
                        <div class="well form-horizontal">
                            <fieldset>
                                <div class="form-group">
                                    <label>Connected PEP, if Applicable </label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <asp:DropDownList ID="DD_ConnectedPEP" runat="server" class="form-control">
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                    <asp:ListItem>No</asp:ListItem>
                                                </asp:DropDownList>
                                            </span>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label>Relationship with PEP</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <asp:TextBox runat="server" class="form-control" ID="txtPEPRelation" placeholder=""></asp:TextBox>

                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label>Business Recommendation Obtain</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <asp:DropDownList runat="server" ID="DDBusinessRecom" class="custom-select mr-sm-2">
                                                    <asp:ListItem>NA</asp:ListItem>
                                                    <asp:ListItem>No</asp:ListItem>
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </span>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label>Very High risk Profile Customer</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <asp:DropDownList runat="server" ID="DDHR" class="custom-select mr-sm-2">
                                                    <asp:ListItem>NA</asp:ListItem>
                                                    <asp:ListItem>No</asp:ListItem>
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </span>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label>BCU Approval in Placed</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <asp:DropDownList runat="server" ID="DD_BCUAppr" class="custom-select mr-sm-2">
                                                    <asp:ListItem>NA</asp:ListItem>
                                                    <asp:ListItem>No</asp:ListItem>
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </span>

                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label>NAB / LEA Proceeding in process</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <asp:DropDownList runat="server" ID="DD_NAB" class="custom-select mr-sm-2">
                                                    <asp:ListItem>NA</asp:ListItem>
                                                    <asp:ListItem>No</asp:ListItem>
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </span>

                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label>Adverse Media Identified</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <asp:DropDownList runat="server" ID="DD_AdrMedia" class="custom-select mr-sm-2">
                                                    <asp:ListItem>NA</asp:ListItem>
                                                    <asp:ListItem>No</asp:ListItem>
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </span>

                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label>BCU Head Approval</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <asp:DropDownList runat="server" ID="DDBCUHead" class="custom-select mr-sm-2">
                                                    <asp:ListItem>NA</asp:ListItem>
                                                    <asp:ListItem>No</asp:ListItem>
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </span>

                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label>BCU Head Approval Date</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <asp:TextBox runat="server" class="form-control" ID="txtBCUHeadAppDate" placeholder="DD/MM/YYYY"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label>HBB Approval</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <asp:DropDownList runat="server" ID="DD_HBBApp" class="custom-select mr-sm-2">
                                                    <asp:ListItem>NA</asp:ListItem>
                                                    <asp:ListItem>No</asp:ListItem>
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </span>

                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label>HBB Approval Date</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <asp:TextBox runat="server" class="form-control" ID="txtHBBAppDate" placeholder="DD/MM/YYYY"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label>CIF Bucket Status</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <asp:DropDownList runat="server" ID="DD_CIFStatus" class="custom-select mr-sm-2">
                                                </asp:DropDownList>
                                            </span>

                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label>Active Case</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <span class="input-group-addon" style="max-width: 50%;">
                                                <asp:DropDownList runat="server" ID="DDActive" class="custom-select mr-sm-2">
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                    <asp:ListItem>No</asp:ListItem>
                                                </asp:DropDownList>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label>Additonal Remarks</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <asp:TextBox runat="server" class="form-control" ID="txtRemarks" placeholder=""></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label>Status</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <asp:TextBox runat="server" class="form-control" ID="txtStatus" placeholder="" Enabled="False"></asp:TextBox>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label>Case Raised By</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <asp:TextBox runat="server" class="form-control" ID="txtCaseRaiseBy" placeholder="" Enabled="False"></asp:TextBox>
                                                <asp:TextBox runat="server" class="form-control" ID="txtCaseRaiseDate" placeholder="" Enabled="False"></asp:TextBox>

                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label>Case Edit By</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <asp:TextBox runat="server" class="form-control" ID="txtEditBy" placeholder="" Enabled="False"></asp:TextBox>
                                                <asp:TextBox runat="server" class="form-control" ID="txtEditDate" placeholder="" Enabled="False"></asp:TextBox>

                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label>Case Forward By</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <asp:TextBox runat="server" class="form-control" ID="txtForwardby" placeholder="" Enabled="False"></asp:TextBox>
                                                <asp:TextBox runat="server" class="form-control" ID="txtForwardDate" placeholder="" Enabled="False"></asp:TextBox>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label>Case Approved By</label>
                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <asp:TextBox runat="server" class="form-control" ID="txtApproveBy" placeholder="" Enabled="False"></asp:TextBox>
                                                <asp:TextBox runat="server" class="form-control" ID="txtApprovedDate" placeholder="" Enabled="False"></asp:TextBox>

                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">

                                    <div class="col-md-8 inputGroupContainer">
                                        <div class="input-group">

                                            <asp:Panel ID="Level1" runat="server" ScrollBars="Auto" Style="display: inline;">
                                                <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" OnClick="btnSave_Click" />
                                                <asp:Button ID="btnForward" Style="margin: 5px;" class="btn btn-primary" runat="server" Text="Forward to HOK" OnClick="btnForward_Click" />
                                            </asp:Panel>
                                            <asp:Panel ID="Level2" runat="server" ScrollBars="Auto" Style="display: inline;">
                                                <asp:Button ID="btnApprovalReq" Style="margin: 5px;" class="btn btn-primary" runat="server" Text="Send for Approval" OnClick="btnApprovalReq_Click" />
                                                <asp:Button ID="btnReOpen" Style="margin: 5px;" class="btn btn-primary" runat="server" Text="Re-Open" OnClick="btnReOpen_Click" />
                                            </asp:Panel>
                                            <asp:Panel ID="Level3" runat="server" ScrollBars="Auto" Style="display: inline;">
                                                <asp:Button ID="btnApproved" Style="margin: 5px;" class="btn btn-primary" runat="server" Text="Approved" OnClick="btnApproved_Click" />
                                                <asp:Button ID="btnReject" Style="margin: 5px;" class="btn btn-primary" runat="server" Text="Reject" OnClick="btnReject_Click" />
                                            </asp:Panel>
                                            <span class="input-group-addon"></span>
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
