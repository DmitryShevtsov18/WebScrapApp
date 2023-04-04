using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;

namespace WebScrapApp.Core.Form
{
    public class SValidationRuleViewName : ValidationRule
    {
        public SControlCustomProperties CustomProperties { get; set; }

        public override ValidationResult Validate(object _value, CultureInfo _cultureInfo)
        {
            ValidationResult ret = ValidationResult.ValidResult;
            string errorCheck = SValidationRuleViewName.GetCheckError(_value, _cultureInfo, this.CustomProperties);

            if (!string.IsNullOrEmpty(errorCheck))
            {
                ret = new ValidationResult(false, errorCheck);
            }

            return ret;
        }

        public static string GetCheckError(object _value, CultureInfo _cultureInfo, SControlCustomProperties _customProperties)
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
            else if (!_customProperties.IsEditForm && SWorkFiles.ExistView(new SView(_customProperties.Page) { Name = value }))
            {
                ret = $"Объект с наименованием {value} уже существует";
            }

            return ret;
        }
    }
}
