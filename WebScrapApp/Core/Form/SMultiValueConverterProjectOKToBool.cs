using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;

namespace WebScrapApp.Core.Form
{
    public class SMultiValueConverterProjectOKToBool : IMultiValueConverter
    {
        public object Convert(object[] _values, Type _targetType, object _parameter, CultureInfo _cultureInfo)
        {
            string nameError = SValidationRuleProjectName.GetCheckError(_values[0], _cultureInfo);
            bool ret = true;

            if (!string.IsNullOrEmpty(nameError))
            {
                ret = false;
            }

            return ret;
        }

        public object[] ConvertBack(object _value, Type[] _targetTypes, object _parameter, CultureInfo _cultureInfo)
        {
            return null;
        }
    }
}
