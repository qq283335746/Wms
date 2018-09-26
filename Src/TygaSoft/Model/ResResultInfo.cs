using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.Model
{
    [Serializable]
    public class ResResultInfo
    {
        public int ResCode { get; set; }

        public string Msg { get; set; }

        public object Data { get; set; }
    }
}
