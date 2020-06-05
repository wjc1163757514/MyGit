using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Class
{
    public class FileRequest
    {
        /// <summary>
        /// 文件数据流
        /// </summary>
        public byte[] File { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileType { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public int FileSize { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }
    }
}