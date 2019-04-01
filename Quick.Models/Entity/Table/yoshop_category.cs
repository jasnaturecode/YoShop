using SqlSugar;

namespace Quick.Models.Entity.Table
{
    /// <summary>
    /// 
    /// </summary>
    public class yoshop_category
    {
        /// <summary>
        /// 
        /// </summary>
        public yoshop_category()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 category_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 parent_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 image_id { get; set; }

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