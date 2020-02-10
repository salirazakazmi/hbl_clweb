<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebDataEntry.Login" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <title>login</title>  
    <link rel="stylesheet" type="text/css" href="css/lg.css"/>  
    <script src="Scripts/jquery-3.4.1.slim.min.js"></script>
    <link href="content/css/bootstrap.min.css" rel="stylesheet" />
    <script src="content/js/bootstrap.bundle.min.js"></script>
    <%--<link href="css/all.css" rel="stylesheet" />--%>
</head>
<body>  

<div class="container">
    
    <div class="row">
      <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">

        <div class="card card-signin my-5">
           <%-- <div class="row" style="align-items:center">
            <img src="Images/hbllogo.jpg" class="img-fluid" style="height: 50px; width: 150px"/>

            </div>

            <div class="row">
            <h2 ">Business Compliance Portal</h2>

            </div>--%>
            
                 <h2 ">Business Compliance Portal</h2>
            
          <div class="card-body">
            
            <h5 class="card-title text-center">Sign In</h5>
            <form class="form-signin" runat="server">
              <div class="form-label-group">
                  <asp:TextBox ID="txtname" class="form-control" runat="server" placeholder="Enter Login ID.." ></asp:TextBox>
               <%-- <input type="email" id="inputEmail" class="form-control" placeholder="Email address" required autofocus>--%>
                <label for="txtname">Login ID</label>
              </div>

              <div class="form-label-group">
                  <asp:TextBox ID="txtPass" TextMode="Password"  class="form-control" runat="server" placeholder="Enter Password.." ></asp:TextBox>
                <%--<input type="password" id="inputPassword" class="form-control" placeholder="Password" required>--%>
                <label for="txtPass">Password</label>
              </div>

                <asp:Label ID="lblerr" CssClass="card-text" value=""  runat="server" ></asp:Label>
             <%-- <div class="custom-control custom-checkbox mb-3">
                <input type="checkbox" class="custom-control-input" id="customCheck1">
                <label class="custom-control-label" for="customCheck1">Remember password</label>
                 
              </div>--%>
              <%--<button class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Sign in</button>--%>
                <asp:Button ID="Button1" runat="server" Text="Login" class="btn btn-lg btn-primary btn-block text-uppercase" OnClick="btnsave_Click" />  
              <hr class="my-4"/>
              
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>  
  
</body>  

</html>
