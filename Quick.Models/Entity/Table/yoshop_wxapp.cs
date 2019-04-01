using SqlSugar;

namespace Quick.Models.Entity.Table
{
    /// <summary>
    /// 
    /// </summary>
    public class yoshop_wxapp
    {
        /// <summary>
        /// 
        /// </summary>
        public yoshop_wxapp()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 wxapp_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String app_name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String app_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String app_secret { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Byte is_service { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 service_image_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Byte is_phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String phone_no { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.UInt32 phone_image_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String mchid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String apikey { get; set; }

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