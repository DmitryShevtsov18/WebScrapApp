using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapApp.Core
{
    public class SReportCreate
    {
        SReportTemplate sReportTemplate;        

        public SReportCreate(SReportTemplate _sReportTemplate)
        {
            sReportTemplate = _sReportTemplate;
        }

        public async Task<SReportCreateExceptionResult> Create()
        {
            SReportCreateExceptionResult result = new SReportCreateExceptionResult();            
            SReport sReport = new SReport();
            SPage sPage = SWorkFiles.ReadPage(new SPage(sReportTemplate.Project) { Name = sReportTemplate.Page });
            SView sView = SWorkFiles.ReadView(new SView(sReportTemplate.Page) { Name = sReportTemplate.View });

            if (sPage != null && sView != null)
            {
                if (sReportTemplate.Fields.Count > 0)
                {
                    SWebParserExceptionResult parsingResult = await new SWebParser(sPage, sView, sReportTemplate.Fields).Parse();
                    if (parsingResult.Type == SExceptionType.None)
                    {
                        sReport.Name = sReportTemplate.ReportName;
                        sReport.Template = sReportTemplate.Name;
                        SReportLine headers = new SReportLine();
                        foreach (SViewField sViewField in sReportTemplate.Fields)
                        {
                            headers.Fields.Add(sViewField.Name);
                        }
                        sReport.Headers = headers;
                        sReport.Lines = parsingResult.ReportLines;

                        result.Report = sReport;
                    }
                    else
                    {
                        result.Type = parsingResult.Type;
                        result.Message = parsingResult.Message;
                    }
                }
                else
                {
                    result.Type = SExceptionType.Warning;
                    result.Message = "No selected fields";
                }
            }
            else
            {
                result.Type = SExceptionType.Warning;
                result.Message = "No template";
            }

            return result;
        }
    }
}
