<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="multiselect.aspx.cs" Inherits="WebDataEntry.Deleted.multiselect" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
            <link href="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css"
                rel="stylesheet" type="text/css" />
            <script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>--%>
            <%--<link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css"
                rel="stylesheet" type="text/css" />
            <script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js"
                type="text/javascript"></script>--%>
            <script src="../css/jquery.min.js"></script>
            <link href="../css/bootstrap.min.css" rel="stylesheet" />
            <link href="../css/multiple-select.css" rel="stylesheet" />
            <script src="../css/bootstrap.min.js"></script>
            <script src="../css/multiple-select.js"></script>
            <script type="text/javascript">
                $(function () {
                    $('[id*=lstHead]').multiselect({ includeSelectAllOption: true });
                });
            </script>
            <div class="col-md-3">
                <label class="control-label" style="margin-top: 10px;">
                    Head</label>
                <asp:ListBox ID="lstHead" runat="server" SelectionMode="Multiple" AutoPostBack="false"
                    OnSelectedIndexChanged="lstHead_SelectedIndexChanged" class="form-control"></asp:ListBox>
                <asp:Button Text="Submit" OnClick="Submit" runat="server" CssClass="btn btn-default" />
            </div>

        </div>
    </form>
</body>
</html>
