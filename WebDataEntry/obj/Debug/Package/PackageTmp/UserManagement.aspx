<%@ Page Title="Entry Scren" Language="C#" MasterPageFile="~/MasterPage_DE.Master" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="WebDataEntry.UserManagement" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>--%>
 <%--CssClass="table table-responsive table-striped table-hover"--%>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH1" runat="server" >
<div class="container"  >
    <br />
    <asp:GridView ID="GridView1" runat="server"  CellPadding ="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" DataKeyNames="ID"
         HorizontalAlign="Center" PageSize="25"  >
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Options">
                        <ItemTemplate>
                            <%--<asp:HyperLink ID="HyperLink1"  runat="server" Text='<%# Eval("MenuOption") %>' PostBackUrl NavigateUrl='<%# Eval("RedirectPage") %>'  ></asp:HyperLink>--%>
                            <asp:LinkButton ID="LinkButton1" runat="server" NavigateUrl='<%# Eval("RedirectPage")%>' AutoPostBack="true" OnClick="LinkButton1_Click" > <%# Eval("MenuOption") %> </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Role ">
                        <ItemTemplate>
                            <asp:Label ID="lblRole" runat="server"  Text='<%# Eval("Role") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblComments" runat="server"  Text='<%# Eval("Comments") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ID" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="thead-dark" />
                <EditRowStyle BackColor="#7C6F57" />
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#E3EAEB" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                <SortedAscendingHeaderStyle BackColor="#246B61" />
                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                <SortedDescendingHeaderStyle BackColor="#15524A" />
            </asp:GridView>

</div>

</asp:Content>

