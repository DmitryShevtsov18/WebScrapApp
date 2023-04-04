using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace WebScrapApp.Core
{
    internal class SEnumDescriptionTypeConverter : EnumConverter
    {
        public SEnumDescriptionTypeConverter(Type _type) : base(_type)
        {

        }

        public override object ConvertTo(ITypeDescriptorContext _context, CultureInfo _culture, object _value, Type _destinationType)
        {
            if (_destinationType == typeof(string))
            {
                if (_value != null)
                {
                    FieldInfo fi = _value.GetType().GetField(_value.ToString());
                    if (fi != null)
                    {
                        var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                        return ((attributes.Length > 0) && (!String.IsNullOrEmpty(attributes[0].Description))) ? attributes[0].Description : _value.ToString();
                    }
                }

                return string.Empty;
            }

            return base.ConvertTo(_context, _culture, _value, _destinationType);
        }
    }
}
