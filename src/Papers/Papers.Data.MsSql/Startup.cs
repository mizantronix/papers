namespace Papers.Data.MsSql
{
    using Microsoft.Extensions.DependencyInjection;

    using Papers.Data.Contract.Repositories;
    using Papers.Data.MsSql.Repositories;
    
    public static class Startup
    {
        public static void RegisterDataDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IChatRepository, ChatRepository>();
            services.AddSingleton<IMessageRepository, MessageRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
        }
    }
}
