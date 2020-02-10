<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="HighchartDemo.aspx.cs" Inherits="WebDataEntry.HighchartDemo" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">--%>
<%--<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">--%>
<link href="bootstrap4/css/bootstrap.min.css" rel="stylesheet" />
<script src="bootstrap4/js/popper.min.js"></script>
<script src="bootstrap4/js/bootstrap.min.js"></script>

<%--<script src="Scripts/jquery-3.4.1.js"></script>--%>
<script src="Scripts/jquery-3.4.1.slim.js"></script>
<script src="highchart/highcharts.js"></script>
<script src="highchart/modules/exporting.js"></script>

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


<%--<div class="wrapper ">
    <div class="sidebar" data-color="purple" data-background-color="white" data-image="../assets/img/sidebar-1.jpg">

        <div class="sidebar-wrapper">
            <ul class="nav">
                <li class="nav-item active  ">
                    <a class="nav-link" href="./dashboard.html">
                        <i class="material-icons">dashboard</i>
                        <p>Dashboard</p>
                    </a>
                </li>
                <li class="nav-item ">
                    <a class="nav-link" href="./user.html">
                        <i class="material-icons">person</i>
                        <p>User Profile</p>
                    </a>
                </li>
            </ul>
        </div>
    </div>
</div>--%>
<!-- Create Grid rows and 3 columns per Row 
    also Create Card with border card inlcude Header, title and Text 
    Text of Card inlcude progress Bar
    -->
<form runat="server">

    <div class="container">
        <div class="row">
            <div class="col">
                <asp:dropdownlist id="DD_Year" runat="server" class="dropdown btn btn-secondary dropdown-toggle"></asp:dropdownlist>
                <asp:dropdownlist id="DD_Month" runat="server" class="dropdown btn btn-secondary dropdown-toggle"></asp:dropdownlist>
            </div>
            <div class="col">
            </div>
            <div class="col">
            </div>
        </div>

        <div class="row">

            <div class="col">
                <div class="card  border-success mb-3" style="max-width: 18rem;">
                    <div class="card-header text-white bg-success">
                        <h3>Distribution </h3>
                    </div>
                    <div class="card-body text-success">
                        <h5 class="card-title">Overall Score
                            <asp:label runat="server" id="lblh5dist" text=""></asp:label>
                        </h5>
                        <div class="card-text progress">
                            <div runat="server" id="pbardist" class="progress-bar bg-success" role="progressbar" style="width: 25%" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="col">
                <div class="card  border-success mb-3" style="max-width: 18rem;">
                    <div class="card-header text-white bg-success">
                        <h3>Commercial</h3>
                    </div>
                    <div class="card-body text-success">
                        <h5 class="card-title">Overall Score
                            <asp:label runat="server" id="lblh5com" text=""></asp:label>
                        </h5>
                        <div class="card-text progress">
                            <div runat="server" id="pbarcom" class="progress-bar bg-success" role="progressbar" style="width: 100%" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                        </div>
                    </div>
                </div>

            </div>
            <div class="col">
                <div class="card  border-success mb-3" style="max-width: 18rem;">
                    <div class="card-header text-white bg-success">
                        <h3>Corporate</h3>
                    </div>
                    <div class="card-body text-success">
                        <h5 class="card-title">Overall Score
                            <asp:label runat="server" id="lblh5cor" text=""></asp:label>
                        </h5>
                        <div class="card-text progress">
                            <div runat="server" id="pbarcor" class="progress-bar bg-success" role="progressbar" style="width: 75%" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col">
                <div id="chart1" style="width: 100%; height: 300px;"></div>
            </div>
            <div class="col">
                <div id="chart2" style="width: 100%; height: 300px;"></div>
            </div>
        </div>
        <div class="row">
        </div>
    </div>



</form>

<script type="text/javascript">
   
    Highcharts.chart('chart1', {
        chart: {
            type: 'bar'
        },
        title: {
            text: 'Threshold Status'
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
            title: {
                //text: 'Threshold Status counts',
                align: 'high'
            },
            labels: {
                overflow: 'Status'
            }
        },
        tooltip: {
            valueSuffix: 'Regions'
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
            color: 'green',
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
            text: 'Threshold Status'
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
            title: {
                text: 'Threshold Status counts',
                align: 'high'
            },
            labels: {
                overflow: 'Status'
            }
        },
        tooltip: {
            valueSuffix: 'Regions'
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
            color: 'green',
            data: <%= this.ChartData2 %>
            }]
    });

</script>


<%--</asp:Content>--%>
