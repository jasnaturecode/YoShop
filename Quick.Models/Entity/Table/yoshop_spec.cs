using SqlSugar;

namespace Quick.Models.Entity.Table
{
    /// <summary>
    /// 
    /// </summary>
    public class yoshop_spec
    {
        /// <summary>
        /// 
        /// </summary>
        public yoshop_spec()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 spec_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String spec_name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 wxapp_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 create_time { get; set; }
    }
}