using SqlSugar;

namespace Quick.Models.Entity.Table
{
    /// <summary>
    /// 
    /// </summary>
    public class yoshop_goods_spec_rel
    {
        /// <summary>
        /// 
        /// </summary>
        public yoshop_goods_spec_rel()
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
        public System.UInt32 spec_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 spec_value_id { get; set; }

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