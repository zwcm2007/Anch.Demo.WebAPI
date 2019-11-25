using Microsoft.EntityFrameworkCore;

namespace Anch.Demo.EntityFrameworkCore
{
    public static class DbContextOptionsConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<DemoDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString);
        }
    }
}