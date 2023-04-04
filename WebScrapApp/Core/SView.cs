using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebScrapApp.Core
{
    public class SView : SObject
    {
        public string Page { get; set; }

        public SViewType ViewType { get; set; } = SViewType.Card;

        public STag Tag { get; set; } = STag.div;

        public string Class { get; set; }

        [XmlIgnore]
        public string Selector
        {
            get
            {
                return $"{this.Tag}.{this.Class.Replace(' ', '.')}";
            }
        }

        public SViewFieldCollection Fields { get; set; } = new SViewFieldCollection();

        public SView() : base()
        {
            
        }

        public SView(string _page) : base()
        {
            this.Page = _page;
        }

        protected SView(SView _copyView) : base(_copyView) 
        { 
            this.Page = _copyView.Page;
            this.ViewType = _copyView.ViewType;
            this.Tag = _copyView.Tag;
            this.Class = _copyView.Class;
            this.Fields = _copyView.Fields.Clone();
        }

        protected override SObject CloneObj()
        {
            return new SView(this);
        }

        public SView Clone()
        {
            return (SView)this.CloneObj();
        }

        public new string GetFilename()
        {
            return $"{this.Page}_{this.Name}";
        }
    }
}
