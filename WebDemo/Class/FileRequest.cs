using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Class
{
    public class FileRequest
    {
        public byte[] File { get; set; }

        public string Path { get; set; }

        public string FileName { get; set; }

        public string StudentName { get; set; }

        public string FileType { get; set; }

        public int FileSize { get; set; }
    }
}