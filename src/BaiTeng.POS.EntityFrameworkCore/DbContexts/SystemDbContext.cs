using Abp.EntityFrameworkCore;
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
    public class SystemDbContext : AbpDbContext
    {
        public SystemDbContext(DbContextOptions<SystemDbContext> options)
            : base(options)
        {
        }

        public DbSet<SM_Account> SM_Accounts { get; set; }
        public DbSet<SM_Function> SM_Functions { get; set; }

        public DbSet<SM_Role> Roles { get; set; }
        public DbSet<SM_RoleFunc> RoleFuncs { get; set; }
        public DbSet<SM_User> Users { get; set; }
    }
}