using Microsoft.EntityFrameworkCore;
using ProductManagement.Infrastructure;

namespace ProductManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<ProductManagementContext>
            (o => o.UseSqlServer(Configuration.
                GetConnectionString("myconn")));

            #region Mapper declaration

            AutoMappperInitiator.Register(services);

            #endregion Mapper declaration

            #region [DI]

            IoC.Register(services);

            #endregion [DI]

            #region [Swagger]

            SwaggerInitiator.Register(services);

            #endregion [Swagger]

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            SwaggerInitiator.Configure(app, env);
            // db.Database.EnsureCreated();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}