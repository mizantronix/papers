namespace Papers.Domain
{
    using Microsoft.Extensions.DependencyInjection;

    using Papers.Data.MsSql;
    using Papers.Domain.Managers;

    public static class Startup
    {
        public static void RegisterDomainDependencies(this IServiceCollection services, string connectionString = null)
        {
            services.RegisterDataDependencies(connectionString);
            services.AddSingleton<IChatManager, ChatManager>();
            services.AddSingleton<IMessageManager, MessageManager>();
            services.AddSingleton<IUserManager, UserManager>();
        }
    }
}
