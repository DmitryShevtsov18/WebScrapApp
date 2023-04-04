using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;

namespace WebScrapApp.Core.Form
{
    public class SValueConverterEnumToBool : IValueConverter
    {
        public object Convert(object _value, Type _targetType, object _param, CultureInfo _culture)
        {
            return _value.Equals(_param);
        }

        public object ConvertBack(object _value, Type _targetType, object _param, CultureInfo _culture)
        {
            return (bool)_value ? _param : Binding.DoNothing;
        }
    }
}
