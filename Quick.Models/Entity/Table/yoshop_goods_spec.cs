using SqlSugar;

namespace Quick.Models.Entity.Table
{
    /// <summary>
    /// 
    /// </summary>
    public class yoshop_goods_spec
    {
        /// <summary>
        /// 
        /// </summary>
        public yoshop_goods_spec()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 goods_spec_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 goods_id { get; set; }

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
        public System.UInt32 stock_num { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 goods_sales { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Double goods_weight { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 wxapp_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String spec_sku_id { get; set; }

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