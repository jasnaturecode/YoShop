 
/* ==============================================================================
* 命名空间：Quick.Models.Application 
* 类 名 称：DbContext
* 创 建 者：Qing
* 创建时间：2019-02-25 19:43:19
* CLR 版本：4.0.30319.42000
* 保存的文件名：DbContext
* 文件版本：V1.0.0.0
*
* 功能描述：N/A 
*
* 修改历史：
*
*
* ==============================================================================
*         CopyRight @ 班纳工作室 2019. All rights reserved
* ==============================================================================*/

using Quick.Models.Entity.Table;
using SqlSugar;

namespace Quick.Models.Application
{
    public partial class DbContext
    {
		/// <summary>
		/// 用来处理事务多表查询和复杂的操作
		/// </summary>
		public SqlSugarClient Db; 
		
		/// <summary>
		/// 用来处理yoshop_category表的常用操作
		/// </summary>
		public DbSet<yoshop_category> yoshop_categoryDb => new DbSet<yoshop_category>(Db); 

		/// <summary>
		/// 用来处理yoshop_delivery表的常用操作
		/// </summary>
		public DbSet<yoshop_delivery> yoshop_deliveryDb => new DbSet<yoshop_delivery>(Db); 

		/// <summary>
		/// 用来处理yoshop_delivery_rule表的常用操作
		/// </summary>
		public DbSet<yoshop_delivery_rule> yoshop_delivery_ruleDb => new DbSet<yoshop_delivery_rule>(Db); 

		/// <summary>
		/// 用来处理yoshop_dictionary表的常用操作
		/// </summary>
		public DbSet<yoshop_dictionary> yoshop_dictionaryDb => new DbSet<yoshop_dictionary>(Db); 

		/// <summary>
		/// 用来处理yoshop_goods表的常用操作
		/// </summary>
		public DbSet<yoshop_goods> yoshop_goodsDb => new DbSet<yoshop_goods>(Db); 

		/// <summary>
		/// 用来处理yoshop_goods_image表的常用操作
		/// </summary>
		public DbSet<yoshop_goods_image> yoshop_goods_imageDb => new DbSet<yoshop_goods_image>(Db); 

		/// <summary>
		/// 用来处理yoshop_goods_spec表的常用操作
		/// </summary>
		public DbSet<yoshop_goods_spec> yoshop_goods_specDb => new DbSet<yoshop_goods_spec>(Db); 

		/// <summary>
		/// 用来处理yoshop_goods_spec_rel表的常用操作
		/// </summary>
		public DbSet<yoshop_goods_spec_rel> yoshop_goods_spec_relDb => new DbSet<yoshop_goods_spec_rel>(Db); 

		/// <summary>
		/// 用来处理yoshop_order表的常用操作
		/// </summary>
		public DbSet<yoshop_order> yoshop_orderDb => new DbSet<yoshop_order>(Db); 

		/// <summary>
		/// 用来处理yoshop_order_address表的常用操作
		/// </summary>
		public DbSet<yoshop_order_address> yoshop_order_addressDb => new DbSet<yoshop_order_address>(Db); 

		/// <summary>
		/// 用来处理yoshop_order_goods表的常用操作
		/// </summary>
		public DbSet<yoshop_order_goods> yoshop_order_goodsDb => new DbSet<yoshop_order_goods>(Db); 

		/// <summary>
		/// 用来处理yoshop_region表的常用操作
		/// </summary>
		public DbSet<yoshop_region> yoshop_regionDb => new DbSet<yoshop_region>(Db); 

		/// <summary>
		/// 用来处理yoshop_setting表的常用操作
		/// </summary>
		public DbSet<yoshop_setting> yoshop_settingDb => new DbSet<yoshop_setting>(Db); 

		/// <summary>
		/// 用来处理yoshop_spec表的常用操作
		/// </summary>
		public DbSet<yoshop_spec> yoshop_specDb => new DbSet<yoshop_spec>(Db); 

		/// <summary>
		/// 用来处理yoshop_spec_value表的常用操作
		/// </summary>
		public DbSet<yoshop_spec_value> yoshop_spec_valueDb => new DbSet<yoshop_spec_value>(Db); 

		/// <summary>
		/// 用来处理yoshop_store_user表的常用操作
		/// </summary>
		public DbSet<yoshop_store_user> yoshop_store_userDb => new DbSet<yoshop_store_user>(Db); 

		/// <summary>
		/// 用来处理yoshop_upload_file表的常用操作
		/// </summary>
		public DbSet<yoshop_upload_file> yoshop_upload_fileDb => new DbSet<yoshop_upload_file>(Db); 

		/// <summary>
		/// 用来处理yoshop_upload_file_used表的常用操作
		/// </summary>
		public DbSet<yoshop_upload_file_used> yoshop_upload_file_usedDb => new DbSet<yoshop_upload_file_used>(Db); 

		/// <summary>
		/// 用来处理yoshop_upload_group表的常用操作
		/// </summary>
		public DbSet<yoshop_upload_group> yoshop_upload_groupDb => new DbSet<yoshop_upload_group>(Db); 

		/// <summary>
		/// 用来处理yoshop_user表的常用操作
		/// </summary>
		public DbSet<yoshop_user> yoshop_userDb => new DbSet<yoshop_user>(Db); 

		/// <summary>
		/// 用来处理yoshop_user_address表的常用操作
		/// </summary>
		public DbSet<yoshop_user_address> yoshop_user_addressDb => new DbSet<yoshop_user_address>(Db); 

		/// <summary>
		/// 用来处理yoshop_wxapp表的常用操作
		/// </summary>
		public DbSet<yoshop_wxapp> yoshop_wxappDb => new DbSet<yoshop_wxapp>(Db); 

		/// <summary>
		/// 用来处理yoshop_wxapp_help表的常用操作
		/// </summary>
		public DbSet<yoshop_wxapp_help> yoshop_wxapp_helpDb => new DbSet<yoshop_wxapp_help>(Db); 

		/// <summary>
		/// 用来处理yoshop_wxapp_navbar表的常用操作
		/// </summary>
		public DbSet<yoshop_wxapp_navbar> yoshop_wxapp_navbarDb => new DbSet<yoshop_wxapp_navbar>(Db); 

		/// <summary>
		/// 用来处理yoshop_wxapp_page表的常用操作
		/// </summary>
		public DbSet<yoshop_wxapp_page> yoshop_wxapp_pageDb => new DbSet<yoshop_wxapp_page>(Db); 

    }
}
