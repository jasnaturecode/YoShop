using AutoMapper;
using Quick.Models.Dto;
using Quick.Models.Entity.Table;
using QuickWeb.Models.ViewModel;

namespace QuickWeb.Extensions.Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class AutoMapperConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public static void Register()
        {
            Mapper.Initialize(m =>
            {
                m.CreateMap<yoshop_store_user, AdminDto>();
                m.CreateMap<yoshop_category, CategoryViewModel>()
                    //映射发生之前
                    //.BeforeMap((src, dst) => { dst.create_time = src.create_time.ConvertToDateTime(); })
                    //映射发生之后
                    .AfterMap((src, dst) => { dst.create_time = src.create_time.ConvertToDateTime();});
            });
        }
    }
}