using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapApp.Core
{
    public class SExceptionResult
    {
        public SExceptionType Type { get; set; } = SExceptionType.None;

        public string Message { get; set; } = "";

        public string Result { get; set; } = "";

        public SReportLineCollection ReportLines { get; set; } = new SReportLineCollection();

        public SReport Report { get; set; } = new SReport();

        public SExceptionResult()
        {
            
        }
    }
}
