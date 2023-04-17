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
using MaterialDesignThemes.Wpf;
using WebScrapApp.Core;

namespace WebScrapApp.Forms
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class ReportsFrame : UserControl
    {
        List<SReportTemplate> listReportTemplates;
        List<SReport> listReports;

        public ReportsFrame()
        {
            InitializeComponent();

            //this.Load();
        }

        private void Load()
        {
            //this.LoadLists();
            //this.BindListViewReportTemplates();
            //this.BindListViewReports();
        }

        private void Button_Click(Object _sender, RoutedEventArgs _e)
        {
            Button button = _sender as Button;

            switch (button.Name)
            {
                case "ButtonReportTemplateCreate":
                    this.ButtonReportTemplateCreateClick();
                    break;
                case "ButtonReportTemplateDelete":
                    this.ButtonReportTemplateDeleteClick();
                    break;
                case "ButtonReportTemplateScrap":
                    this.ButtonReportTemplateScrapClick();
                    break;
                case "ButtonReportDelete":
                    this.ButtonReportDeleteClick();
                    break;
                case "ButtonReportExcel":

                    break;
                default:
                    throw new Exception("");
            }
        }

        private void LoadLists()
        {
            //listReportTemplates = SWorkFiles.ReadReportTemplates();
            //listReports = SWorkFiles.ReadReports();
        }

        private void BindListViewReportTemplates(bool _needSelectToListView = true)
        {
            /*ListViewReportTemplates.ItemsSource = null;

            if (listReportTemplates.Count != 0)
            {
                ListViewReportTemplates.ItemsSource = listReportTemplates;

                if (_needSelectToListView)
                {
                    ListViewReportTemplates.SelectedItem = listReportTemplates[0];
                }
            }*/
        }
        
        private void BindListViewReports(bool _needSelectToListView = true)
        {
            /*ListViewReports.ItemsSource = null;

            if (listReports.Count != 0)
            {
                string reportTemplateName = ListViewReportTemplates.Items.Count > 0 ? ((SReportTemplate)ListViewReportTemplates.SelectedItem).Name : "";
                List<SReport> listReportsByReportTemplate = listReports.Where<SReport>(x => x.Template == reportTemplateName).ToList<SReport>();

                ListViewReports.ItemsSource = listReportsByReportTemplate;

                if (_needSelectToListView)
                {
                    ListViewReports.SelectedItem = listReportsByReportTemplate[0];
                }
            }*/
        }

        private void ButtonReportTemplateCreateClick()
        {
            /*ReportTemplate reportTemplate = new ReportTemplate();
            reportTemplate.ShowDialog();
            if (reportTemplate.DialogResult == true)
            {
                SReportTemplate sReportTemplate = reportTemplate.GetReportTemplate();
                SWorkFiles.WriteReportTemplate(sReportTemplate);
                listReportTemplates.Add(sReportTemplate);
                this.BindListViewReportTemplates();
                ListViewReportTemplates.SelectedItem = sReportTemplate;
            }*/
        }

        private async void ButtonReportTemplateDeleteClick()
        {
            /*var reportTemplate = (SReportTemplate)ListViewReportTemplates.SelectedItem;
            int index = ListViewReportTemplates.SelectedIndex - 1;
            var dialogDelete = new SDialogDelete();
            dialogDelete.Message = $"Вы действительно хотите удалить отчет {reportTemplate.Name}?";
            var dialogResult = await DialogHost.Show(dialogDelete, "DialogHostReports");

            if (dialogResult is bool b && b)
            {
                SWorkFiles.DeleteReportTemplate(reportTemplate);
                listReportTemplates.Remove(reportTemplate);
                this.BindListViewReportTemplates(false);

                index = index >= 0 ? index : ListViewReportTemplates.Items.Count > 0 ? 0 : -1;
                if (index >= 0)
                {
                    ListViewReportTemplates.SelectedIndex = index;                    
                }

                this.DeleteReportByReportTemplate(reportTemplate);
                this.BindListViewReports();
            }*/
        }

        private async void ButtonReportTemplateScrapClick()
        {            
            /*var reportTemplate = (SReportTemplate)ListViewReportTemplates.SelectedItem;
            SExceptionResult result = await new SReportCreate(reportTemplate).Create();
            if (result.Type == SExceptionType.None)
            {
                SReport report = result.Report;
                if (report != null)
                {
                    SWorkFiles.WriteReport(report);
                    listReports.Add(report);
                    this.BindListViewReports(false);
                    ListViewReports.SelectedItem = report;
                }
            }
            else
            {
                // TODO: need dialog
                MessageBox.Show(result.Message);
            }*/
        }

        private async void ButtonReportDeleteClick()
        {
            /*var report = (SReport)ListViewReports.SelectedItem;
            int index = ListViewReports.SelectedIndex - 1;
            var dialogDelete = new SDialogDelete();
            dialogDelete.Message = $"Вы действительно хотите удалить отчет {report.Name}?";
            var dialogResult = await DialogHost.Show(dialogDelete, "DialogHostReports");

            if (dialogResult is bool b && b)
            {
                SWorkFiles.DeleteReport(report);
                listReports.Remove(report);
                this.BindListViewReports(false);

                index = index >= 0 ? index : ListViewReports.Items.Count > 0 ? 0 : -1;
                if (index >= 0)
                {
                    ListViewReports.SelectedIndex = index;
                }
            }*/
        }

        private void DeleteReportByReportTemplate(SReportTemplate _reportTamplate)
        {            
            /*List <SReport> listReportsByReportTemplate = listReports.Where<SReport>(x => x.Template == _reportTamplate.Name).ToList<SReport>();

            foreach (var report in listReportsByReportTemplate)
            {
                SWorkFiles.DeleteReport(report);
                listReports.Remove(report);
            }*/
        }
    }
}
