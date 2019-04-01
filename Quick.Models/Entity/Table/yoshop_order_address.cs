using SqlSugar;

namespace Quick.Models.Entity.Table
{
    /// <summary>
    /// 
    /// </summary>
    public class yoshop_order_address
    {
        /// <summary>
        /// 
        /// </summary>
        public yoshop_order_address()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 order_address_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 province_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 city_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 region_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String detail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 order_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 user_id { get; set; }

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