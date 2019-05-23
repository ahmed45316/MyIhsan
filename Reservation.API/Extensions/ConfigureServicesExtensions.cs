using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reservation.Identity.Data.Context;
using Reservation.Identity.Entities.Entities;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

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
            services.AddIdentityDb(configuration);
            services.AddApiDocumentationServices();
            //services.AddAutoMapper();
            return services;
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
        private static void AddIdentityDb(this IServiceCollection services,IConfiguration _configuration)
        {
            services.AddDbContext<IdentityContext>(cfg =>
            {
                cfg.UseSqlServer(_configuration.GetConnectionString("IdentityContext"))
                    .UseLazyLoadingProxies();
            })
            .AddIdentity<ApplicationUser,ApplicationRole>(option =>
            {
                option.Password.RequireDigit = false;
                option.Password.RequiredLength = 6;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireLowercase = false;
                option.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders(); ;
        }
    }
}
