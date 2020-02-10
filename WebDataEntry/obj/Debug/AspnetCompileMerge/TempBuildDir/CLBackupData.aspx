<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage_DE.Master" CodeBehind="CLBackupData.aspx.cs" Inherits="WebDataEntry.CLBackupData" %>

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

    <h1 style="align-content: center">Backup CL Data</h1>

    <div>
        <table style="width: 100%;">

            <tr>

                <td>
                    Please provide following Information</td>

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
                    <asp:Button ID="btnBackup" runat="server" class="w3-button w3-green" Text="Backup CL" OnClick="btnBackup_Click" />
                </td>

            </tr>
            <tr>
                <td>
                    &nbsp;</td>

            </tr>
            <tr>
                <td>
                    
                    &nbsp;</td>

            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                </td>

            </tr>
            </table>
        <br />



    </div>
</asp:Content>
