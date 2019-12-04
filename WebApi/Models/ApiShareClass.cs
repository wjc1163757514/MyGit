using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class ApiShareClass
    {
        public static string SaveFile(FileRequest request)
        {
            try
            {
                var filePath = string.Format(@"{0}\{1}\{2}\{3}", AppDomain.CurrentDomain.BaseDirectory, "FileSave", request.StudentName, request.FileType);     //保存完整路径
                var fullpath = string.Format(@"{0}\{1}", filePath, request.FileName);     //流写入路径

                //创建文件夹
                Directory.CreateDirectory(filePath);

                //创建文件  
                using (var ms = new MemoryStream())
                {
                    MemoryStream m = new MemoryStream(request.File);
                    string files = string.Format(@"{0}", fullpath);
                    FileStream fs = new FileStream(files, FileMode.OpenOrCreate);
                    m.WriteTo(fs);
                    m.Close();
                    fs.Close();
                    m = null;
                    fs = null;
                }
                //上传完毕，更新到DB
                InsertFileInfoToDB(request);

                return fullpath;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public static void InsertFileInfoToDB(FileRequest request)
        {
            //创建访问路径
            string Path = string.Format("http://www.wangjc.top:886/FileSave/{0}/{1}/{2}", request.StudentName, request.FileType, request.FileName);
            string sql = string.Format("INSERT INTO [DB_Test].[dbo].[FileInfo]([FileName],[FileType],[FileSize],[FilePath] ,[StudentID]) " +
                "VALUES('{0}', '{1}',{2}, '{3}', (SELECT StudentID FROM[DB_Test].[dbo].[Student] WHERE StudentName = '{4}'))"
                , request.FileName, request.FileType, request.FileSize, Path, request.StudentName);
            DBHelper.ExecuteNoQuery(sql);
        }
    }
}