using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;

namespace TygaSoft.TaskProcessor
{
    public class BaseTask
    {
        public int threadCount = int.Parse(ConfigurationManager.AppSettings["ThreadCount"]);
        public int batchSize = int.Parse(ConfigurationManager.AppSettings["BatchSize"]);
        public int queueTimeout = int.Parse(ConfigurationManager.AppSettings["QueueTimeout"]);
        public int transactionTimeout = int.Parse(ConfigurationManager.AppSettings["TransactionTimeout"]);

        public static void Run()
        {
            new RunQueue().TaskStart();
            new ScanQueue().TaskStart();
            new StockWarningTimer().TaskStart();
        }
    }
}
