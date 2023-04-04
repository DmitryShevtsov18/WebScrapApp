using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapApp.Core
{
    public abstract class SObject : Object
    {
        public string Name { get; set; }

        public string Description { get; set; }
        
        protected SObject()
        {

        }

        protected SObject(SObject _copyObject) : this()
        {            
            this.Name = _copyObject.Name;
            this.Description = _copyObject.Description;
        }

        protected abstract SObject CloneObj();        

        public string GetFilename()
        {
            return this.Name;
        }
    }
}
