using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;

namespace WebScrapApp.Core.Form
{
    public class SValidationRuleReportTemplateName : ValidationRule
    {
        public override ValidationResult Validate(object _value, CultureInfo _cultureInfo)
        {
            ValidationResult ret = ValidationResult.ValidResult;
            string errorCheck = SValidationRuleReportTemplateName.GetCheckError(_value, _cultureInfo);

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
                ret = "Наименование не может быть пустым";
            }
            else if (value.Length > 50)
            {
                ret = "Наименование должно быть не длиннее 50 символов";
            }
            else if (SWorkFiles.ExistReportTemplate(new SReportTemplate() { Name = value }))
            {
                ret = $"Шаблон отчета с наименованием {value} уже существует";
            }

            return ret;
        }
    }
}
