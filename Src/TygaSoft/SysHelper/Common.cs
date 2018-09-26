using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.SysHelper
{
    public class Common
    {
        public static char[] separator = new char[] { ',' };

        public static string GetStepCode(string s, string x,bool isRemove)
        {
            if (string.IsNullOrWhiteSpace(s)) return x;
            var items = s.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (isRemove)
            {
                if (items.Contains(x)) items.Remove(x);
            }
            else
            {
                if (!items.Contains(x)) items.Add(x);
            }

            if (items.Count < 1) return "";

            return string.Join(",", items);
        }
    }
}
