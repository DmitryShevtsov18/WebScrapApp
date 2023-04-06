using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebScrapApp.Forms;

namespace WebScrapApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow_old : Window
    {
        public MainWindow_old()
        {
            InitializeComponent();
        }

        private void Button_Click(object _sender, RoutedEventArgs _e)
        {
            Button button = _sender as Button;

            switch (button.Name)
            {
                case "ButtonMenuOpen":
                    this.ButtonMenuOpenClick();
                    break;
                case "ButtonProjects":
                    this.ButtonProjectsClick();
                    break;
                case "ButtonParsers":
                    break;
                case "ButtonReports":
                    this.ButtonReportsClick();
                    break;
                case "ButtonCompare":
                    break;
                case "ButtonSettings":
                    break;
                default:
                    throw new Exception("");
            }
        }

        private void ButtonMenuOpenClick()
        {
            NavDrawer.IsLeftDrawerOpen = false;
            if (ActualWidth > 1600)
            {                
                MenuToggleButton.Visibility = Visibility.Visible;
            }
        }

        private void ButtonProjectsClick()
        {
            this.ButtonMenuOpenClick();

            Projects projects = new Projects();
            projects.ShowDialog();
            if (projects.DialogResult == true)
            {
                //TODO: Reload panel projects
            }
        }

        private void ButtonReportsClick()
        {
            this.ButtonMenuOpenClick();

            Reports reports = new Reports();
            reports.ShowDialog();
            if (reports.DialogResult == true)
            {
                //TODO: Reload panel reports
            }
        }
    }
}
