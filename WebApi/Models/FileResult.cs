using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class FileResult
    {
        public string message { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int ContentLength { get; set; }
        public string ContentType { get; set; }
    }
}