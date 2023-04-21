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
        private string project;
        public string Project 
        {
            get
            {
                return project;
            }
            set
            {
                if (project != value)
                {
                    page = string.Empty;
                    view = string.Empty;
                    this.Fields = new SViewFieldCollection();
                }

                project = value;
            }
        }

        [XmlIgnore]
        public List<SProject> Projects
        {
            get
            {
                return SWorkFiles.ReadProjects();
            }
        }

        private string page;
        public string Page 
        {
            get
            {
                return page;
            }
            set
            {
                if (page != value)
                {
                    view = string.Empty;
                }

                page = value;
            }
        }

        [XmlIgnore]
        public List<SPage> Pages
        {
            get
            {
                return SWorkFiles.ReadPages().Where<SPage>(x => x.Project == this.Project).ToList<SPage>();
            }
        }

        private string view;
        public string View 
        {
            get
            {
                return view;
            }
            set
            {
                if (view != value)
                {
                    this.Fields = new SViewFieldCollection();
                }

                view = value;
            }
        }

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
                return $"{this.Name}_{this.Project}_{this.Page}_{this.View}_{DateTime.Now.ToString("dd_MM_yyyy_HHmmss")}";
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

        [XmlIgnore]
        public string ViewFieldsString
        {
            get 
            {
                string fields = string.Empty;

                foreach (SViewField sViewField in this.Fields)
                {
                    fields += $"{sViewField.Name}; ";
                }

                return fields;
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
