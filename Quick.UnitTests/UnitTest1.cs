using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;

namespace Quick.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DateTimeTest()
        {
            var now = DateTime.Now;
            var dt = now.ToFileTime();
            var dtu = now.ToFileTimeUtc();
            Console.WriteLine(dt);
            Console.WriteLine(dtu);
            Trace.WriteLine($"{now:yyyyMMddHHmmss}");
            Assert.IsTrue(dt == dtu);
        }

        [TestMethod]
        public void JObjectTest()
        {
            var json = "{\"order\":{\"close_days\":\"0\",\"receive_days\":\"15\",\"refund_days\":\"0\"},\"freight_rule\":\"10\"}";
            JObject root = JObject.Parse(json);
            var close_days = root["order"]["close_days"].Value<string>();
            var freight_rule = root["freight_rule"].Value<int>();

            Assert.IsTrue(close_days == "0");
            Assert.IsTrue(freight_rule == 0);
        }
    }
}
