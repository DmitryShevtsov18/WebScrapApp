using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebScrapApp.Core
{
    public class SPage : SObject
    {
        public string Project { get; set; } = "";

        public string Url { get; set; } = "";

        public SScrollType ScrollType { get; set; } = SScrollType.Dynamic;

        public STag ScrollTag { get; set; } = STag.div;

        public string ScrollClass { get; set; } = "";

        [XmlIgnore]
        public string ScrollSelector
        {
            get
            {
                return $"{this.ScrollTag}.{this.ScrollClass.Replace(' ', '.')}";
            }
        }

        [XmlIgnore]
        public List<SView> Views
        {
            get
            {
                return SWorkFiles.ReadViews().Where<SView>(x => x.Page == this.Name).ToList<SView>();
            }
        }

        public SPage() : base()
        {
            
        }

        public SPage(string _project) : base()
        {
            this.Project = _project;
        }

        protected SPage(SPage _copyPage) : base(_copyPage) 
        {
            this.Project = _copyPage.Project;
            this.Url = _copyPage.Url;
            this.ScrollType = _copyPage.ScrollType;
            this.ScrollTag = _copyPage.ScrollTag;
            this.ScrollClass = _copyPage.ScrollClass;
        }

        protected override SObject CloneObj()
        {
            return new SPage(this);
        }

        public SPage Clone()
        {
            return (SPage)this.CloneObj();
        }

        public new string GetFilename()
        {
            return $"{this.Project}_{this.Name}";
        }
    }
}
