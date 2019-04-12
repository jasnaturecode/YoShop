using System.Collections.Generic;
using Newtonsoft.Json;

namespace QuickWeb.Models.RequestModel
{
    /// <summary>
    /// 
    /// </summary>
    public class FileRequest
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("action")]
        public string Action { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("item")]
        public string Item { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("newItemPath")]
        public string NewItemPath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("items")]
        public List<string> Items { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("newPath")]
        public string NewPath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("singleFilename")]
        public string SingleFilename { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("perms")]
        public string Perms { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("permsCode")]
        public string PermsCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("recursive")]
        public bool Recursive { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("destination")]
        public string Destination { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("compressedFilename")]
        public string CompressedFilename { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("folderName")]
        public string FolderName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("toFilename")]
        public string ToFilename { get; set; }
    }

}