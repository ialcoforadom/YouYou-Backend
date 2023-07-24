using Microsoft.OpenApi.Models;
using System.Reflection;

namespace YouYou.Api.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "YouYou.API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"Autenticação em JWT - YouYou: Bearer {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement(){
                        {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                        }
                    });
            });
            return services;
        }
    }
}
