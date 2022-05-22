namespace Papers.Data.MsSql
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;

    using Papers.Data.MsSql.Configuration;
    using Papers.Data.MsSql.Repositories;
    
    public static class Startup
    {
        public static void RegisterDataDependencies(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DataContext>(o => o.UseSqlServer(connectionString));
            services.AddTransient<IChatRepository, ChatRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
