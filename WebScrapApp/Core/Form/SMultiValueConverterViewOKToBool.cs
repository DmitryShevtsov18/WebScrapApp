using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;

namespace WebScrapApp.Core.Form
{
    public class SMultiValueConverterViewOKToBool : IMultiValueConverter
    {
        public object Convert(object[] _values, Type _targetType, object _parameter, CultureInfo _culture)
        {
            //string nameError = SValidationRuleViewName.GetCheckError(_values[0], _culture, new SControlCustomProperties() { Page = (string)_values[3], IsEditForm = (bool)_values[4] });            
            //string classError = SValidationRuleViewClass.GetCheckError(_values[1], _culture);            
            bool ret = true;
            /*
            if (!string.IsNullOrWhiteSpace(nameError))
            {
                ret = false;
            }
            else if (!string.IsNullOrWhiteSpace(classError))
            {
                ret = false;
            }
            */
            return ret;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
