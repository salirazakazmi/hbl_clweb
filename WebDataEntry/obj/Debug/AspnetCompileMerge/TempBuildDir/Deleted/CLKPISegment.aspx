<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_DE.Master" AutoEventWireup="true" CodeBehind="CLKPISegment.aspx.cs" Inherits="WebDataEntry.CLKPISegment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

    
    
    <!-- Create Grid rows and 3 columns per Row 
    also Create Card with border card inlcude Header, title and Text 
    Text of Card inlcude progress Bar
    -->
    <%--<form runat="server">--%>

    <div class="container">
        <div class="row">
            <div class="col">
                <asp:DropDownList ID="DD_Year" runat="server" class="dropdown btn btn-secondary dropdown-toggle"></asp:DropDownList>
                <asp:DropDownList ID="DD_Month" runat="server" class="dropdown btn btn-secondary dropdown-toggle"></asp:DropDownList>
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
                            <asp:Label runat="server" ID="lblh5dist" Text=""></asp:Label>
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
                            <asp:Label runat="server" ID="lblh5com" Text=""></asp:Label>
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
                            <asp:Label runat="server" ID="lblh5cor" Text=""></asp:Label>
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
            <div class="col">
                <div id="chart3" style="width: 100%; height: 300px;"></div>
            </div>
        </div>
        <div class="row">
        </div>
        <div class="row">
        </div>
    </div>

    <!-- Create Card for Show Other KPI-->
    <div class="container">
        <!-- Row 1 of Card -->
        <div class="row">

            <div class="col">
                <div class="card  border-success mb-3" style="max-width: 18rem;">
                    <div class="card-header text-white bg-success">
                        <h3>Distribution </h3>
                        <h6>Alerts </h6>
                    </div>
                    <div class="card-text text-success">
                        <h6 class="card-body">Total
                            <asp:Label runat="server" ID="Label1" Text=""></asp:Label>
                        </h6>
                        <h6 class="card-body">Completed
                            <asp:Label runat="server" ID="Label6" Text=""></asp:Label>
                        </h6>
                        <h6 class="card-body">Completed %
                            <asp:Label runat="server" ID="Label7" Text=""></asp:Label>
                        </h6>
                    </div>
                </div>
            </div>
            <div class="col">
                <div class="card  border-success mb-3" style="max-width: 18rem;">
                    <div class="card-header text-white bg-success">
                        <h3>Distribution </h3>
                        <h6>STR's </h6>
                    </div>
                    <div class="card-text text-success">
                        <h6 class="card-body">Total
                            <asp:Label runat="server" ID="Label2" Text=""></asp:Label>
                        </h6>
                        <h6 class="card-body">Completed
                            <asp:Label runat="server" ID="Label8" Text=""></asp:Label>
                        </h6>
                        <h6 class="card-body">Completed %
                            <asp:Label runat="server" ID="Label9" Text=""></asp:Label>
                        </h6>
                    </div>
                </div>

            </div>
            <div class="col">
                <div class="card  border-success mb-3" style="max-width: 18rem;">
                    <div class="card-header text-white bg-success">
                        <h3>Distribution</h3>
                        <h6>Classroom Training</h6>
                    </div>
                    <div class="card-text text-success">
                        <h6 class="card-body">Total
                            <asp:Label runat="server" ID="Label3" Text=""></asp:Label>
                        </h6>
                        <h6 class="card-body">Completed
                            <asp:Label runat="server" ID="Label10" Text=""></asp:Label>
                        </h6>
                        <h6 class="card-body">Completed %
                            <asp:Label runat="server" ID="Label11" Text=""></asp:Label>
                        </h6>
                    </div>
                </div>
            </div>
            <div class="col">
                <div class="card  border-success mb-3" style="max-width: 18rem;">
                    <div class="card-header text-white bg-success">
                        <h3>Distribution</h3>
                        <h6>E-Learning Training</h6>
                    </div>
                    <div class="card-text text-success">
                        <h6 class="card-body">Total
                            <asp:Label runat="server" ID="Label4" Text=""></asp:Label>
                        </h6>
                        <h6 class="card-body">Completed
                            <asp:Label runat="server" ID="Label12" Text=""></asp:Label>
                        </h6>
                        <h6 class="card-body">Completed %
                            <asp:Label runat="server" ID="Label13" Text=""></asp:Label>
                        </h6>
                    </div>
                </div>
            </div>



        </div>

        <!-- Row 2 of Card -->
        <div class="row">

            <div class="col">
                <div class="card  border-success mb-3" style="max-width: 18rem;">
                    <div class="card-header text-white bg-success">
                        <h3>Distribution </h3>
                        <h6>KYC Review </h6>
                    </div>
                    <div class="card-text text-success">
                        <h6 class="card-body">Total
                            <asp:Label runat="server" ID="Label5" Text=""></asp:Label>
                        </h6>
                        <h6 class="card-body">Completed
                            <asp:Label runat="server" ID="Label14" Text=""></asp:Label>
                        </h6>
                        <h6 class="card-body">Completed %
                            <asp:Label runat="server" ID="Label15" Text=""></asp:Label>
                        </h6>
                    </div>
                </div>
            </div>
            <div class="col">
                <div class="card  border-success mb-3" style="max-width: 18rem;">
                    <div class="card-header text-white bg-success">
                        <h3>Distribution </h3>
                        <h6>Branch Onsite Review </h6>
                    </div>
                    <div class="card-text text-success">
                        <h6 class="card-body">Total
                            <asp:Label runat="server" ID="Label16" Text=""></asp:Label>
                        </h6>
                        <h6 class="card-body">Completed
                            <asp:Label runat="server" ID="Label17" Text=""></asp:Label>
                        </h6>
                        <h6 class="card-body">Completed %
                            <asp:Label runat="server" ID="Label18" Text=""></asp:Label>
                        </h6>
                    </div>
                </div>

            </div>
            <div class="col">
                <div class="card  border-success mb-3" style="max-width: 18rem;">
                    <div class="card-header text-white bg-success">
                        <h3>Distribution</h3>
                        <h6>Top 100 Depositor</h6>
                    </div>
                    <div class="card-text text-success">
                        <h6 class="card-body">Total
                            <asp:Label runat="server" ID="Label19" Text=""></asp:Label>
                        </h6>
                        <h6 class="card-body">Completed
                            <asp:Label runat="server" ID="Label20" Text=""></asp:Label>
                        </h6>
                        <h6 class="card-body">Completed %
                            <asp:Label runat="server" ID="Label21" Text=""></asp:Label>
                        </h6>
                    </div>
                </div>
            </div>
            <div class="col">
                <div class="card  border-success mb-3" style="max-width: 18rem;">
                    <div class="card-header text-white bg-success">
                        <h3>Distribution</h3>
                        <h6>PEP</h6>
                    </div>
                    <div class="card-text text-success">
                        <h6 class="card-body">Total
                            <asp:Label runat="server" ID="Label22" Text=""></asp:Label>
                        </h6>
                        <h6 class="card-body">Completed
                            <asp:Label runat="server" ID="Label23" Text=""></asp:Label>
                        </h6>
                        <h6 class="card-body">Completed %
                            <asp:Label runat="server" ID="Label24" Text=""></asp:Label>
                        </h6>
                    </div>
                </div>
            </div>



        </div>
    </div>

    <%--</form>--%>

    <script type="text/javascript">
   
        Highcharts.chart('chart1', {
            chart: {
                type: 'bar'
            },
            title: {
                text: 'Threshold Status by Region (Distribution)'
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
                text: 'Threshold Status by Branches (Distribution)'
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
                color: 'green',
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
                text: 'Threshold Status by Region (Commercial)'
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
                color: 'green',
                data: <%= this.ChartData3 %>
                    }]
        });

</script>
</asp:Content>
