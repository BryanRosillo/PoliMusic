<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="WebAppPoliMusicV2.SignUp" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h2>Register new user</h2>
<form id="formLogin" runat="server">
    <asp:TextBox ID="txtUsername" runat="server" placeholder="Username"></asp:TextBox>
    <br />
    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox>
    <br />
    <asp:TextBox ID="txtPasswordConfirm" runat="server" TextMode="Password" placeholder="Confirm password"></asp:TextBox>
    <br />
    <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click"/>
    <br />
    <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>
    </form>
</body>
</html>

