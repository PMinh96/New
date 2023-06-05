using Glimpse.AspNet.Tab;
using Microsoft.EntityFrameworkCore;

namespace ProductManagement.Infrastructure
{
    public class ConnectionInitiator
    {
        public static void Register(IServiceCollection services)
        {
            //services.AddDbContext<ProductManagementContext>
            //(o => o.UseSqlServer(Configuration.
            //    GetConnectionString("myconn")));

        }
    }
}
