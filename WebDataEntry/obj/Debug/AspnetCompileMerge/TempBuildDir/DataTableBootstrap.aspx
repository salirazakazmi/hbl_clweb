<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataTableBootstrap.aspx.cs" Inherits="WebDataEntry.DataTableBootstrap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="https://cdn.datatables.net/1.10.19/css/jquery.dataTables.min.css" rel="stylesheet" type="text/css"/>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.1.3/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="https://cdn.datatables.net/1.10.19/css/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />

  
 

</head>
<body>
<%--<script type="text/javascript"  src="https://code.jquery.com/jquery-3.3.1.js"></script>
<script  type="text/javascript"  src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
<script type="text/javascript"   src="https://cdn.datatables.net/1.10.19/js/dataTables.bootstrap4.min.js"></script>--%>



    <form id="form1" runat="server">
    <div class="container">
    
       
    <table id="example" class="table table-borderless " style="width:100%">
        <thead>
            <tr>
                <th>First name</th>
                <th>Last name</th>
                <th>Position</th>
                <th>Office</th>
                <th>Start date</th>
                <th>Salary</th>
            </tr>
        </thead>
        <tbody>
                <tr>
                    <td> row 1</td>
                    <td> row 1</td>
                    <td> row 1</td>
                    <td> row 1</td>
                    <td> row 1</td>
                </tr>
            <tr>
                    <td> row 2</td>
                    <td> row 2</td>
                    <td> row 4</td>
                    <td> row 1</td>
                    <td> row 1</td>
                </tr>
            <tr>
                    <td> row 6</td>
                    <td> row 1</td>
                    <td> row 8</td>
                    <td> row 1</td>
                    <td> row 10</td>
                </tr>
            </tbody>
        <tfoot>
            <tr>
                <th>First name</th>
                <th>Last name</th>
                <th>Position</th>
                <th>Office</th>
                <th>Start date</th>
                <th>Salary</th>
            </tr>
        </tfoot>
    </table>

    </div>
    </form>
    <script src="Scripts/jquery-3.4.1.js"></script>
    <%--<script type="text/javascript"  src="https://code.jquery.com/jquery-3.3.1.js"></script>--%>
<script  type="text/javascript"  src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
<script type="text/javascript"   src="https://cdn.datatables.net/1.10.19/js/dataTables.bootstrap4.min.js"></script>

    <script>
        $(document).ready(function () {
            $('#example').DataTable({
                //pageLength: 10,
                //filter: true,
                //deferRender: true,
                //scrollY: 200,
                //scrollCollapse: true,
                scroller: true
            });
        });

       
    </script>

</body>


 
</html>
