using Microsoft.EntityFrameworkCore;
using YouYou.Business.Models;
using YouYou.Data.Context;
using YouYou.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddRedisConfig(configuration);

builder.Services.AddIdentityConfiguration(configuration);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.WebApiConfig();

builder.Services.AddSwaggerConfig();

builder.Services.AddHttpContextAccessor();

builder.Services.ResolveDependencies();

builder.Services.AddDbContext<YouYouDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("YouYouDbContext"));
});

var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "YouYouApi v1"));
    app.UseCors("Development");
}
//else
//{
//    app.UseDeveloperExceptionPage();
//    app.UseSwagger();
//    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "YouYouApi v1"));
//    app.UseCors("Staging");
//}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.UseIdentityConfiguration();

app.Run();