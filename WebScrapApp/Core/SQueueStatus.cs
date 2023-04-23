using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapApp.Core
{
    [TypeConverter(typeof(SEnumDescriptionTypeConverter))]
    public enum SQueueStatus
    {
        [Description("Отложено")]
        Shelve,
        [Description("В очереди")]
        Queue,
        [Description("В процессе")]
        Processing,
        [Description("Завершено")]
        Completed,
        [Description("Отменено")]
        Canceled,
        [Description("Ошибка")]
        Error
    }
}
