<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage_DE.Master" CodeBehind="Upl_RN10698.aspx.cs" Inherits="WebDataEntry.Upl_RN10698" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    <style type="text/css">
        .progress {
            border: 2px solid red;
            background: linear-gradient();
        }

        .progress-bar {
            border: 4px solid green;
        }
    </style>

    <script>

        $(function () {
            $("#pbardist").progressbar({
                value: 50
            });
        });
    </script>

    <h1 style="align-content: center">Upload PEP RN10698 - Daily MIS </h1>

    <div>
        <table style="width: 100%;">

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
                    <asp:Button ID="btnLoad" runat="server" class="w3-button w3-green" Text="Load Data" OnClick="btnLoad_Click" />
                </td>

            </tr>
            <tr>
                <td>
                    MIS Date:
                    <asp:TextBox ID="txtMISDate" runat="server" ></asp:TextBox> {YYYY-MM-DD}
                </td>

            </tr>
            <tr>
                <td>
                    
                    <asp:Button ID="btnCalculateMIS" runat="server" class="w3-button w3-green" Text="Only Calculate MIS" OnClick="btnCalculateMIS_Click" />
                    &nbsp;(Only use when Re-calculation is required, otherwise it will auto calculated when you load data), Please provide MIS Date</td>

            </tr>
            <tr>
                <td>
                    
                    Please be careful when Adjust the bucket {this option is use to avoid mistake in movement of BCU bucket into Branches}, this option can&#39;t be undo.<br />
                    Please provide correct date MIS Date</td>

            </tr>
            <tr>
                <td>
                    
                    <asp:Button ID="btnCIFMovement" runat="server" class="w3-button w3-green" Text="Upload Bucket Movement" OnClick="btnCIFMovement_Click" />
                    &nbsp;(Please select File from Browse button)</td>

            </tr>
            <tr>
                <td>
                    
                    &nbsp;</td>

            </tr>
            <tr>
                <td>
                    
                    <asp:Button ID="btnAdjustBucket" runat="server" class="w3-button w3-green" Text="Adjust Bucket Movement" OnClick="btnAdjustBucket_Click" />
                    &nbsp;(Please provide MIS date)</td>

            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                </td>

            </tr>
            <tr>
                <td>
                    
                    <asp:Button ID="btnRestart" runat="server" class="w3-button w3-green" Text="Restart" OnClick="btnRestart_Click" />
                </td>


            </tr>
            <tr>
                <td>
                    <div runat="server" id="pbardist" class="progress-bar bg-success" role="progressbar" style="width: 0%" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
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
