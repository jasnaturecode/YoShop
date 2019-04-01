using Masuit.MyBlogs.Core.Extensions.Hangfire;
using Masuit.Tools.Systems;
using QuickWeb.Extensions.Common;

namespace QuickWeb.Extensions.Hangfire
{
    /// <summary>
    /// hangfire后台任务
    /// </summary>
    public class HangfireBackJob : IHangfireBackJob
    {
        /// <summary>
        /// 文章访问记录
        /// </summary>
        /// <param name="pid"></param>
        public void RecordPostVisit(int pid)
        {

        }

        /// <summary>
        /// 防火墙拦截日志
        /// </summary>
        /// <param name="s"></param>
        public static void InterceptLog(IpIntercepter s)
        {
            RedisHelper.IncrBy("interceptCount");
            RedisHelper.LPush("intercept", s);
        }

        /// <summary>
        /// 每天的任务
        /// </summary>
        public void EverydayJob()
        {
            CommonHelper.IPErrorTimes.RemoveWhere(kv => kv.Value < 100); //将访客访问出错次数少于100的移开
            RedisHelper.Set("ArticleViewToken", SnowFlake.GetInstance().GetUniqueShortId(6)); //更新加密文章的密码
            RedisHelper.IncrBy("Interview:RunningDays");
        }

        /// <summary>
        /// 检查友链
        /// </summary>
        public void CheckLinks()
        {

        }
    }
}