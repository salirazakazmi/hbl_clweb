<%@ Page Language="C#" MasterPageFile="~/MasterPage_DE.Master" AutoEventWireup="true" CodeBehind="Alerts.aspx.cs" Inherits="WebDataEntry.Alerts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <h1 style="align-content:center"> FCCM Alerts </h1>
        <div class="auto-style1">

          
            <table style="width: 100%;">
               
                <tr>
                    <td class="auto-style5">
                        <asp:Label ID="lblFilterYear" runat="server" Text="Select Year: "></asp:Label>
                    </td>
                    <td class="auto-style5">
                        <asp:DropDownList ID="DD_Year" runat="server" Width="200px">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style9">
                        <asp:Label ID="lblFilterYear0" runat="server" Text="Month: "></asp:Label>
                    </td>
                    <td class="auto-style9">
                        <asp:DropDownList ID="DD_Month" runat="server" Width="200px">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style15"></td>
                    <td class="auto-style7">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style8">
                        <asp:Label ID="Labelreg" runat="server" Text="Region: "></asp:Label>
                    </td>
                    <td class="auto-style8">
                        <asp:DropDownList ID="DD_Region" runat="server" Width="200px">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style10">
                        <asp:Label ID="Labelarea" runat="server" Text="Area: "></asp:Label>
                    </td>
                    <td class="auto-style10">
                        <asp:DropDownList ID="DD_Area" runat="server" Width="200px">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style9">
                        <asp:Label ID="Label4" runat="server" Text="Branch Code: "></asp:Label>
                    </td>
                    <td class="auto-style9">
                        <asp:DropDownList ID="DD_Branch" runat="server" Width="200px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style8">
                        <asp:Label ID="Label6" runat="server" Text="CaseID: "></asp:Label>

                    </td>
                    <td class="auto-style8">

                        <asp:TextBox ID="txtCaseID" runat="server" Width="200px"></asp:TextBox>

                    </td>
                    <td class="auto-style10">
                        <asp:Label ID="Label12" runat="server" Text="Account#:"></asp:Label>

                    </td>
                    <td class="auto-style10">

                        <asp:TextBox ID="txtAccount" runat="server" Width="200px"></asp:TextBox>

                    </td>
                    <td class="auto-style9">
                        <asp:Label ID="Label13" runat="server" Text="Status: "></asp:Label>
                    </td>
                    <td class="auto-style9">
                        <asp:DropDownList ID="DD_Status" runat="server" Width="200px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style8">&nbsp;</td>
                    <td class="auto-style8">&nbsp;</td>
                    <td class="auto-style10">&nbsp;</td>
                    <td class="auto-style10">
                        <asp:Button ID="cmdFilter" runat="server" OnClick="cmdFilter_Click" Text="Apply Filter" Width="200px" />
                    </td>
                    <td class="auto-style9">
                        &nbsp;</td>
                    <td class="auto-style9">
                        &nbsp;</td>
                </tr>
            </table>

        </div>
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" DataKeyNames="ID" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" AllowPaging="True" PageSize="100" OnPageIndexChanging="GridView1_PageIndexChanging">
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
                        <asp:Label ID="LabelE2" runat="server" Text='<%# Eval("Date", "{0:dd/MM/yyyy}") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("Date", "{0:dd/MM/yyyy}") %>'></asp:Label>
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
                <asp:TemplateField HeaderText="AreaName">
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("AreaName") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="SAM">
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# Eval("SAM") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Region">
                    <ItemTemplate>
                        <asp:Label ID="Label8" runat="server" Text='<%# Eval("Region") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CaseID">
                    <ItemTemplate>
                        <asp:Label ID="LabelC" runat="server" Text='<%# Eval("CaseID") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="External Account">
                    <ItemTemplate>
                        <asp:Label ID="LabelAC" runat="server" Text='<%# Eval("ExternalAccount") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Title">
                    <ItemTemplate>
                        <asp:Label ID="LabelT" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DDTarget" runat="server" SelectedValue='<%# Eval("Status") %>'>
                            <asp:ListItem>Close</asp:ListItem>
                            <asp:ListItem>OutStanding</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label9" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Comments">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtcomments" MaxLength="150"  runat="server" SelectedValue='<%# Eval("Comments") %>'>
                        </asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Labelcom" runat="server" Text='<%# Eval("Comments") %>'></asp:Label>
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
                        <asp:LinkButton ID="Update" runat="server" CommandName="Update" ForeColor="White">Update</asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="Cancel" runat="server" CommandName="Cancel" ForeColor="#FFFFCC">Cancel</asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="Edit" runat="server" CommandName="Edit" Visible='<%# Eval("Lock").ToString()=="Y" ? false:true %>'>Edit</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
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
                            <asp:Button ID="cmdGotoPage" runat="server" Text="Goto Page #" OnClick="cmdGotoPage_Click" />
                            <asp:TextBox ID="txtPage" runat="server" Width="81px"></asp:TextBox> 
                        </asp:TableCell>
                        <asp:TableCell>  </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server" ID="RowSupervisor">
                <asp:TableCell> <asp:Button ID="cmdCheckerReview" runat="server" OnClick="cmdCheckerReview_Click" Text="Checker Review" /></asp:TableCell>
                <asp:TableCell> </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server" ID="RowAdmin">
                <asp:TableCell> <asp:Button ID="cmdLock" runat="server" Text="Lock" OnClick="cmdLock_Click" />  </asp:TableCell>

                <asp:TableCell> <asp:Button ID="cmdUnLock" runat="server" Text="UnLock" OnClick="cmdUnLock_Click" />  </asp:TableCell> 
                
            </asp:TableRow>
        </asp:Table>
        
        
</asp:Content>
