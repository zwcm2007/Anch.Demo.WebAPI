using Abp.Modules;
using Castle.MicroKernel.Registration;
using GisqRealEstate.MaintainWeb.Common.Utils;
using System.Reflection;

namespace GisqRealEstate.MaintainWeb.Common
{
    public class MaintainCommonModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.IocContainer.Register(
                Component.For<IGenerate>().ImplementedBy<DataGenerator>().LifestyleTransient(),
                Component.For<IAppCacheManager>().ImplementedBy<AppCacheManager>().LifestyleSingleton(),
                Component.For<IConfigManager>().ImplementedBy<ConfigManager>().LifestyleSingleton()
                );
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}