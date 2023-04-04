using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;

namespace WebScrapApp.Core.Form
{
    public class SValidationRuleReportTemplateView : ValidationRule
    {
        public override ValidationResult Validate(object _value, CultureInfo _cultureInfo)
        {
            ValidationResult ret = ValidationResult.ValidResult;
            string errorCheck = SValidationRuleReportTemplateView.GetCheckError(_value, _cultureInfo);

            if (!string.IsNullOrEmpty(errorCheck))
            {
                ret = new ValidationResult(false, errorCheck);
            }

            return ret;
        }

        public static string GetCheckError(object _value, CultureInfo _cultureInfo)
        {
            string value = (_value ?? "").ToString();
            string ret = "";

            if (string.IsNullOrWhiteSpace(value))
            {
                ret = "Объект должен быть выбран";
            }

            return ret;
        }
    }
}
