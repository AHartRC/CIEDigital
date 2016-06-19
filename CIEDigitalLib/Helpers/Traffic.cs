using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace CIEDigitalLib.Helpers
{
    /// <summary>
    ///     This class handled the various traffic that occurrs via applications that AH Operations develops.
    /// </summary>
    public static class Traffic
    {
        public static string HTTPGET(string url)
        {
            try
            {
                var request = (HttpWebRequest) WebRequest.Create(url);
                request.Timeout = 100000;
                var response = (HttpWebResponse) request.GetResponse();
                var responseStream = response.GetResponseStream();

                if (responseStream != null)
                    using (var resStream = new StreamReader(responseStream))
                        return resStream.ReadToEnd();
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(url);
                Console.WriteLine(e);
                return null;
            }
        }

        public static Stream HTTPGETStream(string url)
        {
            try
            {
                var request = (HttpWebRequest) WebRequest.Create(url);
                request.Timeout = 100000;
                var response = (HttpWebResponse) request.GetResponse();
                var responseStream = response.GetResponseStream();
                return responseStream;
            }
            catch (Exception e)
            {
                Console.WriteLine(url);
                Console.WriteLine(e);
                return null;
            }
        }

        public static string HTTPPOST(string url, string postData)
        {
            try
            {
                var webRequest = (HttpWebRequest) WebRequest.Create(url);
                webRequest.Method = "POST";
                webRequest.ContentType = "x-www-form-urlencoded";

                var byteArray = Encoding.UTF8.GetBytes(postData);
                using (var requestStream = webRequest.GetRequestStream())
                    requestStream.Write(byteArray, 0, byteArray.Length);
                using (var responseStream = webRequest.GetResponse().GetResponseStream())
                    if (responseStream != null)
                        using (var responseReader = new StreamReader(responseStream))
                            return responseReader.ReadToEnd();
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(url);
                Console.WriteLine(postData);
                Console.WriteLine(e);
                return null;
            }
        }

        public static T JsonRPCGET<T>(string url) where T : class
        {
            var request = (HttpWebRequest) WebRequest.Create(url);
            request.ContentType = "application/json-rpc; charset=utf-8";
            request.Accept = "application/json-rpc, text/javascript, */*";
            request.Method = "GET";

            var response = request.GetResponse();
            var stream = response.GetResponseStream();
            var json = "";

            if (stream != null)
                using (var sr = new StreamReader(stream))
                    while (!sr.EndOfStream)
                        json += sr.ReadLine();
            return Deserialize<T>(json);
        }

        public static string JsonRPCPOST(string url, string strJson)
        {
            var request = (HttpWebRequest) WebRequest.Create(url);
            request.ContentType = "application/json; charset=utf-8";
            request.Accept = "application/json, text/javascript, */*";
            request.Method = "POST";
            var byteArray = Encoding.UTF8.GetBytes(strJson);
            request.ContentLength = byteArray.Length;

            using (var s = request.GetRequestStream())
                s.Write(byteArray, 0, byteArray.Length);

            var json = "";
            var response = request.GetResponse();

            var stream = response.GetResponseStream();
            if (stream != null)
                using (var sr = new StreamReader(stream))
                    while (!sr.EndOfStream)
                        json += sr.ReadLine();
            return json;
        }

        public static T JsonRPCPost<T>(string url, string strJson) where T : class
        {
            var response = JsonRPCPOST(url, strJson);
            var result = Deserialize<T>(response);
            return result;
        }

        public static string Serialize(object obj)
        {
            if (obj == null)
                return null;
            var jss = new JavaScriptSerializer();
            return jss.Serialize(obj);
        }

        public static T Deserialize<T>(string source) where T : class
        {
            if (string.IsNullOrWhiteSpace(source))
                return null;
            var jss = new JavaScriptSerializer();
            var result = jss.Deserialize<T>(source);
            return result;
        }

        //}
        //    return jss.Deserialize(source, typeof(object));
        //    jss.RegisterConverters(new JavaScriptConverter[] { new DynamicJsonConverter() });
        //    JavaScriptSerializer jss = new JavaScriptSerializer();
        //{

        //public static dynamic DynamicDeserialize(string source)
    }
}