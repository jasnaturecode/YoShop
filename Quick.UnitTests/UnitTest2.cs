using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Quick.UnitTests
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void JObjectCreateTest()
        {
            JObject root = new JObject();
            root["id"] = "1";
            root["name"] = "root";

            JObject son = new JObject();
            son["id"] = "1";
            son["name"] = "son";

            root["son"] = son;

            Console.WriteLine(root.ToString());

        }

        public static string json = "[{\"id\":1,\"pid\":0,\"name\":\"北京市\",\"level\":1},{\"id\":2,\"pid\":1,\"name\":\"北京市\",\"level\":2},{\"id\":3,\"pid\":2,\"name\":\"东城区\",\"level\":3},{\"id\":4,\"pid\":2,\"name\":\"西城区\",\"level\":3},{\"id\":5,\"pid\":2,\"name\":\"朝阳区\",\"level\":3},{\"id\":6,\"pid\":2,\"name\":\"丰台区\",\"level\":3},{\"id\":7,\"pid\":2,\"name\":\"石景山区\",\"level\":3},{\"id\":8,\"pid\":2,\"name\":\"海淀区\",\"level\":3},{\"id\":9,\"pid\":2,\"name\":\"门头沟区\",\"level\":3},{\"id\":10,\"pid\":2,\"name\":\"房山区\",\"level\":3},{\"id\":11,\"pid\":2,\"name\":\"通州区\",\"level\":3},{\"id\":12,\"pid\":2,\"name\":\"顺义区\",\"level\":3},{\"id\":13,\"pid\":2,\"name\":\"昌平区\",\"level\":3},{\"id\":14,\"pid\":2,\"name\":\"大兴区\",\"level\":3},{\"id\":15,\"pid\":2,\"name\":\"怀柔区\",\"level\":3},{\"id\":16,\"pid\":2,\"name\":\"平谷区\",\"level\":3},{\"id\":17,\"pid\":2,\"name\":\"密云县\",\"level\":3},{\"id\":18,\"pid\":2,\"name\":\"延庆县\",\"level\":3},{\"id\":19,\"pid\":0,\"name\":\"天津市\",\"level\":1},{\"id\":20,\"pid\":19,\"name\":\"天津市\",\"level\":2},{\"id\":21,\"pid\":20,\"name\":\"和平区\",\"level\":3},{\"id\":22,\"pid\":20,\"name\":\"河东区\",\"level\":3},{\"id\":23,\"pid\":20,\"name\":\"河西区\",\"level\":3},{\"id\":24,\"pid\":20,\"name\":\"南开区\",\"level\":3},{\"id\":25,\"pid\":20,\"name\":\"河北区\",\"level\":3},{\"id\":26,\"pid\":20,\"name\":\"红桥区\",\"level\":3},{\"id\":27,\"pid\":20,\"name\":\"东丽区\",\"level\":3},{\"id\":28,\"pid\":20,\"name\":\"西青区\",\"level\":3},{\"id\":29,\"pid\":20,\"name\":\"津南区\",\"level\":3},{\"id\":30,\"pid\":20,\"name\":\"北辰区\",\"level\":3},{\"id\":31,\"pid\":20,\"name\":\"武清区\",\"level\":3},{\"id\":32,\"pid\":20,\"name\":\"宝坻区\",\"level\":3},{\"id\":33,\"pid\":20,\"name\":\"滨海新区\",\"level\":3},{\"id\":34,\"pid\":20,\"name\":\"宁河县\",\"level\":3},{\"id\":35,\"pid\":20,\"name\":\"静海县\",\"level\":3},{\"id\":36,\"pid\":20,\"name\":\"蓟县\",\"level\":3}]";

        [TestMethod]
        public void JObjectTest()
        {

            List<RegionDto> regions = JsonConvert.DeserializeObject<List<RegionDto>>(json);


            JObject root = new JObject();

            foreach (var province in regions)
            {
                if (province.level == 1)  // 省份
                { 
                    root[province.id.ToString()] = JObject.FromObject(province);

                    JObject second = new JObject();

                    foreach (var city in regions)
                    {

                        if (city.level == 2 && city.pid == province.id) // 城市
                        { 
                            second[city.id.ToString()] = JObject.FromObject(city);

                            JObject third = new JObject();

                            foreach (var region in regions)
                            {
                                if(region.level == 3 && region.pid == city.id)  // 地区
                                    third[region.id.ToString()] = JObject.FromObject(region); 
                            }

                            second[city.id.ToString()]["region"] = third;
                        }
                    }

                    root[province.id.ToString()]["city"] = second;
                }




            }

            Console.WriteLine(root.ToString());
        }


    }


    public class RegionDto
    {
        public System.Int32 id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? pid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Byte? level { get; set; }
    }
}
