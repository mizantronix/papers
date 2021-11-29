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
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;

            services.AddSingleton<IChatRepository, ChatRepository>();
            services.AddSingleton<IMessageRepository, MessageRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
        }
    }
}
