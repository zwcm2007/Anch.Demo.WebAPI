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
    public class ClientDbContext : DbContextBase
    {
        public ClientDbContext(DbContextOptions<ClientDbContext> options)
            : base(options)
        {
        }

        public DbSet<BT_Customer> BT_Customers { get; set; }
        public DbSet<BT_UserRegister> BT_UserRegisters { get; set; }
    }
}