using SqlSugar;

namespace Quick.Models.Entity.Table
{
    /// <summary>
    /// 
    /// </summary>
    public class yoshop_delivery_rule
    {
        /// <summary>
        /// 
        /// </summary>
        public yoshop_delivery_rule()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 rule_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 delivery_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String region { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Double first { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal first_fee { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Double additional { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal additional_fee { get; set; }

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