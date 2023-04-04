using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapApp.Core
{
    public class SDialogDelete
    {
        public string Header { get; set; }

        public string Message { get; set; }

        public SDialogDelete()
        {
            this.Header = "Удалить";
        }
    }
}
