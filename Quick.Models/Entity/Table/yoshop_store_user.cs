using SqlSugar;

namespace Quick.Models.Entity.Table
{
    /// <summary>
    /// 
    /// </summary>
    public class yoshop_store_user
    {
        /// <summary>
        /// 
        /// </summary>
        public yoshop_store_user()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 store_user_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String user_name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String password { get; set; }

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
        public System.Int32 update_time { get; set; }
    }
}