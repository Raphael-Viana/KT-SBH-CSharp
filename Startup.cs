using KTCSharpApi.Configurations;

namespace KTCSharpApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration,
            IWebHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            Environment = hostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllers();
            services.AddDependecyInjection();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

            app.UseCors(configurePolicy: c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseHttpsRedirection();

            app.UseEndpoints(configure: end =>
            {
                end.MapDefaultControllerRoute();
                end.MapGet(pattern: "/", handler: GetHomeTitle);
            });
        }

        private string GetHomeTitle()
        {
            return "KTCsharpApi v1.0.0";
        }
    }
}
