using SqlSugar;

namespace Quick.Models.Entity.Table
{
    /// <summary>
    /// 
    /// </summary>
    public class yoshop_wxapp_help
    {
        /// <summary>
        /// 
        /// </summary>
        public yoshop_wxapp_help()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 help_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 sort { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 wxapp_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 create_time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 update_time { get; set; }
    }
}