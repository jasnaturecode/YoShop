using SqlSugar;

namespace Quick.Models.Entity.Table
{
    /// <summary>
    /// 
    /// </summary>
    public class yoshop_upload_group
    {
        /// <summary>
        /// 
        /// </summary>
        public yoshop_upload_group()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 group_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String group_type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String group_name { get; set; }

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