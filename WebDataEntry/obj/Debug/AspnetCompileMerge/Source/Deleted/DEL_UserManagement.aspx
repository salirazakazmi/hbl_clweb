<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DEL_UserManagement.aspx.cs" Inherits="WebDataEntry.DEL_UserManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BCU User Menu</title>
    <link rel="stylesheet" type="text/css" href="css/logincss.css">
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
        .auto-style2 {
            text-align: right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>  
         <h2 style="background-color: white;color: green; text-align: center; font-style: oblique">Business Compliance Unit</h2>  
        <h3 style="background-color: white;color: green; text-align: center; font-style: oblique">Menu Option</h3>  
        <table>
            <tr>
                <td class="auto-style2">  <asp:LinkButton ID="Logout" runat="server" OnClick="Logout_Click">Logout</asp:LinkButton>                 </td>
            </tr>
        </table>
        <fieldset>  
            <legend style="font-family:Arial Black;color:orangered">
                <asp:Label ID="lblUser" runat="server" Text="Label"></asp:Label>
            </legend>  
            <div class="auto-style1">
                <center>
            <asp:GridView ID="GridView1" runat="server"  CellPadding ="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" DataKeyNames="ID">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Options">
                        <ItemTemplate>
                            <%--<asp:HyperLink ID="HyperLink1"  runat="server" Text='<%# Eval("MenuOption") %>' PostBackUrl NavigateUrl='<%# Eval("RedirectPage") %>'  ></asp:HyperLink>--%>
                            <asp:LinkButton ID="LinkButton1" runat="server" NavigateUrl='<%# Eval("RedirectPage")%>' AutoPostBack="true" OnClick="LinkButton1_Click" > <%# Eval("MenuOption") %> </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Role ">
                        <ItemTemplate>
                            <asp:Label ID="lblRole" runat="server"  Text='<%# Eval("Role") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
             </center>
            </div>
       </fieldset> 
        
    </div>  
    </form>
</body>
</html>
