 
/* ==============================================================================
* 命名空间：ShopDrugs.Controllers
* 类 名 称：TempController
* 创 建 者：Qing
* 创建时间：2018-05-28 15:54:52
* CLR 版本：4.0.30319.42000
* 保存的文件名：TempController
* 文件版本：V1.0.0.0
*
* 功能描述：N/A 
*
* 修改历史：
*
*
* ==============================================================================
*         CopyRight @ 班纳工作室 2018. All rights reserved
* ==============================================================================*/

using Microsoft.AspNetCore.Mvc;
using Quick.IService;

namespace QuickWeb.Controllers
{
    /// <summary>
    /// 不使用构造函数注入的属性，必须是共有成员属性(public)，不能是私有或受保护的成员属性
    /// </summary>
    public class TempController : Controller
    {
        /// <summary>
        /// yoshop_category对象业务方法
        /// </summary>
		public Iyoshop_categoryService CategoryService { get; set; }	

        /// <summary>
        /// yoshop_delivery对象业务方法
        /// </summary>
		public Iyoshop_deliveryService DeliveryService { get; set; }	

        /// <summary>
        /// yoshop_delivery_rule对象业务方法
        /// </summary>
		public Iyoshop_delivery_ruleService DeliveryRuleService { get; set; }	

        /// <summary>
        /// yoshop_dictionary对象业务方法
        /// </summary>
		public Iyoshop_dictionaryService DictionaryService { get; set; }	

        /// <summary>
        /// yoshop_goods对象业务方法
        /// </summary>
		public Iyoshop_goodsService GoodsService { get; set; }	

        /// <summary>
        /// yoshop_goods_image对象业务方法
        /// </summary>
		public Iyoshop_goods_imageService GoodsImageService { get; set; }	

        /// <summary>
        /// yoshop_goods_spec对象业务方法
        /// </summary>
		public Iyoshop_goods_specService GoodsSpecService { get; set; }	

        /// <summary>
        /// yoshop_goods_spec_rel对象业务方法
        /// </summary>
		public Iyoshop_goods_spec_relService GoodsSpecRelService { get; set; }	

        /// <summary>
        /// yoshop_order对象业务方法
        /// </summary>
		public Iyoshop_orderService OrderService { get; set; }	

        /// <summary>
        /// yoshop_order_address对象业务方法
        /// </summary>
		public Iyoshop_order_addressService OrderAddressService { get; set; }	

        /// <summary>
        /// yoshop_order_goods对象业务方法
        /// </summary>
		public Iyoshop_order_goodsService OrderGoodsService { get; set; }	

        /// <summary>
        /// yoshop_region对象业务方法
        /// </summary>
		public Iyoshop_regionService RegionService { get; set; }	

        /// <summary>
        /// yoshop_setting对象业务方法
        /// </summary>
		public Iyoshop_settingService SettingService { get; set; }	

        /// <summary>
        /// yoshop_spec对象业务方法
        /// </summary>
		public Iyoshop_specService SpecService { get; set; }	

        /// <summary>
        /// yoshop_spec_value对象业务方法
        /// </summary>
		public Iyoshop_spec_valueService SpecValueService { get; set; }	

        /// <summary>
        /// yoshop_store_user对象业务方法
        /// </summary>
		public Iyoshop_store_userService StoreUserService { get; set; }	

        /// <summary>
        /// yoshop_upload_file对象业务方法
        /// </summary>
		public Iyoshop_upload_fileService UploadFileService { get; set; }	

        /// <summary>
        /// yoshop_upload_file_used对象业务方法
        /// </summary>
		public Iyoshop_upload_file_usedService UploadFileUsedService { get; set; }	

        /// <summary>
        /// yoshop_upload_group对象业务方法
        /// </summary>
		public Iyoshop_upload_groupService UploadGroupService { get; set; }	

        /// <summary>
        /// yoshop_user对象业务方法
        /// </summary>
		public Iyoshop_userService UserService { get; set; }	

        /// <summary>
        /// yoshop_user_address对象业务方法
        /// </summary>
		public Iyoshop_user_addressService UserAddressService { get; set; }	

        /// <summary>
        /// yoshop_wxapp对象业务方法
        /// </summary>
		public Iyoshop_wxappService WxappService { get; set; }	

        /// <summary>
        /// yoshop_wxapp_help对象业务方法
        /// </summary>
		public Iyoshop_wxapp_helpService WxappHelpService { get; set; }	

        /// <summary>
        /// yoshop_wxapp_navbar对象业务方法
        /// </summary>
		public Iyoshop_wxapp_navbarService WxappNavbarService { get; set; }	

        /// <summary>
        /// yoshop_wxapp_page对象业务方法
        /// </summary>
		public Iyoshop_wxapp_pageService WxappPageService { get; set; }	

	}
}