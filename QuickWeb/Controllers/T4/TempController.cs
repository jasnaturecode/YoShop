 
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
		private readonly Iyoshop_categoryService CategoryService;	

		private readonly Iyoshop_deliveryService DeliveryService;	

		private readonly Iyoshop_delivery_ruleService DeliveryruleService;	

		private readonly Iyoshop_dictionaryService DictionaryService;	

		private readonly Iyoshop_goodsService GoodsService;	

		private readonly Iyoshop_goods_imageService GoodsimageService;	

		private readonly Iyoshop_goods_specService GoodsspecService;	

		private readonly Iyoshop_goods_spec_relService GoodsspecrelService;	

		private readonly Iyoshop_orderService OrderService;	

		private readonly Iyoshop_order_addressService OrderaddressService;	

		private readonly Iyoshop_order_goodsService OrdergoodsService;	

		private readonly Iyoshop_regionService RegionService;	

		private readonly Iyoshop_settingService SettingService;	

		private readonly Iyoshop_specService SpecService;	

		private readonly Iyoshop_spec_valueService SpecvalueService;	

		private readonly Iyoshop_store_userService StoreuserService;	

		private readonly Iyoshop_upload_fileService UploadfileService;	

		private readonly Iyoshop_upload_file_usedService UploadfileusedService;	

		private readonly Iyoshop_upload_groupService UploadgroupService;	

		private readonly Iyoshop_userService UserService;	

		private readonly Iyoshop_user_addressService UseraddressService;	

		private readonly Iyoshop_wxappService WxappService;	

		private readonly Iyoshop_wxapp_helpService WxapphelpService;	

		private readonly Iyoshop_wxapp_navbarService WxappnavbarService;	

		private readonly Iyoshop_wxapp_pageService WxapppageService;	

	}
}