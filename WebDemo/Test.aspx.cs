using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebDemo.Class;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace WebDemo
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadRepeater();
            }
        }

        //发送邮件
        protected async void Button1_Click(object sender, EventArgs e)
        {
            string TargetEmail = "wbwangjc@centaline.com.cn,wbwangjc@centaline.com.cn";
            string[] ListTargetEmail = Regex.Split(TargetEmail, ",", RegexOptions.IgnoreCase);
            ApiEmailReuqest ApiParameter = new ApiEmailReuqest()
            {
                UserEmail = "1163757514@qq.com",   //1163757514@qq.com
                UserEmailPassWord = "mncervgdnpcsjcgg",   //uoteepfhtjepjecd
                ToEmailAddress = ListTargetEmail.ToList<string>(),
                CCEmailAddress = ListTargetEmail.ToList<string>(),
                EmailBody = "早上好哟~~~"
            };

            string EmailResultMsg = await UserServer.UserSendEmail(ApiParameter);
            Response.Write("<script>alert('" + EmailResultMsg + "');</script>");
        }

        //生成excel
        protected void Button1_Click1(object sender, EventArgs e)
        {
            DataTable tblDatas = new DataTable("Datas");

            DataRow newRow;
            newRow = tblDatas.NewRow();
            newRow["Product"] = "大话西游";
            newRow["Version"] = "2.0";
            newRow["Description"] = "我很喜欢";
            tblDatas.Rows.Add(newRow);

            newRow = tblDatas.NewRow();
            newRow["Product"] = "梦幻西游";
            newRow["Version"] = "3.0";
            newRow["Description"] = "比大话更幼稚";
            tblDatas.Rows.Add(newRow);

            Response.Write("<script>alert('" + ShareClass.DataTableToCsv(tblDatas, @"E:\Test\EccelTest.xls") + "');</script>");

        }

        //上传文件
        protected async void Btn_UploadingFile_Click(object sender, EventArgs e)
        {

            if (Session["UserSession"] == null)
            {
                Response.Redirect("Login.aspx", false);
            }
            else
            {
                try
                {
                    byte[] buffer = null;

                    string[] FileTypeList = Request.Files[0].FileName.Split('.');

                    string resultMessage = "";

                    UserSession userSession = (UserSession)Session["UserSession"];

                    if (Request.Files[0].FileName != "")
                    {
                        buffer = new byte[Request.Files[0].InputStream.Length];
                        Request.Files[0].InputStream.Read(buffer, 0, buffer.Length);
                        FileRequest request = new FileRequest()
                        {
                            File = buffer,
                            FileName = Request.Files[0].FileName,
                            UserName = userSession.UserName,
                            FileSize = Request.Files[0].ContentLength,
                            FileType = FileTypeList[FileTypeList.Length - 1]
                        };

                        FileResult result = await UserServer.UserPostFile(request, userSession.Token);
                        resultMessage = result.Message;
                    }
                    else
                    {
                        resultMessage = "未选择文件!";
                    }

                    Response.Write(string.Format("<script> alert('{0}');</script>",resultMessage).ToString());
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        /// <summary>
        /// 加载文件列表
        /// </summary>
        private async void LoadRepeater()
        {
            if (Session["UserSession"] == null)
            {
                Response.Redirect("Login.aspx", false);
            }
            else
            {
                UserSession userSession = (UserSession)Session["UserSession"];
                this.Repeater1.DataSource = await UserServer.UserGetFileList(userSession.UserName, userSession.Token);
                this.Repeater1.DataBind();
            }

        }

        protected void Btn_LoadRepeater_Click(object sender, EventArgs e)
        {
            LoadRepeater();
        }

        protected void Btn_BackHomePage_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx", false);
        }
    }
}