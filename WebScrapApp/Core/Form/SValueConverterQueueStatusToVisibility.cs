using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;
using System.Windows;

namespace WebScrapApp.Core.Form
{
    public class SValueConverterQueueStatusToVisibility : IValueConverter
    {
        public object Convert(object _value, Type _targetType, object _parameter, CultureInfo _cultureInfo)
        {
            SQueueStatus status = (SQueueStatus)_value;
            Visibility visibility = Visibility.Collapsed;
            string buttonName = (string)_parameter;

            switch (buttonName)
            {
                case "ButtonQueueShelve":
                    if (status == SQueueStatus.Queue)
                    {
                        visibility = Visibility.Visible;
                    }
                    break;
                case "ButtonQueueInQueue":
                    if (status == SQueueStatus.Shelve)
                    {
                        visibility = Visibility.Visible;
                    }
                    break;
                case "ButtonQueueCancel":
                    if (status == SQueueStatus.Shelve ||
                        status == SQueueStatus.Queue)
                    {
                        visibility = Visibility.Visible;
                    }
                    break;
                case "ButtonQueueOpen":
                    if (status == SQueueStatus.Completed)
                    {
                        visibility = Visibility.Visible;
                    }
                    break;
                case "ButtonQueueDelete":
                    if (status != SQueueStatus.Processing)
                    {
                        visibility = Visibility.Visible;
                    }
                    break;
            }

            return visibility;
        }

        public object ConvertBack(object _value, Type _targetType, object _parameter, CultureInfo _cultureInfo)
        {
            return null;
        }
    }
}
