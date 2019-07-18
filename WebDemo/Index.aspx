<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebDemo.Index" %>

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
            <h1 style="font-size:30px">WEB发布+DB的小测试啦,GIT</h1>
            <div id="div_btn">
            <asp:Button CssClass="btn" ID="Btn_Close" runat="server" Text="退出" OnClick="Btn_Close_Click" />
            <asp:Button CssClass="btn" ID="Btn_Load" runat="server" Text="展示所有数据" OnClick="Btn_Load_Click" />
                <asp:Button CssClass="btn" ID="Btn_ClearCache" runat="server" Text="清除缓存" OnClick="Btn_ClearCache_Click" />
            </div>
            <table>
                <tr>
                    <th>编号</th>
                    <th>姓名</th>
                    <th>时间</th>
                    <th>性别</th>
                </tr>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("STUDENTID") %></td>
                        <td><%#Eval("STUDENTNAME") %></td>
                        <td><%#Eval("CREATETIME") %></td>
                        <td><%#Eval("SEX") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            </table>
        </div>
     </form>
       <iframe src="html/ICP.html" frameBorder="0" scrolling="no" width="100%" height="150" style="margin-top:100px"></iframe>
  </body>
</html>
