using BaiTeng.POS.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaiTeng.POS.EntityFrameworkCore
{
    /// <summary>
    /// SM_FunctionsMapping
    /// </summary>
    public class SM_FunctionMapping : EntityTypeConfigurationBase<SM_Function>
    {
        protected override string TableName => "SM_Functions";

        public override void Configure(EntityTypeBuilder<SM_Function> builder)
        {
            base.Configure(builder);

            // 主键
            builder.HasKey(e => e.Id);
            // 关系
            // 属性
            builder.Property(e => e.Id).HasColumnName("vFuncID");
            builder.Property(e => e.FuncCode).HasColumnName("vFuncCode").IsRequired();
            builder.Property(e => e.FuncName).HasColumnName("vFuncName").IsRequired();
        }
    }
}