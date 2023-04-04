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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WebScrapApp.Core;

namespace WebScrapApp.Forms
{
    /// <summary>
    /// Interaction logic for ReportTemplate.xaml
    /// </summary>
    public partial class ReportTemplate : Window
    {
        SReportTemplate reportTemplate;
        List<SProject> listProjects;

        public ReportTemplate()
        {
            InitializeComponent();
            this.Load();
        }

        public SReportTemplate GetReportTemplate()
        {
            return reportTemplate;
        }

        private void Load()
        {
            reportTemplate = new SReportTemplate();
            listProjects = SWorkFiles.ReadProjects();

            this.BindReportTemplate();
        }

        private void Button_Click(Object _sender, RoutedEventArgs _e)
        {
            Button button = _sender as Button;

            switch (button.Name)
            {
                case "ButtonOK":
                    this.ButtonOKClick();
                    break;
                case "ButtonCancel":
                    this.ButtonCancelClick();
                    break;
                default:
                    throw new Exception("");
            }
        }

        private void BindReportTemplate()
        {
            TextBoxName.DataContext = reportTemplate;            
            ComboBoxProject.DataContext = reportTemplate;
        }

        private void BindComboBoxPage()
        {            
            ComboBoxPage.DataContext = reportTemplate;
        }

        private void BindComboBoxView()
        {            
            ComboBoxView.DataContext = reportTemplate;
        }

        private void ButtonOKClick()
        {
            this.DialogResult = true;
            this.Close();
        }

        private void ButtonCancelClick()
        {
            this.Close();
        }

        private void ComboBoxProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                reportTemplate.Project = ((SProject)e.AddedItems[0]).Name;
                this.BindComboBoxPage();
            }
        }

        private void ComboBoxPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                reportTemplate.Page = ((SPage)e.AddedItems[0]).Name;
                this.BindComboBoxView();
            }
        }

        private void ComboBoxView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                reportTemplate.View = ((SView)e.AddedItems[0]).Name;
            }
        }
    }
}
