using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for Page.xaml
    /// </summary>
    public partial class PageFrame : UserControl
    {
        private SProject project;
        private SPage page;
        private bool isEdit;
        private List<SView> listViews;
        private List<SView> listViewsOfPage;
        private SView selectedView;
        private int selectedViewIndex;

        public event EventHandler OnClosing;

        public bool DialogResult { get; set; } = false;

        public SFrameOpenType OpenType { get; set; } = SFrameOpenType.Create;

        public PageFrame(SProject _project)
        {
            InitializeComponent();

            project = _project;
            this.OpenType = SFrameOpenType.Create;

            this.Load();
        }

        public PageFrame(SProject _project, SPage _page, bool _isEdit = false)
        {
            InitializeComponent();

            project = _project;
            page = _page;
            isEdit = _isEdit;
            this.OpenType = _isEdit ? SFrameOpenType.Edit : SFrameOpenType.Copy;
            
            this.Load();
        }

        public SPage GetSPage()
        {
            return page;
        }

        private void Load()
        {
            this.initPage();

            this.BindForm();
            this.UpdateDesign();
            this.LoadListViews();
            this.LoadListViewsOfPage();
            this.BindListViewViews();
            this.SelectView();
        }

        private void initPage()
        {
            if (page is null)
            {
                page = new SPage();
            }
            page.Project = project.Name;
        }

        private void LoadListViews()
        {
            listViews = SWorkFiles.ReadViews();
        }

        private void LoadListViewsOfPage()
        {
            if (listViews != null)
            {
                listViewsOfPage = listViews.Where<SView>(x => x.Page == page.Name).ToList<SView>();
            }
        }

        private void BindListViewViews()
        {
            ListViewViews.ItemsSource = null;
            if (listViews.Count > 0)
            {
                ListViewViews.ItemsSource = listViewsOfPage;
            }
        }

        private void BindForm()
        {
            TextBlockPageName.DataContext = page;
            TextBoxName.DataContext = page;
            TextBoxDescription.DataContext = page;
            TextBoxUrl.DataContext = page;
            RadioScrollTypeDynamic.DataContext = page;
            RadioScrollTypeDynamicLink.DataContext = page;
            RadioScrollTypeNumericPages.DataContext = page;
            ComboBoxScrollTag.DataContext = page;
            TextBoxClass.DataContext = page;

            CheckBoxIsEditForm.IsChecked = isEdit;
            TextBoxProjectName.Text = page.Project;
        }

        private void UpdateDesign()
        {
            TextBoxName.IsReadOnly = isEdit;
        }

        private void SelectView(SView _view)
        {
            if (ListViewViews.Items.Count > 0)
            {
                ListViewViews.SelectedItem = _view;
            }
        }

        private void SelectView(int _index = 0)
        {
            //if (ListViewViews.Items.Count > 0)
            {
                ListViewViews.SelectedIndex = _index;
            }
        }

        private void WriteView(SView _view, bool _isCreate = false, bool _isChange = false)
        {
            if (_isCreate)
            {
                SWorkFiles.WriteView(_view);
                listViews.Add(_view);
            }
            else
            {
                if (_view != null && listViews.IndexOf(selectedView) >= 0)
                {
                    SWorkFiles.WriteView(_view);

                    if (_isChange)
                    {
                        listViews.Remove(selectedView);
                        listViews.Insert(selectedViewIndex, _view);
                    }
                }
            }
        }

        private void RemoveView(SView _view)
        {
            if (_view != null)
            {
                SWorkFiles.DeleteView(_view);
                listViews.Remove(_view);
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

        private void OpenFrame(ViewFrame _viewFrame)
        {
            GridFrame.Children.Clear();

            if (_viewFrame != null)
            {
                GridFrame.Children.Add(_viewFrame);
                this.ChangeGrid(true);
            }
        }

        private void CloseFrame(object _sender, EventArgs _e)
        {
            ViewFrame viewFrame = _sender as ViewFrame;

            this.ChangeGrid();

            if (viewFrame.DialogResult)
            {
                SView sView = viewFrame.GetSView();

                switch (viewFrame.OpenType)
                {
                    case SFrameOpenType.Create:
                        this.WriteView(sView, true);
                        break;
                    case SFrameOpenType.Edit:
                        this.WriteView(sView, false, true);
                        break;
                    case SFrameOpenType.Copy:
                        this.WriteView(sView, true);
                        break;
                    default:
                        throw new Exception("");
                }

                this.LoadListViewsOfPage();
                this.BindListViewViews();
                this.SelectView(sView);
            }
        }

        private void Button_Click(Object _sender, RoutedEventArgs _e)
        {
            Button button = _sender as Button;

            switch (button.Name)
            {
                case "ButtonViewCreate":
                    this.ButtonViewCreateClick();
                    break;
                case "ButtonViewEdit":
                    this.ButtonViewEditClick();
                    break;
                case "ButtonViewDelete":
                    this.ButtonViewDeleteClick();
                    break;
                case "ButtonViewCopy":
                    this.ButtonViewCopyClick();
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

        private void ListViewViews_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                selectedView = (SView)e.AddedItems[0];
                if (selectedView != null)
                {
                    selectedViewIndex = ListViewViews.Items.IndexOf(selectedView);
                }
            }
        }

        private void ButtonViewCreateClick()
        {
            ViewFrame viewFrame = new ViewFrame(page);
            viewFrame.OnClosing += CloseFrame;

            this.OpenFrame(viewFrame);
        }

        private void ButtonViewEditClick()
        {
            SView sView = selectedView.Clone();
            ViewFrame viewFrame = new ViewFrame(page, sView, true);
            viewFrame.OnClosing += CloseFrame;

            this.OpenFrame(viewFrame);
        }

        private async void ButtonViewDeleteClick()
        {
            var dialogDelete = new SDialogDelete();
            dialogDelete.Message = $"Вы действительно хотите удалить объект {selectedView.Name}?";
            var dialogResult = await DialogHost.Show(dialogDelete, "DialogHostWindow");

            if (dialogResult is bool b && b)
            {
                this.RemoveView(selectedView);
                this.LoadListViewsOfPage();
                this.BindListViewViews();
                int index = selectedViewIndex > 0 ? selectedViewIndex - 1 : listViews.Count > 0 ? 0 : -1;
                this.SelectView(index);
            }
        }

        private void ButtonViewCopyClick()
        {
            SView sView = selectedView.Clone();
            ViewFrame viewFrame = new ViewFrame(page, sView);
            viewFrame.OnClosing += CloseFrame;

            this.OpenFrame(viewFrame);
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
        
    }
}
