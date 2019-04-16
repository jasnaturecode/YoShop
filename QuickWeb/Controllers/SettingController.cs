using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Masuit.Tools.Logging;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Quick.Common;
using Quick.Common.Helpers;
using Quick.IService;
using Quick.Models.Dto;
using Quick.Models.Entity.Table;
using QuickWeb.Controllers.Common;
using QuickWeb.Extensions;
using QuickWeb.Extensions.Common;
using QuickWeb.Models.ViewModel;

namespace QuickWeb.Controllers
{
    /// <summary>
    /// 系统设置
    /// </summary>
    public class SettingController : AdminBaseController
    {
        /// <summary>
        /// yoshop_setting对象业务方法
        /// </summary>
        public Iyoshop_settingService SettingService { get; set; }

        /// <summary>
        /// yoshop_delivery对象业务方法
        /// </summary>
        public Iyoshop_deliveryService DeliveryService { get; set; }

        /// <summary>
        /// yoshop_delivery_rule对象业务方法
        /// </summary>
        public Iyoshop_delivery_ruleService DeliveryRuleService { get; set; }

        /// <summary>
        /// yoshop_region对象业务方法
        /// </summary>
        public Iyoshop_regionService RegionService { get; set; }


        #region 商城设置

        /// <summary>
        /// 商城设置
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/setting/store")]
        public IActionResult Store()
        {
            var setting = SettingService.GetFirstEntity(l => l.wxapp_id == GetAdminSession().wxapp_id && l.key == QuickKeys.StoreSetting);
            JObject model = JObject.Parse(setting.values);
            return View(model);
        }

        /// <summary>
        /// 保存商城设置
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("/setting/store"), ValidateAntiForgeryToken]
        public IActionResult Store(string name, string is_notice, string notice)
        {
            var setting = SettingService.GetFirstEntity(l => l.wxapp_id == GetAdminSession().wxapp_id && l.key == QuickKeys.StoreSetting);

            JObject model = JObject.Parse(setting.values);
            model["name"] = name;
            model["is_notice"] = is_notice;
            model["notice"] = notice;

            try
            {
                var result = model.ObjectToJson();
                var time = DateTimeExtensions.GetCurrentTimeStamp();
                SettingService.Update(x => new yoshop_setting() { values = result, update_time = time }, l => l.key == setting.key && l.wxapp_id == setting.wxapp_id);

                //初始化系统设置参数
                CommonHelper.SystemSettings = SettingService.LoadEntities(l => l.wxapp_id == setting.wxapp_id).ToList().ToDictionary(s => s.key, s => JObject.Parse(s.values));
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }

            return Yes("更新成功");
        }

        #endregion

        #region 交易设置
        /// <summary>
        /// 交易设置
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/setting/trade")]
        public IActionResult Trade()
        {
            var setting = SettingService.GetFirstEntity(l => l.wxapp_id == GetAdminSession().wxapp_id && l.key == QuickKeys.TradeSetting);
            JObject model = JObject.Parse(setting.values);
            return View(model);
        }

