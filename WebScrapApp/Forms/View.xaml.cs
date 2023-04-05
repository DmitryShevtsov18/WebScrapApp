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
    /// Interaction logic for Object.xaml
    /// </summary>
    public partial class View : Window
    {
        public SPage page;
        public SView view;
        public bool isEdit;
        //private List<SViewField> listViewFields;
        //private List<SViewField> listViewFieldsOfPage;
        private SViewField selectedViewField;
        private int selectedViewFieldIndex;

        public View(SPage _page)
        {
            InitializeComponent();

            page = _page;

            this.Load();
        }

        public View(SPage _page, SView _view, bool _isEdit = false)
        {
            InitializeComponent();

            page = _page;
            view = _view;
            isEdit = _isEdit;

            this.Load();
        }

        public SView GetSView()
        {
            return view;
        }

        private void Load()
        {
            this.initView();

            this.BindForm();
            this.UpdateDesign();
            this.BindListViewViewFields();
            this.SelectViewField();
        }

        private void initView()
        {
            if (view is null)
            {
                view = new SView();
            }
            view.Page = page.Name;
        }

        private void BindListViewViewFields()
        {
            ListViewViewFields.ItemsSource = null;
            if (view.Fields.Count > 0)
            {
                ListViewViewFields.ItemsSource = view.Fields.Cast<SViewField>().ToList<SViewField>();
            }
        }

        private void BindForm()
        {
            TextBoxName.DataContext = view;
            TextBoxDescription.DataContext = view;            
            ComboBoxTag.DataContext = view;
            TextBoxClass.DataContext = view;                                            

            CheckBoxIsEditForm.IsChecked = isEdit;
            TextBoxPageName.Text = view.Page;
        }

        private void UpdateDesign()
        {
            TextBoxName.IsReadOnly = isEdit;
        }

        private void SelectViewField(SViewField _viewField)
        {
            if (ListViewViewFields.Items.Count > 0)
            {
                ListViewViewFields.SelectedItem = _viewField;
            }
        }

        private void SelectViewField(int _index = 0)
        {
            //if (ListViewViewFields.Items.Count > 0)
            {
                ListViewViewFields.SelectedIndex = _index;
            }
        }

        private void WriteViewField(SViewField _viewField, bool _isCreate = false, bool _isChange = false)
        {
            if (_isCreate)
            {
                view.Fields.Add(_viewField);                
            }
            else
            {
                if (_viewField != null && _isChange)
                {
                    view.Fields.Remove(selectedViewField);
                    view.Fields.Insert(selectedViewFieldIndex, _viewField);
                }
            }
        }

        private void RemoveViewField(SViewField _viewField)
        {
            if (_viewField != null)
            {
                view.Fields.Remove(_viewField);
            }
        }

        private void Button_Click(Object _sender, RoutedEventArgs _e)
        {
            Button button = _sender as Button;

            switch (button.Name)
            {
                case "ButtonViewFieldCreate":
                    this.ButtonViewFieldCreateClick();
                    break;
                case "ButtonViewFieldEdit":
                    this.ButtonViewFieldEditClick();
                    break;
                case "ButtonViewFieldDelete":
                    this.ButtonViewFieldDeleteClick();
                    break;
                case "ButtonViewFieldUp":
                    this.ButtonViewFieldUpClick();
                    break;
                case "ButtonViewFieldDown":
                    this.ButtonViewFieldDownClick();
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

        private void ListViewViewFields_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                selectedViewField = (SViewField)e.AddedItems[0];
                if (selectedViewField != null)
                {
                    selectedViewFieldIndex = ListViewViewFields.Items.IndexOf(selectedViewField);
                }
            }
        }

        private async void ButtonViewFieldCreateClick()
        {
            var viewField = new SViewField();
            var dialogResult = await DialogHost.Show(viewField, "DialogHostView");

            if (dialogResult is bool b && b)
            {
                this.WriteViewField(viewField, true);                
                this.BindListViewViewFields();
                this.SelectViewField(viewField);
            }
        }

        private async void ButtonViewFieldDeleteClick()
        {
            var dialogDelete = new SDialogDelete();
            dialogDelete.Message = $"Вы действительно хотите удалить поле {selectedViewField.Name}?";
            var dialogResult = await DialogHost.Show(dialogDelete, "DialogHostView");

            if (dialogResult is bool b && b)
            {
                this.RemoveViewField(selectedViewField);                
                this.BindListViewViewFields();
                int index = selectedViewFieldIndex > 0 ? selectedViewFieldIndex - 1 : view.Fields.Count > 0 ? 0 : -1;
                this.SelectViewField(index);
            }
        }

        private async void ButtonViewFieldEditClick()
        {
            var viewField = selectedViewField.Clone();
            var dialogResult = await DialogHost.Show(viewField, "DialogHostView");

            if (dialogResult is bool b && b)
            {
                this.WriteViewField(viewField, false, true);                
                this.BindListViewViewFields();
                this.SelectViewField(viewField);
            }

            /*int index = ListViewViewFields.SelectedIndex;
            var viewField = (SViewField)ListViewViewFields.SelectedItem;
            var viewFieldEdit = viewField.Clone();
            var dialogResult = await DialogHost.Show(viewFieldEdit, "DialogHostView");

            if (dialogResult is bool b && b)
            {                
                view.Fields.Remove(viewField);
                view.Fields.Insert(index, viewFieldEdit);
                ListViewViewFields.ItemsSource = view.Fields.Cast<SViewField>().ToList<SViewField>();
                ListViewViewFields.SelectedItem = viewFieldEdit;
            }*/
        }

        private void ButtonViewFieldUpClick()
        {
            int index = selectedViewFieldIndex - 1;
            if (index >= 0)
            {
                selectedViewFieldIndex = index;
                this.WriteViewField(selectedViewField, false, true);
                this.BindListViewViewFields();
                this.SelectViewField(selectedViewField);
            }
        }

        private void ButtonViewFieldDownClick()
        {
            int index = selectedViewFieldIndex + 1;
            if (index < ListViewViewFields.Items.Count)
            {
                selectedViewFieldIndex = index;
                this.WriteViewField(selectedViewField, false, true);
                this.BindListViewViewFields();
                this.SelectViewField(selectedViewField);
            }
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
