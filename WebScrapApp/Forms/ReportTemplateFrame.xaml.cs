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
    public partial class ReportTemplateFrame : UserControl
    {
        SReportTemplate reportTemplate;
        SViewFieldCollection remainFields;

        public event EventHandler OnClosing;

        public bool DialogResult { get; set; } = false;

        public SFrameOpenType OpenType { get; set; } = SFrameOpenType.Create;

        public ReportTemplateFrame()
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
            this.InitReportTemplate();
            this.BindForm();
        }

        private void BindForm()
        {
            TextBoxName.DataContext = reportTemplate;
            ComboBoxProject.DataContext = reportTemplate;
        }

        private void InitReportTemplate()
        {
            reportTemplate = new SReportTemplate();
        }

        private void BindComboBoxPage()
        {
            ComboBoxPage.DataContext = reportTemplate;
        }

        private void BindComboBoxView()
        {
            ComboBoxView.DataContext = reportTemplate;
        }

        private void BindListBoxSelectedFields()
        {
            ListBoxSelectedFields.ItemsSource = reportTemplate.Fields.Cast<SViewField>().ToList<SViewField>();
        }

        private void BindListBoxRemainFields()
        {
            ListBoxRemainFields.ItemsSource = remainFields;
        }

        private void LoadListBoxFields()
        {
            remainFields = reportTemplate.ViewFields;
            reportTemplate.Fields.Clear();
        }

        private void Button_Click(Object _sender, RoutedEventArgs _e)
        {
            Button button = _sender as Button;

            switch (button.Name)
            {
                case "ButtonSelectAllFields":
                    break;
                case "ButtonSelectField":
                    break;
                case "ButtonUnSelectField":
                    break;
                case "ButtonUnSelectAllFields":
                    break;
                case "ButtonOK":
                    this.ButtonOKClick();
                    break;
                case "ButtonClose":
                case "ButtonCancel":
                    this.ButtonCancelClick();
                    break;
                default:
                    throw new Exception("");
            }
        }

        private void ButtonOKClick()
        {
            this.DialogResult = true;
            this.OnClosing(this, EventArgs.Empty);
        }

        private void ButtonCancelClick()
        {
            this.OnClosing(this, EventArgs.Empty);
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
                this.LoadListBoxFields();
                this.BindListBoxSelectedFields();
                this.BindListBoxRemainFields();
            }
        }
    }
}
