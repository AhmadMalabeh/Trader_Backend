using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Trader_Backend.API.Middleware;
using Trader_Backend.Application.Interfaces.Repositories;
using Trader_Backend.Application.Interfaces.Services;
using Trader_Backend.Application.Services;
using Trader_Backend.Application.Validators.UserValidators;
using Trader_Backend.Infrastructure.Data;
using Trader_Backend.Infrastructure.Data.Repositories;

namespace Trader_Backend.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<AppDbContext>(options =>
                            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestDtoValidator>();

            //Dependency Injection for Repositories and Services
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IInvitationCodeRepository, InvitationCodeRepository>();
            builder.Services.AddScoped<IInvitationCodeService, InvitationCodeService>();


            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            // 🚀 استبدل جزء الـ AddOpenApi القديم بهذا السطرين السحريين وبدون تعقيدات الـ Models!
            builder.Services.AddOpenApi();

            builder.Services
                        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddMicrosoftIdentityWebApi(
                            builder.Configuration.GetSection("AzureAd"));



            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                app.UseSwaggerUI(options => {
                    options.SwaggerEndpoint("/openapi/v1.json", "Trader API v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
