using Anch.Demo.Core;
using Anch.Demo.Core.Configuration;
using Anch.Demo.Core.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Anch.Demo.EntityFrameworkCore.DbMigrations
{
    /* This class is needed to run EF Core PMC commands. Not used anywhere else */

    public class DemoDbContextFactory : IDesignTimeDbContextFactory<DemoDbContext>
    {
        public DemoDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DemoDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            DbContextOptionsConfigurer.Configure(
                builder,
                configuration.GetConnectionString(DemoConsts.ConnectionStringName)
            );

            return new DemoDbContext(builder.Options);
        }
    }
}