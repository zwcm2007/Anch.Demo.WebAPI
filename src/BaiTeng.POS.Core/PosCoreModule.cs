using Abp.Modules;
using System.Reflection;

namespace BaiTeng.POS.Core
{
    /// <summary>
    /// 核心业务层模块
    /// </summary>
    public class PosCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}