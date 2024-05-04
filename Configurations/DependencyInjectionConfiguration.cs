namespace KTCSharpApi.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddDependecyInjection(this IServiceCollection services)
        {
            services.AddScoped<Contexts.MongoDBContext>();
            services.AddScoped<Repositories.Interfaces.ICadPessoaRepository, Repositories.Implementations.CadPessoaRepository>();
            return services;
        }
    }
}
