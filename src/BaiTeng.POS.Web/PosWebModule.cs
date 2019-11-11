using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Reflection.Extensions;
using BaiTeng.POS.Application;
using BaiTeng.POS.Core.Configuration;
using BaiTeng.POS.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace BaiTeng.POS.Web
{
    [DependsOn(
        typeof(ArchivesApplicationModule),
        typeof(PosEntityFrameworkCoreModule),
        typeof(AbpAspNetCoreModule))]
    public class PosWebModule : AbpModule
    {
        //private readonly IConfiguration _configuration;
        private readonly IConfigurationRoot _appConfiguration;

        public PosWebModule(IWebHostEnvironment env/*, IConfiguration configuration*/)
        {
            //_configuration = configuration;
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(PosConsts.ConnectionStringName);
#if DEBUG
            Configuration.Modules.AbpWebCommon().SendAllExceptionsToClients = true;
#endif
            Configuration.Modules.AbpAspNetCore().CreateControllersForAppServices(
                     typeof(ArchivesApplicationModule).GetAssembly()
                 );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}