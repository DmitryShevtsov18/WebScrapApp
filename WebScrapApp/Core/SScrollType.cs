using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapApp.Core
{
    [TypeConverter(typeof(SEnumDescriptionTypeConverter))]
    public enum SScrollType
    {
        [Description("Динамеческая")]
        Dynamic,
        [Description("По кнопке")]
        DynamicLink,
        [Description("Страницы")]
        NumericPages
    }
}
