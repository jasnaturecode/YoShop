using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace QuickWeb.Extensions.UEditor
{
    /// <summary>
    /// Config 的摘要说明
    /// </summary>
    public static class UeditorConfig
    {
        private static bool noCache = true;
        private static JObject BuildItems()
        {
            var json = File.ReadAllText(Path.Combine(AppContext.BaseDirectory,"App_Data","ueconfig.json"));
            return JObject.Parse(json);
        }

        /// <summary>
        /// 
        /// </summary>
        public static JObject Items
        {
            get
            {
                if (noCache || _items == null)
                {
                    _items = BuildItems();
                }
                return _items;
            }
        }

        private static JObject _items;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetValue<T>(string key)
        {
            return Items[key].Value<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static String[] GetStringList(string key)
        {
            return Items[key].Select(x => x.Value<String>()).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static String GetString(string key)
        {
            return GetValue<String>(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetInt(string key)
        {
            return GetValue<int>(key);
        }
    }
}