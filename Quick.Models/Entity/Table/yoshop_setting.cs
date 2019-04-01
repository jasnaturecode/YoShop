using SqlSugar;

namespace Quick.Models.Entity.Table
{
    /// <summary>
    /// 
    /// </summary>
    public class yoshop_setting
    {
        /// <summary>
        /// 
        /// </summary>
        public yoshop_setting()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.String key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String describe { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String values { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 wxapp_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 update_time { get; set; }
    }
}