<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PEPMISDashboard .aspx.cs" Inherits="WebDataEntry.PEPMISDashboard " %>


<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8" />
    <link rel="apple-touch-icon" sizes="76x76" href="assets_th1/img/notification.png">
    <link rel="icon" type="image/png" href="assets_th1/img/notification.png">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title>Compliance Ladder Dashboard
    </title>
    <meta content='width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0, shrink-to-fit=no' name='viewport' />
    <!--     Fonts and icons     -->

    <link href="assets_th1/css/micon.css" rel="stylesheet" />
    <link href="assets_th1/css/font-awesome.min.css" rel="stylesheet" />
    <!-- CSS Files -->
    <link href="assets_th1/css/material-dashboard.css?v=2.1.1" rel="stylesheet" />
    <%--    <!-- CSS Just for demo purpose, don't include it in your project -->
    <link href="assets_th1/demo/demo.css" rel="stylesheet" />--%>


    <script src="Scripts/jquery-3.4.1.slim.js"></script>
    <script src="highchart/highcharts.js"></script>
    <script src="highchart/modules/exporting.js"></script>


    <style type="text/css">
        .progress {
            border: 2px solid red;
            background: linear-gradient();
        }

        .progress-bar {
            border: 4px solid green;
        }
    </style>


    <style type="text/css">
        @import 'highchart/css/highcharts.css';

        #container {
            height: 200px;
            max-width: 200px;
            margin: 0 auto;
        }

        /* Link the series colors to axis colors */
        .highcharts-color-0 {
            fill: #7cb5ec;
            stroke: #7cb5ec;
        }

        .highcharts-axis.highcharts-color-0 .highcharts-axis-line {
            stroke: #7cb5ec;
        }

        .highcharts-axis.highcharts-color-0 text {
            fill: #7cb5ec;
        }

        .highcharts-color-1 {
            fill: #90ed7d;
            stroke: #90ed7d;
        }

        .highcharts-axis.highcharts-color-1 .highcharts-axis-line {
            stroke: #90ed7d;
        }

        .highcharts-axis.highcharts-color-1 text {
            fill: #90ed7d;
        }


        .highcharts-yaxis .highcharts-axis-line {
            stroke-width: 2px;
        }
    </style>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <%--<script src="bootstrap4/js/bootstrap.min.js"></script>--%>
    <style type="text/css">
        #panel, #flip {
            padding: 0px;
            text-align: center;
            background-color: #008C93;
            color: white;
            border: solid 1px #c3c3c3;
        }

        #panel {
            padding: 0px;
            display: none;
        }

        #panel1, #flip1 {
            padding: 0px;
            text-align: center;
            background-color: #e5eecc;
            border: solid 1px #c3c3c3;
        }

        #panel1 {
            padding: 0px;
            display: none;
        }
    </style>

</head>

