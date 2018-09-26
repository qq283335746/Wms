using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TygaSoft.Model;

namespace TygaSoft.IMessaging
{
    public interface IScanQueue
    {
        RunQueueInfo Receive();

        RunQueueInfo Receive(int timeout);

        void Send(RunQueueInfo model);
    }
}
