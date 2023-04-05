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

        public View(SPage _page)
        {
            InitializeComponent();

            page = _page;
            view = new SView();
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
            //this.BindPage();
            //this.UpdateDesign();
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

        private void BindPage()
        {
            /*TextBoxName.DataContext = view;
            TextBoxDescription.DataContext = view;
            ListViewViewFields.ItemsSource = view.Fields.Cast<SViewField>().ToList<SViewField>();
            if (view.Fields.Count != 0)
            {
                ListViewViewFields.SelectedItem = view.Fields[0];
            }
            ComboBoxTag.DataContext = view;
            TextBoxSelector.DataContext = view;                                
            ListBoxViews.ItemsSource = page.Views.Cast<SView>().ToList<SView>();
            CheckBoxIsEditForm.IsChecked = isEdit; */           
        }

        private void UpdateDesign()
        {
            //TextBoxName.IsReadOnly = isEdit;            
        }

        private async void ButtonViewFieldCreateClick()
        {
            /*var viewField = new SViewField();            
            var dialogResult = await DialogHost.Show(viewField, "DialogHostView");

            if (dialogResult is bool b && b)
            {
                view.Fields.Add(viewField);
                ListViewViewFields.ItemsSource = view.Fields.Cast<SViewField>().ToList<SViewField>();
                ListViewViewFields.SelectedItem = viewField;
            }*/
        }

        private async void ButtonViewFieldDeleteClick()
        {
            /*SViewField viewField = (SViewField)ListViewViewFields.SelectedItem;
            int index = ListViewViewFields.SelectedIndex - 1;
            var dialogDelete = new SDialogDelete();
            dialogDelete.Message = $"Вы действительно хотите удалить поле {viewField.Name}?";
            var dialogResult = await DialogHost.Show(dialogDelete, "DialogHostView");

            if (dialogResult is bool b && b)
            {
                view.Fields.Remove(viewField);

                ListViewViewFields.ItemsSource = view.Fields.Cast<SViewField>().ToList<SViewField>();
                index = index >= 0 ? index : ListViewViewFields.Items.Count > 0 ? 0 : -1;
                if (index >= 0)
                {
                    ListViewViewFields.SelectedIndex = index;
                }
            }*/
        }

        private async void ButtonViewFieldEditClick()
        {
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
            /*int index = ListViewViewFields.SelectedIndex - 1;
            if (index >= 0)
            {
                var viewField = (SViewField)ListViewViewFields.SelectedItem;                
                view.Fields.Remove(viewField);
                view.Fields.Insert(index, viewField);
                ListViewViewFields.ItemsSource = view.Fields.Cast<SViewField>().ToList<SViewField>();
                ListViewViewFields.SelectedItem = viewField;
            }*/
        }

        private void ButtonViewFieldDownClick()
        {
            /*int index = ListViewViewFields.SelectedIndex + 1;
            if (index < ListViewViewFields.Items.Count)
            {
                var viewField = (SViewField)ListViewViewFields.SelectedItem;
                view.Fields.Remove(viewField);
                view.Fields.Insert(index, viewField);
                ListViewViewFields.ItemsSource = view.Fields.Cast<SViewField>().ToList<SViewField>();
                ListViewViewFields.SelectedItem = viewField;
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

        private void TextBoxSelector_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            /*if (e.Key == Key.Space)
            {
                //e.Handled = true;
            }
            base.OnPreviewKeyDown(e);*/
        }

    }
}
