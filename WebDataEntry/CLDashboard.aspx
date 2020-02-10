<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CLDashboard.aspx.cs" Inherits="WebDataEntry.CLDashboard" %>


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
                        <li class="nav-item text-dark active ">
                            <a class="nav-link" href="CLdashboard.aspx">
                                <i class="material-icons">dashboard</i>
                                <p>High Level Summary</p>
                            </a>
                        </li>
                        <li class="nav-item  ">
                            <a class="nav-link" href="CLdashboardbySegment.aspx">
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
                            <a class="navbar-brand" href="#">Compliance Ladder </a>
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
                                        <h3 class="card-title text-success">Retail</></h3>
                                        <h3 class="card-title">
                                            <asp:Label runat="server" ID="lblh5dist" Text=""></asp:Label>
                                        </h3>

                                    </div>
                                    <div class="card-footer ">
                                        <div runat="server" id="pbardist" class="progress-bar bg-success" role="progressbar" style="width: 25%" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>

                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card card-stats">
                                    <div class="card-header card-header-warning card-header-icon">
                                        <div class="card-icon">
                                            <i class="material-icons">store</i>
                                        </div>
                                        <h3 class="card-title text-success">Commercial</></h3>
                                        <h3 class="card-title">
                                            <asp:Label runat="server" ID="lblh5com" Text=""></asp:Label>

                                        </h3>
                                    </div>
                                    <div class="card-footer">
                                        <div runat="server" id="pbarcom" class="progress-bar bg-success" role="progressbar" style="width: 100%" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card card-stats">
                                    <div class="card-header card-header-info card-header-icon">
                                        <div class="card-icon">
                                            <i class="fa fa-twitter"></i>
                                        </div>
                                        <h3 class="card-title text-success">Corporate</></h3>
                                        <h3 class="card-title">
                                            <asp:Label runat="server" ID="lblh5cor" Text=""></asp:Label>
                                        </h3>
                                    </div>
                                    <div class="card-footer">
                                        <div runat="server" id="pbarcor" class="progress-bar bg-success" role="progressbar" style="width: 75%" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <!-- Start Region Threshold Charts-->
                        <div class="row">
                            <div class="col-md-4">
                                <div class="card card-chart">
                                    <div class="card-header card-header-success">
                                        <%--<div class="ct-chart" id="dailySalesChart"></div>--%>
                                        Status by Region (Retail)
                                    </div>
                                    <div class="card-body">
                                        <div id="chartreg1" style="width: 100%; height: 250px;"></div>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card card-chart">
                                    <div class="card-header card-header-warning">
                                        <%--<div class="ct-chart" id="websiteViewsChart"></div>--%>
                                        Status by Region (Commercial)
                                    </div>
                                    <div class="card-body">
                                        <div id="chartreg2" style="width: 100%; height: 250px;"></div>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card card-chart">
                                    <div class="card-header card-header-info">
                                        <%--<div class="ct-chart" id="completedTasksChart"></div>--%>
                                        Status by Region (Corporate)
                                    </div>
                                    <div class="card-body">
                                        <div id="chartreg3" style="width: 100%; height: 250px;"></div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <!-- Start Branch Threshold Charts-->
                        <div class="row">
                            <div class="col-md-4">
                                <div class="card card-chart">
                                    <div class="card-header card-header-success">
                                        <%--<div class="ct-chart" id="dailySalesChart"></div>--%>
                                        Status by Branches (Retail)
                                    </div>
                                    <div class="card-body">
                                        <div id="chart1" style="width: 100%; height: 250px;"></div>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card card-chart">
                                    <div class="card-header card-header-warning">
                                        <%--<div class="ct-chart" id="websiteViewsChart"></div>--%>
                                        Status by Branches (Commercial)
                                    </div>
                                    <div class="card-body">
                                        <div id="chart2" style="width: 100%; height: 250px;"></div>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card card-chart">
                                    <div class="card-header card-header-info">
                                        <%--<div class="ct-chart" id="completedTasksChart"></div>--%>
                                        Status by Branches (Corporate)
                                    </div>
                                    <div class="card-body">
                                        <div id="chart3" style="width: 100%; height: 250px;"></div>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <!-- Start Region Total Score only Charts-->
                        <div class="row">
                            <div class="col-md-4">
                                <div class="card card-chart">
                                    <div class="card-header card-header-success">
                                        <%--<div class="ct-chart" id="dailySalesChart"></div>--%>
                                        Total Score by Region (Retail)
                                    </div>
                                    <div class="card-body">
                                        <div id="charttot1" style="width: 100%; height: 350px;"></div>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card card-chart">
                                    <div class="card-header card-header-warning">
                                        Total Score by Region (Commercial)
                                    </div>
                                    <div class="card-body">
                                        <div id="charttot2" style="width: 100%; height: 350px;"></div>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card card-chart">
                                    <div class="card-header card-header-info">
                                        Total Score by Region (Corporate)
                                    </div>
                                    <div class="card-body">
                                        <div id="charttot3" style="width: 100%; height: 350px;"></div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <!-- Table Dynamic -->
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card">
                                    <div class="card-header card-header-success">
                                        <h4 class="card-title">Region Score (Retail)</h4>
                                       
                                    </div>
                                    <div class="card-body table-responsive">
                                        <asp:Table ID="myTable" runat="server" class="table table-hover" GridLines="Vertical" BorderStyle="Solid">
                                            <asp:TableRow CssClass="text-warning">
                                                <asp:TableCell>Region</asp:TableCell>
                                                <asp:TableCell>Status</asp:TableCell>
                                                <asp:TableCell># Branches </asp:TableCell>
                                                <asp:TableCell>Total Score </asp:TableCell>
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
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card">
                                    <div class="card-header card-header-info">
                                        <h4 class="card-title">Region Score (Corporate)</h4>
                                     
                                    </div>
                                    <div class="card-body table-responsive">
                                        <asp:Table ID="myTable3" runat="server" class="table table-hover" GridLines="Vertical" BorderStyle="Solid">
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
                        <div id="flip">Click to View Retail Score Details</div>
                        <div id="panel">
                            <div class="row">

                                <div class="col-md-12">
                                    <div class="card">

                                        <div class="card-header card-header-success">
                                            Region Score Details (Retail)
                                        </div>
                                        <div class="card-body table-responsive ">
                                            <asp:Button ID="btndownload" class="btn material-icons" runat="server" Text="Download" OnClick="btndownload_Click" />
                                            <asp:GridView ID="GridView1" runat="server" PageSize="20" AllowPaging="True" CellPadding="0" Font-Size="XX-Small" ForeColor="#333333" GridLines="Vertical" OnPageIndexChanging="GridView1_PageIndexChanging" Height="657px">
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


                        <div id="flip1" class="bg-success">Click to See More ....</div>
                        <div id="panel1">
                            <!--Table by Default-->
                            <!-- Retail Chart by Region Score Consolidated -->
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="card">

                                        <div class="card-header card-header-success">
                                            Region Score (Retail)
                                        </div>
                                        <div class="card-body table-responsive">

                                            <div id="chart4"></div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- Commercial Chart by Region Score Consolidated -->
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="card">

                                        <div class="card-header card-header-warning">
                                            Region Score (Commercial)
                                        </div>
                                        <div class="card-body table-responsive">

                                            <div id="chart5"></div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- Corporate Chart by Region Score Consolidated -->
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="card">

                                        <div class="card-header card-header-info">
                                            Region Score (Corporate)
                                        </div>
                                        <div class="card-body table-responsive">

                                            <div id="chart6"></div>

                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <!-- end of panel -->

                    </div>
                </div>

            </div>
        </div>

        <!--   Core JS Files   -->
    </form>
    <!-- distrubution Threshold Chart by Region -->

    <script type="text/javascript">
        //  Threshold distubution by Region
        Highcharts.chart('chartreg1', {
            chart: {
                type: 'bar'
            },
            title: {
                text: ''
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                categories: <%= this.ChartLabelRegdist %>,
                title: {
                    text: null
                }
            },
            yAxis: {
                min: 0,
                title: false,
                //title: {
                //    text: 'Threshold Status counts',
                //    align: 'high'
                //},
                labels: {
                    overflow: 'Status'
                }
            },
            tooltip: {
                valueSuffix: ' Regions'
            },
            plotOptions: {
                bar: {
                    dataLabels: {
                        enabled: true
                    }
                }
            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'top',
                x: -40,
                y: 80,
                floating: true,
                borderWidth: 1,
                backgroundColor: ((Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'),
                shadow: true
            },
            credits: {
                enabled: false
            },
            series: [{
                //name: '',
                showInLegend: false,    
                color: '#008C93',
                data: <%= this.ChartDataRegdist %>
                }]
        });

    </script>

    <script type="text/javascript">
        // Commercial Threshold by Region
        Highcharts.chart('chartreg2', {
            chart: {
                type: 'bar'
            },
            title: {
                text: ''
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                categories: <%= this.ChartLabelRegComm %>,
                title: {
                    text: null
                }
            },
            yAxis: {
                min: 0,
                title: false,
                //title: {
                //    text: 'Threshold Status counts',
                //    align: 'high'
                //},
                labels: {
                    overflow: 'Status'
                }
            },
            tooltip: {
                valueSuffix: ' Regions'
            },
            plotOptions: {
                bar: {
                    dataLabels: {
                        enabled: true
                    }
                }
            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'top',
                x: -40,
                y: 80,
                floating: true,
                borderWidth: 1,
                backgroundColor: ((Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'),
                shadow: true
            },
            credits: {
                enabled: false
            },
            series: [{
                //name: '',
                showInLegend: false,    
                color: '#008C93',
                data: <%= this.ChartDataRegCorp %>
                }]
        });

    </script>

    <script type="text/javascript">
        //corporate Threshold by Region
        Highcharts.chart('chartreg3', {
            chart: {
                type: 'bar'
            },
            title: {
                text: ''
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                categories: <%= this.ChartLabelRegCorp %>,
                title: {
                    text: null
                }
            },
            yAxis: {
                min: 0,
                title: false,
                //title: {
                //    text: 'Threshold Status counts',
                //    align: 'high'
                //},
                labels: {
                    overflow: 'Status'
                }
            },
            tooltip: {
                valueSuffix: ' Regions'
            },
            plotOptions: {
                bar: {
                    dataLabels: {
                        enabled: true
                    }
                }
            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'top',
                x: -40,
                y: 80,
                floating: true,
                borderWidth: 1,
                backgroundColor: ((Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'),
                shadow: true
            },
            credits: {
                enabled: false
            },
            series: [{
                //name: '',
                showInLegend: false,    
                color: '#008C93',
                data: <%= this.ChartDataRegCorp %>
                }]
        });

    </script>


    <script type="text/javascript">
        // distrubution Threshold Branch Data
        Highcharts.chart('chart1', {
            chart: {
                type: 'bar'
            },
            title: {
                text: ''
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                categories: <%= this.ChartLabel2 %>,
                title: {
                    text: null
                }
            },
            yAxis: {
                min: 0,
                title: false,
            
                labels: {
                    overflow: 'Status'
                }
            },
            tooltip: {
                valueSuffix: ' Branches'
            },
            plotOptions: {
                bar: {
                    dataLabels: {
                        enabled: true
                    }
                }
            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'top',
                x: -40,
                y: 80,
                floating: true,
                borderWidth: 1,
                backgroundColor: ((Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'),
                shadow: true
            },
            credits: {
                enabled: false
            },
            series: [{
                color: '#008C93',
                showInLegend: false,   
                data: <%= this.ChartData2 %>
                }]
        });

    </script>


    <script type="text/javascript">
        // Commercial Threshold Branch Data
        Highcharts.chart('chart2', {
            chart: {
                type: 'bar'
            },
            title: {
                text: ''
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                categories: <%= this.ChartLabel3 %>,
                title: {
                    text: null
                }
            },
            yAxis: {
                min: 0,
                title: false,
            
                labels: {
                    overflow: 'Status'
                }
            },
            tooltip: {
                valueSuffix: ' Branches'
            },
            plotOptions: {
                bar: {
                    dataLabels: {
                        enabled: true
                    }
                }
            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'top',
                x: -40,
                y: 80,
                floating: true,
                borderWidth: 1,
                backgroundColor: ((Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'),
                shadow: true
            },
            credits: {
                enabled: false
            },
            series: [{
                color: '#008C93',
                showInLegend: false,   
                data: <%= this.ChartData3 %>
                }]
        });

    </script>

    <script type="text/javascript">
        // corporate threshold by branch
        Highcharts.chart('chart3', {
            chart: {
                type: 'bar'
            },
            title: {
                text: ''
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                categories: <%= this.ChartLabels %>,
                title: {
                    text: null
                }
            },
            yAxis: {
                min: 0,
                title: false,
                //title: {
                //    text: 'Threshold Status counts',
                //    align: 'high'
                //},
                labels: {
                    overflow: 'Status'
                }
            },
            tooltip: {
                valueSuffix: ' Branches'
            },
            plotOptions: {
                bar: {
                    dataLabels: {
                        enabled: true
                    }
                }
            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'top',
                x: -40,
                y: 80,
                floating: true,
                borderWidth: 1,
                backgroundColor: ((Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'),
                shadow: true
            },
            credits: {
                enabled: false
            },
            series: [{
                //name: '',
                showInLegend: false,    
                color: '#008C93',
                data: <%= this.ChartData1 %>
                }]
        });

    </script>

    <script type="text/javascript">
        // distibution chart consolidated
        Highcharts.chart('chart4', {
            chart: {
                type: 'column'
            },
            title: {
                text: ''
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                categories: <%= this.ChartLabel4 %>,
                title: {
                    text: null
                }
            },
            yAxis: {
                min: 0,
                title: false,
                //title: {
                //    text: 'Threshold Status counts',
                //    align: 'high'
                //},
                labels: {
                    overflow: 'Status'
                }
            },
            tooltip: {
                valueSuffix: ' Values'
            },
            plotOptions: {
                bar: {
                    dataLabels: {
                        enabled: true
                    }
                }
            },
            legend: {
                //layout: 'vertical',
                align: 'left',
                verticalAlign: 'top',
                x: 30,
                //y: 80,
                floating: true,
                borderWidth: 1,
                backgroundColor: ((Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'),
                shadow: true
            },
            credits: {
                enabled: false
            },
            series: [{
                name: 'Total Score',
                showInLegend: true,    
                color: '#008C93',
                data: <%= this.ChartData4 %>
                },
                {
                    name: 'Alert',
                    showInLegend: true,    
                    color: '#FC142F',
                    data: <%= this.ChartData4s2 %>
                    },
                {
                    name: 'Classroom Training',
                    showInLegend: true,    
                    color: '#00B22F',
                    data: <%= this.ChartData4s3 %>
                    },
                {
                    name: 'E-Learning Training',
                    showInLegend: true,    
                    color: '#00D257',
                    data: <%= this.ChartData4s4 %>
                    },
                {
                    name: 'KYC',
                    showInLegend: true,    
                    color: '#1E489C',
                    data: <%= this.ChartData4s5 %>
                    },
                {
                    name: 'Branch Onsite Reveiew',
                    showInLegend: true,    
                    color: '#1E9DFF',
                    data: <%= this.ChartData4s6 %>
                    },
                {
                    name: 'Top 100 ',
                    showInLegend: true,    
                    color: '#FF9D4E',
                    data: <%= this.ChartData4s7 %>
                    }
                
            ]
        });

    </script>

    <script type="text/javascript">
        // Commercial chart consolidated
        Highcharts.chart('chart5', {
            chart: {
                type: 'column'
            },
            title: {
                text: ''
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                categories: <%= this.ChartLabel5 %>,
                title: {
                    text: null
                }
            },
            yAxis: {
                min: 0,
                title: false,
                //title: {
                //    text: 'Threshold Status counts',
                //    align: 'high'
                //},
                labels: {
                    overflow: 'Status'
                }
            },
            tooltip: {
                valueSuffix: ' Values'
            },
            plotOptions: {
                bar: {
                    dataLabels: {
                        enabled: true
                    }
                }
            },
            legend: {
                //layout: 'vertical',
                align: 'left',
                verticalAlign: 'top',
                x: 30,
                //y: 80,
                floating: true,
                borderWidth: 1,
                backgroundColor: ((Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'),
                shadow: true
            },
            credits: {
                enabled: false
            },
            series: [{
                name: 'Total Score',
                showInLegend: true,    
                color: '#008C93',
                data: <%= this.ChartData5 %>
                },
                {
                    name: 'Alert',
                    showInLegend: true,    
                    color: '#FC142F',
                    data: <%= this.ChartData5s2 %>
                    },
                {
                    name: 'Classroom Training',
                    showInLegend: true,    
                    color: '#00B22F',
                    data: <%= this.ChartData5s3 %>
                    },
                {
                    name: 'E-Learning Training',
                    showInLegend: true,    
                    color: '#00D257',
                    data: <%= this.ChartData5s4 %>
                    },
                {
                    name: 'KYC',
                    showInLegend: true,    
                    color: '#1E489C',
                    data: <%= this.ChartData5s5 %>
                    },
                {
                    name: 'Branch Onsite Reveiew',
                    showInLegend: true,    
                    color: '#1E9DFF',
                    data: <%= this.ChartData5s6 %>
                    },
                {
                    name: 'Top 100 ',
                    showInLegend: true,    
                    color: '#FF9D4E',
                    data: <%= this.ChartData5s7 %>
                    }
                
            ]
        });

    </script>

    <script type="text/javascript">
        // Corporate chart consolidated
        Highcharts.chart('chart6', {
            chart: {
                type: 'column'
            },
            title: {
                text: ''
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                categories: <%= this.ChartLabel6 %>,
                title: {
                    text: null
                }
            },
            yAxis: {
                min: 0,
                title: false,
                //title: {
                //    text: 'Threshold Status counts',
                //    align: 'high'
                //},
                labels: {
                    overflow: 'Status'
                }
            },
            tooltip: {
                valueSuffix: ' Values'
            },
            plotOptions: {
                bar: {
                    dataLabels: {
                        enabled: true
                    }
                }
            },
            legend: {
                //layout: 'vertical',
                align: 'left',
                verticalAlign: 'top',
                x: 30,
                //y: 80,
                floating: true,
                borderWidth: 1,
                backgroundColor: ((Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'),
                shadow: true
            },
            credits: {
                enabled: false
            },
            series: [{
                name: 'Total Score',
                showInLegend: true,    
                color: '#008C93',
                data: <%= this.ChartData6 %>
                },
                {
                    name: 'Alert',
                    showInLegend: true,    
                    color: '#FC142F',
                    data: <%= this.ChartData6s2 %>
                    },
                {
                    name: 'Classroom Training',
                    showInLegend: true,    
                    color: '#00B22F',
                    data: <%= this.ChartData6s3 %>
                    },
                {
                    name: 'E-Learning Training',
                    showInLegend: true,    
                    color: '#00D257',
                    data: <%= this.ChartData6s4 %>
                    },
                {
                    name: 'KYC',
                    showInLegend: true,    
                    color: '#1E489C',
                    data: <%= this.ChartData6s5 %>
                    },
                {
                    name: 'Branch Onsite Reveiew',
                    showInLegend: true,    
                    color: '#1E9DFF',
                    data: <%= this.ChartData6s6 %>
                    },
                {
                    name: 'Top 100 ',
                    showInLegend: true,    
                    color: '#FF9D4E',
                    data: <%= this.ChartData6s7 %>
                    }
                
            ]
        });

    </script>


    <script type="text/javascript">
        // distibution Chart only for Total value
        Highcharts.chart('charttot1', {
            chart: {
                type: 'bar'
            },
            title: {
                text: ''
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                categories: <%= this.ChartLabel4 %>,
                title: {
                    text: null
                }
            },
            yAxis: {
                min: 0,
                title: false,
                //title: {
                //    text: 'Threshold Status counts',
                //    align: 'high'
                //},
                labels: {
                    overflow: 'Status'
                }
            },
            tooltip: {
                valueSuffix: ' Values'
            },
            plotOptions: {
                bar: {
                    dataLabels: {
                        enabled: true
                    }
                }
            },
            legend: {
                //layout: 'vertical',
                align: 'left',
                verticalAlign: 'top',
                x: 30,
                //y: 80,
                floating: true,
                borderWidth: 1,
                backgroundColor: ((Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'),
                shadow: true
            },
            credits: {
                enabled: false
            },
            series: [{
                name: '',
                showInLegend: false,    
                color: '#008C93',
                data: <%= this.ChartData4 %>
                }
            ]
        });

    </script>

    <!-- Commercial Chart Total Score Only -->

    <script type="text/javascript">
        // Commercial Chart only for Total value
        Highcharts.chart('charttot2', {
            chart: {
                type: 'bar'
            },
            title: {
                text: ''
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                categories: <%= this.ChartLabel5 %>,
                title: {
                    text: null
                }
            },
            yAxis: {
                min: 0,
                title: false,
                //title: {
                //    text: 'Threshold Status counts',
                //    align: 'high'
                //},
                labels: {
                    overflow: 'Status'
                }
            },
            tooltip: {
                valueSuffix: ' Values'
            },
            plotOptions: {
                bar: {
                    dataLabels: {
                        enabled: true
                    }
                }
            },
            legend: {
                //layout: 'vertical',
                align: 'left',
                verticalAlign: 'top',
                x: 30,
                //y: 80,
                floating: true,
                borderWidth: 1,
                backgroundColor: ((Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'),
                shadow: true
            },
            credits: {
                enabled: false
            },
            series: [{
                name: '',
                showInLegend: false,    
                color: '#008C93',
                data: <%= this.ChartData5 %>
                }
            ]
        });

    </script>

    <!-- Corporate Chart Total Score Only -->
    <script type="text/javascript">
        // Corporate Chart only for Total value
        Highcharts.chart('charttot3', {
            chart: {
                type: 'bar'
            },
            title: {
                text: ''
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                categories: <%= this.ChartLabel6 %>,
                title: {
                    text: null
                }
            },
            yAxis: {
                min: 0,
                title: false,
                //title: {
                //    text: 'Threshold Status counts',
                //    align: 'high'
                //},
                labels: {
                    overflow: 'Status'
                }
            },
            tooltip: {
                valueSuffix: ' Values'
            },
            plotOptions: {
                bar: {
                    dataLabels: {
                        enabled: true
                    }
                }
            },
            legend: {
                //layout: 'vertical',
                align: 'left',
                verticalAlign: 'top',
                x: 30,
                //y: 80,
                floating: true,
                borderWidth: 1,
                backgroundColor: ((Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'),
                shadow: true
            },
            credits: {
                enabled: false
            },
            series: [{
                name: '',
                showInLegend: false,    
                color: '#008C93',
                data: <%= this.ChartData6 %>
                }
            ]
        });

    </script>

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
