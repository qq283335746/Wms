using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.TaskProcessor
{
    public interface ITask
    {
        void TaskStart();

        void WorkProcess();
    }
}
