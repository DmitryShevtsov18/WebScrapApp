using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapApp.Core
{
    public enum SQueueStatus
    {
        Shelve,
        Queue,
        Processing,
        Completed,
        Canceled,
        Error
    }
}
