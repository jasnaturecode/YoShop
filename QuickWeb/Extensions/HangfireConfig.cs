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
        /// hangfire初始化
        /// </summary>
        public static void Start()
        {
            RecurringJob.AddOrUpdate(() => CheckLinks(), Cron.HourInterval(5)); //每5h检查友链
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
            HangfireHelper.CreateJob(typeof(IHangfireBackJob), nameof(HangfireBackJob.RecordPostVisit),"default",new Random().Next(1,10000));
        }
    }
}