<body class="">
    <form runat="server">
        <div class="wrapper ">
            <div class="sidebar" data-color="purple" data-background-color="white" data-image="assets_th1/img/sidebar-1.jpg">
                <!--
        Tip 1: You can change the color of the sidebar using: data-color="purple | azure | green | orange | danger"

        Tip 2: you can also add an image using data-image tag
    -->
                <div class="logo">
                    <img src="Images/hbl-logo-white-bk.png" />

                </div>

                <div class="sidebar-wrapper">
                    <ul class="nav">
                        <li class="nav-item active ">
                            <a class="nav-link" href="CLdashboard.aspx">
                                <i class="material-icons">dashboard</i>
                                <p>Segment</p>
                            </a>
                        </li>
                        <li class="nav-item text-dark">
                            <a class="nav-link" href="CLDashboardRegion.aspx">
                                <i class="material-icons">person</i>
                                <p>Region</p>
                            </a>
                        </li>
                        <li class="nav-item text-dark">
                            <a class="nav-link" href="CLDashboardSAM.aspx">
                                <i class="material-icons">library_books</i>
                                <p>SAM</p>
                            </a>
                        </li>
                        <li class="nav-item text-dark">
                            <a class="nav-link" href="CLDashboardArea.aspx">
                                <i class="material-icons">location_ons</i>
                                <p>Area</p>
                            </a>
                        </li>
                        <li class="nav-item text-dark">
                            <a class="nav-link" href="CLDashboardBranch.aspx">
                                <i class="material-icons">location_ons</i>
                                <p>Branches</p>
                            </a>
                        </li>
                        <li class="nav-item ">
                            <a class="nav-link" href="UserManagement.aspx">
                                <i class="material-icons"></i>
                                <p>Home Page</p>
                            </a>
                        </li>
                        <li class="nav-item text-dark">
                            <a class="nav-link" href="Login.aspx">
                                <i class="material-icons"></i>
                                <p>Logout</p>
                            </a>
                        </li>

                        <li class="nav-item text-success ">
                            <a class="nav-link text-success" href="#">
                                <asp:Label ID="lblUser" runat="server" Text="Login UserID"></asp:Label>
                            </a>
                        </li>
                        <li class="nav-item "></li>


                    </ul>
                </div>
            </div>
            <div class="main-panel">
                <!-- Navbar -->
                <nav class="navbar navbar-expand-lg navbar-transparent navbar-absolute fixed-top ">
                    <div class="container-fluid">
                        <div class="navbar-wrapper">
                            <a class="navbar-brand" href="#">PEP MIS Dashboard </a>
                        </div>
                        <%--<button class="navbar-toggler" type="button" data-toggle="collapse" aria-controls="navigation-index" aria-expanded="false" aria-label="Toggle navigation">
                            <span class="sr-only">Toggle navigation</span>
                            <span class="navbar-toggler-icon icon-bar"></span>
                            <span class="navbar-toggler-icon icon-bar"></span>
                            <span class="navbar-toggler-icon icon-bar"></span>
                        </button>--%>
                        <div class="collapse navbar-collapse justify-content-end">

                            <ul class="navbar-nav">
                                <li class="nav-item">Year:
                                    <asp:DropDownList ID="DD_Year" runat="server" class="dropdown btn btn-secondary dropdown-toggle"></asp:DropDownList>
                                </li>
                                <li class="nav-item">Month:
                                   <asp:DropDownList ID="DD_Month" runat="server" class="dropdown btn btn-secondary dropdown-toggle"></asp:DropDownList>
                                </li>
                                <li class="nav-item">Day:
                                   <asp:DropDownList ID="DD_Day" runat="server" class="dropdown btn btn-secondary dropdown-toggle"></asp:DropDownList>
                                </li>

                            </ul>

                            <ul class="navbar-nav">
                                <li class="nav-item">

                                    <asp:Button ID="btnSearch" runat="server" class="btn btn-white btn-round btn-just-icon" Text="Load" OnClick="btnSearch_Click" Font-Size="XX-Small" />
                                    <%--<i class="material-icons">search</i>
                                    <div class="ripple-container"></div>--%>
                                </li>
                            </ul>
                        </div>
                    </div>
                </nav>
                <!-- End Navbar -->

                <div class="content">

                    <div class="container-fluid">


                        <div class="row">

                            <div class="col-md-4">
                                <div class="card card-stats">
                                    <div class="card-header card-header-success card-header-icon">
                                        <div class="card-icon">
                                            <i class="material-icons">content_copy</i>
                                        </div>
                                        <h3 class="card-title text-success">Total Open CIF</></h3>
                                        <h3 class="card-title">
                                            <asp:Label runat="server" ID="lblh5dist" Text=""></asp:Label>
                                        </h3>

                                    </div>

                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card card-stats">
                                    <div class="card-header card-header-warning card-header-icon">
                                        <div class="card-icon">
                                            <i class="material-icons">store</i>
                                        </div>
                                        <h5 class="card-title text-success">Open CIF (22-July-2018 to 24-July-2019)</></h5>
                                        <h3 class="card-title">
                                            <asp:Label runat="server" ID="lblh5com" Text=""></asp:Label>

                                        </h3>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card card-stats">
                                    <div class="card-header card-header-info card-header-icon">
                                        <div class="card-icon">
                                            <i class="fa fa-twitter"></i>
                                        </div>
                                        <h5 class="card-title text-success">Open CIF (After 25-July-2019)</></h5>
                                        <h3 class="card-title">
                                            <asp:Label runat="server" ID="lblh5cor" Text=""></asp:Label>
                                        </h3>
                                    </div>

                                </div>
                            </div>

                        </div>
                        <!-- Table Dynamic -->
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card">
                                    <div class="card-header card-header-success">
                                        <h4 class="card-title">Region Case with Aging</h4>

                                    </div>
                                    <div class="card-body table-responsive">
                                        <asp:Table ID="myTable" runat="server" class="table table-hover" GridLines="Vertical" BorderStyle="Solid">
                                            <asp:TableRow CssClass="text-warning">
                                                <asp:TableCell>Region</asp:TableCell>
                                                <asp:TableCell>Total Score </asp:TableCell>
                                                <asp:TableCell>1-2 Days </asp:TableCell>
                                                <asp:TableCell>3-5 Days</asp:TableCell>
                                                <asp:TableCell>6- 10 Days</asp:TableCell>
                                                <asp:TableCell>11 - 15 Days</asp:TableCell>
                                                <asp:TableCell>16+ Days</asp:TableCell>
                                                <asp:TableCell>PEP</asp:TableCell>
                                                <asp:TableCell>Top 100 Depositor</asp:TableCell>
                                                <asp:TableCell>Rank</asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card">
                                    <div class="card-header card-header-warning">
                                        <h4 class="card-title">Region Score (Commercial)</h4>
                                        <p class="card-category">Detail of Score</p>
                                    </div>
                                    <div class="card-body table-responsive">
                                        <asp:Table ID="mytable2" runat="server" class="table table-hover" GridLines="Vertical" BorderStyle="Solid">
                                            <asp:TableRow CssClass="text-warning">
                                                <asp:TableCell>Region</asp:TableCell>
                                                <asp:TableCell>Status</asp:TableCell>
                                                <asp:TableCell># Branches </asp:TableCell>
                                                <asp:TableCell>Total </asp:TableCell>
                                                <asp:TableCell>Alert </asp:TableCell>
                                                <asp:TableCell>ClassRoom </asp:TableCell>
                                                <asp:TableCell>E-Learning</asp:TableCell>
                                                <asp:TableCell>KYC</asp:TableCell>
                                                <asp:TableCell>Branch Onsite</asp:TableCell>
                                                <asp:TableCell>PEP</asp:TableCell>
                                                <asp:TableCell>Top 100 Depositor</asp:TableCell>
                                                <asp:TableCell>Rank</asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </div>
                                </div>
                            </div>
                        </div>
        
                        <!-- Table Dynamic END -->

                        <!-- Start of panel 1 -->
                        <div id="flip">Click to View Distribution Score Details</div>
                        <div id="panel">
                            <div class="row">

                                <div class="col-md-12">
                                    <div class="card">

                                        <div class="card-header card-header-success">
                                            Region Score Details (Distribution)
                                        </div>
                                        <div class="card-body table-responsive ">
                                            <asp:Button ID="btndownload" class="btn material-icons" runat="server" Text="Download" OnClick="btndownload_Click" />
                                            <asp:GridView ID="GridView1" runat="server" PageSize="20" AllowPaging="True" CellPadding="5" Font-Size="XX-Small" style="width:100%;" ForeColor="#333333" GridLines="Vertical" OnPageIndexChanging="GridView1_PageIndexChanging" >
                                                <AlternatingRowStyle BackColor="White" />
                                                <EditRowStyle BackColor="#7C6F57" />
                                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Right" />
                                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                                <SortedAscendingHeaderStyle BackColor="#246B61" />
                                                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                                <SortedDescendingHeaderStyle BackColor="#15524A" />
                                            </asp:GridView>
                                        </div>

                                    </div>
                                </div>

                            </div>
                        </div>
                        <!-- Panel toggle end -->


                    </div>
                </div>

            </div>
        </div>

        <!--   Core JS Files   -->
    </form>
    <!-- distrubution Threshold Chart by Region -->


    <script>
          $(document).ready(function(){
              $("#flip").click(function(){
                  $("#panel").slideToggle("slow");
              });
          });

          $(document).ready(function(){
              $("#flip1").click(function(){
                  $("#panel1").slideToggle("slow");
              });
          });
    </script>


</body>

</html>
