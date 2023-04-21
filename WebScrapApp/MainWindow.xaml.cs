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
    public partial class MainWindow : Window
    {
        UserControl currentFrame;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object _sender, RoutedEventArgs _e)
        {
            Button button = _sender as Button;

            switch (button.Name)
            {
                case "ButtonOpenMenu":
                    this.ButtonOpenMenuClick();
                    break;
                case "ButtonCloseMenu":
                    this.ButtonCloseMenuClick();
                    break;
                case "ButtonMinimize":
                    this.ButtonMinimizeClick();
                    break;
                case "ButtonMaximize":
                    this.ButtonMaximizeClick();
                    break;
                case "ButtonRestore":
                    this.ButtonRestoreClick();
                    break;
                case "ButtonClose":
                    this.ButtonCloseClick();
                    break;
                default:
                    throw new Exception("");
            }
        }

        private void ButtonOpenMenuClick()
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenuClick()
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ButtonMinimizeClick()
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ButtonMaximizeClick()
        {
            this.WindowState = WindowState.Maximized;
            ButtonMaximize.Visibility = Visibility.Hidden;
            ButtonRestore.Visibility = Visibility.Visible;
        }

        private void ButtonRestoreClick()
        {
            this.WindowState = WindowState.Normal;
            ButtonMaximize.Visibility = Visibility.Visible;
            ButtonRestore.Visibility = Visibility.Hidden;
        }

        private void ButtonCloseClick()
        {
            this.Close();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {                        
            GridMain.Children.Clear();
            currentFrame = null;

            if (e.AddedItems.Count > 0)
            {
                switch (((ListViewItem)e.AddedItems[0]).Name)
                {
                    case "ItemHome":                        
                        break;
                    case "ItemProjects":
                        currentFrame = new ProjectsFrame();
                        break;
                    case "ItemParsers":
                        break;
                    case "ItemReports":
                        currentFrame = new ReportsFrame();
                        break;
                    case "ItemCompare":
                        break;
                    case "ItemQueuis":
                        currentFrame = new QueueFrame();
                        break;
                    default:
                        break;
                }

                if (currentFrame != null)
                {
                    GridMain.Children.Add(currentFrame);
                }
            }
        }
    }
}
