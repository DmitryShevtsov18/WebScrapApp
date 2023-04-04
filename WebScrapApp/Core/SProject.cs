using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebScrapApp.Core
{
    public class SProject : SObject
    {
        [XmlIgnore]
        public List<SPage> Pages 
        {
            get
            {
                return SWorkFiles.ReadPages().Where<SPage>(x => x.Project == this.Name).ToList<SPage>();
            }
        }

        public SProject() : base()
        {
            
        }        

        protected SProject(SProject _copyProject) : base(_copyProject) 
        {
            
        }

        protected override SObject CloneObj()
        {
            return new SProject(this);
        }

        public SProject Clone()
        {
            return (SProject)this.CloneObj();
        }
    }
}
