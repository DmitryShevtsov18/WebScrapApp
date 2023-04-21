using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapApp.Core
{
    public class SWebParserExceptionResult : SExceptionResult
    {
        public SReportLineCollection ReportLines { get; set; } = new SReportLineCollection();

        public SWebParserExceptionResult()
        {

        }
    }
}
