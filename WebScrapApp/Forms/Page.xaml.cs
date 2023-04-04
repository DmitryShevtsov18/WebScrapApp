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
    public partial class Page : Window
    {
        private SProject project;
        private SPage page;
        private bool isEdit;
        private List<SView> listViews;
        private List<SView> listViewsOfPage;
        private SView selectedView;
        private int selectedViewIndex;

        public Page(SProject _project)
        {
            InitializeComponent();

            project = _project;            

            this.Load();
        }

        public Page(SProject _project, SPage _page, bool _isEdit = false)
        {
            InitializeComponent();

            project = _project;
            page = _page;
            isEdit = _isEdit;
            
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
            View view = new View(page);

            if (view.ShowDialog() == true)
            {
                SView sView = view.GetSView();
                this.WriteView(sView, true);
                this.LoadListViewsOfPage();
                this.BindListViewViews();
                this.SelectView(sView);
            }

            /*View view = new View(page);
            view.ShowDialog();
            if (view.DialogResult == true)
            {
                SView sView = view.GetSView();
                page.Views.Add(sView);
                ListViewViews.ItemsSource = page.Views.Cast<SView>().ToList<SView>();
                ListViewViews.SelectedItem = sView;
            }*/
        }

        private void ButtonViewEditClick()
        {
            SView sView = selectedView.Clone();
            View view = new View(page, sView, true);

            if (view.ShowDialog() == true)
            {
                sView = view.GetSView();
                this.WriteView(sView, false, true);
                this.LoadListViewsOfPage();
                this.BindListViewViews();
                this.SelectView(sView);
            }

            /*int index = ListViewViews.SelectedIndex;            
            SView sView = (SView)ListViewViews.SelectedItem;
            SView sViewEdit = sView.Clone();
            View view = new View(page, sViewEdit, true);
            view.ShowDialog();
            if (view.DialogResult == true)
            {
                sViewEdit = view.GetSView();
                page.Views.Remove(sView);
                page.Views.Insert(index, sViewEdit);
                ListViewViews.ItemsSource = page.Views.Cast<SView>().ToList<SView>();
                ListViewViews.SelectedItem = sViewEdit;
            }*/
        }

        private async void ButtonViewDeleteClick()
        {
            var dialogDelete = new SDialogDelete();
            dialogDelete.Message = $"Вы действительно хотите удалить объект {selectedView.Name}?";
            var dialogResult = await DialogHost.Show(dialogDelete, "DialogHostProjects");

            if (dialogResult is bool b && b)
            {
                this.RemoveView(selectedView);
                this.LoadListViewsOfPage();
                this.BindListViewViews();
                int index = selectedViewIndex > 0 ? selectedViewIndex - 1 : listViews.Count > 0 ? 0 : -1;
                this.SelectView(index);
            }

            /*var view = (SView)ListViewViews.SelectedItem;
            int index = ListViewViews.SelectedIndex - 1;
            var dialogDelete = new SDialogDelete();
            dialogDelete.Message = $"Вы действительно хотите удалить объект {view.Name}?";
            var dialogResult = await DialogHost.Show(dialogDelete, "DialogHostPage");

            if (dialogResult is bool b && b)
            {                
                page.Views.Remove(view);
                ListViewViews.ItemsSource = page.Views.Cast<SView>().ToList<SView>();

                index = index >= 0 ? index : ListViewViews.Items.Count > 0 ? 0 : -1;
                if (index >= 0)
                {
                    ListViewViews.SelectedItem = (SView)ListViewViews.Items[index];
                }
            }*/
        }

        private void ButtonViewCopyClick()
        {
            SView sView = selectedView.Clone();
            View view = new View(page, sView);

            if (view.ShowDialog() == true)
            {
                sView = view.GetSView();
                this.WriteView(sView, true);
                this.LoadListViewsOfPage();
                this.BindListViewViews();
                this.SelectView(sView);
            }

            /*SView sView = (SView)ListViewViews.SelectedItem;
            SView sViewCopy = sView.Clone();
            View view = new View(page, sViewCopy);
            view.ShowDialog();
            if (view.DialogResult == true)
            {
                sViewCopy = view.GetSView();
                page.Views.Add(sViewCopy);
                ListViewViews.ItemsSource = page.Views.Cast<SView>().ToList<SView>();
                ListViewViews.SelectedItem = sViewCopy;
            }*/
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

    }
}
