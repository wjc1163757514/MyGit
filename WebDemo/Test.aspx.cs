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
                if (Session["Login"] == null || Session["Login"].ToString() != "True")
                {
                    Response.Redirect("Login.aspx");
                }
                LoadRepeater();
            }
        }

        //发送邮件
        protected void Button1_Click(object sender, EventArgs e)
        {
            string TargetEmail = "wbwangjc@centaline.com.cn,wbwangjc@centaline.com.cn";
            string[] ListTargetEmail = Regex.Split(TargetEmail, ",", RegexOptions.IgnoreCase);
            ApiEmailReuqest ApiParameter = new ApiEmailReuqest()
            {
                UserEmail = "1163757514@qq.com",   //1163757514@qq.com
                UserEmailPassWord = "uoteepfhtjepjecd",   //uoteepfhtjepjecd
                ToEmailAddress = ListTargetEmail.ToList<string>(),
                CCEmailAddress = ListTargetEmail.ToList<string>(),
                EmailBody = "早上好哟~~~"
            };
            Response.Write("<script>alert('" + ShareClass.SendEmailFile(ApiParameter) + "');</script>");
        }

        //生成excel
        protected void Button1_Click1(object sender, EventArgs e)
        {
            DataTable tblDatas = new DataTable("Datas");
            DataColumn dc = null;
            dc = tblDatas.Columns.Add("ID", Type.GetType("System.Int32"));
            dc.AutoIncrement = true;//自动增加
            dc.AutoIncrementSeed = 1;//起始为1
            dc.AutoIncrementStep = 1;//步长为1
            dc.AllowDBNull = false;//

            dc = tblDatas.Columns.Add("Product", Type.GetType("System.String"));
            dc = tblDatas.Columns.Add("Version", Type.GetType("System.String"));
            dc = tblDatas.Columns.Add("Description", Type.GetType("System.String"));

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

            Response.Write("<script>alert('" + ShareClass.dataTableToCsv(tblDatas, @"E:\Test\EccelTest.xls") + "');</script>");

        }

        //上传文件
        protected void Btn_UploadingFile_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] buffer = null;
                string[] FileTypeList = Request.Files[0].FileName.Split('.');
                if (Request.Files[0].FileName != "")
                {
                    buffer = new byte[Request.Files[0].InputStream.Length];
                    Request.Files[0].InputStream.Read(buffer, 0, buffer.Length);
                }
                FileRequest request = new FileRequest()
                {
                    File = buffer,
                    FileName =Request.Files[0].FileName,
                    StudentName = ShareClass.UserName,
                    FileSize= Request.Files[0].ContentLength,
                    FileType= FileTypeList[FileTypeList.Length - 1]
                };
                string str = ShareClass.UploadingFile(request);
                FileResult result = JsonConvert.DeserializeObject<FileResult>(str);
                string message = string.Format("<script> alert('{0}');</script>", result.message);
                Response.Write(message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void LoadRepeater()
        {
            string Url = ShareClass.AutoApiUrl + "GetFileList?UserName="+ShareClass.UserName;
            this.Repeater1.DataSource = ShareClass.GetDataTableByUrl(Url);
            this.Repeater1.DataBind();
        }

        protected void Btn_LoadRepeater_Click(object sender, EventArgs e)
        {
            LoadRepeater();
        }

        protected void Btn_BackHomePage_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }
    }
}