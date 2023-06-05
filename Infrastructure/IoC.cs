using ProductManagement.Services.Implements;
using ProductManagement.Services.InterfaceService;

namespace ProductManagement.Infrastructure
{
    public static class IoC
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<IBrandServices, BrandServices>();
            services.AddTransient<IProductServices, ProductService>();
            services.AddTransient<IWareHouseServices, WareHouseService>();
        }
    }
}