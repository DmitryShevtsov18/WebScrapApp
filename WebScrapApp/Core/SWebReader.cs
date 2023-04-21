using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PuppeteerSharp;
using PuppeteerSharp.Input;
using System.Threading;

namespace WebScrapApp.Core
{
    public class SWebReader
    {
        SPage sPage;
        BrowserFetcher browserFetcher;
        IBrowser browser;
        IPage page;
        ClickOptions clickOptions;

        public SWebReader(SPage _page)
        {
            sPage = _page;

            this.Load();
        }

        private void Load()
        {
            clickOptions = new ClickOptions();
            clickOptions.OffSet = new Offset(1, 1);
        }

        private async Task<bool> openBrowser()
        {
            browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);

            browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            return browser != null;
        }

        private async Task<bool> openPage()
        {
            if (!string.IsNullOrEmpty(sPage.Url))
            {
                page = await browser.NewPageAsync();
                await page.GoToAsync(sPage.Url);                
            }            

            return page != null;
        }

        private async Task scrollPage()
        {
            switch (sPage.ScrollType)
            {
                case SScrollType.Dynamic:
                    break;
                case SScrollType.DynamicLink:
                    var dynLink = await page.QuerySelectorAsync(sPage.ScrollSelector);
                    while (dynLink != null)
                    {
                        await dynLink.ClickAsync(clickOptions);
                        Thread.Sleep(500);
                        dynLink = await page.QuerySelectorAsync(sPage.ScrollSelector);
                    }
                    break;
                case SScrollType.NumericPages:
                    break;
                default:
                    throw new System.Exception("");
            }
        }

        private async Task<string> GetContent()
        {
            return await page.GetContentAsync();
        }

        private async Task closeBrowser()
        {
            if (browser != null)
            {
                await browser.CloseAsync();
            }
        }

        private async Task closePage()
        {
            if (page != null)
            {
                await page.CloseAsync();
            }
        }

        public async Task<SExceptionResult> Read()
        {
            SExceptionResult result = new SExceptionResult();

            try
            {                
                if (await this.openBrowser())
                {
                    if (await this.openPage())
                    {
                        await this.scrollPage();

                        string content = await this.GetContent();
                        if (!string.IsNullOrEmpty(content))
                        {
                            result.Result = content;
                        }
                        else
                        {
                            result.Type = SExceptionType.Warning;
                            result.Message = "No content";
                        }
                    }
                    else
                    {
                        result.Type = SExceptionType.Warning;
                        result.Message = "No page";
                    }
                }
                else
                {
                    result.Type = SExceptionType.Warning;
                    result.Message = "No browser";
                }
            }
            catch (System.Exception ex)
            {
                result.Type = SExceptionType.Error;
                result.Message = ex.Message;
            }
            finally
            {
                await this.closePage();
                await this.closeBrowser();
            }

            return result;
        }
    }
}
