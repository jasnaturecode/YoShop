using System;
using System.Net.Http;
using System.Threading.Tasks;
using Masuit.Tools;
using Masuit.Tools.Core.Net;
using Masuit.Tools.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using QuickWeb.Extensions;

namespace QuickWeb.Areas.Api.Controllers
{
    /// <summary>
    /// 公共API
    /// </summary>
    [Route("api")]
    [ApiController]
    public class ToolController : ControllerBase
    {
        /// <summary>
        /// 根据经纬度获取详细地理信息
        /// </summary>
        /// <param name="lat">纬度</param>
        /// <param name="lng">经度</param>
        /// <returns></returns>
        [HttpPost("tools/position"), ResponseCache(Duration = 600, VaryByQueryKeys = new[] { "lat", "lng" }, VaryByHeader = HeaderNames.Cookie)]
        public async Task<PhysicsAddress> Position(string lat, string lng)
        {
            if (string.IsNullOrEmpty(lat) || string.IsNullOrEmpty(lng))
            {
                var ip = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
#if DEBUG
                Random r = new Random();
                ip = $"{r.StrictNext(210)}.{r.StrictNext(255)}.{r.StrictNext(255)}.{r.StrictNext(255)}";
#endif
                PhysicsAddress address = await ip.GetPhysicsAddressInfo();
                return address;
            }
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri("http://api.map.baidu.com")
            };
            string s = client.GetStringAsync($"/geocoder/v2/?location={lat},{lng}&output=json&pois=1&ak={AppConfig.BaiduAk}").Result;
            PhysicsAddress physicsAddress = JsonConvert.DeserializeObject<PhysicsAddress>(s);
            return physicsAddress;
        }

        /// <summary>
        /// 根据详细地址获取经纬度
        /// </summary>
        /// <param name="addr">详细地理信息</param>
        /// <returns></returns>
        [HttpPost("tools/address"), ResponseCache(Duration = 600, VaryByQueryKeys = new[] { "addr" }, VaryByHeader = HeaderNames.Cookie)]
        public async Task<Location> Address(string addr)
        {
            if (string.IsNullOrEmpty(addr))
            {
                var ip = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
#if DEBUG
                Random r = new Random();
                ip = $"{r.StrictNext(210)}.{r.StrictNext(255)}.{r.StrictNext(255)}.{r.StrictNext(255)}";
#endif
                PhysicsAddress address = await ip.GetPhysicsAddressInfo();
                if (address.Status == 0)
                {
                    return address.AddressResult.Location;
                }
            }
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri("http://api.map.baidu.com")
            };
            string s = client.GetStringAsync($"/geocoder/v2/?output=json&address={addr}&ak={AppConfig.BaiduAk}").Result;
            var physicsAddress = JsonConvert.DeserializeAnonymousType(s, new
            {
                status = 0,
                result = new
                {
                    location = new Location()
                }
            });
            return physicsAddress.result.location;
        }

        /// <summary>
        /// 获取ip地址详细地理信息
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        [HttpPost("tools/ipinfo"), ResponseCache(Duration = 600, VaryByQueryKeys = new[] { "ip" }, VaryByHeader = HeaderNames.Cookie)]
        public async Task<PhysicsAddress> GetIpInfo(string ip)
        {
            if (string.IsNullOrEmpty(ip))
            {
                ip = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
            PhysicsAddress address = await ip.GetPhysicsAddressInfo();
            return address;
        }
    }
}