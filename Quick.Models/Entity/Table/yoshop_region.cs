using SqlSugar;

namespace Quick.Models.Entity.Table
{
    /// <summary>
    /// 
    /// </summary>
    public class yoshop_region
    {
        /// <summary>
        /// 
        /// </summary>
        public yoshop_region()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? pid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String shortname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String merger_name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Byte? level { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String pinyin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String zip_code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String first { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String lng { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String lat { get; set; }
    }
}