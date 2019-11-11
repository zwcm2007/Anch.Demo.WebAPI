using Anch.Demo.Core;
using Microsoft.EntityFrameworkCore;

namespace Anch.Demo.EntityFrameworkCore
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    //[AutoRepositoryTypes(
    //    typeof(IRepository<>),
    //    typeof(IRepository<,>),
    //    typeof(DemoRepositoryBase<>),
    //    typeof(DemoRepositoryBase<,>)
    //)]
    public class BusinessDbContext : DbContextBase
    {
        public BusinessDbContext(DbContextOptions<BusinessDbContext> options)
            : base(options)
        {
        }

        public DbSet<V_CARSET> V_CARSETS { get; set; }

    }
}