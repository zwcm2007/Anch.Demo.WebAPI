using Microsoft.EntityFrameworkCore;

namespace Anch.Demo.EntityFrameworkCore.DbMigrations
{
    public static class DbContextOptionsConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<DemoDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString,
                x => x.MigrationsAssembly("Anch.Demo.EntityFrameworkCore.DbMigrations"));
        }
    }
}