using SqlSugar;

namespace Quick.Models.Entity.Table
{
    /// <summary>
    /// 
    /// </summary>
    public class yoshop_upload_file
    {
        /// <summary>
        /// 
        /// </summary>
        public yoshop_upload_file()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 file_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String storage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 group_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String file_url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String file_name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 file_size { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String file_type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String extension { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Byte is_delete { get; set; }

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