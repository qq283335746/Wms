using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace TygaSoft.SysHelper
{
    public class MapHelper
    {
        public static readonly string AK = "NnwxmFFvpZVb7GIAf6i7NMzUMj40wlXu";

        public static string GetLocationByIP(string ip)
        {
            if(ip == "::1") return JObject.Parse(HttpHelper.DoGet(string.Format(@"http://api.map.baidu.com/location/ip?ak={0}&coor=bd09ll", AK))).ToString();
            return JObject.Parse(HttpHelper.DoGet(string.Format(@"http://api.map.baidu.com/location/ip?ak={0}&coor=bd09ll&ip={1}", AK,ip))).ToString();
        }

        public string GetStaticImage(int w, int h, string center, string markers,string markerStyles)
        {
            var list = new List<string>();
            list.Add("ak=" + AK + "");

            if (w > 1024) w = 1024;
            if (h > 1024) h = 1024;
            if (w > 0) list.Add("width=" + w + "");
            if (h > 0) list.Add("height=" + h + "");
            if (!string.IsNullOrWhiteSpace(center)) list.Add("center=" + center + "");
            if (!string.IsNullOrWhiteSpace(markers)) list.Add("markers=" + markers + "");
            if (!string.IsNullOrWhiteSpace(markerStyles)) list.Add("markerStyles=" + markerStyles + "");

            return string.Format("{0}?{1}", "http://api.map.baidu.com/staticimage/v2", string.Join("&", list));
        }
    }
}
