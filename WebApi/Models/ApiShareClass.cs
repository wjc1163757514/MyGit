using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class ApiShareClass
    {
        public static string SaveFile(byte[] file, string path, string fileName)
        {
            try
            {
                var serverPath = @"";   //存储路径
                var filename = fileName;
                var filePath = string.Format(@"{0}\{1}", serverPath + @path, filename);     //保存完整路径
                var fullpath = filePath;     //流写入路径

                //创建文件  

                using (var ms = new MemoryStream())
                {
                    MemoryStream m = new MemoryStream(file);
                    string files = string.Format(@"{0}", fullpath);
                    FileStream fs = new FileStream(files, FileMode.OpenOrCreate);
                    m.WriteTo(fs);
                    m.Close();
                    fs.Close();
                    m = null;
                    fs = null;
                    return fullpath;
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public static void InsertFile(FileRequest request) {
            string sql = string.Format("INSERT INTO [DB_Test].[dbo].[FileInfo]([FileName],[FileType],[FileSize],[FilePath] ,[StudentID]) " +
                "VALUES('{0}', '{1}',{2}, '{3}', (SELECT StudentID FROM[DB_Test].[dbo].[Student] WHERE StudentName = '{4}'))"
                ,request.FileName,request.FileType,request.FileSize,("http://www.wangjc.top:8003/FileSave/" +request.FileName),request.StudentName);
            DBHelper.ExecuteNoQuery(sql);
        }
    }
}