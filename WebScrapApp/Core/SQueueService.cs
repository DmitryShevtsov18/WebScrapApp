using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WebScrapApp.Core
{
    public class SQueueService
    {
        private SQueue executingQueue;
        public event EventHandler OnExecutedQueue;

        public SQueue Queue
        {
            get
            {
                return executingQueue;
            }
        }

        public SQueueService()
        {
            
        }

        public void Start()
        {
            while (true)
            {
                try
                {
                    if (this.NextQueue())
                    {
                        this.ExecuteQueue();
                    }
                }
                catch (Exception _ex)
                {
                    this.AddLog(_ex.Message, true);
                }

                this.OnExecutedQueue(this, EventArgs.Empty);
            }
        }

        private bool NextQueue()
        {
            List<SQueue> listQueue = SWorkFiles.ReadQueuis();
            listQueue = listQueue.Where<SQueue>(x => x.Status == SQueueStatus.Queue).ToList<SQueue>();
            listQueue = listQueue.OrderBy(x => x.CreatedDateTime).ToList<SQueue>();

            if (listQueue.Count > 0)
            {
                executingQueue = listQueue[0];
            }
            else
            {
                executingQueue = null;
                Thread.Sleep(10000);
            }

            return executingQueue != null;
        }

        private async void ExecuteQueue()
        {
            if (executingQueue != null)
            {
                //for test
                Thread.Sleep(10000);
                this.AddLog("OK");
                this.FinishQueue();
                /*SReportTemplate sReportTemplate = SWorkFiles.ReadReportTemplate(new SReportTemplate() { Name = executingQueue.Template });

                SReportCreateExceptionResult result = await new SReportCreate(sReportTemplate).Create();
                if (result.Type == SExceptionType.None)
                {
                    SReport sReport = result.Report;
                    if (sReport != null)
                    {
                        SWorkFiles.WriteReport(sReport);

                        this.AddLog("OK");
                        this.FinishQueue();
                    }
                    else
                    {
                        this.AddLog("Report empty", true);
                    }
                }
                else
                {
                    this.AddLog(result.Message, true);
                }*/
            }
        }

        private void AddLog(string _log, bool _isError = false)
        {
            if (executingQueue != null)
            {
                executingQueue.Log.Add(_log);
                if (_isError)
                {
                    executingQueue.Status = SQueueStatus.Error;
                }
            }
        }

        private void FinishQueue()
        {
            if (executingQueue != null)
            {
                executingQueue.Status = SQueueStatus.Completed;
                executingQueue.Progress = 100;

                SWorkFiles.WriteQueue(executingQueue);
            }
        }
    }
}
