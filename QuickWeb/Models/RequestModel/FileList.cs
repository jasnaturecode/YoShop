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
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("rights")]
        public string rights { get; set; }
        [JsonProperty("size")]
        public long size { get; set; }
        [JsonProperty("date")]
        public string date { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
    }
}
