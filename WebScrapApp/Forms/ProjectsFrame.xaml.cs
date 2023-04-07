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
    /// Interaction logic for Projects.xaml
    /// </summary>
    public partial class ProjectsFrame : UserControl
    {
        private List<SProject> listProjects;
        private SProject selectedProject;
        private int selectedProjectIndex;
        private List<SPage> listPages;
        private List<SPage> listPagesOfProject;
        private SPage selectedPage;
        private int selectedPageIndex;

        public ProjectsFrame()
        {
            InitializeComponent();

            this.Load();
        }

        private void Load()
        {
            this.LoadListProjects();
            this.LoadListPages();
            this.BindListViewProjects();
            this.SelectProject();
        }

        private void LoadListProjects()
        {
            listProjects = SWorkFiles.ReadProjects();
        }

        private void LoadListPages()
        {
            listPages = SWorkFiles.ReadPages();
        }

        private void LoadListPagesOfProject()
        {
            if (listPages != null && selectedProject != null)
            {
                listPagesOfProject = listPages.Where<SPage>(x => x.Project == selectedProject.Name).ToList<SPage>();
            }
        }

        private void BindListViewProjects()
        {
            ListViewProjects.ItemsSource = null;
            if (listProjects.Count > 0)
            {
                ListViewProjects.ItemsSource = listProjects;
            }
        }

        private void BindListViewPages()
        {
            ListViewPages.ItemsSource = null;
            if (listPagesOfProject.Count > 0)
            {
                ListViewPages.ItemsSource = listPagesOfProject;
            }
        }

        private void BindForm()
        {
            TextBoxName.DataContext = selectedProject;
            TextBoxDescription.DataContext = selectedProject;
        }

        private void SelectProject(SProject _project)
        {
            if (ListViewProjects.Items.Count > 0)
            {
                ListViewProjects.SelectedItem = _project;
            }
        }

        private void SelectProject(int _index = 0)
        {
            //if (ListViewProjects.Items.Count > 0)
            {
                ListViewProjects.SelectedIndex = _index;
            }
        }

        private void SelectPage(SPage _page)
        {
            if (ListViewPages.Items.Count > 0)
            {
                ListViewPages.SelectedItem = _page;
            }
        }

        private void SelectPage(int _index = 0)
        {
            //if (ListViewPages.Items.Count > 0)
            {
                ListViewPages.SelectedIndex = _index;
            }
        }

        private void WriteProject(SProject _project, bool _isCreate = false)
        {
            if (_isCreate)
            {
                SWorkFiles.WriteProject(_project);
                listProjects.Add(_project);
            }
            else
            {
                if (_project != null && listProjects.IndexOf(_project) >= 0)
                {
                    SWorkFiles.WriteProject(_project);
                }
            }
        }

        private void WritePage(SPage _page, bool _isCreate = false, bool _isChange = false)
        {
            if (_isCreate)
            {
                SWorkFiles.WritePage(_page);
                listPages.Add(_page);
            }
            else
            {
                if (_page != null && listPages.IndexOf(selectedPage) >= 0)
                {
                    SWorkFiles.WritePage(_page);

                    if (_isChange)
                    {
                        listPages.Remove(selectedPage);
                        listPages.Insert(selectedPageIndex, _page);
                    }
                }
            }
        }

        private void RemoveProject(SProject _project)
        {
            if (_project != null)
            {
                SWorkFiles.DeleteProject(_project);
                listProjects.Remove(_project);
            }
        }

        private void RemovePage(SPage _page)
        {
            if (_page != null)
            {
                SWorkFiles.DeletePage(_page);
                listPages.Remove(_page);
            }
        }

        private void Button_Click(Object _sender, RoutedEventArgs _e)
        {
            Button button = _sender as Button;

            switch (button.Name)
            {
                case "ButtonProjectCreate":
                    this.ButtonProjectCreateClick();
                    break;
                case "ButtonProjectDelete":
                    this.ButtonProjectDeleteClick();
                    break;
                case "ButtonProjectCopy":
                    this.ButtonProjectCopyClick();
                    break;
                case "ButtonPageCreate":
                    this.ButtonPageCreateClick();
                    break;
                case "ButtonPageEdit":
                    this.ButtonPageEditClick();
                    break;
                case "ButtonPageDelete":
                    this.ButtonPageDeleteClick();
                    break;
                case "ButtonPageCopy":
                    this.ButtonPageCopyClick();
                    break;
                default:
                    throw new Exception("");
            }
        }
        
        private void ListViewProjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                selectedProject = (SProject)e.AddedItems[0];
                if (selectedProject != null)
                {
                    selectedProjectIndex = ListViewProjects.Items.IndexOf(selectedProject);
                }
                this.BindForm();
                this.LoadListPagesOfProject();
                this.BindListViewPages();
                this.SelectPage();
            }

            if (e.RemovedItems.Count != 0)
            {
                this.WriteProject((SProject)e.RemovedItems[0]);
            }
        }

        private void ListViewPages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                selectedPage = (SPage)e.AddedItems[0];
                if (selectedPage != null)
                {
                    selectedPageIndex = ListViewPages.Items.IndexOf(selectedPage);
                }
            }
        }

        private async void ButtonProjectCreateClick()
        {
            SProject project = new SProject();
            var dialogResult = await DialogHost.Show(project, "DialogHostProjectsFrame");

            if (dialogResult is bool b && b)
            {
                this.WriteProject(project, true);
                this.BindListViewProjects();
                this.SelectProject(project);
            }
        }

        private async void ButtonProjectDeleteClick()
        {
            var dialogDelete = new SDialogDelete();
            dialogDelete.Message = $"Вы действительно хотите удалить проект {selectedProject.Name}?";
            var dialogResult = await DialogHost.Show(dialogDelete, "DialogHostProjectsFrame");

            if (dialogResult is bool b && b)
            {
                this.RemoveProject(selectedProject);
                this.BindListViewProjects();
                int index = selectedProjectIndex > 0 ? selectedProjectIndex - 1 : listProjects.Count > 0 ? 0 : -1;
                this.SelectProject(index);
            }
        }

        private async void ButtonProjectCopyClick()
        {
            SProject project = selectedProject.Clone();
            var dialogResult = await DialogHost.Show(project, "DialogHostProjectsFrame");

            if (dialogResult is bool b && b)
            {
                this.WriteProject(project, true);
                this.BindListViewProjects();
                this.SelectProject(project);
            }
        }

        private void ButtonPageCreateClick()
        {
            Page page = new Page(selectedProject);

            if (page.ShowDialog() == true)
            {
                SPage sPage = page.GetSPage();
                this.WritePage(sPage, true);
                this.LoadListPagesOfProject();
                this.BindListViewPages();
                this.SelectPage(sPage);
            }
        }

        private void ButtonPageEditClick()
        {
            SPage sPage = selectedPage.Clone();
            Page page = new Page(selectedProject, sPage, true);

            if (page.ShowDialog() == true)
            {
                sPage = page.GetSPage();
                this.WritePage(sPage, false, true);
                this.LoadListPagesOfProject();
                this.BindListViewPages();
                this.SelectPage(sPage);
            }            
        }

        private async void ButtonPageDeleteClick()
        {
            var dialogDelete = new SDialogDelete();
            dialogDelete.Message = $"Вы действительно хотите удалить страницу {selectedPage.Name}?";
            var dialogResult = await DialogHost.Show(dialogDelete, "DialogHostProjectsFrame");

            if (dialogResult is bool b && b)
            {
                this.RemovePage(selectedPage);
                this.LoadListPagesOfProject();
                this.BindListViewPages();
                int index = selectedPageIndex > 0 ? selectedPageIndex - 1 : listPages.Count > 0 ? 0 : -1;
                this.SelectPage(index);
            }
        }

        private void ButtonPageCopyClick()
        {
            SPage sPage = selectedPage.Clone();
            Page page = new Page(selectedProject, sPage);

            if (page.ShowDialog() == true)
            {
                sPage = page.GetSPage();
                this.WritePage(sPage, true);
                this.LoadListPagesOfProject();
                this.BindListViewPages();
                this.SelectPage(sPage);
            }
        }

        /*
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.WriteProject(selectedProject);
        }
        */
    }
}
