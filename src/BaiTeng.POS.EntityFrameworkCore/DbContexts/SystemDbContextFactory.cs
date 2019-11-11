using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using BaiTeng.POS.Core.Configuration;
using BaiTeng.POS.Core.Web;
using BaiTeng.POS.Web;

namespace BaiTeng.POS.EntityFrameworkCore
{
    /* This class is needed to run EF Core PMC commands. Not used anywhere else */

    public class SystemDbContextFactory : IDesignTimeDbContextFactory<SystemDbContext>
    {
        public SystemDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SystemDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            DbContextOptionsConfigurer.Configure(
                builder,
                configuration.GetConnectionString(PosConsts.ConnectionStringName)
            );

            return new SystemDbContext(builder.Options);
        }
    }
}