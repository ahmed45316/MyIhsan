using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NetCore.AutoRegisterDi;
using Reservation.Common.Core;
using Reservation.Common.IdentityInterfaces;
using Reservation.Identity.Data.Context;
using Reservation.Identity.Data.SeedData;
using Reservation.Identity.Service.Core;
using Reservation.Identity.Service.Dtos;
using Reservation.Identity.Service.Services;
using Reservation.Identity.Service.UnitOfWork;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

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
        [Obsolete]
        public static IServiceCollection AddRegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors();      
            services.JWTSettings(configuration);
            services.AddApiDocumentationServices();
            services.RegisterIdentityCores();
            services.AddIdentiyUnitOfWork();
            services.RegisterIdentityAssemply();
            services.RegisterAutoMapper();
            services.RegistersDtos();
            services.RegisterMainCore();
            return services;
        }

        [Obsolete]
        private static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper();
        }
        private static void RegisterMainCore(this IServiceCollection services)
        {
            services.AddTransient<IHandlerResponse, HandlerResponse>();
            services.AddTransient<IResponseResult, ResponseResult>();
            services.AddTransient<IResult, Result>();
            services.AddTransient<IDataPagging, DataPagging>();
        }
        private static void RegisterIdentityCores(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseService<,>), typeof(BaseService<,>));            
            services.AddTransient(typeof(IBusinessBaseParameter<>), typeof(BusinessBaseParameter<>));
            services.AddTransient<ITokenBusiness, TokenBusiness>();
            services.AddTransient<IDecodingValidToken, DecodingValidToken>();         
            services.AddSingleton<IDataInitialize, DataInitialize>();
        }
        private static void AddApiDocumentationServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "Reservation API V1", Version = "v1" });

                options.DescribeAllEnumsAsStrings();
                var filePath = Path.Combine(AppContext.BaseDirectory, "Reservation.API.xml");
                options.IncludeXmlComments(filePath);

                // Swagger 2.+ support
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                options.AddSecurityRequirement(security);

            });
        }       
        private static void AddIdentiyUnitOfWork(this IServiceCollection services)
        {
            services.AddTransient(typeof(IIdentityUnitOfWork<>), typeof(IdentityUnitOfWork<>));
        }
        private static void RegisterIdentityAssemply(this IServiceCollection services)
        {
            var servicesToScan = Assembly.GetAssembly(typeof(LoginServices)); //..or whatever assembly you need
            services.RegisterAssemblyPublicNonGenericClasses(servicesToScan)
              .Where(c => c.Name.EndsWith("Services"))
              .AsPublicImplementedInterfaces();
        }
        private static void RegistersDtos(this IServiceCollection services)
        {
            services.AddSingleton<IUserDto, UserDto>();
            services.AddSingleton<IScreenDto, ScreenDto>();
            services.AddSingleton<IRoleDto, RoleDto>();
            services.AddSingleton<IGetRoleDto, GetRoleDto>();
            services.AddSingleton<IUpdateRoleDto, UpdateRoleDto>();
            services.AddSingleton<IMenuDto, MenuDto>();
            services.AddSingleton<IUserLoginReturn, UserLoginReturn>();
        }
        private static void JWTSettings(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Site"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Site"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"])),
                };
            });
        }
    }
}
