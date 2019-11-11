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