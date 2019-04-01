using SqlSugar;

namespace Quick.Models.Entity.Table
{
    /// <summary>
    /// 
    /// </summary>
    public class yoshop_delivery
    {
        /// <summary>
        /// 
        /// </summary>
        public yoshop_delivery()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 delivery_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Byte method { get; set; }

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