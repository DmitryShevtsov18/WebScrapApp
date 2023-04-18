using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebScrapApp.Core
{
    public class SReportTemplate : SObject
    {        
        public string Project { get; set; }

        [XmlIgnore]
        public List<SProject> Projects
        {
            get
            {
                return SWorkFiles.ReadProjects();
            }
        }

        public string Page { get; set; }

        [XmlIgnore]
        public List<SPage> Pages
        {
            get
            {
                return SWorkFiles.ReadPages().Where<SPage>(x => x.Project == this.Project).ToList<SPage>();
            }
        }

        public string View { get; set; }

        [XmlIgnore]
        public List<SView> Views
        {
            get
            {
                return SWorkFiles.ReadViews().Where<SView>(x => x.Page == this.Page).ToList<SView>();
            }
        }

        [XmlIgnore]
        public string ReportName
        {
            get
            {
                return $"{this.Project}_{this.Page}_{this.View}";
            }
        }

        public SViewFieldCollection Fields { get; set; } = new SViewFieldCollection();

        [XmlIgnore]
        public SViewFieldCollection ViewFields
        {
            get
            {
                if (string.IsNullOrEmpty(this.View))
                {
                    return new SViewFieldCollection();
                }
                else
                {
                    SView sView = SWorkFiles.ReadView(new SView(this.Page) { Name = this.View });
                    if (sView is null)
                    {
                        return new SViewFieldCollection();
                    }
                    else
                    {
                        return sView.Fields;
                    }
                }
            }
        }

        public SReportTemplate() : base()
        {
            
        }

        protected SReportTemplate(SReportTemplate _copyReportTemplate) : base(_copyReportTemplate) 
        {
            this.Project = _copyReportTemplate.Project;
            this.Page = _copyReportTemplate.Page;
            this.View = _copyReportTemplate.View;
            this.Fields = _copyReportTemplate.Fields.Clone();
        }

        protected override SObject CloneObj()
        {
            return new SReportTemplate(this);
        }

        public SReportTemplate Clone()
        {
            return (SReportTemplate)this.CloneObj();
        }
    }
}
