<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CLOtherCharts.aspx.cs" Inherits="WebDataEntry.CLOtherCharts" %>


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
    <script src="https://code.highcharts.com/modules/data.js"></script>
    <script src="https://code.highcharts.com/modules/drilldown.js"></script>

    <script src="https://code.highcharts.com/modules/networkgraph.js"></script>
    <script src="https://code.highcharts.com/modules/sankey.js"></script>


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
        /*@import 'https://code.highcharts.com/css/highcharts.css';*/
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
                                <li class="nav-item">Segment:  
                                   <asp:DropDownList ID="DD_Segment" runat="server" class="dropdown btn btn-secondary dropdown-toggle"></asp:DropDownList>
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

                            <div class="col-md-6">
                                <div class="card card-stats">
                                    <div class="card-header card-header-success card-header-icon">
                                        <div class="card-icon">
                                            <i class="material-icons">content_copy</i>
                                        </div>
                                        <h3 class="card-title text-success">YTD Score</></h3>
                                        <h3 class="card-title">
                                            <asp:Label runat="server" ID="lblh5dist" Text=""></asp:Label>
                                        </h3>

                                    </div>
                                    <div class="card-footer ">
                                        <div runat="server" id="pbardist" class="progress-bar bg-success" role="progressbar" style="width: 25%" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>

                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="card card-stats">
                                    <div class="card-header card-header-warning card-header-icon">
                                        <div class="card-icon">
                                            <i class="material-icons">store</i>
                                        </div>
                                        <h3 class="card-title text-success">MTD Score</></h3>
                                        <h3 class="card-title">
                                            <asp:Label runat="server" ID="lblh5mtd" Text=""></asp:Label>

                                        </h3>
                                    </div>
                                    <div class="card-footer">
                                        <div runat="server" id="pbarmtd" class="progress-bar bg-success" role="progressbar" style="width: 100%" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                    </div>
                                </div>
                            </div>
                            
                        </div>

                        
                        <!-- Table Dynamic -->
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card">
                                    <div class="card-header card-header-success">
                                        <h4 class="card-title">Monthly Score Breakup </h4>

                                    </div>
                                    <div class="card-body table-responsive">
                                        <asp:Table ID="myTable" runat="server" class="table table-hover" GridLines="Vertical" BorderStyle="Solid" Style="font-size: x-small">
                                            <asp:TableRow CssClass="text-warning">
                                                <asp:TableCell>Month</asp:TableCell>
                                                <asp:TableCell ColumnSpan="3">Alerts </asp:TableCell>
                                                <asp:TableCell ColumnSpan="3">STR </asp:TableCell>
                                                <asp:TableCell ColumnSpan="3">Classromm Trainings</asp:TableCell>
                                                <asp:TableCell ColumnSpan="3">e-Learning Trainings</asp:TableCell>
                                                <asp:TableCell ColumnSpan="3">KYC-Peroidic Review </asp:TableCell>
                                                <asp:TableCell ColumnSpan="3">Branch Onsite</asp:TableCell>
                                                <asp:TableCell ColumnSpan="2">PEP</asp:TableCell>
                                                <asp:TableCell ColumnSpan="2">Top100 Depositor</asp:TableCell>
                                                <asp:TableCell ColumnSpan="1">Score</asp:TableCell>


                                            </asp:TableRow>
                                            <asp:TableRow CssClass="text-warning">
                                                <asp:TableCell></asp:TableCell>
                                                <asp:TableCell>Sends </asp:TableCell>
                                                <asp:TableCell>Completed% </asp:TableCell>
                                                <asp:TableCell>Score</asp:TableCell>
                                                <asp:TableCell>Raised </asp:TableCell>
                                                <asp:TableCell>Converted</asp:TableCell>
                                                <asp:TableCell>Score</asp:TableCell>
                                                <asp:TableCell>Total </asp:TableCell>
                                                <asp:TableCell>Completed%</asp:TableCell>
                                                <asp:TableCell>Score</asp:TableCell>
                                                <asp:TableCell>Total </asp:TableCell>
                                                <asp:TableCell>Completed%</asp:TableCell>
                                                <asp:TableCell>Score</asp:TableCell>
                                                <asp:TableCell>Total </asp:TableCell>
                                                <asp:TableCell>Completed%</asp:TableCell>
                                                <asp:TableCell>Score</asp:TableCell>
                                                <asp:TableCell>Total </asp:TableCell>
                                                <asp:TableCell>Completed%</asp:TableCell>
                                                <asp:TableCell>Score</asp:TableCell>
                                                <asp:TableCell>Total</asp:TableCell>
                                                <asp:TableCell>Outstanding</asp:TableCell>
                                                <asp:TableCell>Completion%</asp:TableCell>
                                                <asp:TableCell>Score</asp:TableCell>
                                                <asp:TableCell>Total</asp:TableCell>

                                            </asp:TableRow>
                                        </asp:Table>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Table Dynamic -->

                        <!-- Start Sanky Charts-->
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card card-chart">
                                    <div class="card-header card-header-warning">
                                        Selected Month Region Score
                                    </div>
                                    <div class="card-body">
                                        <div id="charttot2" style="width: 100%; height: 350px;"></div>
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
                                        Status by Region (Distribution)
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
                                        Status by Branches (Distribution)
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
                                        Total Score by Region (Distribution)
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
                                        <div id="charttot21" style="width: 100%; height: 350px;"></div>
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



                        <div id="flip1" class="bg-success">Click to See More ....</div>
                        <div id="panel1">
                            <!--Table by Default-->
                            <!-- Distribution Chart by Region Score Consolidated -->
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="card">

                                        <div class="card-header card-header-success">
                                            Region Score (Distribution)
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
            //chart: {
            //    type: 'column',
            //    height: '100%'
            //},
            title: {
                text: ''
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                categories: <%= this.ChartAlertLabel %>,
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
                type: 'column',
                name: 'Total',
                showInLegend: true,    
                color: '#008C93',
                data: <%= this.ChartAlertTotal %>
                },
                {
                    type: 'column',
                    name: 'Closed',
                    showInLegend: true,    
                    color: '#FC142F',
                    data: <%= this.ChartAlertClosed %>
                    },
                {
                    type: 'pie',
                    innerSize: '50%',
                    name: 'Completed %',
                    showInLegend: true,    
                    color: '#00B22F',
                    //data: <%= this.ChartAlertRatio %>,
                    data: [90, 10],
                    center: [200, 80],
                    size: 50,
                    showInLegend: true,
                    dataLabels: {
                        enabled: true
                    }
                   
                },
                {
                    type: 'column',
                    innerSize: '50%',
                    name: 'Score',
                    showInLegend: true,    
                    color: '#00D257',
                    data: <%= this.ChartAlertScore %>,
                   
                    showInLegend: true,
                    dataLabels: {
                        enabled: true
                    }
                }
                
            ]
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
                type: 'bar',
                height: '100%'
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

    <!-- Sanky Chart MTD Total Score Only -->

    <script type="text/javascript">
        // Sanky Chart only for Total value
        Highcharts.chart('charttot2', {

            title: {
                text: ''
            },
            series: [{
                keys: ['from', 'to', 'weight'],
                dataLabels: {
                    enabled: true,
                    linkFormat: ''
                },
                data: [ <%= this.SankyChartMTDRegion %>
                   
                 ],
                type: 'sankey',
                name: 'Total Score'
            }]

        });

    </script>

    <script type="text/javascript">
        // Commercial Chart only for Total value
        Highcharts.chart('charttot21', {

            title: {
                text: ''
            },
            series: [{
                keys: ['from', 'to', 'weight'],
                
                data: [ 

['Peshawar','Bannu',94.79],
['Peshawar','Chitral',98.31],
['Peshawar','D.I. Khan',94.97],
['Peshawar','Hangu',97.47],
['Peshawar','Kohat',94.32],
['Peshawar','Peshawar-Cantt',90.86],
['Peshawar','Peshawar-City',94.99],
['Quetta','Makran',82.82],
['Quetta','Quetta City',78.91],
['Quetta','Zarghoon',83.16],
['Sahiwal','Burewala',100],
['Sahiwal','Chichawatni',94.52],
['Sahiwal','Kasur',96.11],
['Sahiwal','Okara',100],
['Sahiwal','Pakpattan',100],
['Sahiwal','Pattoki',100],
['Sahiwal','Sahiwal Commercial',93.64],
['Sahiwal','Sahiwal Rural',97.9],
['Sargodha','Gillwala',85.38],
['Sargodha','Jhung',90.03],
['Sargodha','Khushab',95.07],
['Sargodha','Kutchery Bazar',80.01],
['Sargodha','Mianwali',84.04],
['Sargodha','Paf',76.59],
['Sialkot','Cantt',74.13],
['Sialkot','Circular Road',80.9],
['Sialkot','City',84.72],
['Sialkot','Daska',85],
['Sialkot','Narowal',82.37],
['Sialkot','Neikapura',73.36],
['Sialkot','Shakarghar',77.25],
['Sukkur','Dadu',76.43],
['Sukkur','F.F.C. Mirpur Mathelo',66.35],
['Sukkur','Kashmore',68.47],
['Sukkur','Khairpur',82.68],
['Sukkur','Larkana',81.1],
['Sukkur','Sukkur',69.99]



                ],
                type: 'sankey',
                name: 'Total Score'
            }]

        });

    </script>

    <!-- Corporate Chart Total Score Only -->
    <script type="text/javascript">
        // Corporate Chart only for Total value
        Highcharts.addEvent(
    Highcharts.Series,
    'afterSetOptions',
    function (e) {
        var colors = Highcharts.getOptions().colors,
            i = 0,
            nodes = {};

        if (
            this instanceof Highcharts.seriesTypes.networkgraph &&
            e.options.id === '08'
        ) {
            e.options.data.forEach(function (link) {

                if (link[0] === '08') {
                    nodes['08'] = {
                        id: '08',
                        marker: {
                            radius: 20
                        }
                    };
                    nodes[link[1]] = {
                        id: link[1],
                        marker: {
                            radius: 10
                        },
                        color: colors[i++]
                    };
                } else if (nodes[link[0]] && nodes[link[0]].color) {
                    nodes[link[1]] = {
                        id: link[1],
                        color: nodes[link[0]].color
                    };
                }
            });

            e.options.nodes = Object.keys(nodes).map(function (id) {
                return nodes[id];
            });
        }
    }
);


        // high chart
        Highcharts.chart('charttot3', {
            chart: {
                type: 'networkgraph',
                height: '100%'
            },
            title: {
                text: ''
            },
            subtitle: {
                text: ''
            },
            plotOptions: {
                networkgraph: {
                    keys: ['from', 'to', 'weight'],
                    layoutAlgorithm: {
                        enableSimulation: true,
                        friction: -0.9
                    }
                }
            },
            series: [{
                dataLabels: {
                    enabled: true,
                    linkFormat: ''
                },
                id: '08',
                nodes: [{
                    id: 'Karachi',
                    color: 'red'
                }, {
                    id: 'Lahore',
                    color: 'orange'
                }],
                data: [ <%= this.SankyChartMTDRegion %>,
                    <%= this.SankyChartMTDArea %>
                    //['Proto Indo-European', 'Balto-Slavic'],
                    //['Proto Indo-European', 'Germanic'],
                    //['Proto Indo-European', 'Celtic'],
                    //['Proto Indo-European', 'Italic'],
                    //['Proto Indo-European', 'Hellenic'],
                    //['Proto Indo-European', 'Anatolian'],
               
                    //['South Slavic', 'Belarusian'],
                    //['South Slavic', 'Rusyn']
                ]
            }]
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
