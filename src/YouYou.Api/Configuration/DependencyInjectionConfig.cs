using YouYou.Api.Extensions;
using YouYou.Business.Interfaces;
using YouYou.Business.Services;
using YouYou.Data.Context;
using YouYou.Business.ErrorNotifications;
using YouYou.Business.Interfaces.Emails;
using YouYou.Data.Repository;

namespace YouYou.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<YouYouDbContext>();

            services.AddSingleton<IUriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });

            services.AddScoped<IErrorNotifier, ErrorNotifier>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddSingleton<IEmailSettings, EmailSettings>();
            services.AddScoped<IUser, AspNetUser>();
            services.AddScoped<ILogExceptionRepository, LogExceptionRepository>();


            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPersonRepository, PersonRepository>();

            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IDeviceHistoryRepository, DeviceHistoryRepository>();


            return services;
        }
    }
}
