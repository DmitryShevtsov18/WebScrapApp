using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebScrapApp.Core
{
    public class SViewField : SObject
    {
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

        public SViewField() : base()
        {
            
        }

        protected SViewField(SViewField _copyViewField) : base(_copyViewField) 
        {
            this.Tag = _copyViewField.Tag;
            this.Class = _copyViewField.Class;
        }

        protected override SObject CloneObj()
        {
            return new SViewField(this);
        }

        public SViewField Clone()
        {
            return (SViewField)this.CloneObj();
        }
    }
}
