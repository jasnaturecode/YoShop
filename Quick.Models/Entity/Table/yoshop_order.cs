using SqlSugar;

namespace Quick.Models.Entity.Table
{
    /// <summary>
    /// 
    /// </summary>
    public class yoshop_order
    {
        /// <summary>
        /// 
        /// </summary>
        public yoshop_order()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 order_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String order_no { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal total_price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal pay_price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Byte pay_status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 pay_time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal express_price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String express_company { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String express_no { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Byte delivery_status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 delivery_time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Byte receipt_status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 receipt_time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Byte order_status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String transaction_id { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 update_time { get; set; }
    }
}