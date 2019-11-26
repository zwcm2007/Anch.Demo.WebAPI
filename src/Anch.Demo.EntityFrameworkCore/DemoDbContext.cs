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

        // 用户
        public DbSet<User> Users { get; set; }

        // 角色
        public DbSet<Role> Roles { get; set; }

        // 用户角色关联
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

            // 初始化数据
            modelBuilder.Entity<Role>().HasData(
              new Role { Id = "1", Name = "系统管理员", Description = "系统管理员" },
              new Role { Id = "2", Name = "查询用户", Description = "普通用户" });

            modelBuilder.Entity<User>().HasData(
              new User { Id = 1, UserName = "test", FactName = "冯珏庆", Address = "祥园路28号", Mobile = "18668180673" }
             );

            modelBuilder.Entity<UserRole>().HasData(
              new UserRole { UserID = 1, RoleID = "1" }
             );
        }
    }
}