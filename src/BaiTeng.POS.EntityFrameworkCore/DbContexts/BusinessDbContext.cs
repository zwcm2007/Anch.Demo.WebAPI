using BaiTeng.POS.Core;
using Microsoft.EntityFrameworkCore;

namespace BaiTeng.POS.EntityFrameworkCore
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