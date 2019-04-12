using System;
using System.IO;
using System.Linq;
using System.Net;
using Masuit.Tools;
using Microsoft.AspNetCore.Http;

namespace QuickWeb.Extensions.UEditor
{
    /// <summary>
    /// Crawler 的摘要说明
    /// </summary>
    public class CrawlerHandler : Handler
    {
        private string[] _sources;
        private Crawler[] _crawlers;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public CrawlerHandler(HttpContext context) : base(context)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string Process()
        {
            _sources = Request.Form["source[]"];
            if (_sources == null || _sources.Length == 0)
            {
                return WriteJson(new
                {
                    state = "参数错误：没有指定抓取源"
                });
            }
            _crawlers = _sources.Select(x => new Crawler(x).Fetch()).ToArray();
            return WriteJson(new
            {
                state = "SUCCESS",
                list = _crawlers.Select(x => new
                {
                    state = x.State,
                    source = x.SourceUrl,
                    url = x.ServerUrl
                })
            });
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Crawler
    {
        /// <summary>
        /// 
        /// </summary>
        public string SourceUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ServerUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string State { get; set; }

        //private HttpServerUtility Server { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceUrl"></param>
        public Crawler(string sourceUrl)
        {
            SourceUrl = sourceUrl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Crawler Fetch()
        {
            if (!(SourceUrl.IsExternalAddress()))
            {
                State = "INVALID_URL";
                return this;
            }
            var request = WebRequest.Create(SourceUrl) as HttpWebRequest;
            using (var response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    State = "Url returns " + response.StatusCode + ", " + response.StatusDescription;
                    return this;
                }
                if (response.ContentType.IndexOf("image") == -1)
                {
                    State = "Url is not an image";
                    return this;
                }
                ServerUrl = PathFormatter.Format(Path.GetFileName(SourceUrl), UeditorConfig.GetString("catcherPathFormat"));
                var savePath = AppContext.BaseDirectory + "wwwroot" + ServerUrl;
                if (!Directory.Exists(Path.GetDirectoryName(savePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                }
                try
                {
                    using (var stream = response.GetResponseStream())
                    {
                        using (var ms = new MemoryStream())
                        {
                            stream.CopyTo(ms);
                            File.WriteAllBytes(savePath, ms.GetBuffer());
                        }
                        //var (url, success) = CommonHelper.UploadImage(savePath);
                        //if (success)
                        //{
                        //    ServerUrl = url;
                        //    BackgroundJob.Enqueue(() => File.Delete(savePath));
                        //}
                    }
                    State = "SUCCESS";
                }
                catch (Exception e)
                {
                    State = "抓取错误：" + e.Message;
                }
                return this;
            }
        }
    }
}