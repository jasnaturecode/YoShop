using SqlSugar;

namespace Quick.Models.Entity.Table
{
    /// <summary>
    /// 
    /// </summary>
    public class yoshop_goods_image
    {
        /// <summary>
        /// 
        /// </summary>
        public yoshop_goods_image()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 goods_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 image_id { get; set; }

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