using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.Model
{
    [Serializable]
    public class UserInfo
    {
        public UserInfo() { }
        public UserInfo(string appCode,string appName)
        {
            this.AppCode = appCode;
            this.AppName = appName;
        }

        public string AppCode { get; set; }
        public string AppName { get; set; }
    }
}
