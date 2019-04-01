using SqlSugar;

namespace Quick.Models.Entity.Table
{
    /// <summary>
    /// 
    /// </summary>
    public class yoshop_upload_file_used
    {
        /// <summary>
        /// 
        /// </summary>
        public yoshop_upload_file_used()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 used_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 file_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 from_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String from_type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 wxapp_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 create_time { get; set; }
    }
}