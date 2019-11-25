using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anch.Demo.EntityFrameworkCore
{
    /// <summary>
    /// 实体类型配置基类
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    public abstract class EntityTypeConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(TableName);
        }

        /// <summary>
        /// 映射表名
        /// </summary>
        protected virtual string TableName
        {
            get
            {
                string className = this.GetType().Name;
                return $"{className.Substring(0, className.Length - "Mapping".Length)}";
            }
        }
    }
}