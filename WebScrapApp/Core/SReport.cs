using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapApp.Core
{
    public class SReport : SObject
    {
        public string Template { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        public SReportLine Headers { get; set; } = new SReportLine();

        public SReportLineCollection Lines { get; set; } = new SReportLineCollection();

        public SReport() : base()
        {
            
        }

        protected SReport(SReport _copyReport) : base(_copyReport) 
        {
            this.Template = _copyReport.Template;
            this.CreatedDateTime = DateTime.Now;
            this.Headers = _copyReport.Headers.Clone();
        }

        protected override SObject CloneObj()
        {
            return new SReport(this);
        }

        public SReport Clone()
        {
            return (SReport)this.CloneObj();
        }

        public new string GetFilename()
        {
            return $"{this.Template}_{this.Name}_{this.CreatedDateTime}";
        }
    }
}
