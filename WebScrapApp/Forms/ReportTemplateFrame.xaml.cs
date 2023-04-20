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

            this.OpenType = SFrameOpenType.Create;

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
            TextBlockReportTemplateName.DataContext = reportTemplate;
            TextBoxName.DataContext = reportTemplate;
            ComboBoxProject.DataContext = reportTemplate;            
        }

        private void InitReportTemplate()
        {
            reportTemplate = new SReportTemplate();
        }

        private void BindComboBoxPage()
        {
            ComboBoxPage.DataContext = null;
            ComboBoxPage.DataContext = reportTemplate;
        }

        private void BindComboBoxView()
        {
            ComboBoxView.DataContext = null;
            ComboBoxView.DataContext = reportTemplate;
        }

        private void BindListBoxSelectedFields()
        {
            ListBoxSelectedFields.ItemsSource = null;
            ListBoxSelectedFields.ItemsSource = reportTemplate.Fields.Cast<SViewField>().ToList<SViewField>();
        }

        private void BindListBoxRemainFields()
        {
            ListBoxRemainFields.ItemsSource = null;
            ListBoxRemainFields.ItemsSource = remainFields;
        }

        private void LoadListBoxFields()
        {
            remainFields = reportTemplate.ViewFields;
            reportTemplate.Fields.Clear();
        }

        private void SelectField(SViewField _sViewField)
        {
            remainFields.Remove(_sViewField);
            reportTemplate.Fields.Add(_sViewField);
        }

        private void UnSelectField(SViewField _sViewField)
        {
            remainFields.Add(_sViewField);
            reportTemplate.Fields.Remove(_sViewField);
        }

        private void BindListBoxesFields(bool _needLoad = false)
        {
            if (_needLoad)
            {
                this.LoadListBoxFields();
            }

            this.BindListBoxSelectedFields();
            this.BindListBoxRemainFields();

            if (_needLoad)
            {
                if (ListBoxRemainFields.HasItems)
                {
                    ListBoxRemainFields.SelectedIndex = 0;
                }
            }
        }

        private void Button_Click(Object _sender, RoutedEventArgs _e)
        {
            Button button = _sender as Button;

            switch (button.Name)
            {
                case "ButtonSelectAllFields":
                    this.ButtonSelectAllFieldClick();
                    break;
                case "ButtonSelectField":
                    this.ButtonSelectFieldClick();
                    break;
                case "ButtonUnSelectField":
                    this.ButtonUnSelectFieldClick();
                    break;
                case "ButtonUnSelectAllFields":
                    this.ButtonUnSelectAllFieldClick();
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

        private void ButtonSelectAllFieldClick()
        {
            if (ListBoxRemainFields.HasItems)
            {
                List<SViewField> listFields = ListBoxRemainFields.Items.Cast<SViewField>().ToList<SViewField>();
                foreach (var sViewField in listFields)
                {                 
                    this.SelectField(sViewField);
                    this.BindListBoxesFields();
                }
                ListBoxSelectedFields.SelectedIndex = 0;
            }
        }

        private void ButtonSelectFieldClick()
        {
            if (ListBoxRemainFields.SelectedItem != null)
            {
                SViewField sViewField = ListBoxRemainFields.SelectedItem as SViewField;
                int index = ListBoxRemainFields.SelectedIndex - 1;

                this.SelectField(sViewField);
                this.BindListBoxesFields();

                ListBoxSelectedFields.SelectedItem = sViewField;
                index = index >= 0 ? index : ListBoxRemainFields.HasItems ? 0 : -1;
                if (index >= 0)
                {
                    ListBoxRemainFields.SelectedIndex = index;
                }
            }
        }

        private void ButtonUnSelectFieldClick()
        {
            if (ListBoxSelectedFields.SelectedItem != null)
            {
                SViewField sViewField = ListBoxSelectedFields.SelectedItem as SViewField;
                int index = ListBoxSelectedFields.SelectedIndex - 1;

                this.UnSelectField(sViewField);
                this.BindListBoxesFields();

                ListBoxRemainFields.SelectedItem = sViewField;
                index = index >= 0 ? index : ListBoxSelectedFields.HasItems ? 0 : -1;
                if (index >= 0)
                {
                    ListBoxSelectedFields.SelectedIndex = index;
                }
            }
        }

        private void ButtonUnSelectAllFieldClick()
        {
            if (ListBoxSelectedFields.HasItems)
            {
                List<SViewField> listFields = ListBoxSelectedFields.Items.Cast<SViewField>().ToList<SViewField>();
                foreach (var sViewField in listFields)
                {
                    this.UnSelectField(sViewField);
                    this.BindListBoxesFields();
                }
                ListBoxRemainFields.SelectedIndex = 0;
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
                this.BindComboBoxView();
                this.BindListBoxesFields(true);
            }
        }

        private void ComboBoxPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                reportTemplate.Page = ((SPage)e.AddedItems[0]).Name;
                this.BindComboBoxView();
                this.BindListBoxesFields(true);
            }
        }

        private void ComboBoxView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                reportTemplate.View = ((SView)e.AddedItems[0]).Name;
                this.BindListBoxesFields(true);
            }
        }
    }
}
