using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;

namespace WebScrapApp.Core
{
    public class SParameters
    {
        private const string ProgramDirectory = "WESCAP";
        private const string ProjectDirectory = "Projects";
        private const string TemplateDirectory = "Templates";
        private const string ReportDirectory = "Reports";
        private const string ReportTemplateDirectory = "ReportTemplates";
        private const string PageDirectory = "Pages";
        private const string ViewDirectory = "Views";
        private const string ParserDirectory = "Parsers";
        private const string QueueDirectory = "Queuis";

        RegistryKey currentUserKey;
        SParameterKey parameterKey;
        string programDirectory;

        public SParameters(SParameterKey _parameterKey)
        {            
            parameterKey = _parameterKey;

            this.Load();            
        }

        private void Load()
        {
            currentUserKey = Registry.CurrentUser;
            programDirectory = string.Format("{0}\\{1}",
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                SParameters.ProgramDirectory);
        }        

        private string GetDirectory()
        {
            string path = "";

            switch (parameterKey)
            {
                case SParameterKey.PageDirectory:
                    path = SParameters.PageDirectory;
                    break;
                case SParameterKey.ProjectDirectory:
                    path = SParameters.ProjectDirectory;
                    break;
                case SParameterKey.ReportDirectory:
                    path = SParameters.ReportDirectory;
                    break;
                case SParameterKey.ReportTemplateDirectory:
                    path = SParameters.ReportTemplateDirectory;
                    break;
                case SParameterKey.TemplateDirectory:
                    path = SParameters.TemplateDirectory;
                    break;
                case SParameterKey.ViewDirectory:
                    path = SParameters.ViewDirectory;
                    break;
                case SParameterKey.ParserDirectory:
                    path = SParameters.ParserDirectory;
                    break;
                case SParameterKey.QueueDirectory:
                    path = SParameters.QueueDirectory;
                    break;
                default:
                    throw new System.Exception("");
            }

            return $"{programDirectory}\\{path}";
        }

        private RegistryKey GetKey()
        {
            RegistryKey programKey = currentUserKey.OpenSubKey(this.ParameterName(SParameterKey.WESCAP), true);

            if (programKey is null)
            {
                programKey = currentUserKey.CreateSubKey(this.ParameterName(SParameterKey.WESCAP));
                this.CreateDirectory(programDirectory);
                programKey.SetValue(this.ParameterName(SParameterKey.ProgramDirectory), programDirectory);
                programKey = currentUserKey.OpenSubKey(this.ParameterName(SParameterKey.WESCAP), true);
            }

            return programKey;
        }

        public string GetParameter()
        {
            RegistryKey programKey = this.GetKey();
            object parameter = programKey.GetValue(this.ParameterName(parameterKey));            
            
            if (parameter is null)
            {
                string directory = this.GetDirectory();
                this.CreateDirectory(directory);
                programKey.SetValue(this.ParameterName(parameterKey), directory);
                parameter = programKey.GetValue(this.ParameterName(parameterKey));
            }            

            string value = parameter as string;

            programKey.Close();

            return value;
        }

        public void SetParameter(string _value)
        {
            RegistryKey programKey = this.GetKey();
            programKey.SetValue(this.ParameterName(parameterKey), _value);
            programKey.Close();
        }

        private void CreateDirectory(string _directory)
        {
            if (!Directory.Exists(_directory))
            {
                Directory.CreateDirectory(_directory);
            }
        }

        private string ParameterName(SParameterKey _key)
        {
            return Enum.GetName(typeof(SParameterKey), _key);
        }

        public static string GetParameter(SParameterKey _key)
        {
            return new SParameters(_key).GetParameter();            
        }

        public static void SetParameter(SParameterKey _key, string _value)
        {
            new SParameters(_key).SetParameter(_value);
        }
    }
}
