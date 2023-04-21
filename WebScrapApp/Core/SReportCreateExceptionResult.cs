using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapApp.Core
{
    public class SReportCreateExceptionResult : SExceptionResult
    {
        public SReport Report { get; set; } = new SReport();

        public SReportCreateExceptionResult()
        {

        }
    }
}
