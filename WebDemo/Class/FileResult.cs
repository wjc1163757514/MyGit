using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Class
{
    public class FileResult
    {
        public string Message { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int ContentLength { get; set; }
        public string ContentType { get; set; }
    }
}