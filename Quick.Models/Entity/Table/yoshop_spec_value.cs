using SqlSugar;

namespace Quick.Models.Entity.Table
{
    /// <summary>
    /// 
    /// </summary>
    public class yoshop_spec_value
    {
        /// <summary>
        /// 
        /// </summary>
        public yoshop_spec_value()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 spec_value_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String spec_value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 spec_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 wxapp_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 create_time { get; set; }
    }
}