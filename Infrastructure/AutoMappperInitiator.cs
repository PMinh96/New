using AutoMapper;

namespace ProductManagement.Infrastructure
{
    public static class AutoMappperInitiator
    {
        public static void Register(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}