using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COLEGIOSM.Services.Models
{
    public class LogModel
    {
        public string Url { get; set; }
        public string Request { get; set; }
        public string Exception { get; set; }
        public string Method { get; set; }
    }
}
