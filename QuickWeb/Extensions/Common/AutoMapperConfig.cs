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
                m.CreateMap<yoshop_category, CategoryDto>()
                    //映射发生之前
                    //.BeforeMap((src, dst) => { dst.create_time = src.create_time.ConvertToDateTime(); })
                    //.BeforeMap((src, dst) => { dst.update_time = src.update_time.ConvertToDateTime(); })
                    //映射发生之后
                    .AfterMap((src, dst) => { dst.create_time = src.create_time.ConvertToDateTime(); })
                    .AfterMap((src, dst) => { dst.update_time = src.update_time.ConvertToDateTime(); });
                m.CreateMap<CategoryDto, yoshop_category>()
                    .ForMember(dst => dst.create_time, opt => { opt.MapFrom(src => src.create_time.ConvertToTimeStamp()); })
                    .ForMember(dst => dst.update_time, opt => { opt.MapFrom(src => src.update_time.ConvertToTimeStamp()); });
                m.CreateMap<yoshop_user, UserDto>()
                    .ForMember(dst => dst.create_time, opt => { opt.MapFrom(src => src.create_time.ConvertToDateTime()); })
                    .ForMember(dst => dst.update_time, opt => { opt.MapFrom(src => src.update_time.ConvertToDateTime()); });

                m.CreateMap<yoshop_delivery_rule, DeliveryRuleRegionViewModel>()
                    .ForMember(dst => dst.region_content, opt => { opt.Ignore(); });

            });
        }
    }
}