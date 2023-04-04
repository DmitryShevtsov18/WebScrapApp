using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;

namespace WebScrapApp.Core.Form
{
    public class SMultiValueConverterViewFieldOKToBool : IMultiValueConverter
    {
        public object Convert(object[] _values, Type _targetType, object _parameter, CultureInfo _culture)
        {
            string nameError = SValidationRuleViewFieldName.GetCheckError(_values[0], _culture);
            string classError = SValidationRuleViewFieldClass.GetCheckError(_values[1], _culture);
            bool ret = true;

            if (!string.IsNullOrWhiteSpace(nameError))
            {
                ret = false;
            }
            else if (!string.IsNullOrWhiteSpace(classError))
            {
                ret = false;
            }

            return ret;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
