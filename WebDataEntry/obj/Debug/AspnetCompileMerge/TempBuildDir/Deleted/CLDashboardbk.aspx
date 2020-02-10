<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CLDashboard.aspx.cs" Inherits="WebDataEntry.CLDashboardbk" %>


<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8" />
    <link rel="apple-touch-icon" sizes="76x76" href="assets_th1/img/apple-icon.png">
    <link rel="icon" type="image/png" href="assets_th1/img/favicon.png">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title>Compliance Ladder Dashboard
    </title>
    <meta content='width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0, shrink-to-fit=no' name='viewport' />
    <!--     Fonts and icons     -->
    <%--<link rel="stylesheet" type="text/css" href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700|Roboto+Slab:400,700|Material+Icons" />
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css">--%>

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
            background-color: #e5eecc;
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
                            <a class="nav-link" href="./dashboard.html">
                                <i class="material-icons">dashboard</i>
                                <p>Segment</p>
                            </a>
                        </li>
                        <li class="nav-item text-dark">
                            <a class="nav-link" href="./user.html">
                                <i class="material-icons">person</i>
                                <p>Region</p>
                            </a>
                        </li>
                        <li class="nav-item text-dark">
                            <a class="nav-link" href="./typography.html">
                                <i class="material-icons">library_books</i>
                                <p>SAM</p>
                            </a>
                        </li>
                        <li class="nav-item text-dark">
                            <a class="nav-link" href="./tables.html">
                                <i class="material-icons">location_ons</i>
                                <p>Area</p>
                            </a>
                        </li>
                        <li class="nav-item text-dark">
                            <a class="nav-link" href="./tables.html">
                                <i class="material-icons">location_ons</i>
                                <p>Branches</p>
                            </a>
                        </li>
                        <li class="nav-item ">
                            <a class="nav-link" href="./tables.html">
                                <i class="material-icons"></i>
                                <p>Home Page</p>
                            </a>
                        </li>
                        <li class="nav-item text-dark">
                            <a class="nav-link" href="./tables.html">
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
                            <a class="navbar-brand" href="#">Compliance Ladder Dashboard</a>
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

                            <div class="col-md-4">
                                <div class="card card-stats">
                                    <div class="card-header card-header-success card-header-icon">
                                        <div class="card-icon">
                                            <i class="material-icons">content_copy</i>
                                        </div>
                                        <h3 class="card-title text-success">Distribution</></h3>
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
                        <div class="row">
                            <div class="col-md-4">
                                <div class="card card-chart">
                                    <div class="card-header card-header-success">
                                        <%--<div class="ct-chart" id="dailySalesChart"></div>--%>
                                        Threshold by Region (Distribution)
                                    </div>
                                    <div class="card-body">
                                        <div id="chart1" style="width: 100%; height: 250px;"></div>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card card-chart">
                                    <div class="card-header card-header-success">
                                        <%--<div class="ct-chart" id="websiteViewsChart"></div>--%>
                                        Threshold by Branches (Distribution)
                                    </div>
                                    <div class="card-body">
                                        <div id="chart2" style="width: 100%; height: 250px;"></div>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card card-chart">
                                    <div class="card-header card-header-warning">
                                        <%--<div class="ct-chart" id="completedTasksChart"></div>--%>
                                        Threshold by Branches (Commercial)
                                    </div>
                                    <div class="card-body">
                                        <div id="chart3" style="width: 100%; height: 250px;"></div>
                                    </div>

                                </div>
                            </div>
                        </div>

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
                        <!-- Start of panel 1 -->
                        <div id="flip" class="bg-success text-dark">Click to See Details</div>
                        <div id="panel">
                            <div class="row">

                                <div class="col-md-12">
                                    <div class="card">

                                        <div class="card-header card-header-success">
                                            Region Score Detils (Distribution)
                                        </div>
                                        <div class="card-body table-responsive ">
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
                            <div class="row">
                                <div class="col-lg-6 col-md-12">
                                    <div class="card">
                                        <div class="card-header card-header-warning">
                                            <h4 class="card-title">Employees Stats</h4>
                                            <p class="card-category">New employees on 15th September, 2016</p>
                                        </div>
                                        <div class="card-body table-responsive">
                                            <table class="table table-hover">
                                                <thead class="text-warning">
                                                    <th>ID</th>
                                                    <th>Name</th>
                                                    <th>Salary</th>
                                                    <th>Country</th>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>1</td>
                                                        <td>Dakota Rice</td>
                                                        <td>$36,738</td>
                                                        <td>Niger</td>
                                                    </tr>

                                                </tbody>
                                            </table>
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

    <script type="text/javascript">
   
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
                data: <%= this.ChartData1 %>
                }]
        });

</script>

    <script type="text/javascript">
   
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
                categories: <%= this.ChartLabel3 %>,
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
                data: <%= this.ChartData3 %>
                }]
        });

</script>

    <script type="text/javascript">
   
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
