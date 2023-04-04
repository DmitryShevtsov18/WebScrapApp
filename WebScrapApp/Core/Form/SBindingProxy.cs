using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WebScrapApp.Core.Form
{
    public class SBindingProxy : Freezable
    {
        public static readonly DependencyProperty DataProperty = 
            DependencyProperty.Register(nameof(Data), typeof(object), typeof(SBindingProxy), new UIPropertyMetadata(null));

        protected override Freezable CreateInstanceCore()
        {
            return new SBindingProxy();
        }

        public object Data
        {
            get 
            { 
                return GetValue(DataProperty); 
            }
            set 
            { 
                SetValue(DataProperty, value); 
            }
        }        
    }
}