        /// <summary>
        /// 保存交易设置
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("/setting/trade"), ValidateAntiForgeryToken]
        public IActionResult Trade(string close_days, string receive_days, string refund_days, string freight_rule)
        {
            var setting = SettingService.GetFirstEntity(l => l.wxapp_id == GetAdminSession().wxapp_id && l.key == QuickKeys.TradeSetting);

            JObject model = JObject.Parse(setting.values);
            model["order"]["close_days"] = close_days;
            model["order"]["receive_days"] = receive_days;
            model["order"]["refund_days"] = refund_days;
            model["freight_rule"] = freight_rule;

            try
            {
                var result = model.ObjectToJson();
                var time = DateTimeExtensions.GetCurrentTimeStamp();
                SettingService.Update(x => new yoshop_setting() { values = result, update_time = time }, l => l.key == setting.key && l.wxapp_id == setting.wxapp_id);

                //初始化系统设置参数
                CommonHelper.SystemSettings = SettingService.LoadEntities(l => l.wxapp_id == setting.wxapp_id).ToList().ToDictionary(s => s.key, s => JObject.Parse(s.values));
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }

            return Yes("更新成功");
        }
        #endregion

        #region 配送设置

        /// <summary>
        /// 运费模板列表
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/setting.delivery/index")]
        public IActionResult DeliveryIndex(int? page, int? size)
        {
            int total = 0;
            var list = DeliveryService.LoadPageEntities<uint>(page ?? 1, size ?? 15, ref total, l => true, x => x.sort, true).Mapper<IEnumerable<DeliveryDto>>();
            return View(new DeliveryListViewModel(list, total));
        }

        /// <summary>
        /// 添加运费模板页面
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/setting.delivery/add")]
        public IActionResult DeliveryAdd()
        {
            ViewData["regions"] = GetRegionTreeData().ObjectToJson();
            return View(new yoshop_delivery());
        }

        /// <summary>
        /// 保存运费模板
        /// </summary>
        /// <param name="model"></param>
        /// <param name="additionals"></param>
        /// <param name="additional_fees"></param>
        /// <param name="firsts"></param>
        /// <param name="first_fees"></param>
        /// <param name="regions"></param>
        /// <returns></returns>
        [HttpPost, Route("/setting.delivery/add"), ValidateAntiForgeryToken]
        public IActionResult DeliveryAdd(yoshop_delivery model, List<string> regions, List<double> firsts, List<decimal> first_fees, List<double> additionals, List<decimal> additional_fees)
        {
            var dt = DateTime.Now.ConvertToTimeStamp();
            var wxapp_id = GetAdminSession().wxapp_id;

            model.create_time = dt;
            model.update_time = dt;
            model.wxapp_id = wxapp_id;

            try
            {
                //LogManager.Info(regions.ObjectToJson());
                uint delivery_id = (uint)DeliveryService.AddEntityReturnIdentity(model);
                if (delivery_id > 0)
                {
                    if (regions != null && regions.Any())
                    {
                        List<yoshop_delivery_rule> rules = new List<yoshop_delivery_rule>();

                        for (int i = 0; i < regions.Count; i++)
                        {
                            var _ = new yoshop_delivery_rule
                            {
                                region = regions[i],
                                first = firsts[i],
                                first_fee = first_fees[i],
                                additional = additionals[i],
                                additional_fee = additional_fees[i],
                                delivery_id = delivery_id,
                                wxapp_id = wxapp_id,
                                create_time = dt
                            };
                            rules.Add(_);
                        }
                        DeliveryRuleService.AddEntities(rules);
                    }
                    else
                    {
                        return No("请选择可配送区域");
                    }
                }
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }

            return YesRedirect("添加成功", "/setting.delivery/index");
        }

        /// <summary>
        /// 编辑运费模板页面
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/setting.delivery/edit/delivery_id/{id}")]
        public IActionResult DeliveryEdit(uint id)
        {
            var model = DeliveryService.GetFirstEntity(l => l.delivery_id == id);
            ViewData["regions"] = GetRegionTreeData().ObjectToJson();
            var list = DeliveryRuleService.LoadEntities(l => l.delivery_id == id)?.ToList();
            if (list != null && list.Any())
            {
                var rules = list.Mapper<List<DeliveryRuleRegionViewModel>>();
                foreach (var item in rules)
                    item.region_content = GetRegionContent(item.region);

                ViewData["rules"] = rules;
            }
            return View(model);
        }

        /// <summary>
        /// 保存运费模板
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="additionals"></param>
        /// <param name="additional_fees"></param>
        /// <param name="firsts"></param>
        /// <param name="first_fees"></param>
        /// <param name="regions"></param>
        /// <returns></returns>
        [HttpPost, Route("/setting.delivery/edit/delivery_id/{id}"), ValidateAntiForgeryToken]
        public IActionResult DeliveryEdit(uint id, yoshop_delivery model, List<string> regions, List<double> firsts, List<decimal> first_fees, List<double> additionals, List<decimal> additional_fees)
        {
            var dt = DateTime.Now.ConvertToTimeStamp();

            try
            {
                var delivery = DeliveryService.GetFirstEntity(l => l.delivery_id == id);
                delivery.name = model.name;
                delivery.method = model.method;
                delivery.sort = model.sort;
                delivery.update_time = dt;

                if (regions != null && regions.Any())
                {
                    List<yoshop_delivery_rule> rules = new List<yoshop_delivery_rule>();

                    for (int i = 0; i < regions.Count; i++)
                    {
                        var _ = new yoshop_delivery_rule
                        {
                            region = regions[i],
                            first = firsts[i],
                            first_fee = first_fees[i],
                            additional = additionals[i],
                            additional_fee = additional_fees[i],
                            delivery_id = delivery.delivery_id,
                            wxapp_id = delivery.wxapp_id,
                            create_time = dt
                        };
                        rules.Add(_);
                    }

                    if (DeliveryRuleService.Delete(l => l.delivery_id == delivery.delivery_id))
                        DeliveryRuleService.AddEntities(rules);
                }
                else
                {
                    return No("请选择可配送区域");
                }
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }

            return YesRedirect("编辑成功", "/setting.delivery/index");
        }

        /// <summary>
        /// 保存运费模板
        /// </summary>
        /// <param name="delivery_id"></param>
        /// <returns></returns>
        [HttpPost, Route("/setting.delivery/delete"), ValidateAntiForgeryToken]
        public IActionResult DeliveryDelete(uint delivery_id)
        {
            try
            {
                DeliveryService.Delete(l => l.delivery_id == delivery_id);
                DeliveryRuleService.Delete(l => l.delivery_id == delivery_id);
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return Yes("保存成功");
        }

        /// <summary>
        /// 得到地区数据
        /// </summary>
        /// <returns></returns>
        private JObject GetRegionTreeData()
        {
            var regions = RegionService.GetAll().Select<RegionDto>().ToList();

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
                                if (region.level == 3 && region.pid == city.id)  // 地区
                                    third[region.id.ToString()] = JObject.FromObject(region);
                            }

                            second[city.id.ToString()]["region"] = third;
                        }
                    }

                    root[province.id.ToString()]["city"] = second;
                }
            }

            return root;
        }

        /// <summary>
        /// 得到地区详情
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        private string GetRegionContent(string region)
        {
            var ids = region.Split(',').Select(item => item.ToUInt32());

            var sb = new StringBuilder();

            if (ids != null && ids.Any())
            {
                var kv = new Dictionary<string, IEnumerable<string>>();

                foreach (var id in ids)
                {
                    var model = RegionService.GetFirstEntity(l => l.id == id);
                    var key = model.merger_name.Split(',')[model.level.ToInt32() - 1];
                    if (kv.ContainsKey(key))
                        kv[key] = kv[key].Append(model.shortname);
                    else
                        kv.Add(key, new List<string> { model.shortname });
                }
                var result = kv.ToDictionary(x => x.Key, x => string.Join('、', x.Value));

                foreach (var item in result)
                {
                    sb.Append($"{item.Key}(<span class=\"am-link-muted\">{item.Value}</span>)");
                }
            }

            return sb.ToString();
        }

        #endregion

        #region 短信设置
        /// <summary>
        /// 短信设置
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/setting/sms")]
        public IActionResult Sms()
        {
            var setting = SettingService.GetFirstEntity(l => l.wxapp_id == GetAdminSession().wxapp_id && l.key == QuickKeys.SmsSetting);
            JObject model = JObject.Parse(setting.values);
            return View(model);
        }

        /// <summary>
        /// 短信设置
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("/setting/sms"), ValidateAntiForgeryToken]
        public IActionResult Sms(string sms_default, string sms_aliyun_AccessKeyId, string sms_aliyun_AccessKeySecret, string sms_aliyun_sign, string sms_aliyun_order_pay_is_enable)
        {
            var setting = SettingService.GetFirstEntity(l => l.wxapp_id == GetAdminSession().wxapp_id && l.key == QuickKeys.SmsSetting);
            JObject model = JObject.Parse(setting.values);

            if ("aliyun".Equals(sms_default))
            {
                model["engine"]["aliyun"]["AccessKeyId"] = sms_aliyun_AccessKeyId;
                model["engine"]["aliyun"]["AccessKeySecret"] = sms_aliyun_AccessKeySecret;
                model["engine"]["aliyun"]["sign"] = sms_aliyun_sign;
                model["engine"]["aliyun"]["order_pay"]["is_enable"] = sms_aliyun_order_pay_is_enable;
            }

            try
            {
                var result = model.ObjectToJson();
                var time = DateTimeExtensions.GetCurrentTimeStamp();
                SettingService.Update(x => new yoshop_setting() { values = result, update_time = time }, l => l.key == setting.key && l.wxapp_id == setting.wxapp_id);

                //初始化系统设置参数
                CommonHelper.SystemSettings = SettingService.LoadEntities(l => l.wxapp_id == setting.wxapp_id).ToList().ToDictionary(s => s.key, s => JObject.Parse(s.values));
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }

            return Yes("更新成功");
        }

        /// <summary>
        /// 短信测试
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("/setting/smstest"), ValidateAntiForgeryToken]
        public IActionResult SmsTest(string AccessKeyId, string AccessKeySecret, string sign, string msg_type, string template_code, string accept_phone)
        {
            if (string.IsNullOrEmpty(AccessKeyId))
                return No("请填写 AccessKeyId");
            if (string.IsNullOrEmpty(AccessKeySecret))
                return No("请填写 AccessKeySecret");
            if (string.IsNullOrEmpty(sign))
                return No("请填写 短信签名");
            if (string.IsNullOrEmpty(template_code))
                return No("请填写 请填写 模板ID");
            if (string.IsNullOrEmpty(accept_phone))
                return No("请填写 接收手机号");
            return Yes("发送成功");
        }

        #endregion

        #region 上传设置
        /// <summary>
        /// 上传设置
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/setting/storage")]
        public IActionResult Storage()
        {
            var setting = SettingService.GetFirstEntity(l => l.wxapp_id == GetAdminSession().wxapp_id && l.key == QuickKeys.UploadSetting);
            JObject model = JObject.Parse(setting.values);
            return View(model);
        }

        /// <summary>
        /// 保存上传设置
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("/setting/storage")]
        public IActionResult Storage(string storage_default, string qiniu_bucket, string qiniu_access_key, string qiniu_secret_key, string qiniu_domain)
        {
            var setting = SettingService.GetFirstEntity(l => l.wxapp_id == GetAdminSession().wxapp_id && l.key == QuickKeys.UploadSetting);
            JObject model = JObject.Parse(setting.values);

            if ("qiniu".Equals(storage_default))
            {
                model["default"] = "qiniu";
                model["engine"]["qiniu"]["bucket"] = qiniu_bucket;
                model["engine"]["qiniu"]["access_key"] = qiniu_access_key;
                model["engine"]["qiniu"]["secret_key"] = qiniu_secret_key;
                model["engine"]["qiniu"]["domain"] = qiniu_domain;
            }
            else
            {
                model["default"] = "local";
            }

            try
            {
                var result = model.ObjectToJson();
                var time = DateTimeExtensions.GetCurrentTimeStamp();
                SettingService.Update(x => new yoshop_setting() { values = result, update_time = time }, l => l.key == setting.key && l.wxapp_id == setting.wxapp_id);

                //初始化系统设置参数
                CommonHelper.SystemSettings = SettingService.LoadEntities(l => l.wxapp_id == setting.wxapp_id).ToList().ToDictionary(s => s.key, s => JObject.Parse(s.values));
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }

            return Yes("更新成功");
        }
        #endregion

        #region 其他设置

        /// <summary>
        /// 清理缓存
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/setting.cache/clear")]
        public IActionResult CacheClear()
        {
            return View();
        }

        /// <summary>
        /// 环境检测
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/setting.science/index")]
        public IActionResult ScienceIndex()
        {
            return View();
        }

        #endregion
    }
}