using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;

namespace WebScrapApp.Core.Form
{
    public class SValueConverterBoolInvertor : IValueConverter
    {
        public object Convert(object _value, Type _targetType, object _parameter, CultureInfo _cultureInfo)
        {
            return !(bool)_value;
        }

        public object ConvertBack(object _value, Type _targetType, object _parameter, CultureInfo _cultureInfo)
        {
            return null;
        }
    }
}
