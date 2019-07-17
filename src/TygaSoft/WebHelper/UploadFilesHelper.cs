using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Web.Configuration;

namespace TygaSoft.WebHelper
{
    public class UploadFilesHelper
    {
        static readonly string FilesRoot = WebConfigurationManager.AppSettings["FilesRoot"];

        public enum FileExtension
        {
            jpg = 255216, gif = 7173, bmp = 6677, png = 13780, xls = 208207, xlsx = 8075, doc = 208207, docx = 8075
            // 255216 jpg;7173 gif;6677 bmp;13780 png; 7790 exe dll; 8297 rar; 6063 xml;6033 html;239187 aspx;117115 cs;119105 js;210187 txt;255254 sql;xls = 208207 
        }

        /// <summary>
        /// 使用文件固定字节法验证上传图片是否合法
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileLen"></param>
        /// <returns></returns>
        public static bool IsPictureValidated(Stream stream, int fileLen)
        {
            if (fileLen == 0) return false;

            //自定义一个数组，包含所有允许上传的文件扩展名
            FileExtension[] fes = { FileExtension.jpg, FileExtension.gif, FileExtension.bmp, FileExtension.png };

            byte[] imgArray = new byte[fileLen];
            stream.Read(imgArray, 0, fileLen);
            MemoryStream ms = new MemoryStream(imgArray);
            BinaryReader br = new BinaryReader(ms);
            string fileBuffer = "";
            byte buffer;
            try
            {
                buffer = br.ReadByte();
                fileBuffer = buffer.ToString();
                buffer = br.ReadByte();
                fileBuffer += buffer.ToString();
            }
            catch
            {
            }
            br.Close();
            ms.Close();
            foreach (FileExtension item in fes)
            {
                if (Int32.Parse(fileBuffer) == (int)item)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 使用文件固定字节法验证文件是否合法
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileLen"></param>
        /// <returns></returns>
        public static bool IsFileValidated(Stream stream, int fileLen)
        {
            if (fileLen == 0) return false;

            //自定义一个数组，包含所有允许上传的文件扩展名，这里只定义xls扩展名
            FileExtension[] fes = { FileExtension.gif, FileExtension.bmp, FileExtension.jpg, FileExtension.png, FileExtension.xls, FileExtension.xlsx, FileExtension.doc, FileExtension.docx };

            byte[] imgArray = new byte[fileLen];
            stream.Read(imgArray, 0, fileLen);
            MemoryStream ms = new MemoryStream(imgArray);
            BinaryReader br = new BinaryReader(ms);
            string fileBuffer = "";
            byte buffer;
            try
            {
                buffer = br.ReadByte();
                fileBuffer = buffer.ToString();
                buffer = br.ReadByte();
                fileBuffer += buffer.ToString();
            }
            catch
            {
            }
            br.Close();
            ms.Close();
            foreach (FileExtension item in fes)
            {
                if (Int32.Parse(fileBuffer) == (int)item)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 指定存储目录key，是否生成缩略图，上传文件
        /// 返回所有文件路径，如果是生成缩略图，则包含缩略图文件路径
        /// </summary>
        /// <param name="file"></param>
        /// <param name="key"></param>
        /// <param name="isCreateThumbnail"></param>
        /// <returns></returns>
        public string[] Upload(HttpPostedFile file, string key, bool isCreateThumbnail)
        {
            if (file == null || file.ContentLength == 0) throw new ArgumentException("没有获取到任何上传的文件", "file");
            int size = file.ContentLength;
            string fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (!IsFileValidated(file.InputStream, size)) throw new ArgumentException("上传文件不在规定的上传文件范围内");
            if (isCreateThumbnail)
            {
                if ((fileExtension != ".jpg") || (fileExtension != ".jpg"))
                {
                    throw new ArgumentException("创建缩略图只支持.jpg格式的文件，请检查");
                }
            }
            string dir = ConfigHelper.GetValueByKey(key);
            if (string.IsNullOrWhiteSpace(dir))
            {
                throw new ArgumentException("未找到" + key + "的相关配置，请检查", "key");
            }

            string paths = "";

            dir = VirtualPathUtility.AppendTrailingSlash(dir);
            string rndName = FilesHelper.GetFormatDateTime();
            string fName = rndName + fileExtension;
            string filePath = dir + rndName.Substring(0, 8) + "/";
            string fullPath = HttpContext.Current.Server.MapPath(filePath);
            if (!Directory.Exists(fullPath)) Directory.CreateDirectory(fullPath);
            file.SaveAs(fullPath + fName);

            paths += filePath + fName;
            if (isCreateThumbnail)
            {
                ImagesHelper ih = new ImagesHelper();
                string[] whBPicture = ConfigHelper.GetValueByKey("BPicture").Split(',');
                string[] whMPicture = ConfigHelper.GetValueByKey("MPicture").Split(',');
                string[] whSPicture = ConfigHelper.GetValueByKey("SPicture").Split(',');
                string bPicturePath = filePath + rndName + "_b" + fileExtension;
                string mPicturePath = filePath + rndName + "_m" + fileExtension;
                string sPicturePath = filePath + rndName + "_s" + fileExtension;
                ih.CreateThumbnailImage(fullPath + fName, HttpContext.Current.Server.MapPath(bPicturePath), int.Parse(whBPicture[0]), int.Parse(whBPicture[1]));
                ih.CreateThumbnailImage(fullPath + fName, HttpContext.Current.Server.MapPath(mPicturePath), int.Parse(whMPicture[0]), int.Parse(whMPicture[1]));
                ih.CreateThumbnailImage(fullPath + fName, HttpContext.Current.Server.MapPath(sPicturePath), int.Parse(whSPicture[0]), int.Parse(whSPicture[1]));
                paths += "," + bPicturePath;
                paths += "," + mPicturePath;
                paths += "," + sPicturePath;
            }
            else
            {
                paths += "," + filePath + fName;
                paths += "," + filePath + fName;
                paths += "," + filePath + fName;
            }

            return paths.Split(',');
        }

        /// <summary>
        /// 上传文件，并返回文件存储的虚拟路径
        /// </summary>
        /// <param name="file"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string UploadOriginalFile(HttpPostedFile file, string key)
        {
            var currTime = DateTime.Now;
            var rndFolder = GetRandomFolder(key, currTime);
            string fileName = string.Format("{0}{1}", rndFolder, VirtualPathUtility.GetExtension(file.FileName));
            string saveVirtualDir = string.Format("{0}/{1}/{2}/{3}", FilesRoot, key, currTime.ToString("yyyyMM"), rndFolder);

            file.SaveAs(string.Format("{0}\\{1}", HttpContext.Current.Server.MapPath(saveVirtualDir), fileName));
            return string.Format("{0}/{1}", saveVirtualDir, fileName);
        }

        /// <summary>
        /// 获取唯一随机数
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <returns></returns>
        public static string GetRandomFolder(string key,DateTime currTime)
        {
            string rndFolder = "";
            string fullPath = HttpContext.Current.Server.MapPath(string.Format("{0}/{1}/{2}", FilesRoot, key, currTime.ToString("yyyyMM")));
            if (!Directory.Exists(fullPath))
            {
                rndFolder = string.Format("{0}{1}", currTime.ToString("dd"),"0001");
                fullPath = string.Format("{0}\\{1}",fullPath, rndFolder);
                Directory.CreateDirectory(fullPath);
            }
            else
            {
                rndFolder = string.Format("{0}{1}", currTime.ToString("dd"),(Directory.GetDirectories(fullPath).Length + 1).ToString().PadLeft(4, '0'));
                fullPath = string.Format("{0}\\{1}", fullPath, rndFolder);
                Directory.CreateDirectory(fullPath);
            }

            return rndFolder;
        }
    }
}
