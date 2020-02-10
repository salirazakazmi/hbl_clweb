<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Loginold.aspx.cs" Inherits="WebDataEntry.Loginold" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <title>login</title>  
    <link rel="stylesheet" type="text/css" href="css/logincss.css">  
</head>
    <body>  
    <form id="form1" runat="server">  
    <div>  
         <h1 style="background-color: white;color: green; text-align: center; font-style: oblique">HBL</h1>
         <h2 style="background-color: white;color: green; text-align: center; font-style: oblique">Business Compliance Unit</h2>  
        <fieldset>  
            <legend style="font-family:Arial Black;color:orangered">Login</legend>  
            <table align="center" border="0" cellpadding="4" cellspacing="4">  
            <tr><td align="center"><asp:TextBox ID="txtname" class="textbox" runat="server" placeholder="Enter Login ID.."></asp:TextBox></td></tr>  
            <tr><td align="center"> <asp:TextBox ID="txtPass" TextMode="Password"  class="textbox" runat="server" placeholder="Enter Password.." ></asp:TextBox></td></tr>  
            <tr><td align="center"> <asp:Label ID="lblerr" value=""  runat="server" ></asp:Label></td></tr>  
            
            <tr><td align="center">  
                <asp:Button ID="btnsave" runat="server" Text="Login" class="button button4" OnClick="btnsave_Click" />  
                <asp:Button ID="btnreset" runat="server" Text="Cancel" class="button button4" OnClick="btnreset_Click" />  
            </td></tr>                  
         </table>  
       </fieldset>  
    </div>  
    <footer>  
        <p style="background-color: white; font-weight: bold; color:blue; text-align: center; font-style: oblique">© <script> document.write(new Date().toDateString()); </script></p>  
    </footer>  
    </form>    
</body>  

</html>
