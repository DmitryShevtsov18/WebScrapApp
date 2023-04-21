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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using WebScrapApp.Core;

namespace WebScrapApp.Forms
{
    /// <summary>
    /// Логика взаимодействия для QueueFrame.xaml
    /// </summary>
    public partial class QueueFrame : UserControl
    {
        private List<SQueue> listQueuis;
        private SQueue selectedQueue;
        private int selectedQueueIndex;

        public QueueFrame()
        {
            InitializeComponent();

            this.Load();
        }

        private void Load()
        {
            this.LoadListQueuis();
            //this.LoadListPages();
            this.BindListViewQueuis();
            this.SelectQueue();
        }

        private void LoadListQueuis()
        {
            listQueuis = SWorkFiles.ReadQueuis();
        }

        private void BindListViewQueuis()
        {
            ListViewQueuis.ItemsSource = null;
            if (listQueuis.Count > 0)
            {
                ListViewQueuis.ItemsSource = listQueuis;
            }
        }

        private void BindForm()
        {
            //TextBlockProjectName.DataContext = selectedProject;
        }

        private void SelectQueue(SQueue _queue)
        {
            if (ListViewQueuis.HasItems)
            {
                ListViewQueuis.SelectedItem = _queue;
            }
        }

        private void SelectQueue(int _index = 0)
        {
            //if (ListViewQueuis.HasItems)
            {
                ListViewQueuis.SelectedIndex = _index;
            }
        }

        private void WriteQueue(SQueue _queue)
        {
            if (_queue != null)
            {
                SWorkFiles.WriteQueue(_queue);
                listQueuis.Remove(selectedQueue);
                listQueuis.Insert(selectedQueueIndex, _queue);
            }
        }

        private void RemoveQueue(SQueue _queue)
        {
            if (_queue != null)
            {
                SWorkFiles.DeleteQueue(_queue);
                listQueuis.Remove(_queue);
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

        private void OpenFrame(PageFrame _pageFrame)
        {
            GridFrame.Children.Clear();

            if (_pageFrame != null)
            {
                GridFrame.Children.Add(_pageFrame);
                this.ChangeGrid(true);
            }
        }

        private void CloseFrame(object _sender, EventArgs _e)
        {
            //PageFrame pageFrame = _sender as PageFrame;

            this.ChangeGrid();
            /*
            if (pageFrame.DialogResult)
            {
                SPage sPage = pageFrame.GetSPage();

                switch (pageFrame.OpenType)
                {
                    case SFrameOpenType.Create:
                        this.WritePage(sPage, true);
                        break;
                    case SFrameOpenType.Edit:
                        this.WritePage(sPage, false, true);
                        break;
                    case SFrameOpenType.Copy:
                        this.WritePage(sPage, true);
                        break;
                    default:
                        throw new Exception("");
                }

                this.LoadListPagesOfProject();
                this.BindListViewPages();
                this.SelectPage(sPage);
            }*/
        }

        private DependencyObject FindParentControl(DependencyObject _control, Type _type)
        {
            DependencyObject parent = _control.GetParentObject();
            while (parent != null)
            {
                if (parent.GetType() == _type)
                {
                    break;
                }
                parent = parent.GetParentObject();
            }

            return parent;
        }

        private void FindQueueByButtonAndSelect(Button _button)
        {
            var parent = this.FindParentControl(_button, typeof(ContentPresenter));
            if (parent != null)
            {
                ContentPresenter contentPresenter = (ContentPresenter)parent;
                SQueue sQueue = (SQueue)contentPresenter.Content;

                this.SelectQueue(sQueue);
            }
        }

        private void ChangeQueueStatus(SQueueStatus _status)
        {
            if (selectedQueue != null)
            {
                SQueue sQueueCopy = selectedQueue.Clone();
                sQueueCopy.Status = _status;

                this.WriteQueue(sQueueCopy);
                this.BindListViewQueuis();
                this.SelectQueue(sQueueCopy);
            }
        }

        private void Button_Click(Object _sender, RoutedEventArgs _e)
        {
            Button button = _sender as Button;

            switch (button.Name)
            {
                case "ButtonQueueShelve":
                    this.FindQueueByButtonAndSelect(button);
                    this.ButtonShelveClick();
                    break;
                case "ButtonQueueInQueue":
                    this.FindQueueByButtonAndSelect(button);
                    this.ButtonInQueueClick();
                    break;
                case "ButtonQueueCancel":
                    this.FindQueueByButtonAndSelect(button);
                    this.ButtonCancelClick();
                    break;
                case "ButtonQueueOpen":
                    this.FindQueueByButtonAndSelect(button);
                    this.ButtonOpenClick();
                    break;
                case "ButtonQueueDelete":
                    this.FindQueueByButtonAndSelect(button);
                    this.ButtonDeleteClick();
                    break;
                default:
                    throw new Exception("");
            }
        }

        private void ListViewQueuis_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                selectedQueue = (SQueue)e.AddedItems[0];
                if (selectedQueue != null)
                {
                    selectedQueueIndex = ListViewQueuis.Items.IndexOf(selectedQueue);
                }
                this.BindForm();
            }
        }

        private void ButtonShelveClick()
        {
            this.ChangeQueueStatus(SQueueStatus.Shelve);
        }

        private void ButtonInQueueClick()
        {
            this.ChangeQueueStatus(SQueueStatus.Queue);
        }

        private void ButtonCancelClick()
        {
            this.ChangeQueueStatus(SQueueStatus.Canceled);
        }

        private void ButtonOpenClick()
        {
        }

        private void ButtonDeleteClick()
        {
        }
    }
}
