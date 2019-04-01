using SqlSugar;

namespace Quick.Models.Entity.Table
{
    /// <summary>
    /// 
    /// </summary>
    public class yoshop_goods
    {
        /// <summary>
        /// 
        /// </summary>
        public yoshop_goods()
        {
        }

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
        public System.UInt32 category_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Byte spec_type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Byte deduct_stock_type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 sales_initial { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 sales_actual { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 goods_sort { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 delivery_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Byte goods_status { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 update_time { get; set; }
    }
}