<%@ Page Language="C#" MasterPageFile="~/MasterPage_DE.Master" AutoEventWireup="true" CodeBehind="PEP_Annexure23Summary.aspx.cs" Inherits="WebDataEntry.PEP_Annexure23Summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

     <h1 style="align-content:center"> PEP Annexure23 Summary </h1>
       <div style="align-content: center">

         

         <h1 style="background-color: white;color: green; text-align: center; font-style: oblique">Compliance Ladder</h1>
         <h2 style="background-color: white;color: green; text-align: center; font-style: oblique">PEP Annexure-23 Summary</h2>

            <table >
                <tr>
                    <td style="width: 20%; text-align: left">&nbsp;</td>
                    <td style="width: 40%; ">&nbsp;</td>
                    <td style="width: 40%; text-align: right">&nbsp;</td>
                    <td style="width: 40%; text-align: right"><asp:HyperLink ID="HyperLink1" CssClass="button button4" runat="server" NavigateUrl="~/UserManagement.aspx">Home</asp:HyperLink></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblFilterYear" CssClass="label" runat="server" Text="Select Year: "></asp:Label>
                    </td>
                    <td >
                        <asp:DropDownList ID="DD_Year" CssClass="form-control" runat="server" Width="200px">
                        </asp:DropDownList>
                    </td>
                    <td >
                        <asp:Label ID="Labelreg" runat="server" CssClass="label" Text="Region: "></asp:Label>
                    </td>
                    <td >
                        <asp:DropDownList ID="DD_Region" class="form-control" runat="server" Width="200px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblFilterYear0" CssClass="label" runat="server" Text="Month: "></asp:Label>
                    </td>
                    <td >
                        <asp:DropDownList ID="DD_Month" CssClass="form-control" runat="server" Width="200px">
                        </asp:DropDownList>
                    </td>
                    <td >
                        <asp:Label ID="Labelarea" CssClass="label" runat="server" Text="Cluster: "></asp:Label>
                    </td>
                    <td >
                        <asp:DropDownList ID="DD_Area" runat="server" Width="200px" CssClass="form-control">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td >
                        <asp:Label ID="Label4" CssClass="label" runat="server" Text="Branch Code: "></asp:Label>
                    </td>
                    <td >
                        <asp:DropDownList ID="DD_Branch" runat="server" Width="200px" CssClass="form-control">
                        </asp:DropDownList>
                    </td>
                    <td >

                        <asp:Label ID="Label12" CssClass="label" runat="server" Text="Account#:"></asp:Label>

                    </td>
                    <td >

                        <asp:TextBox ID="txtAccount" runat="server" Width="200px" CssClass="form-control"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td >
                        <asp:Label ID="Label13" CssClass="label" runat="server" Text="Status: "></asp:Label>

                    </td>
                    <td >

                        <asp:DropDownList ID="DD_Status" class="form-control" runat="server" Width="200px">
                        </asp:DropDownList>

                    </td>
                    <td >
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                </tr>
                <tr>
                    <td >
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                </tr>
                <tr>
                    <td >
                        <%--<asp:Label ID="Label6" runat="server" Text="CaseID: "></asp:Label>--%>

                    </td>
                    <td >

                        <%--<asp:TextBox ID="txtCaseID" runat="server" Width="200px"></asp:TextBox>--%>

                    </td>
                    <td >
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                </tr>
                <tr>
                    <td >
                        &nbsp;</td>
                    <td >

                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td >
                        <asp:Button ID="cmdFilter" CssClass="button button4" runat="server" OnClick="cmdFilter_Click" Text="Apply Filter" Width="150px" />
                     
                    </td>
                    <td >
                        &nbsp;</td>
                    <td >
                        <asp:Button ID="cmdDownload" CssClass="button button4" runat="server" OnClick="cmdExcel_Click" Text="Download" Width="150px" /></td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td >
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                </tr>
            </table>
            </div>
      
        <script>
            $(document).ready(function () {
                $('#GridView1').DataTable();
                } );
        </script>
            
        <asp:GridView ID="GridView1" 
            runat="server" AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" AllowPaging="True" 
            PageSize="100" OnPageIndexChanging="GridView1_PageIndexChanging" CssClass="Grid"  >
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#2461BF" Font-Size="Small" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>

        <asp:Table ID="tableControl" runat="server" >
             <asp:TableRow runat="server" ID="TableRow1">
                        <asp:TableCell> </asp:TableCell>
                        <asp:TableCell>  
                            <asp:Button ID="cmdGotoPage" class="button button4"  runat="server" Text="Goto Page #" OnClick="cmdGotoPage_Click" />
                            <asp:TextBox ID="txtPage" CssClass="textboxform" runat="server" Width="81px"></asp:TextBox> 
                        </asp:TableCell>
                        <asp:TableCell>  </asp:TableCell>
            </asp:TableRow>
            
        </asp:Table>
        
        
</asp:Content>
