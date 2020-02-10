<%@ Page Language="C#" MasterPageFile="~/MasterPage_DE.Master" AutoEventWireup="true" CodeBehind="PEP_Annexure23.aspx.cs" Inherits="WebDataEntry.PEP_Annexure23" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

     <h1 style="align-content:center"> PEP Annexure23 </h1>
        <div class="auto-style1">

            <table style="width: 100%;">
                <tr>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td>&nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/UserManagement.aspx">Home</asp:HyperLink></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblFilterYear" CssClass="label" runat="server" Text="Select Year: "></asp:Label>
                    </td>
                    <td >
                        <asp:DropDownList ID="DD_Year" CssClass="myListBox" runat="server" Width="200px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblFilterYear0" CssClass="label" runat="server" Text="Month: "></asp:Label>
                    </td>
                    <td >
                        <asp:DropDownList ID="DD_Month" CssClass="myListBox" runat="server" Width="200px">
                        </asp:DropDownList>
                    </td> 
                    <td ></td>
                    <td >&nbsp;</td>
                </tr>
                <tr>
                    <td >
                        <asp:Label ID="Labelreg" runat="server" Text="Region: "></asp:Label>
                    </td>
                    <td >
                        <asp:DropDownList ID="DD_Region" class="textboxform" runat="server" Width="200px">
                        </asp:DropDownList>
                    </td>
                    <td >
                        <asp:Label ID="Labelarea" runat="server" Text="Cluster: "></asp:Label>
                    </td>
                    <td >
                        <asp:DropDownList ID="DD_Area" runat="server" Width="200px">
                        </asp:DropDownList>
                    </td>
                    <td >
                        <asp:Label ID="Label4" runat="server" Text="Branch Code: "></asp:Label>
                    </td>
                    <td >
                        <asp:DropDownList ID="DD_Branch" runat="server" Width="200px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td >
                        <%--<asp:Label ID="Label6" runat="server" Text="CaseID: "></asp:Label>--%>

                    </td>
                    <td >

                        <%--<asp:TextBox ID="txtCaseID" runat="server" Width="200px"></asp:TextBox>--%>

                    </td>
                    <td >
                        <asp:Label ID="Label12" runat="server" Text="Account#:"></asp:Label>

                    </td>
                    <td >

                        <asp:TextBox ID="txtAccount" runat="server" Width="200px"></asp:TextBox>

                    </td>
                    <td >
                        <asp:Label ID="Label13" runat="server" Text="Status: "></asp:Label>
                    </td>
                    <td >
                        <asp:DropDownList ID="DD_Status" runat="server" Width="200px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td >
                        <asp:Button ID="cmdFilter" CssClass="button button4" runat="server" OnClick="cmdFilter_Click" Text="Apply Filter" Width="150px" />
                     
                    </td>
                    <td >
                        &nbsp;</td>
                    <td >
                        <asp:Button ID="cmdDownload" CssClass="button button4" runat="server" OnClick="cmdExcel_Click" Text="Download" Width="150px" /></td>
                </tr>
            </table>

        </div>
        <asp:GridView ID="GridView1" 
             runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" DataKeyNames="ID" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" AllowPaging="True" PageSize="100" OnPageIndexChanging="GridView1_PageIndexChanging" CssClass="Grid" Font-Size="X-Small">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="Select">
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkHeader_CheckedChanged" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ID">
                    <EditItemTemplate>
                        <asp:Label ID="LabelE1" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Month/Year">
                    <EditItemTemplate>
                        <asp:Label ID="LabelE2" runat="server" Text='<%# Eval("TargetDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("TargetDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="BranchCode">
                    <EditItemTemplate>
                        <asp:Label ID="Label11" runat="server" Text='<%# Eval("BranchCode") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("BranchCode") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cluster">
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("Cluster") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Region">
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# Eval("Region") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Account No">
                    <ItemTemplate>
                        <asp:Label ID="Label8" runat="server" Text='<%# Eval("AccountNo") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Title">
                    <ItemTemplate>
                        <asp:Label ID="LabelC" runat="server" Text='<%# Eval("AccountTitle") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Business Nature">
                    <ItemTemplate>
                        <asp:Label ID="LabelAC" runat="server" Text='<%# Eval("NatureofBusiness") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Risk">
                    <ItemTemplate>
                        <asp:Label ID="LabelT" runat="server" Text='<%# Eval("Risk") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="AccountStatus">
                    <ItemTemplate>
                        <asp:Label ID="LabelStat" runat="server" Text='<%# Eval("AccountStatus") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SBP Remarks">
                    <ItemTemplate>
                        <asp:Label ID="Labelsbpre" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Final Status">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DDfld1" runat="server" SelectedValue='<%# Eval("FinalStatus") %>'>
                            <asp:ListItem>RTD</asp:ListItem>
                            <asp:ListItem>Deceased Account</asp:ListItem>
                            <asp:ListItem>Debit Block</asp:ListItem>
                            <asp:ListItem>Corporate Relationship</asp:ListItem>
                            <asp:ListItem>Remediated</asp:ListItem>
                            <asp:ListItem>AC Closed</asp:ListItem>
                            <asp:ListItem>Not PEP</asp:ListItem>
                            <asp:ListItem>Account not yet opened remediation required</asp:ListItem>
                            <asp:ListItem>NA - Not pertained to BB</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblfld1" runat="server" Text='<%# Eval("FinalStatus") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="PEP Approval GH">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtfld2" MaxLength="10"  runat="server" Text='<%# Eval("PEP_Approval_GH") %>'>
                        </asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblfld2" runat="server" Text='<%# Eval("PEP_Approval_GH") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="PEP Marked in System">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DDfld3" runat="server" SelectedValue='<%# Eval("PEP_MarkedinSystem") %>'>
                            <asp:ListItem>Y</asp:ListItem>
                            <asp:ListItem>N</asp:ListItem>
                            <asp:ListItem> </asp:ListItem>
                            
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblfld3" runat="server" Text='<%# Eval("PEP_MarkedinSystem") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="ZNA CIF">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtfld4" MaxLength="15"  runat="server" Text='<%# Eval("ZNA_CIF") %>'>
                        </asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblfld4" runat="server" Text='<%# Eval("ZNA_CIF") %>' ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="ZNA Activation Date">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtfld5" MaxLength="15"  runat="server" Text='<%# Eval("ZNA_ActivationDate") %>'>
                        </asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblfld5" runat="server" Text='<%# Eval("ZNA_ActivationDate") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="ZNA Account">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtfld6" MaxLength="15"  runat="server" Text='<%# Eval("ZNA_Account") %>'>
                        </asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblfld6" runat="server" Text='<%# Eval("ZNA_Account") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                
                
                <asp:TemplateField HeaderText="Comments">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtfld7" MaxLength="150"  runat="server" Text='<%# Eval("EntryRemarks") %>'>
                        </asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblfld7" runat="server" Text='<%# Eval("EntryRemarks") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Lock">
                    <ItemTemplate>
                        <asp:Label ID="Label10" runat="server" Text='<%# Eval("Lock") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Operations">
                    <EditItemTemplate>
                        <asp:LinkButton ID="Update" class="button button4" runat="server" CommandName="Update" ForeColor="White">Update</asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="Cancel" class="button button4" runat="server" CommandName="Cancel" ForeColor="#FFFFCC">Cancel</asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate> 
                        <asp:LinkButton ID="Edit" class="button button4" runat="server" CommandName="Edit" Visible='<%# Eval("Lock").ToString()=="Y" ? false:true %>'>Edit</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#2461BF" Font-Size="Small" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>

        <asp:Table ID="tableControl" runat="server" style="width: 100%;">
             <asp:TableRow runat="server" ID="TableRow1">
                        <asp:TableCell> </asp:TableCell>
                        <asp:TableCell>  
                            <asp:Button ID="cmdGotoPage" class="button button4"  runat="server" Text="Goto Page #" OnClick="cmdGotoPage_Click" />
                            <asp:TextBox ID="txtPage" CssClass="textboxform" runat="server" Width="81px"></asp:TextBox> 
                        </asp:TableCell>
                        <asp:TableCell>  </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server" ID="RowSupervisor">
                <asp:TableCell> <asp:Button ID="cmdCheckerReview" class="button button4"  runat="server" OnClick="cmdCheckerReview_Click" Text="Checker Review" /></asp:TableCell>
                <asp:TableCell> </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server" ID="RowAdmin">
                <asp:TableCell> <asp:Button ID="cmdLock" class="button button4"  runat="server" Text="Lock" OnClick="cmdLock_Click" />  </asp:TableCell>

                <asp:TableCell> <asp:Button ID="cmdUnLock" class="button button4"  runat="server" Text="UnLock" OnClick="cmdUnLock_Click" />  </asp:TableCell> 
                
            </asp:TableRow>
        </asp:Table>
        
        
  </asp:Content>
