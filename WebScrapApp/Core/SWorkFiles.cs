using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Web.UI.WebControls;

namespace WebScrapApp.Core
{
    public class SWorkFiles
    {
        private static string FilenameXml(SParameterKey _key, string _filename)
        {
            return $"{SParameters.GetParameter(_key)}\\{_filename}.xml";
        }

        public static void DeletePage(SPage _page)
        {
            if (SWorkFiles.ExistPage(_page))
            {
                File.Delete(SWorkFiles.FilenameXml(SParameterKey.PageDirectory, _page.GetFilename()));
            }
        }

        public static void DeleteProject(SProject _project)
        {
            if (SWorkFiles.ExistProject(_project))
            {
                File.Delete(SWorkFiles.FilenameXml(SParameterKey.ProjectDirectory, _project.GetFilename()));
            }
        }

        public static void DeleteReport(SReport _report)
        {
            if (SWorkFiles.ExistReport(_report))
            {
                File.Delete(SWorkFiles.FilenameXml(SParameterKey.ReportDirectory, _report.GetFilename()));
            }
        }

        public static void DeleteReportTemplate(SReportTemplate _reportTemplate)
        {
            if (SWorkFiles.ExistReportTemplate(_reportTemplate))
            {
                File.Delete(SWorkFiles.FilenameXml(SParameterKey.ReportTemplateDirectory, _reportTemplate.GetFilename()));
            }
        }

        public static void DeleteView(SView _view)
        {
            if (SWorkFiles.ExistView(_view))
            {
                File.Delete(SWorkFiles.FilenameXml(SParameterKey.ViewDirectory, _view.GetFilename()));
            }
        }

        public static bool ExistPage(SPage _page)
        {
            return File.Exists(SWorkFiles.FilenameXml(SParameterKey.PageDirectory, _page.GetFilename()));
        }

        public static bool ExistProject(SProject _project)
        {
            return File.Exists(SWorkFiles.FilenameXml(SParameterKey.ProjectDirectory, _project.GetFilename()));
        }

        public static bool ExistReport(SReport _report)
        {
            return File.Exists(SWorkFiles.FilenameXml(SParameterKey.ReportDirectory, _report.GetFilename()));
        }

        public static bool ExistReportTemplate(SReportTemplate _reportTemplate)
        {
            return File.Exists(SWorkFiles.FilenameXml(SParameterKey.ReportTemplateDirectory, _reportTemplate.GetFilename()));
        }

        public static bool ExistView(SView _view)
        {
            return File.Exists(SWorkFiles.FilenameXml(SParameterKey.ViewDirectory, _view.GetFilename()));
        }

        public static SPage ReadPage(SPage _page)
        {
            SPage ret = null;

            if (SWorkFiles.ExistPage(_page))
            {
                ret = (SPage)new SSerialization(typeof(SPage)).Read(SWorkFiles.FilenameXml(SParameterKey.PageDirectory, _page.GetFilename()));
            }

            return ret;
        }

        public static List<SPage> ReadPages()
        {
            List<SPage> ret = new List<SPage>();
            DirectoryInfo directoryInfo = new DirectoryInfo(SParameters.GetParameter(SParameterKey.PageDirectory));

            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                ret.Add((SPage)new SSerialization(typeof(SPage)).Read(fileInfo.FullName));
            }

            return ret;
        }

        public static List<SProject> ReadProjects()
        {
            List<SProject> ret = new List<SProject>();
            DirectoryInfo directoryInfo = new DirectoryInfo(SParameters.GetParameter(SParameterKey.ProjectDirectory));

            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                ret.Add((SProject)new SSerialization(typeof(SProject)).Read(fileInfo.FullName));
            }

            return ret;
        }

        public static List<SReport> ReadReports()
        {
            List<SReport> ret = new List<SReport>();
            DirectoryInfo directoryInfo = new DirectoryInfo(SParameters.GetParameter(SParameterKey.ReportDirectory));

            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                ret.Add((SReport)new SSerialization(typeof(SReport)).Read(fileInfo.FullName));
            }

            return ret;
        }

        public static List<SReportTemplate> ReadReportTemplates()
        {
            List<SReportTemplate> ret = new List<SReportTemplate>();
            DirectoryInfo directoryInfo = new DirectoryInfo(SParameters.GetParameter(SParameterKey.ReportTemplateDirectory));

            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                ret.Add((SReportTemplate)new SSerialization(typeof(SReportTemplate)).Read(fileInfo.FullName));
            }

            return ret;
        }

        public static SView ReadView(SView _view)
        {
            SView ret = null;

            if (SWorkFiles.ExistView(_view))
            {
                ret = (SView)new SSerialization(typeof(SView)).Read(SWorkFiles.FilenameXml(SParameterKey.ViewDirectory, _view.GetFilename()));
            }

            return ret;
        }

        public static List<SView> ReadViews()
        {
            List<SView> ret = new List<SView>();
            DirectoryInfo directoryInfo = new DirectoryInfo(SParameters.GetParameter(SParameterKey.ViewDirectory));

            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                ret.Add((SView)new SSerialization(typeof(SView)).Read(fileInfo.FullName));
            }

            return ret;
        }

        public static void WritePage(SPage _page)
        {
            new SSerialization(_page).Write(SWorkFiles.FilenameXml(SParameterKey.PageDirectory, _page.GetFilename()));
        }

        public static void WriteProject(SProject _project)
        {            
            new SSerialization(_project).Write(SWorkFiles.FilenameXml(SParameterKey.ProjectDirectory, _project.GetFilename()));
        }

        public static void WriteReport(SReport _report)
        {
            new SSerialization(_report).Write(SWorkFiles.FilenameXml(SParameterKey.ReportDirectory, _report.GetFilename()));
        }

        public static void WriteReportTemplate(SReportTemplate _reportTemplate)
        {
            new SSerialization(_reportTemplate).Write(SWorkFiles.FilenameXml(SParameterKey.ReportTemplateDirectory, _reportTemplate.GetFilename()));
        }

        public static void WriteView(SView _view)
        {
            new SSerialization(_view).Write(SWorkFiles.FilenameXml(SParameterKey.ViewDirectory, _view.GetFilename()));
        }
    }
}
