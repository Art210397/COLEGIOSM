using System;
using System.Collections.Generic;

namespace COLEGIOSM.Models
{
    public partial class Logs
    {
        public int LogId { get; set; }
        public string Url { get; set; }
        public string Request { get; set; }
        public DateTime Date { get; set; }
        public string Exception { get; set; }
        public string Method { get; set; }
    }
}
