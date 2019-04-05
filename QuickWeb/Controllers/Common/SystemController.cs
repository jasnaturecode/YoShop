using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Masuit.MyBlogs.Core.Extensions.Hangfire;
using Masuit.Tools;
using Masuit.Tools.Hardware;
using Masuit.Tools.Logging;
using Masuit.Tools.Models;
using Microsoft.AspNetCore.Mvc;
using QuickWeb.Controllers.Common;
using QuickWeb.Extensions.Common;
using QuickWeb.Hubs;

namespace QuickWeb.Controllers
{
    /// <summary>
    /// 健康检查
    /// </summary>
    public class SystemController : BaseController
    {
        /// <summary>
        /// 心跳检测
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("health")]
        public OkResult Check()
        {
            return Ok();
        }

        /// <summary>
        /// 获取硬件基本信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetBaseInfo()
        {
            List<CpuInfo> cpuInfo = SystemInfo.GetCpuInfo();
            RamInfo ramInfo = SystemInfo.GetRamInfo();
            string osVersion = SystemInfo.GetOsVersion();
            var total = new StringBuilder();
            var free = new StringBuilder();
            var usage = new StringBuilder();
            SystemInfo.DiskTotalSpace().ForEach(kv =>
            {
                total.Append(kv.Key + kv.Value + " | ");
            });
            SystemInfo.DiskFree().ForEach(kv => free.Append(kv.Key + kv.Value + " | "));
            SystemInfo.DiskUsage().ForEach(kv => usage.Append(kv.Key + kv.Value.ToString("P") + " | "));
            IList<string> mac = SystemInfo.GetMacAddress();
            IList<string> ips = SystemInfo.GetIPAddress();
            var span = DateTime.Now - CommonHelper.StartupTime;
            var boot = DateTime.Now - SystemInfo.BootTime();

            return Json(new
            {
                runningTime = $"{span.Days}天{span.Hours}小时{span.Minutes}分钟",
                bootTime = $"{boot.Days}天{boot.Hours}小时{boot.Minutes}分钟",
                cpuInfo,
                ramInfo,
                osVersion,
                diskInfo = new
                {
                    total = total.ToString(),
                    free = free.ToString(),
                    usage = usage.ToString()
                },
                netInfo = new
                {
                    mac,
                    ips
                }
            });
        }

        /// <summary>
        /// 获取历史性能计数器
        /// </summary>
        /// <returns></returns>
        public ActionResult GetHistoryList()
        {
            return Json(new
            {
                cpu = MyHub.PerformanceCounter.Select(c => new[]
                {
                    c.Time,
                    c.CpuLoad
                }),
                mem = MyHub.PerformanceCounter.Select(c => new[]
                {
                    c.Time,
                    c.MemoryUsage
                }),
                temp = MyHub.PerformanceCounter.Select(c => new[]
                {
                    c.Time,
                    c.Temperature
                }),
                read = MyHub.PerformanceCounter.Select(c => new[]
                {
                    c.Time,
                    c.DiskRead
                }),
                write = MyHub.PerformanceCounter.Select(c => new[]
                {
                    c.Time,
                    c.DiskWrite
                }),
                down = MyHub.PerformanceCounter.Select(c => new[]
                {
                    c.Time,
                    c.Download
                }),
                up = MyHub.PerformanceCounter.Select(c => new[]
                {
                    c.Time,
                    c.Upload
                }),
            });
        }

        /// <summary>
        /// 邮件测试
        /// </summary>
        /// <param name="smtp"></param>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <param name="port"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public ActionResult MailTest(string smtp, string user, string pwd, int port, string to)
        {
            try
            {
                new Email()
                {
                    EnableSsl = true,
                    Body = "发送成功，网站邮件配置正确！",
                    SmtpServer = smtp,
                    Username = user,
                    Password = pwd,
                    SmtpPort = port,
                    Subject = "网站测试邮件",
                    Tos = to
                }.Send();
                return ResultData(null, true, "测试邮件发送成功，网站邮件配置正确！");
            }
            catch (Exception e)
            {
                return ResultData(null, false, "邮件配置测试失败！错误信息：\r\n" + e.Message + "\r\n\r\n详细堆栈跟踪：\r\n" + e.StackTrace);
            }
        }

        /// <summary>
        /// 路径测试
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ActionResult PathTest(string path)
        {
            if (!(path.EndsWith("/") || path.EndsWith("\\")))
            {
                return ResultData(null, false, "路径不存在");
            }

            if (path.Equals("/") || path.Equals("\\"))
            {
                return ResultData(null, true, "根路径正确");
            }

            try
            {
                bool b = Directory.Exists(path);
                return ResultData(null, b, b ? "根路径正确" : "路径不存在");
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return ResultData(null, false, "路径格式不正确！错误信息：\r\n" + e.Message + "\r\n\r\n详细堆栈跟踪：\r\n" + e.StackTrace);
            }
        }

