<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebDemo.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script src="http://code.jquery.com/jquery-latest.js"></script>
    <link href="CSS/Login.css" rel="stylesheet" />
    <title>先登录哟</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="content">
        <h1 style="font-size:30px">WelCome To My TestDemo</h1>
            账号：<asp:TextBox CssClass="Txt" ID="Txt_User" runat="server"></asp:TextBox>
            <br /><br />
            密码：<asp:TextBox CssClass="Txt" ID="Txt_Pwd" runat="server" TextMode="Password"></asp:TextBox>
            <br /><br />
            <asp:Button ID="Btn" runat="server" Text="登录" OnClick="Btn_Click"/>
        </div>
    </form>
       <iframe src="html/ICP.html" frameBorder="0" scrolling="no" width="100%" height="150" style="margin-top:100px"></iframe>
</body>
</html>
