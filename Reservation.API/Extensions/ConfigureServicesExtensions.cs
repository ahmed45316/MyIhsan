using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using Reservation.Common.Core;
using Reservation.Identity.Data.Context;
using Reservation.Identity.Service.Core;
using Reservation.Identity.Service.Services;
using Reservation.Identity.Service.UnitOfWork;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace Reservation.API.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ConfigureServicesExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddRegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors();
            services.AddApiDocumentationServices();
            services.RegisterIdentityCores();
            services.AddIdentiyUnitOfWork();
            services.RegisterIdentityAssemply();
            return services;
        }
        private static void RegisterIdentityCores(this IServiceCollection services)
        {
            services.AddTransient<IHandlerResponse, HandlerResponse>();
            services.AddTransient<IResponseResult, ResponseResult>();
            services.AddTransient<IResult,Result>();
            services.AddTransient(typeof(IBusinessBaseParameter<>), typeof(BusinessBaseParameter<>));
            services.AddTransient<ITokenBusiness, TokenBusiness>();
        }
        private static void AddApiDocumentationServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "Reservation API First Version", Version = "v1" });

                options.DescribeAllEnumsAsStrings();
                var filePath = Path.Combine(AppContext.BaseDirectory, "Reservation.API.xml");
                options.IncludeXmlComments(filePath);

            });
        }       
        private static void AddIdentiyUnitOfWork(this IServiceCollection services)
        {
            services.AddTransient(typeof(IIdentityUnitOfWork<>), typeof(IdentityUnitOfWork<>));
        }
        private static void RegisterIdentityAssemply(this IServiceCollection services)
        {
            var assemblyToScan = Assembly.GetAssembly(typeof(LoginServices)); //..or whatever assembly you need
            services.RegisterAssemblyPublicNonGenericClasses(assemblyToScan)
              .Where(c => c.Name.EndsWith("Services"))
              .AsPublicImplementedInterfaces();
        }
    }
}
