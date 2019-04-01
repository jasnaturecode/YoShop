using SqlSugar;

namespace Quick.Models.Entity.Table
{
    /// <summary>
    /// 
    /// </summary>
    public class yoshop_wxapp_navbar
    {
        /// <summary>
        /// 
        /// </summary>
        public yoshop_wxapp_navbar()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 wxapp_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String wxapp_title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Byte top_text_color { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String top_background_color { get; set; }

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