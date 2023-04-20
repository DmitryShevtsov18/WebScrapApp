using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;

namespace WebScrapApp.Core.Form
{
    public class SMultiValueConverterReportTemplateOKToBool : IMultiValueConverter
    {
        public object Convert(object[] _values, Type _targetType, object _parameter, CultureInfo _culture)
        {
            string nameError = SValidationRuleReportTemplateName.GetCheckError(_values[0], _culture);
            string projectError = SValidationRuleReportTemplateProject.GetCheckError(_values[1], _culture);
            string pageError = SValidationRuleReportTemplatePage.GetCheckError(_values[2], _culture);
            string viewError = SValidationRuleReportTemplateView.GetCheckError(_values[3], _culture);
            bool listBoxSelectedFieldsHasItems = (bool)_values[4];
            bool ret = true;

            if (!string.IsNullOrWhiteSpace(nameError))
            {
                ret = false;
            }
            else if (!string.IsNullOrWhiteSpace(projectError))
            {
                ret = false;
            }
            else if (!string.IsNullOrWhiteSpace(pageError))
            {
                ret = false;
            }
            else if (!string.IsNullOrWhiteSpace(viewError))
            {
                ret = false;
            }
            else if (!listBoxSelectedFieldsHasItems)
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
