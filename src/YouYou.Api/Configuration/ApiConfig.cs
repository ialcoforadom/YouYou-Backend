using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace YouYou.Api.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection WebApiConfig(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
               options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())); 

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("Development",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

                options.AddPolicy("Staging",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            return services;
        }
    }
}
