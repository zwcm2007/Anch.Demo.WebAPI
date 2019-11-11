using Abp.Modules;
using System.Reflection;

namespace Anch.Demo.Core
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