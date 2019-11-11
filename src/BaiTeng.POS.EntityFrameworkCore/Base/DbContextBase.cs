using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace BaiTeng.POS.EntityFrameworkCore
{
    /// <summary>
    /// 数据库上下文基类
    /// add by fengjq 2019年10月19日06:19:52
    /// </summary>
    public abstract class DbContextBase : AbpDbContext
    {
        public DbContextBase(DbContextOptions options)
            : base(options)
        {
        }

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