using Masuit.MyBlogs.Core.Extensions.Hangfire;

namespace QuickWeb.Extensions.Hangfire
{
    /// <summary>
    /// hangfire后台任务
    /// </summary>
    public interface IHangfireBackJob
    {
        /// <summary>
        /// 文章访问记录
        /// </summary>
        /// <param name="pid"></param>
        void RecordPostVisit(int pid);

        /// <summary>
        /// 每日任务
        /// </summary>
        void EverydayJob();

        /// <summary>
        /// 友链检查
        /// </summary>
        void CheckLinks();
    }
}