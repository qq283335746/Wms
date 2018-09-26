using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TygaSoft.Model;

namespace TygaSoft.SysHelper
{
    public class EnumHelper
    {
        public static int GetValue(Type enumType, string name)
        {
            var list = GetList(enumType);
            var item = list.FirstOrDefault(x => x.Value == name);
            if (item != null) return int.Parse(item.Key);

            return -1;
        }

        public static IList<KeyValueInfo> GetList(Type enumType)
        {
            var list = new List<KeyValueInfo>();
            var values = Enum.GetValues(enumType);
            foreach (var item in values)
            {
                list.Add(new KeyValueInfo(((int)item).ToString(), Enum.GetName(enumType, item)));
            }
            return list;
        }
    }
}
