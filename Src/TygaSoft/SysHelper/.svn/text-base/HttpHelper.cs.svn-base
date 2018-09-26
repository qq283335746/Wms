using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace TygaSoft.SysHelper
{
    public class HttpHelper
    {
        public static string DoGet(string url)
        {
            var result = string.Empty;
            var statusCode = -1;
            DoHttpGet(url, "application/json; charset=utf-8", out statusCode, out result);

            return result;
        }

        public static void DoHttpGet(string url, string contentType, out int statusCode, out string result)
        {
            HttpWebRequest req = null;
            HttpWebResponse res = null;
            try
            {
                req = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
                req.Method = "Get";
                if(!string.IsNullOrEmpty(contentType)) req.Headers.Add("ContentType", contentType);
                res = (HttpWebResponse)req.GetResponse();
                Stream sw = res.GetResponseStream();
                StreamReader reader = new StreamReader(sw);
                result = reader.ReadToEnd();
                statusCode = (int)res.StatusCode;
                res.Close();
                sw.Close();
                reader.Close();

            }
            catch (Exception)
            {
                if (req != null) req = null;
                if (res != null)
                {
                    res.Close();
                    res = null;
                }
                throw;
            }
        }


        public static string DoPost(string url, string content, string encodingName, string contentType)
        {
            var result = string.Empty;
            var statusCode = -1;

            DoHttpPost(url, content, encodingName, contentType, out statusCode, out result);

            return result;
            
        }

        public static void DoHttpPost(string url, string content,string encodingName,string contentType, out int statusCode, out string result)
        {
            if (string.IsNullOrEmpty(encodingName)) encodingName = "utf-8";
            if (string.IsNullOrEmpty(contentType)) contentType = "application/x-www-form-urlencoded";
            HttpWebRequest req = null;
            HttpWebResponse res = null;
            statusCode = -1;
            result = string.Empty;

            try
            {
                Encoding encoding = Encoding.GetEncoding(encodingName);
                byte[] data = encoding.GetBytes(content);

                req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                req.ContentType = contentType;
                req.ContentLength = data.Length;

                Stream reqStream = req.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();

                req.AllowAutoRedirect = false;
                res = (HttpWebResponse)req.GetResponse();
                Stream responseStream = res.GetResponseStream();

                var streamReader = new StreamReader(responseStream);
                result = streamReader.ReadToEnd();
                //获取响应结果的http状态码，200-请求成功
                statusCode = (int)res.StatusCode;

                res.Close();
                responseStream.Close();
                streamReader.Close();
            }
            catch (Exception ex)
            {
                if (req != null) req = null;
                if (res != null)
                {
                    res.Close();
                    res = null;
                }
                result = ex.Message;
            }
        }
    }
}
