<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CLDashboardRegion.aspx.cs" Inherits="WebDataEntry.CLDashboardRegion" %>


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
                        <li class="nav-item text-dark  ">
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
                        <li class="nav-item active">
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
                                <li class="nav-item">Region:
                                   <asp:DropDownList ID="DD_Region" runat="server" class="dropdown btn btn-secondary dropdown-toggle"></asp:DropDownList>
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
                                        <h3 class="card-title text-success">Acheived Score</></h3>
                                        <h4 class="card-title">
                                            <asp:Label runat="server" ID="lblh5dist" Text=""></asp:Label>

                                        </h4>
                                        <h4 class="card-title">
                                            <asp:Label runat="server" ID="lblwbscore" Text=""></asp:Label>
                                        </h4>

                                    </div>
                                    <%--<div class="card-footer ">
                                        <div runat="server" id="pbardist" class="progress-bar bg-success" role="progressbar" style="width: 25%" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>

                                    </div>--%>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card card-stats">
                                    <div class="card-header card-header-warning card-header-icon">
                                        <div class="card-icon">
                                            <i class="material-icons">store</i>
                                        </div>
                                        <h3 class="card-title text-success">Status</></h3>
                                        <h4 class="card-title">
                                            <asp:Label runat="server" ID="lblh5com" Text=""></asp:Label>
                                        </h4>
                                        <h4 class="card-title">
                                            <asp:Label runat="server" ID="lblRank" Text=""></asp:Label>
                                        </h4>
                                    </div>
                                    <%--<div class="card-footer">
                                        
                                        Rank <asp:Label runat="server" ID="lblrank" Text=""></asp:Label>
                                    </div>--%>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card card-stats">
                                    <div class="card-header card-header-info card-header-icon">
                                        <div class="card-icon">
                                            <i class="fa fa-twitter"></i>
                                        </div>
                                        <h3 class="card-title text-success">
                                            <asp:Label runat="server" ID="lblsegment" Text=""></asp:Label></></h3>
                                        <h4 class="card-title">
                                            <asp:Label runat="server" ID="lblregion" Text=""></asp:Label>
                                        </h4>
                                        <h4 class="card-title">
                                            <asp:Label runat="server" ID="Label1" Text=" "></asp:Label>
                                        </h4>
                                    </div>
                                    <%--<div class="card-footer">
                                        <div runat="server" id="pbarcor" class="progress-bar bg-success" role="progressbar" style="width: 75%" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                    </div>--%>
                                </div>
                            </div>

                        </div>
                        <!-- Start Region Threshold Charts-->
                        <div class="row">
                            <div class="col-md-4">
                                <div class="card card-chart">
                                    <div class="card-header card-header-success">
                                        <%--<div class="ct-chart" id="dailySalesChart"></div>--%>
                                        Status by Branches
                                    </div>
                                    <div class="card-body">
                                        <div id="chartthresholdbranch" style="width: 100%; height: 260px;"></div>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card card-chart">
                                    <div class="card-header card-header-warning">
                                        <%--<div class="ct-chart" id="websiteViewsChart"></div>--%>
                                        Mantas Alerts
                                    </div>
                                    <div class="card-body">
                                        <table class="table table-hover">
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Total Alerts</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lbltotalalerts"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Completed</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblclosedalerts"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Acheivement</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblratioalerts"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Acheived Score</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblscorealerts"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Maximum Score</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblmaxalert"></asp:Label></h4>
                                                </td>
                                            </tr>
                                        </table>


                                    </div>

                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card card-chart">
                                    <div class="card-header card-header-info">
                                        <%--<div class="ct-chart" id="completedTasksChart"></div>--%>
                                        STR
                                    </div>
                                    <div class="card-body">
                                        <table class="table table-hover">
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Total STR Raised</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lbltotalstr"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">STR Converted</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblconvertedstr"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">STR Score</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblscorestr"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Acheived Score</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblscorepstr"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Bonus Score</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblmaxpstr"></asp:Label></h4>
                                                </td>
                                            </tr>
                                        </table>

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
                                        Classroom Training
                                    </div>
                                    <div class="card-body">
                                        <table class="table table-hover">
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Total Training</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblCLtotal"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Completed</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblCLClosed"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Acheivement</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblCLRatio"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Acheived Score</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblCLScore"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Maximum Score</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblmaxclscore"></asp:Label></h4>
                                                </td>
                                            </tr>
                                        </table>

                                    </div>

                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card card-chart">
                                    <div class="card-header card-header-warning">
                                        <%--<div class="ct-chart" id="websiteViewsChart"></div>--%>
                                        e-Learning Training
                                    </div>
                                    <div class="card-body">
                                        <table class="table table-hover">
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Total Training</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblELtotal"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Completed</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblELClosed"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Acheivement</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblELRatio"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Acheived Score</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblELScore"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Maximum Score</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblelmaxscore"></asp:Label></h4>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card card-chart">
                                    <div class="card-header card-header-info">
                                        KYC - Periodic Review
                                    </div>
                                    <div class="card-body">
                                        <table class="table table-hover">
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Total Reviews</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblKYCtotal"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Completed</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblKYCclosed"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Acheivement</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblKYCratio"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Acheived Score</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblKYCScore"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Maximum Score</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblmaxkycscore"></asp:Label></h4>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <!-- Start KYC L, M, H cart add on 15-Jan-2020 -->
                        <div class="row">
                            <div class="col-md-4">
                                <div class="card card-chart">
                                    <div class="card-header card-header-success">
                                        <%--<div class="ct-chart" id="dailySalesChart"></div>--%>
                                        KYC - High Risk
                                    </div>
                                    <div class="card-body">
                                        <table class="table table-hover">
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Total Reviews</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblKYCHtotal"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Completed</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblKYCHclosed"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Acheived Score</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblKYCHscore"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            
                                        </table>

                                    </div>

                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card card-chart">
                                    <div class="card-header card-header-warning">
                                        <%--<div class="ct-chart" id="websiteViewsChart"></div>--%>
                                        KYC - Medium Risk
                                    </div>
                                    <div class="card-body">
                                        <table class="table table-hover">
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Total Reviews</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblKYCMtotal"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Completed</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblKYCMclosed"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Acheived Score</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblKYCMscore"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card card-chart">
                                    <div class="card-header card-header-info">
                                        KYC - Low Risk
                                    </div>
                                    <div class="card-body">
                                        <table class="table table-hover">
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Total Reviews</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblKYCLtotal"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Completed</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblKYCLclosed"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Acheived Score</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblKYCLscore"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <!-- Start Region Total Score only Charts-->
                        <div class="row">
                            <div class="col-md-4">
                                <div class="card card-chart">
                                    <div class="card-header card-header-success">
                                        Branch Onsite Reviews
                                    </div>
                                    <div class="card-body">
                                        <table class="table table-hover">
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Total Reviews</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblBRTotal"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Completed</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblBRclosed"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Acheivement</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblBRratio"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Acheived Score</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblBRscore"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Maximum Score</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblmaxbrscore"></asp:Label></h4>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card card-chart">
                                    <div class="card-header card-header-warning">
                                        PEP / NGO
                                    </div>
                                    <div class="card-body">
                                        <table class="table table-hover">
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Total PEP</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblPEPtotal"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Closed</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblPEPClosed"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Acheivement</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblPEPratio"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Deducted Score</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblPEPscore"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Maximum Score</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblmaxpepscore"></asp:Label></h4>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card card-chart">
                                    <div class="card-header card-header-info">
                                        Top 100 Depositor
                                    </div>
                                    <div class="card-body">
                                        <table class="table table-hover">
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">- </h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">- </h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">- </h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">- </h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Acheivement</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lbltop100ratio"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Acheived Score</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lbltop100score"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Maximum Score</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lblmaxtop100score"></asp:Label></h4>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <!-- Start top 100 RMC, Self, KYC add on 15-Jan-2020 -->
                        <div class="row">
                            <div class="col-md-4">
                                <div class="card card-chart">
                                    <div class="card-header card-header-success">
                                        <%--<div class="ct-chart" id="dailySalesChart"></div>--%>
                                        Top 100 - RMC Observations
                                    </div>
                                    <div class="card-body">
                                        <table class="table table-hover">
                                            
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Acheived Score</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lbltop100RMCscore"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            
                                        </table>

                                    </div>

                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card card-chart">
                                    <div class="card-header card-header-warning">
                                        <%--<div class="ct-chart" id="websiteViewsChart"></div>--%>
                                        Top 100 - Self Identified
                                    </div>
                                    <div class="card-body">
                                        <table class="table table-hover">
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Acheived Score</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lbltop100Selfscore"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card card-chart">
                                    <div class="card-header card-header-info">
                                        Top 100 - KYC Review
                                    </div>
                                    <div class="card-body">
                                        <table class="table table-hover">
                                            <tr>
                                                <td style="width: 70%">
                                                    <h4 class="card-title text-success">Acheived Score</h4>
                                                </td>
                                                <td style="width: 30%;">
                                                    <h4 class="card-title text-success" style="text-align: right">
                                                        <asp:Label runat="server" ID="lbltop100KYCscore"></asp:Label></h4>
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <!-- Table Dynamic -->
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card">
                                    <div class="card-header card-header-success">
                                        <h4 class="card-title">Area's Score </h4>

                                    </div>
                                    <div class="card-body table-responsive">
                                        <asp:Table ID="myTable" runat="server" class="table table-hover" GridLines="Vertical" BorderStyle="Solid">
                                            <asp:TableRow CssClass="text-warning">
                                                <asp:TableCell>Area Name</asp:TableCell>
                                                <%--<asp:TableCell>Status</asp:TableCell>--%>
                                                <asp:TableCell>Number of Branches </asp:TableCell>
                                                <asp:TableCell>Acheived Score </asp:TableCell>
                                                <asp:TableCell>Alert </asp:TableCell>
                                                <asp:TableCell>ClassRoom </asp:TableCell>
                                                <asp:TableCell>E-Learning</asp:TableCell>
                                                <asp:TableCell>KYC</asp:TableCell>
                                                <asp:TableCell>Branch Onsite</asp:TableCell>
                                                <asp:TableCell>PEP</asp:TableCell>
                                                <asp:TableCell>Top100 Depositor </asp:TableCell>
                                                <asp:TableCell>STR Score</asp:TableCell>
                                                <%--<asp:TableCell>Rank</asp:TableCell>--%>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Table Dynamic -->
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card">
                                    <div class="card-header card-header-success">
                                        <h4 class="card-title">Branch Scores </h4>

                                    </div>
                                    <div class="card-body table-responsive">
                                        <asp:Table ID="tblBranch" runat="server" class="table table-hover" GridLines="Vertical" BorderStyle="Solid">
                                            <asp:TableRow CssClass="text-warning">
                                                <asp:TableCell>Branch Code</asp:TableCell>
                                                <asp:TableCell>Branch Name</asp:TableCell>

                                                <asp:TableCell HorizontalAlign="Center">Acheived Score </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Center">Alert </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Center">ClassRoom</asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Center">E-Learning</asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Center">KYC</asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Center">Branch Onsite</asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Center">PEP</asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Center">Top100 Depositor</asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Center">STR</asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Center">Region Rank</asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Table Dynamic END -->

                        <!-- Start of panel 1 -->
                        <div id="flip">Click to View Details</div>
                        <div id="panel">
                            <div class="row">

                                <div class="col-md-12">
                                    <div class="card">

                                        <div class="card-header card-header-success">
                                            Region Score Details
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


                        <%--                        <div id="flip1" class="bg-success">Click to See More ....</div>
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
                            
                        </div>--%>
                        <!-- end of panel -->

                    </div>
                </div>

            </div>
        </div>

        <!--   Core JS Files   -->
    </form>
    <!-- distrubution Threshold Chart by Region -->

    <script type="text/javascript">
        // distibution chart consolidated
        <%--       Highcharts.chart('chart4', {
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
        });--%>

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

    <!-- Chart for Threshold Count by Branch -->
    <script type="text/javascript">
        // distrubution Threshold Branch Data
        Highcharts.chart('chartthresholdbranch', {
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
                categories: <%= this.ChartLabelThBranch %>,
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
                column: {
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
                data: <%= this.ChartDataThBranch %>
                }]
        });

    </script>


</body>

</html>
