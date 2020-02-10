<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage_DE.Master" CodeBehind="Upl_CLCompileFiles.aspx.cs" Inherits="WebDataEntry.Upl_CLCompileFiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <style type="text/css">
        .progress {
            border: 2px solid red;
            background: linear-gradient();
        }

        .progress-bar {
            border: 4px solid green;
        }
        .auto-style1 {
            height: 24px;
        }
    </style>

    <script>

        $(function () {
            $("#pbardist").progressbar({
                value: 50
            });
        });
    </script>

    <h1 style="align-content: center">Upload Pre-Calculated Data of CL</h1>

    <div>
        <table style="width: 100%;">

            <tr>
                <td class="auto-style1">
                    Caution: This option is very sensitive and un-reversable, It will upload the Compiled / Calculated CL files into Database, once uploaded then value will reflect directly on dashboard, make sure your data is corrected.<br />
                    It is highly recommended backup the CL data before run this option.
                    <br />
                    File should be in .xlsx (Excel File - Do not add / delete or change column in Excel File as per decided Format, it will corrupt the existing data)</td>
            </tr>

            <tr>
                <td class="auto-style1">
                    <asp:fileupload id="FileUpload1" class="w3-button w3-green" runat="server" xmlns:asp="#unknown" />
                </td>
            </tr>
            <tr>
                <td>Selected File name := 
                    <asp:Label ID="lblFile" runat="server"></asp:Label>

                </td>
            </tr>
            <tr>

                <td>
                    <asp:button id="btnView" runat="server" class="w3-button w3-green" text="View File" onclick="Button1_Click" xmlns:asp="#unknown" />
                </td>

            </tr>
            <tr>

                <td>
                    Before Upload please provide following Information</td>

            </tr>
            <tr>

                <td>
                    Year:
                    <asp:TextBox ID="txtYear" runat="server" Width="99px" ></asp:TextBox> &nbsp;{YYYY: e.g 2019}</td>

            </tr>
            <tr>

                <td>
                    Month:<asp:TextBox ID="txtMonth" runat="server" Width="81px"></asp:TextBox>
                    {MM: e.g 09}</td>

            </tr>
            <tr>

                <td>
                    Remarks:
                    <asp:TextBox ID="txtRemarks" runat="server" Width="309px"></asp:TextBox>
                </td>

            </tr>
            <tr>

                <td>
                    Select which to Update:
                    <asp:DropDownList ID="ddSource" runat="server">
                         <asp:ListItem Value="1">Branch Data</asp:ListItem>
                         <asp:ListItem Value="2">Region / SAM / Area Data</asp:ListItem>
                    </asp:DropDownList>
                </td>

            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnLoad" runat="server" class="w3-button w3-green" Text="Upload Data" OnClick="btnLoad_Click" />
                </td>

            </tr>
            <tr>
                <td class="auto-style1">
                    
                    <asp:Button ID="btnGrandTotal" runat="server" class="w3-button w3-green" Text="Calculate Grand Total" OnClick="btnGrandTotal_Click" />
                </td>

            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                </td>

            </tr>
            </table>
        <br />
        <asp:gridview id="GridView1" runat="server" xmlns:asp="#unknown" cellpadding="4" forecolor="#333333" gridlines="None" allowpaging="True" pagesize="100">
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
        </asp:gridview>



    </div>
</asp:Content>
