using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Configuration;

namespace TygaSoft.WebHelper
{
    public class ConfigHelper
    {
        /// <summary>
        /// 获取当前key对应的value值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValueByKey(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString();
        }

        /// <summary>
        /// 获取应用程序web.config中的文件配置路径，并返回物理路径
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetFullPath(string key)
        {
            string appSetting = System.Configuration.ConfigurationManager.AppSettings[key].ToString();
            if (!Path.IsPathRooted(appSetting)) appSetting = System.Web.HttpContext.Current.Server.MapPath(appSetting);

            return appSetting;
        }
    }
}
