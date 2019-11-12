using Abp.EntityFrameworkCore;
using Anch.Demo.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

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
    public class DemoDbContext : AbpDbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //
            // 添加实体映射
            var typeConfigs = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => !t.IsAbstract)
                .Where(t => t.GetInterfaces().Any(x => x.IsGenericType &&
                         x.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)
                       ))
                .ToList();

            foreach (var config in typeConfigs)
            {
                dynamic instance = Activator.CreateInstance(config);
                modelBuilder.ApplyConfiguration(instance);
            }
        }
    }
}