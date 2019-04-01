using AutoMapper;

namespace QuickWeb.Extensions.Common
{
    public static class AutoMapperConfig
    {
        public static void Register()
        {
            Mapper.Initialize(m =>
            {
                //m.CreateMap<Broadcast, BroadcastInputDto>();
            });
        }
    }
}