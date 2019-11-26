using Abp.AspNetCore;
using Abp.Castle.Logging.Log4Net;
using Abp.EntityFrameworkCore;
using Anch.Demo.EntityFrameworkCore;
using Castle.Facilities.Logging;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;

namespace Anch.Demo.Web
{
    public class Startup
    {
        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddAbpDbContext<DemoDbContext>(options =>
            {
                options.DbContextOptions.UseMySql(options.ConnectionString);
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(CustomApiExceptionFilter), 10);
            })
            .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new LowercaseContractResolver())
            /*.AddControllersAsServices()*/
            ;

            // Bearer认证-add by fengjq 20180509
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = JwtClaimTypes.Name,
                    RoleClaimType = JwtClaimTypes.Role,
                    ValidIssuer = "http://localhost:23648",
                    ValidAudience = "api",
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(0),
                    //IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Consts.Secret))
                };
            });

            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            });

            // Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
            });

            // Cors
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    var origins = Configuration["AllowDomain"]?.Split(',');
                    builder.WithOrigins(origins).AllowAnyMethod()
                         .AllowAnyHeader()
                         .AllowAnyOrigin()
                         //.AllowCredentials()
                         ;
                });
            });

            //
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //
            // Configure Abp and Dependency Injection
            return services.AddAbp<DemoWebModule>(options =>
            {
                // Configure Log4Net logging
                options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpLog4Net().WithConfig("log4net.config")
                );
                //options.DisableAllInterceptors = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAbp();

            //app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo API V1");
            }); // URL: /swagger
        }
    }
}