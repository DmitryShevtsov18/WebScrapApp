using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WebScrapApp.Core.Form
{
    public class SControlCustomProperties : DependencyObject
    {
        public static readonly DependencyProperty PageProperty =
            DependencyProperty.Register("Page", typeof(string), typeof(SControlCustomProperties), new FrameworkPropertyMetadata(""));

        public string Page
        {
            get 
            { 
                return (string)GetValue(PageProperty); 
            }
            set 
            { 
                SetValue(PageProperty, value); 
            }
        }

        public static readonly DependencyProperty ProjectProperty =
            DependencyProperty.Register("Project", typeof(string), typeof(SControlCustomProperties), new FrameworkPropertyMetadata(""));

        public string Project
        {
            get
            {
                return (string)GetValue(ProjectProperty);
            }
            set
            {
                SetValue(ProjectProperty, value);
            }
        }

        public static readonly DependencyProperty IsEditFormProperty =
            DependencyProperty.Register("IsEditForm", typeof(bool), typeof(SControlCustomProperties), new FrameworkPropertyMetadata(false));

        public bool IsEditForm
        {
            get
            {
                return (bool)GetValue(IsEditFormProperty);
            }
            set
            {
                SetValue(IsEditFormProperty, value);
            }
        }
    }
}
