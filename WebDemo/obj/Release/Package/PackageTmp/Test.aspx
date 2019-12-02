<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="WebDemo.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>文件管理</title>
    <link href="CSS/Index.css" rel="stylesheet" />
    <style type="text/css">
        #File {
        width:240px;
        height:150px;
        margin:auto;
        margin-top:20px;
        }
    </style>
</head>


    <body>
    <form id="form2" runat="server">
        <div id="content">
            <h1 style="font-size: 30px">管理一手我的文件</h1>
            <div id="div_btn">
            <asp:Button CssClass="btn" ID="Txt_TargetEmail" runat="server" Text="House730日报表发送" OnClick="Button1_Click" Enabled="False" />
            <asp:Button CssClass="btn" ID="Button1" runat="server" Text="生成excel" OnClick="Button1_Click1" Enabled="False" />
            <asp:Button CssClass="btn" ID="Btn_LoadRepeater" runat="server" Text="刷新文件列表" OnClick="Btn_LoadRepeater_Click"/>
            <asp:Button CssClass="btn" ID="Btn_BackHomePage" runat="server" Text="返回首页" OnClick="Btn_BackHomePage_Click"/>
            </div>
            <div id="File"><asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:Button CssClass="btn" ID="Btn_UploadingFile" runat="server" Text="上传指定文件" OnClick="Btn_UploadingFile_Click"/></div>
            <table>
                <tr>
                    <th>文件编号</th>
                    <th>文件名</th>
                    <th>文件类型</th>
                    <th>大小</th>
                    <th>上传时间</th>
                    <th>操作</th>
                </tr>
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("ID") %></td>
                            <td><%#Eval("FileName") %></td>
                            <td><%#Eval("FileType") %></td>
                            <td><%#Eval("FileSize") %></td>
                            <td><%#Eval("CreateTime") %></td>
                            <td><a href="<%#Eval("FilePath") %>" target="_blank">查看</a></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </form>
    <iframe src="html/ICP.html" frameborder="0" scrolling="no" width="100%" height="150" style="margin-top: 100px"></iframe>
</body>
</html>
