using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Web;

namespace TygaSoft.WebHelper
{
    public class TempFolder
    {
        string fileRoot = ConfigurationManager.AppSettings["FilesRoot"];
        DateTime currTime = DateTime.Now;

        public string GetTempFolderUrl()
        {
            return string.Format("{0}/{1}", fileRoot.TrimEnd('/'), "Temp");
        }

        public string GetTempFolderPath()
        {
            var path = HttpContext.Current.Server.MapPath(GetTempFolderUrl());
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            return path;
        }

        public string GetTempFolderPath(string key)
        {
            var path = HttpContext.Current.Server.MapPath(GetTempFolderUrl());
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            return path;
        }

        public void DoTempFile()
        {
            var tempPath = GetTempFolderPath();
            var lastDir = currTime.ToString("yyyyMMdd");
            var prevDir = currTime.AddDays(-1).ToString("yyyyMMdd");
            var lastDirPath = string.Format("{0}\\{1}", tempPath, lastDir);
            var prevDirPath = string.Format("{0}\\{1}", tempPath, prevDir);
            var dirs = Directory.GetDirectories(tempPath);
            foreach (var dir in dirs)
            {
                var dirName = dir.Substring(dir.LastIndexOf('\\')+1);
                if (dirName != prevDir && dirName != lastDir)
                {
                    var subDirs = Directory.GetDirectories(dir);
                    foreach (var subDir in subDirs)
                    {
                        var subFiles = Directory.GetFiles(subDir);
                        foreach (var f in subFiles)
                        {
                            Task.Factory.StartNew(() => { File.Delete(f); });
                        }
                        Task.Factory.StartNew(() => { Directory.Delete(subDir); });
                    }
                    var files = Directory.GetFiles(dir);
                    foreach (var f in files)
                    {
                        Task.Factory.StartNew(() => { File.Delete(f); });
                    }
                    Task.Factory.StartNew(() => { Directory.Delete(dir); });
                }
            }
            if (!Directory.Exists(lastDirPath)) Directory.CreateDirectory(lastDirPath);
        }

        public void CopyToTemp(string excelFileName,out string tempUrl)
        {
            var sourceUrl = HttpContext.Current.Server.MapPath("~/App_Data/Template/" + excelFileName + "");
            var rndDir = string.Format(@"{0}\{1}\{2}", GetTempFolderPath(), currTime.ToString("yyyyMMdd"), Path.GetRandomFileName());
            if (!Directory.Exists(rndDir)) Directory.CreateDirectory(rndDir);
            tempUrl = string.Format(@"{0}\{1}", rndDir, excelFileName);
            File.Copy(sourceUrl, tempUrl);
        }

        public string GetTempUrl(string toFileName)
        {
            DoTempFile();
            return string.Format(@"{0}/{1}/{2}/{3}", GetTempFolderUrl(), currTime.ToString("yyyyMMdd"), Path.GetRandomFileName(), toFileName);
        }

        public string GetTempUrl(string toFileName,bool isCheckExist)
        {
            DoTempFile();

            var path = string.Format(@"{0}/{1}/{2}", GetTempFolderUrl(), currTime.ToString("yyyyMMdd"), Path.GetRandomFileName());
            if (isCheckExist)
            {
                var fullPath = HttpContext.Current.Server.MapPath(path);
                if (!Directory.Exists(fullPath)) Directory.CreateDirectory(fullPath);
            }
            path += "/" + toFileName + "";

            return path;
        }

        public string GetExportSourceUrl(string fromFileName)
        {
            return string.Format(@"{0}/{1}", "~/App_Data/Template", fromFileName);
        }
    }
}
