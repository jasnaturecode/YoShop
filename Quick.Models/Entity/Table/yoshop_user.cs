using SqlSugar;

namespace Quick.Models.Entity.Table
{
    /// <summary>
    /// 
    /// </summary>
    public class yoshop_user
    {
        /// <summary>
        /// 
        /// </summary>
        public yoshop_user()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 user_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String open_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String nickName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String avatarUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Byte gender { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String country { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String province { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String city { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 address_id { get; set; }

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