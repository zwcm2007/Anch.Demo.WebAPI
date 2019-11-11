//using Abp.EntityFramework.Configuration;
using Abp.EntityFrameworkCore;
using Abp.Modules;
using System.Reflection;
using Anch.Demo.Core;
using Microsoft.EntityFrameworkCore;
using Castle.MicroKernel.Registration;

namespace Anch.Demo.EntityFrameworkCore
{
    [DependsOn(
        typeof(DemoCoreModule),
        typeof(AbpEntityFrameworkCoreModule)
        //typeof(AbpDapperModule)
        )]
    public class DemoEntityFrameworkCoreModule : AbpModule
    {
        public DemoEntityFrameworkCoreModule()
        {
        }

        public override void Initialize()
        {
            IocManager.IocContainer.Register(
                Component.For<ISqlExecuter<DemoDbContext>>().ImplementedBy<SqlExecuter<DemoDbContext>>().LifestyleTransient()
            );
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //// add by fenqjq 20180921
            //DapperExtensions.DapperExtensions.SetMappingAssemblies(new List<Assembly> { typeof(MaintainEntityFrameworkModule).GetAssembly() });
        }

        public override void PostInitialize()
        {
            //if (!SkipDbSeed)
            //{
            //    SeedHelper.SeedHostDb(IocManager);
            //}
        }
    }
}