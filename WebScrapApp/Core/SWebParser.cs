using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using System.Web.UI.WebControls;
using AngleSharp.Dom;
using System.Diagnostics.Metrics;
using System.Runtime.Remoting.Contexts;

namespace WebScrapApp.Core
{
    public class SWebParser
    {
        SPage sPage;
        SView sView;
        SViewFieldCollection fields;
        SReportLineCollection sReportLineCollection;

        public SWebParser(SPage _sPage, SView _sView, SViewFieldCollection _fields)
        { 
            sPage = _sPage;
            sView = _sView;
            fields = _fields;
            sReportLineCollection = new SReportLineCollection();
        }

        private async Task DoParse(string _content)
        {
            if (!string.IsNullOrEmpty(_content))
            {
                string tag = $"{sView.Tag}";
                var context = BrowsingContext.New(Configuration.Default);
                var document = await context.OpenAsync(req => req.Content(_content));

                var views = document.QuerySelectorAll(tag)
                    .Where(e => e.ClassName == sView.Class)
                    .ToList();

                List<string> parsedViewContents = new List<string>();
                foreach (var view in views)
                {
                    string viewContent = view.InnerHtml;
                    if (!string.IsNullOrEmpty(viewContent))
                    {
                        sReportLineCollection.Add(await this.ParseView(viewContent));
                    }
                }
            }
        }

        private async Task<SReportLine> ParseView(string _viewContent)
        {
            SReportLine reportLine = new SReportLine();

            var context = BrowsingContext.New(Configuration.Default);
            var document = await context.OpenAsync(req => req.Content(_viewContent));

            foreach (SViewField viewField in fields)
            {
                var elem = document.QuerySelector(viewField.Selector);
                var value = elem != null ? elem.TextContent : string.Empty;
                reportLine.Fields.Add(value);
            }
            
            return reportLine;
        }

        private SReportLineCollection GetReportLineCollection()
        {
            return sReportLineCollection;
        }

        public async Task<SWebParserExceptionResult> Parse()
        {
            SWebParserExceptionResult result = new SWebParserExceptionResult();            

            try
            {
                SExceptionResult readingResult = await new SWebReader(sPage).Read();
                if (readingResult.Type == SExceptionType.None)
                {
                    await this.DoParse(readingResult.Result);

                    SReportLineCollection collection = this.GetReportLineCollection();
                    if (collection.Count != 0)
                    {
                        result.ReportLines = collection;
                    }
                    else
                    {
                        result.Type = SExceptionType.Warning;
                        result.Message = "No data";
                    }
                }
                else
                {
                    result.Type = readingResult.Type;
                    result.Message = readingResult.Message;
                }
            }
            catch (System.Exception ex)
            {
                result.Type = SExceptionType.Error; 
                result.Message = ex.Message;
            }

            return result;
        }
    }
}
