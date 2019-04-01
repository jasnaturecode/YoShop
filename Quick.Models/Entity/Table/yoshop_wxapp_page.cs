using SqlSugar;

namespace Quick.Models.Entity.Table
{
    /// <summary>
    /// 
    /// </summary>
    public class yoshop_wxapp_page
    {
        /// <summary>
        /// 
        /// </summary>
        public yoshop_wxapp_page()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 page_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Byte page_type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String page_data { get; set; }

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