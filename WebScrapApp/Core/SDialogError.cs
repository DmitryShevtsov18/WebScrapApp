using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapApp.Core
{
    public class SDialogError
    {
        public string Header { get; set; }

        public string Message { get; set; }

        public SDialogError()
        {
            this.Header = "Ошибка";
        }
    }
}
