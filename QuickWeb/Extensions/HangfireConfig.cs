using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using QuickWeb.Extensions.Common;
using QuickWeb.Extensions.Hangfire;

namespace QuickWeb.Extensions
{
    /// <summary>
    /// hangfire任务配置
    /// </summary>
    public class HangfireConfig
    {
        /// <summary>
        /// Cron表达式范例：
        ///  每隔5秒执行一次：*/5 * * * * ?
        ///  每隔1分钟执行一次：0 */1 * * * ?
        ///  每隔5小时执行一次：0 * */5 * * ?
        ///  每天23点执行一次：0 0 23 * * ?
        ///  每天凌晨1点执行一次：0 0 1 * * ?
        ///  每月1号凌晨1点执行一次：0 0 1 1 * ?
        ///  每月最后一天23点执行一次：0 0 23 L * ?
        ///  每周星期天凌晨1点实行一次：0 0 1 ? * L
        ///  在26分、29分、33分执行一次：0 26,29,33 * * * ?
        ///  每天的0点、13点、18点、21点都执行一次：0 0 0,13,18,21 * * ?
        /// </summary>
        public static void Start()
        {
            RecurringJob.AddOrUpdate(() => CheckLinks(), "0 * */5 * * ?",TimeZoneInfo.Local); //每5h检查友链
            RecurringJob.AddOrUpdate(() => EverydayJob(), Cron.Daily(5), TimeZoneInfo.Local); //每天的任务
            RecurringJob.AddOrUpdate(() => EveryweekJob(), Cron.Weekly(DayOfWeek.Monday, 5), TimeZoneInfo.Local); //每周的任务
        }

        /// <summary>
        /// 检查友链
        /// </summary>
        public static void CheckLinks()
        {
            HangfireHelper.CreateJob(typeof(IHangfireBackJob), nameof(HangfireBackJob.CheckLinks), "default");
        }

        /// <summary>
        /// 每日任务
        /// </summary>
        public static void EverydayJob()
        {
            HangfireHelper.CreateJob(typeof(IHangfireBackJob), nameof(HangfireBackJob.EverydayJob), "default");
        }

        /// <summary>
        /// 每周任务
        /// </summary>
        public static void EveryweekJob()
        {
            HangfireHelper.CreateJob(typeof(IHangfireBackJob), nameof(HangfireBackJob.RecordPostVisit), "default", new Random().Next(1, 10000));
        }
    }
}
