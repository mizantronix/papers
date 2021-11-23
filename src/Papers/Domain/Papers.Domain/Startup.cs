namespace Papers.Domain
{
    using Microsoft.Extensions.DependencyInjection;
    using Papers.Data.MsSql;
    using Papers.Domain.Managers;

    public static class Startup
    {
        public static void RegisterDomainDependencies(this IServiceCollection services)
        {
            services.RegisterDataDependencies();
            services.AddSingleton<IMessageManager, MessageManager>();
        }
    }
}
