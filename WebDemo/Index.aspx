﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebDemo.Index" Debug="true" Async="true"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>数据展示</title>
    <script src="http://code.jquery.com/jquery-latest.js"></script>
    <link href="CSS/Index.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="content">
            <h1 style="font-size: 30px">WEB发布+DB的小测试啦,GIT</h1>
            <div id="div_btn">
                <asp:Button CssClass="btn" ID="Btn_Close" runat="server" Text="退出" OnClick="Btn_Close_Click" />
                <asp:Button CssClass="btn" ID="Btn_Load" runat="server" Text="展示所有数据" OnClick="Btn_Load_Click" />
                <asp:Button CssClass="btn" ID="Btn_ClearCache" runat="server" Text="清除缓存" OnClick="Btn_ClearCache_Click" />
                <asp:Button CssClass="btn" ID="Btn_File" runat="server" Text="管理文件"  OnClick="Btn_File_Click"/>
            </div>
            <table>
                <tr>
                    <th>用户ID</th>
                    <th>用户名</th>
                    <th>注册</th>
                    <th>性别</th>
                </tr>
                <asp:Repeater ID="Repeater1" runat="server" OnItemCreated="Repeater1_ItemCreated">
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("UserID") %></td>
                            <td><%#Eval("UserName") %></td>
                            <td><%#Eval("CreateTime") %></td>
                            <td><%#Eval("Sex") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div id="Email">
                <h2 style="text-align: center;">这里可以发送邮件哦</h2>
                <br />
                请输入收件邮箱：<asp:TextBox ID="Txt_TargetEmail" runat="server"></asp:TextBox>
                <br />
                <br />
                请输入邮件内容：<asp:TextBox ID="Txt_EmailBody" runat="server" TextMode="MultiLine"></asp:TextBox>
                <br />
                <br />
                <asp:Button CssClass="btn2" ID="Btn_SendEmail" runat="server" Text="发送" OnClick="Btn_SendEmail_Click" />
            </div>
        </div>
    </form>
    <iframe src="html/ICP.html" frameborder="0" scrolling="no" width="100%" height="150" style="margin-top: 100px"></iframe>
</body>
</html>
