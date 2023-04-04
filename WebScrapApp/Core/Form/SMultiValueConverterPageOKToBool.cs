using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;

namespace WebScrapApp.Core.Form
{
    public class SMultiValueConverterPageOKToBool : IMultiValueConverter
    {
        public object Convert(object[] _values, Type _targetType, object _parameter, CultureInfo _culture)
        {
            string nameError = SValidationRulePageName.GetCheckError(_values[0], _culture, new SControlCustomProperties() { Project = (string)_values[4], IsEditForm = (bool)_values[5] });
            string urlError = SValidationRulePageUrl.GetCheckError(_values[1], _culture);
            string classError = SValidationRulePageScrollClass.GetCheckError(_values[2], _culture);
            bool radioScrollTypeDynamic = (bool)_values[3];
            bool ret = true;

            if (!string.IsNullOrWhiteSpace(nameError))
            {
                ret = false;
            }
            else if (!string.IsNullOrWhiteSpace(urlError))
            {
                ret = false;
            }
            else if (!radioScrollTypeDynamic && !string.IsNullOrWhiteSpace(classError))
            {
                ret = false;
            }

            return ret;
        }

        public object[] ConvertBack(object _value, Type[] _targetTypes, object _parameter, CultureInfo _culture)
        {
            return null;
        }
    }
}
