using SqlSugar;

namespace Quick.Models.Entity.Table
{
    /// <summary>
    /// 
    /// </summary>
    public class yoshop_order_goods
    {
        /// <summary>
        /// 
        /// </summary>
        public yoshop_order_goods()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 order_goods_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 goods_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String goods_name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 image_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Byte deduct_stock_type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Byte spec_type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String spec_sku_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 goods_spec_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String goods_attr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String goods_no { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal goods_price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal line_price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Double goods_weight { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 total_num { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal total_price { get; set; }

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