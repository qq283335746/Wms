using System;
using TygaSoft.Model;

namespace TygaSoft.IMessaging
{
    public interface IRunQueue
    {
        RunQueueInfo Receive();

        RunQueueInfo Receive(int timeout);

        void Send(RunQueueInfo model);
    }
}
