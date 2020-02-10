<%@ Page Language="C#"  MasterPageFile="~/MasterPage_DE.Master" AutoEventWireup="true" CodeBehind="TestDashboard.aspx.cs" Inherits="WebDataEntry.TestDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH1" runat="server">

     
	<!--Import Google Icon Font-->
      <%--<link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">--%>
    <link href="Scripts/icon.css" rel="stylesheet" />
      <!--Import materialize.css-->
    <link href="Scripts/materialize.min.css" rel="stylesheet" />
      <%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/materialize/1.0.0/css/materialize.min.css">--%>
      <%--<script src="https://code.jquery.com/jquery-3.1.0.js"></script>--%>
    <script src="Scripts/jquery-3.4.1.js"></script>
      <!--Let browser know website is optimized for mobile-->
      <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <script src="Chart.js"></script>
      <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.4.0/Chart.min.js"></script>--%>
      <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/materialize/1.0.0/js/materialize.min.js"></script>--%>

<style type="text/css">
.bg{
	/*
        background-image: linear-gradient(to top left,#8a4592,#35b0a9);
    */
    background-image: linear-gradient(to left,#808080,#b5d7ef);

}
/*
    nav{
	background-image: linear-gradient(to top right,#35b0a9,#8a4592); 
	padding-left: 300px;
}
.content{
	padding-left: 300px;
	height:100%;
}*/
.card-bg{
	background: rgba(0,0,0,0);
}
/*@media only screen and (max-width: 992px){
	.content,nav{
		padding-left: 0;
	}
}*/
</style>
<script type="text/javascript">
	$(document).ready(function(){
		//$('.sidenav').sidenav();
	});
</script>
	<%--<div class="navbar-fixed">
	<nav>
		<div class="nav-wrapper">
			<a href="#" class="brand-logo center">Administrator</a>
			<a href="" data-target="slide-out" class="sidenav-trigger"><i class="material-icons">menu</i></a>		
		</div>
	</nav>
	</div>--%>
	<%--<ul class="sidenav sidenav-fixed bg" id="slide-out">
		<li>
			<div class="user-view">
				<div class="background">
					<img src="cd.png" width="100%">
				</div>
				<a href="#"><img src="pp.jpg" class="circle"></a>
				<a href="#" class="white-text name">Maya Circle</a>
				<a href="#" class="white-text email">MayaCircle@email.com</a>
			</div>	
		</li>
		<li><a href="" class="white-text"><i class="material-icons">home</i>Dashboard</a></li>
		<li><a href="" class="white-text"><i class="material-icons">mail</i>Data Member</a></li>
	</ul>
	--%>
    <div class="content bg">
        <div class="col s12">
				<h1 class="blue-grey-text">DashBoard</h1>
		</div>
		<div class="container">
		<div class="row">
			<%--<div class="col s12">
				<h1 class="blue-grey-text">DashBoard</h1>
			</div>--%>
			<div class="col s12 m6 l3">
				<div class="card card-bg black-text">
					<div class="card-content center">
						<p>Revenue</p>
						<h5>$12,476.00</h5>
						<i class="material-icons small green-text">keyboard_arrow_up</i><br>
						<b class="green-text">%12</b>
					</div>
				</div>
			</div>
			<div class="col s12 m6 l3">
				<div class="card card-bg black-text">
					<div class="card-content center">
						<p>Click</p>
						<h5>2400</h5>
						<i class="material-icons small red-text">keyboard_arrow_down</i><br>
						<b class="red-text">%10</b>
					</div>
				</div>
			</div>
			<div class="col s12 l3 m6">
				<div class="card card-bg black-text">
					<div class="card-content center">
						<p>Users</p>
						<h5>5000,00</h5>
						<i class="material-icons small green-text">keyboard_arrow_up</i><br>
						<b class="green-text">%7</b>
					</div>
				</div>
			</div>
			<div class="col s12 l3 m6">
				<div class="card card-bg black-text">
					<div class="card-content center">
						<p>Conversion Rate</p>
						<h5>0,80%</h5>
						<i class="material-icons small green-text">keyboard_arrow_up</i><br>
						<b class="green-text">%25</b>
					</div>
				</div>
			</div>
			<div class="col l12 m6 s12">
				<div class="card card-bg">
					<div class="card-content">
						<canvas id="myChart"></canvas>
					</div>
				</div>
			</div>
			<div class="col l12 m6 s12">
				<div class="card card-bg">
					<div class="card-content">
						<canvas id="myChart2"></canvas>
					</div>
				</div>
			</div>
		</div>
	</div>
	</div>
	<script type="text/javascript">
		var chr=document.getElementById("myChart").getContext("2d");
		var chr2=document.getElementById("myChart2").getContext("2d");
		var myChart=new Chart(chr,{
			type:'bar',
			data:{
			    //labels:['january','febuary','march','april','mei','juni','juli'],
                labels:<% =this.ChartLabels %>,
				datasets:[{
					label:'PEP Status',
				    //data:[1100,1250,1090,1400,1150,1450,1107],
                    data:<% =this.ChartData1 %>,
					backgroundColor:'rgba(0,0,0,0.6)',
					borderColor: '#fff',
					borderWidth:1,
				}]
			},
			options:{
				legend:{
					labels:{
						fontColor:'#fff',
					}

				}
			}
		});
		var myChart2=new Chart(chr2,{
			type:'line',
			data:{
				labels:['Monday','Tuesday','Wednesday','Thursday','fiday'],
				datasets:[{
					label:'Data Users',
					data:[100,512,150,120,190],
					backgroundColor:'rgba(0,255,0,0.6)',
					borderColor:'#fff',
					borderWidth:1,
				}]
			},
			options:{
				legend:{
					labels:{
						fontColor:'#fff',
					}
				}
			}
		});
	</script>
<div>
        <asp:Button ID="ShowGraph" runat="server" OnClick="ShowGraph_Click" Text="Show Graph" />
</div>

    <script type="text/javascript">
		
        <%--var randomScalingFactor = function () { return Math.round(Math.random() * 100) };
        var lineChartData = {
            //labels: ["January", "February", "March", "April", "May", "June", "July"],
            labels: <% =this.ChartLabels %>,
            datasets: [
                {
                    label: "Query Count",
                    fillColor: "rgba(220,220,220,0.2)",
                    strokeColor: "rgba(220,220,220,1)",
                    pointColor: "rgba(220,220,220,1)",
                    pointStrokeColor: "#fff",
                    pointHighlightFill: "#fff",
                    pointHighlightStroke: "rgba(220,220,220,1)",
                    //data: [0, 1, 4, 6, 10, 8, 6]
                    data: <% =this.ChartData1 %>
                },
                {
                    label: "My Second dataset",
                    fillColor: "rgba(151,187,205,0.2)",
                    strokeColor: "rgba(151,187,205,1)",
                    pointColor: "rgba(151,187,205,1)",
                    pointStrokeColor: "#fff",
                    pointHighlightFill: "#fff",
                    pointHighlightStroke: "rgba(151,187,205,1)",
                    //data: [28, 48, 40, 19, 86, 27, 90]
                    data: <% =this.ChartData2 %>
                }
            ]
        }

        function DrawChart() {
            var ctx = document.getElementById("barChart").getContext("2d");
            window.myLine = new Chart(ctx).Line(lineChartData, {
                responsive: true
            });
        }--%>


        <%--var chr=document.getElementById("myChart").getContext("2d");
        var myChart=new Chart(chr,{
            type:'bar',
            data:{
                labels:<% =this.ChartLabels %>,
                //labels:<% =this.lstLabel %>,
                datasets:[{
                    label:'Data Visitor',
                    data:<% =this.ChartData1 %>,
                    //data:<% =this.lstdata %>,
                    backgroundColor:'rgba(0,0,0,0.6)',
                    borderColor: '#fff',
                    borderWidth:1,
                }]
            },
            options:{
                legend:{
                    labels:{
                        fontColor:'#fff',
                    }

                }
            }
        });
		--%>
	</script>


</asp:Content>