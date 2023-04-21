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
        private List<SReportTemplate> listReportTemplates;
        private SReportTemplate selectedReportTemplate;
        private int selectedReportTemplateIndex;
        private List<SReport> listReports;
        private List<SReport> listReportsOfReportTemplate;
        private SReport selectedReport;
        private int selectedReportIndex;

        public ReportsFrame()
        {
            InitializeComponent();

            this.Load();
        }

        private void Load()
        {
            this.LoadListReportTemplates();
            this.LoadListReports();
            this.BindListViewReportTemplates();
            this.SelectReportTemplate();
        }

        private void LoadListReportTemplates()
        {
            listReportTemplates = SWorkFiles.ReadReportTemplates();
        }

        private void LoadListReports()
        {
            listReports = SWorkFiles.ReadReports();
        }

        private void LoadListReportsOfReportTemplate()
        {
            if (listReports != null && selectedReportTemplate != null)
            {
                listReportsOfReportTemplate = listReports.Where<SReport>(x => x.Template == selectedReportTemplate.Name).ToList<SReport>();
            }
        }

        private void BindListViewReportTemplates()
        {
            ListViewReportTemplates.ItemsSource = null;
            if (listReportTemplates.Count > 0)
            {
                ListViewReportTemplates.ItemsSource = listReportTemplates;
            }
        }

        private void BindListViewReports()
        {
            ListViewReports.ItemsSource = null;
            if (listReportsOfReportTemplate.Count > 0)
            {
                ListViewReports.ItemsSource = listReportsOfReportTemplate;
            }
        }

        private void BindForm()
        {
            TextBlockReportTemplateName.DataContext = selectedReportTemplate;
        }

        private void SelectReportTemplate(SReportTemplate _sReportTemplate)
        {
            if (ListViewReportTemplates.HasItems)
            {
                ListViewReportTemplates.SelectedItem = _sReportTemplate;
            }
        }

        private void SelectReportTemplate(int _index = 0)
        {
            //if (ListViewProjects.Items.Count > 0)
            {
                ListViewReportTemplates.SelectedIndex = _index;
            }
        }

        private void SelectReport(SReport _report)
        {
            if (ListViewReports.HasItems)
            {
                ListViewReports.SelectedItem = _report;
            }
        }

        private void SelectReport(int _index = 0)
        {
            //if (ListViewPages.Items.Count > 0)
            {
                ListViewReports.SelectedIndex = _index;
            }
        }

        private void WriteReportTemplate(SReportTemplate _sReportTemplate)
        {
            if (_sReportTemplate != null)
            {
                SWorkFiles.WriteReportTemplate(_sReportTemplate);
                listReportTemplates.Add(_sReportTemplate);
            }
        }

        private void WriteReport(SReport _sReport)
        {
            if (_sReport != null)
            {
                SWorkFiles.WriteReport(_sReport);
                listReports.Add(_sReport);
            }
        }

        private void RemoveReportTemplate(SReportTemplate _sReportTemplate)
        {
            if (_sReportTemplate != null)
            {
                SWorkFiles.DeleteReportTemplate(_sReportTemplate);
                listReportTemplates.Remove(_sReportTemplate);
            }
        }

        private void RemoveReport(SReport _report)
        {
            if (_report != null)
            {
                SWorkFiles.DeleteReport(_report);
                listReports.Remove(_report);
            }
        }

        private void RemoveReportsByReportTemplate()
        {
            foreach (var report in listReportsOfReportTemplate)
            {
                this.RemoveReport(report);
            }
        }

        private void ChangeGrid(bool _openFrame = false)
        {
            if (_openFrame)
            {
                GridMain.Visibility = Visibility.Hidden;
                GridFrame.Visibility = Visibility.Visible;
            }
            else
            {
                GridMain.Visibility = Visibility.Visible;
                GridFrame.Visibility = Visibility.Hidden;
            }
        }

        private void OpenFrame(ReportTemplateFrame _reportTemplateFrame)
        {
            GridFrame.Children.Clear();

            if (_reportTemplateFrame != null)
            {
                GridFrame.Children.Add(_reportTemplateFrame);
                this.ChangeGrid(true);
            }
        }

        private void CloseFrame(object _sender, EventArgs _e)
        {
            ReportTemplateFrame reportTemplateFrame = _sender as ReportTemplateFrame;

            this.ChangeGrid();

            if (reportTemplateFrame.DialogResult)
            {
                SReportTemplate sReportTemplate = reportTemplateFrame.GetReportTemplate();                

                switch (reportTemplateFrame.OpenType)
                {
                    case SFrameOpenType.Create:
                        this.WriteReportTemplate(sReportTemplate);
                        break;
                    case SFrameOpenType.Edit:
                        break;
                    case SFrameOpenType.Copy:
                        break;
                    default:
                        throw new Exception("");
                }

                this.BindListViewReportTemplates();
                this.SelectReportTemplate(sReportTemplate);
            }
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

        private void ListViewReportTemplates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                selectedReportTemplate = (SReportTemplate)e.AddedItems[0];
                if (selectedReportTemplate != null)
                {
                    selectedReportTemplateIndex = ListViewReportTemplates.Items.IndexOf(selectedReportTemplate);
                }
                this.BindForm();
                this.LoadListReportsOfReportTemplate();
                this.BindListViewReports();
                this.SelectReport();                
            }
        }

        private void ListViewReports_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                selectedReport = (SReport)e.AddedItems[0];
                if (selectedReport != null)
                {
                    selectedReportIndex = ListViewReports.Items.IndexOf(selectedReport);
                }
            }
        }

        private void ButtonReportTemplateCreateClick()
        {
            ReportTemplateFrame reportTamplateFrame = new ReportTemplateFrame();
            reportTamplateFrame.OnClosing += CloseFrame;

            this.OpenFrame(reportTamplateFrame);
        }

        private async void ButtonReportTemplateDeleteClick()
        {
            var dialogDelete = new SDialogDelete();
            dialogDelete.Message = $"Вы действительно хотите удалить отчет {selectedReportTemplate.Name}?";
            var dialogResult = await DialogHost.Show(dialogDelete, "DialogHostWindow");

            if (dialogResult is bool b && b)
            {
                this.RemoveReportsByReportTemplate();
                this.RemoveReportTemplate(selectedReportTemplate);
                this.BindListViewReportTemplates();
                int index = selectedReportTemplateIndex > 0 ? selectedReportTemplateIndex - 1 : listReportTemplates.Count > 0 ? 0 : -1;
                this.SelectReportTemplate(index);
            }
        }

        private async void ButtonReportTemplateScrapClick()
        {
            SQueue sQueue = new SQueue(selectedReportTemplate.Name);
            sQueue.Name = selectedReportTemplate.ReportName;

            SWorkFiles.WriteQueue(sQueue);

            var dialogNotification = new SDialogNotification();
            dialogNotification.Message = $"Отчет {sQueue.Name} добавлен в очередь на выгрузку.";
            await DialogHost.Show(dialogNotification, "DialogHostWindow");

            /*SReportCreateExceptionResult result = await new SReportCreate(selectedReportTemplate).Create();
            if (result.Type == SExceptionType.None)
            {
                SReport sReport = result.Report;
                if (sReport != null)
                {
                    this.WriteReport(sReport);
                    this.LoadListReportsOfReportTemplate();
                    this.BindListViewReports();
                    this.SelectReport(sReport);
                }
            }
            else
            {
                MessageBox.Show(result.Message);
            }*/
        }

        private async void ButtonReportDeleteClick()
        {
            var dialogDelete = new SDialogDelete();
            dialogDelete.Message = $"Вы действительно хотите удалить отчет {selectedReport.Name}?";
            var dialogResult = await DialogHost.Show(dialogDelete, "DialogHostWindow");

            if (dialogResult is bool b && b)
            {
                this.RemoveReport(selectedReport);
                this.BindListViewReports();
                int index = selectedReportIndex > 0 ? selectedReportIndex - 1 : listReportsOfReportTemplate.Count > 0 ? 0 : -1;
                this.SelectReport(index);
            }
        }
    }
}
