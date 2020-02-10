<%@ Page Language="C#" MasterPageFile="~/MasterPage_DE.Master" AutoEventWireup="true" CodeBehind="ExcelAnnexure201912001.aspx.cs" Inherits="WebDataEntry.ExcelAnnexure201912001" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <h1 style="align-content: center">SBP Annexure 20191201 - Branch Banking Regional Tracking </h1>
    <div class="auto-style1">


        <table style="width: 100%;">

            <tr>
                <td class="auto-style8">
                    <asp:Label ID="Label14" runat="server" Text="Assingto: "></asp:Label>
                </td>
                <td class="auto-style8">
                    <asp:DropDownList ID="DD_Segment" runat="server" Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td class="auto-style8">
                </td>
                <td class="auto-style8">
                </td>
                <td class="auto-style10">
                </td>
                <td class="auto-style10">
                </td>
                <td class="auto-style9">
                </td>
                <td class="auto-style9">
                </td>
            </tr>

            <tr>
                <td class="auto-style8">
                    <asp:Label ID="Label1" runat="server" Text="Annexure Ref: "></asp:Label>
                </td>
                <td class="auto-style8">
                    <asp:DropDownList ID="DD_Annexure" runat="server" Width="200px">
                    </asp:DropDownList>
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
            <!-- Additional Search -->
            <tr>
                <td class="auto-style8">
                    <asp:Label ID="lblSComments" runat="server" Text="Comments "></asp:Label>
                </td>
                <td class="auto-style8">

                    <asp:TextBox ID="txtSComment" runat="server" Width="200px"></asp:TextBox>

                </td>
                <td class="auto-style10">
                </td>
                <td class="auto-style10">
                </td>
                <td class="auto-style9">
                </td>
                <td class="auto-style9">
                </td>
            </tr>
            <tr>
                <td class="auto-style8">&nbsp;</td>
                <td class="auto-style8">&nbsp;</td>
                <td class="auto-style10">&nbsp;</td>
                <td class="auto-style10">
                    <asp:Button ID="cmdFilter" runat="server" OnClick="cmdFilter_Click" Text="Apply Filter" Width="200px" />
                </td>
                <td class="auto-style9">&nbsp;</td>
                <td class="auto-style9">
                    <asp:Button ID="cmdDownload" runat="server" OnClick="cmdDownload_Click" Text="DownLoad" Width="200px" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMsg" runat="server" Text=''></asp:Label>
                </td>
            </tr>
        </table>

    </div>
    <asp:GridView ID="GridView1" runat="server" Font-Size="Small" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"  GridLines="Vertical" ShowFooter="True" DataKeyNames="id" OnRowCancelingEdit="GridView1_RowCancelingEdit" 
        OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" AllowPaging="True" PageSize="100" OnPageIndexChanging="GridView1_PageIndexChanging" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found">
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
            <asp:TemplateField HeaderText="Sr No">
                <ItemTemplate>
                    <asp:Label ID="lblC1" runat="server" Text='<%# Eval("C1") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Annexure Ref">
                <ItemTemplate>
                    <asp:Label ID="lblC2" runat="server" Text='<%# Eval("C2") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Annexure Desc">
                <ItemTemplate>
                    <asp:Label ID="lblC3" runat="server" Text='<%# Eval("C3") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Branch Name">
                <ItemTemplate>
                    <asp:Label ID="lblC4" runat="server" Text='<%# Eval("C4") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Relationship Type">
                <ItemTemplate>
                    <asp:Label ID="lblC5" runat="server" Text='<%# Eval("C5") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
           
            <asp:TemplateField HeaderText="Account Title">
                <ItemTemplate>
                    <asp:Label ID="lblC6" runat="server" Text='<%# Eval("C6") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Acc Open Date">
                <ItemTemplate>
                    <asp:Label ID="lblC7" runat="server" Text='<%# Eval("C7") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
                        <asp:TemplateField HeaderText="Account No.">
                <ItemTemplate>
                    <asp:Label ID="lblC8" runat="server" Text='<%# Eval("C8") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Currency">
                <ItemTemplate>
                    <asp:Label ID="lblC9" runat="server" Text='<%# Eval("C9") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Inspection / Rectification Date">
                <ItemTemplate>
                    <asp:Label ID="lblC10" runat="server" Text='<%# Eval("C10") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="DAYS IRREGULARITY PERSISTED">
                <ItemTemplate>
                    <asp:Label ID="lblC11" runat="server" Text='<%# Eval("C11") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="BALANCE AS ON INSPECTION">
                <ItemTemplate>
                    <asp:Label ID="lblC12" runat="server" Text='<%# Eval("C12") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="DETAIL OF MISSING INFORMATION">
                <ItemTemplate>
                    <asp:Label ID="lblC13" runat="server" Text='<%# Eval("C13") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="NAME(S) OF INDIVIDUALS WHOSE CDD WAS NOT PERFORMED">
                <ItemTemplate>
                    <asp:Label ID="lblC14" runat="server" Text='<%# Eval("C14") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Remarks">
                <ItemTemplate>
                    <asp:Label ID="lblC15" runat="server" Text='<%# Eval("C15") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Assing to">
                <ItemTemplate>
                    <asp:Label ID="lblC16" runat="server" Text='<%# Eval("AssignTo") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Region Comments">
                <EditItemTemplate>
                    <asp:TextBox ID="txtcomments"  MaxLength="150" runat="server" Text='<%# Eval("Comments") %>'>
                    </asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Labelcom" runat="server" Text='<%# Eval("Comments") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status">
                <EditItemTemplate>
                    <asp:DropDownList ID="DDStatus" runat="server" SelectedValue='<%# Eval("Status") %>'>
                        <asp:ListItem>Open</asp:ListItem>
                        <asp:ListItem>Closed</asp:ListItem>
                        <asp:ListItem>Reverted</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblrstatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
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

    <asp:Table ID="tableControl" runat="server" Style="width: 100%;">
        <asp:TableRow runat="server" ID="TableRow1">
            <asp:TableCell> </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="cmdGotoPage" runat="server" Text="Goto Page #" OnClick="cmdGotoPage_Click" />
                <asp:TextBox ID="txtPage" runat="server" Width="81px"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell>  </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" ID="RowSupervisor">
            <asp:TableCell>
                <asp:Button ID="cmdCheckerReview" runat="server" OnClick="cmdCheckerReview_Click" Text="Checker Review" /></asp:TableCell>
            <asp:TableCell> </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" ID="RowAdmin">
            <asp:TableCell>
                <asp:Button ID="cmdLock" runat="server" Text="Lock" OnClick="cmdLock_Click" />
            </asp:TableCell>

            <asp:TableCell>
                <asp:Button ID="cmdUnLock" runat="server" Text="UnLock" OnClick="cmdUnLock_Click" />
            </asp:TableCell>

        </asp:TableRow>
    </asp:Table>


</asp:Content>
