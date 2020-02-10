<%@ Page Language="C#" MasterPageFile="~/MasterPage_DE.Master" AutoEventWireup="true" CodeBehind="PEP_Case_Report.aspx.cs" Inherits="WebDataEntry.PEP_Case_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <h1 style="align-content: center">PEP Case Action List </h1>
    <div class="auto-style1">


        <table style="width: 100%;">

            <tr>
                <td>
                    <asp:Label ID="lblFilterYear" runat="server" Text="Select Year: "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="DD_Year" runat="server" Width="200px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblFilterYear0" runat="server" Text="Month: "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="DD_Month" runat="server" Width="200px">
                    </asp:DropDownList>
                </td>
                <td class="auto-style15"></td>
                <td class="auto-style7">&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Labelreg" runat="server" Text="Region: "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="DD_Region" runat="server" Width="200px">
                    </asp:DropDownList>
                </td>
                <td>

                    <asp:Label ID="Label13" runat="server" Text="Status: "></asp:Label>

                </td>
                <td>

                    <asp:DropDownList ID="DD_Status" runat="server" Width="200px">
                    </asp:DropDownList>

                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="CaseID: "></asp:Label>

                </td>
                <td>

                    <asp:TextBox ID="txtCaseID" runat="server" Width="200px"></asp:TextBox>

                </td>
                <td>
                    <asp:Label ID="Label12" runat="server" Text="Customer:"></asp:Label>

                </td>
                <td>

                    <asp:TextBox ID="txtAccount" runat="server" Width="200px"></asp:TextBox>

                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style8">&nbsp;</td>
                <td class="auto-style8">&nbsp;</td>
                <td class="auto-style10">&nbsp;</td>
                <td class="auto-style10">
                    <asp:Button ID="cmdFilter" runat="server" OnClick="cmdFilter_Click" Text="Apply Filter" Width="200px" />
                </td>
                <td class="auto-style9">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
            </tr>
        </table>
        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/PEP_Case_DE.aspx">New PEP Case</asp:LinkButton>

    </div>
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" DataKeyNames="ID" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" AllowPaging="True" PageSize="100" OnPageIndexChanging="GridView1_PageIndexChanging" ShowHeaderWhenEmpty="true">
        <AlternatingRowStyle BackColor="White" />
        <EmptyDataTemplate>
            <div class="text-center">No record found</div>
        </EmptyDataTemplate>
        
        <Columns>
            <asp:TemplateField HeaderText="Select">
                <HeaderTemplate>
                    <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkHeader_CheckedChanged" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelect" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CaseID">
                <EditItemTemplate>
                    <asp:Label ID="LabelE1" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Created Date">
                <EditItemTemplate>
                    <asp:Label ID="LabelE2" runat="server" Text='<%# Eval("CaseRaiseDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("CaseRaiseDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CustomerID">
                <EditItemTemplate>
                    <asp:Label ID="Label11" runat="server" Text='<%# Eval("CustomerID") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("CustomerID") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="FullName">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="CustomerType">
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("CustomerType") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Region">
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("RegionName") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ReasonforPEP">
                <ItemTemplate>
                    <asp:Label ID="LabelC" runat="server" Text='<%# Eval("ReasonforPEP") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Position">
                <ItemTemplate>
                    <asp:Label ID="LabelAC" runat="server" Text='<%# Eval("Position") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ProductApplied">
                <ItemTemplate>
                    <asp:Label ID="LabelT" runat="server" Text='<%# Eval("ProductApplied") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("CaseStatus") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Comments">
                <ItemTemplate>
                    <asp:Label ID="Labelcom" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Action">
                    
                    <ItemTemplate>
                        <asp:LinkButton ID="btnOpenForm" runat="server" CommandName="Open" CommandArgument='<%# Eval("ID") %>' OnClick="OpenForm_Click" >Open</asp:LinkButton>
                        <%--<asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" Visible='<%# Eval("Lock").ToString()=="Y" ? false:true %>'>Open</asp:LinkButton>--%>
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

    <asp:Table ID="tableControl" runat="server" Style="width: 100%;">
        <asp:TableRow runat="server" ID="TableRow1">
            <asp:TableCell> </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="cmdGotoPage" runat="server" Text="Goto Page #" OnClick="cmdGotoPage_Click" />
                <asp:TextBox ID="txtPage" runat="server" Width="81px"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell>  </asp:TableCell>
        </asp:TableRow>
        <%--<asp:TableRow runat="server" ID="RowSupervisor">
                <asp:TableCell> <asp:Button ID="cmdCheckerReview" runat="server" OnClick="cmdCheckerReview_Click" Text="Checker Review" /></asp:TableCell>
                <asp:TableCell> </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server" ID="RowAdmin">
                <asp:TableCell> <asp:Button ID="cmdLock" runat="server" Text="Lock" OnClick="cmdLock_Click" />  </asp:TableCell>

                <asp:TableCell> <asp:Button ID="cmdUnLock" runat="server" Text="UnLock" OnClick="cmdUnLock_Click" />  </asp:TableCell> 
                
            </asp:TableRow>--%>
    </asp:Table>


</asp:Content>
