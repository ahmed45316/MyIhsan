using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyIhsan.API.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class UseServiceExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseRegisteredService(this IApplicationBuilder app)
        {
            app.UseCors(builder => builder
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials());

            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyIhsan API V1");
                c.DocumentTitle = "MyIhsan Api Documentation";
                c.DocExpansion(DocExpansion.None);
            });

            return app;
        }
    }
}
