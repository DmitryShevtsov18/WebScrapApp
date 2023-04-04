using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapApp.Core
{
    public class SReportLine
    {
        public List<string> Fields { get; set; } = new List<string>();

        public SReportLine()
        {
            
        }

        public SReportLine Clone()
        {
            SReportLine ret = new SReportLine();

            ret.Fields = this.Fields.ToList<string>();

            return ret;
        }
    }
}
