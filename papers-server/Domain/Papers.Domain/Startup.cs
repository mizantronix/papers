namespace Papers.Domain
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Papers.Data.MsSql;
    using Papers.Domain.Managers;

    public static class Startup
    {
        public static void RegisterDomainDependencies(this IServiceCollection services, string connectionString)
        {
            services.RegisterDataDependencies(connectionString);
            services.AddTransient<IChatManager, ChatManager>();
            services.AddTransient<IMessageManager, MessageManager>();
            services.AddTransient<IUserManager, UserManager>();
        }
    }
}
