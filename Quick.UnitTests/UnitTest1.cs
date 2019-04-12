using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace Quick.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var now = DateTime.Now;
            var dt = now.ToFileTime();
            var dtu = now.ToFileTimeUtc();
            Console.WriteLine(dt);
            Console.WriteLine(dtu);
            Trace.WriteLine($"{now:yyyyMMddHHmmss}");
            Assert.IsTrue(dt == dtu);
        }
    }
}
