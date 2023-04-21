using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapApp.Core
{
    public class SQueue : SObject
    {
        public string Template { get; set; }

        public SQueueStatus Status { get; set; } = SQueueStatus.Queue;

        public List<string> Log { get; set; } = new List<string>();

        public SQueue() : base()
        {

        }

        public SQueue(string _template) : base()
        {
            this.Template = _template;
        }

        protected SQueue(SQueue _copyQueue) : base(_copyQueue)
        {
            this.Template = _copyQueue.Template;
        }

        protected override SObject CloneObj()
        {
            return new SQueue(this);
        }

        public SQueue Clone()
        {
            return (SQueue)this.CloneObj();
        }

        public new string GetFilename()
        {
            return this.Name;
        }
    }
}
