<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="ExcelRead.aspx.cs" Inherits="WebDataEntry.Deleted.ExcelRead" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 330px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width:100%;">
            <tr>
                <td class="auto-style1">Reading PEP RN10698 - Daily Update</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
    <asp:fileupload id="FileUpload1" runat="server" xmlns:asp="#unknown" />
                </td>
                <td>
                    <asp:Label ID="lblFile" runat="server"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td>
    <asp:button id="btnView" runat="server" text="View File" onclick="Button1_Click" xmlns:asp="#unknown" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="lblmsg" runat="server" Text="lblmsg"></asp:Label>
                </td>
                <td>
    <asp:Button ID="btnLoad" runat="server" Text="Load Data" OnClick="btnLoad_Click" />
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <br />
    <asp:gridview id="GridView1" runat="server" xmlns:asp="#unknown" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" PageSize="100" >
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>

        

    </div>
    </form>
</body>
</html>
