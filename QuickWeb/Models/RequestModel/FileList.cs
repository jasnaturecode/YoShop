using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QuickWeb.Models.RequestModel
{
    /// <summary>
    /// 
    /// </summary>
    public class FileList
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("name")]
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("rights")]
        public string rights { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("size")]
        public long size { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("date")]
        public string date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("type")]
        public string type { get; set; }
    }
}