        /// <summary>
        /// 清空性能计数器缓存
        /// </summary>
        /// <returns></returns>
        public ActionResult ClearPerfCounter()
        {
            MyHub.PerformanceCounter.Clear();
            return Ok();
        }

        #region 网站防火墙

        /// <summary>
        /// 获取全局IP黑名单
        /// </summary>
        /// <returns></returns>
        public ActionResult IpBlackList()
        {
            return ResultData(CommonHelper.DenyIP);
        }

        /// <summary>
        /// 获取地区IP黑名单
        /// </summary>
        /// <returns></returns>
        public ActionResult AreaIPBlackList()
        {
            return ResultData(CommonHelper.DenyAreaIP);
        }

        /// <summary>
        /// 获取IP地址段黑名单
        /// </summary>
        /// <returns></returns>
        public ActionResult GetIPRangeBlackList()
        {
            return ResultData(System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "DenyIPRange.txt")));
        }

        /// <summary>
        /// 设置IP地址段黑名单
        /// </summary>
        /// <returns></returns>
        public ActionResult SetIPRangeBlackList(string content)
        {
            System.IO.File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "DenyIPRange.txt"), content, Encoding.UTF8);
            CommonHelper.DenyIPRange.Clear();
            var lines = System.IO.File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "DenyIPRange.txt")).Where(s => s.Split(' ').Length > 2);
            foreach (var line in lines)
            {
                try
                {
                    var strs = line.Split(' ');
                    CommonHelper.DenyIPRange[strs[0]] = strs[1];
                }
                catch (IndexOutOfRangeException)
                {
                }
            }

            return ResultData(null);
        }

        /// <summary>
        /// 全局IP白名单
        /// </summary>
        /// <returns></returns>
        public ActionResult IpWhiteList()
        {
            return ResultData(System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "whitelist.txt")));
        }

        /// <summary>
        /// 设置IP黑名单
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public ActionResult SetIpBlackList(string content)
        {
            CommonHelper.DenyIP = content;
            System.IO.File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "denyip.txt"), CommonHelper.DenyIP, Encoding.UTF8);
            return ResultData(null);
        }

        /// <summary>
        /// 设置IP白名单
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public ActionResult SetIpWhiteList(string content)
        {
            System.IO.File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "whitelist.txt"), content, Encoding.UTF8);
            CommonHelper.IPWhiteList.Add(content);
            return ResultData(null);
        }

        /// <summary>
        /// 获取拦截日志
        /// </summary>
        /// <returns></returns>
        public ActionResult InterceptLog()
        {
            var list = RedisHelper.LRange<IpIntercepter>("intercept", 0, -1);
            return ResultData(new
            {
                interceptCount = RedisHelper.Get("interceptCount"),
                list
            });
        }

        /// <summary>
        /// 清除拦截日志
        /// </summary>
        /// <returns></returns>
        public ActionResult ClearInterceptLog()
        {
            bool b = RedisHelper.Del("intercept") > 0;
            return ResultData(null, b, b ? "拦截日志清除成功！" : "拦截日志清除失败！");
        }

        /// <summary>
        /// 将IP添加到白名单
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public ActionResult AddToWhiteList(string ip)
        {
            if (ip.MatchInetAddress())
            {
                string ips = System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "whitelist.txt"));
                List<string> list = ips.Split(',').Where(s => !string.IsNullOrEmpty(s)).ToList();
                list.Add(ip);
                System.IO.File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "whitelist.txt"), string.Join(",", list.Distinct()), Encoding.UTF8);
                CommonHelper.IPWhiteList = list;
                foreach (var kv in CommonHelper.DenyAreaIP)
                {
                    foreach (string item in list)
                    {
                        CommonHelper.DenyAreaIP[kv.Key].Remove(item);
                    }
                }

                System.IO.File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "denyareaip.txt"), CommonHelper.DenyAreaIP.ToJsonString(), Encoding.UTF8);
                return ResultData(null);
            }

            return ResultData(null, false);
        }

        /// <summary>
        /// 将IP添加到黑名单
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public ActionResult AddToBlackList(string ip)
        {
            if (ip.MatchInetAddress())
            {
                CommonHelper.DenyIP += "," + ip;
                System.IO.File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "denyip.txt"), CommonHelper.DenyIP, Encoding.UTF8);
                CommonHelper.IPWhiteList.Remove(ip);
                System.IO.File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "whitelist.txt"), string.Join(",", CommonHelper.IPWhiteList.Distinct()), Encoding.UTF8);
                return ResultData(null);
            }

            return ResultData(null, false);
        }

        #endregion
    }
}