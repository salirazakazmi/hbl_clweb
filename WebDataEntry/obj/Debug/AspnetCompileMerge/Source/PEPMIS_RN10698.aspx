<%@ Page Language="C#" MasterPageFile="~/MasterPage_DE.Master" AutoEventWireup="true" CodeBehind="PEPMIS_RN10698.aspx.cs" Inherits="WebDataEntry.PEPMIS_RN10698" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <h1 style="align-content: center">PEP MIS Status for RN10698 </h1>
    <div class="auto-style1">


        <table style="width: 100%;">



            <tr>
                <td class="auto-style8">
                    <asp:Label ID="Labelreg" runat="server" Text="Region: "></asp:Label>
                </td>
                <td class="auto-style8">
                    <asp:DropDownList ID="DD_Region" runat="server" Width="200px">
                    </asp:DropDownList>
                </td>
                <td class="auto-style10"></td>
                <td class="auto-style10"></td>
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
                    <asp:Label ID="Label6" runat="server" Text="CIF #: "></asp:Label>

                </td>
                <td class="auto-style8">

                    <asp:TextBox ID="txtCaseID" runat="server" Width="200px"></asp:TextBox>

                </td>
                <td class="auto-style10">
                    <asp:Label ID="Label12" runat="server" Text="Request ID:"></asp:Label>

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
                <td class="auto-style9">&nbsp;</td>
                <td class="auto-style9">
                    <asp:Button ID="btnDownLoad" runat="server" OnClick="downloadCSV" Text="Download" Width="200px" />

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>

    </div>
    <asp:GridView ID="GridView1" runat="server" GridLines="Vertical" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"  ShowFooter="True" DataKeyNames="RequestID" AllowPaging="True" PageSize="100" OnPageIndexChanging="GridView1_PageIndexChanging">
        <AlternatingRowStyle BackColor="White" />
        <Columns>

            <asp:TemplateField HeaderText="ID">

                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("RequestID") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CIF">
                <ItemTemplate>
                    <asp:Label ID="LabelC" runat="server" Text='<%# Eval("CIFNumber") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="BranchCode">

                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("BranchCode") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Region">
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("RegionName") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Customer Name">
                <ItemTemplate>
                    <asp:Label ID="LabelCname" runat="server" Text='<%# Eval("CustName") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Last Status">
                <ItemTemplate>
                    <asp:Label ID="LabelT" runat="server" Text='<%# Eval("StatusDesc") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Comments">
                <ItemTemplate>
                    <asp:Label ID="Labelcom" runat="server" Text='<%# Eval("Comments") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Bucket">
                <ItemTemplate>
                    <asp:Label ID="lblBucket" runat="server" Text='<%# Eval("Bucket") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="LastDate">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("LastDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="LastTime">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("LastTM") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="FirstDate">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("FirstDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total Days">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("Days") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Holidays">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("Holidays") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total Working Days">
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("WorkingDays") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
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



</asp:Content>
