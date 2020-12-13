using System;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using FeedBack.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace FeedBack.Helpers
{
    public static class AuthenticationExtension
    {
         public static IServiceCollection AddTokenAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var key = Encoding.ASCII.GetBytes(config.GetSection("AppSettings:Secret").Value);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }

                            return Task.CompletedTask;
                        },
                        OnTokenValidated = async context =>
                        {
                            var userService = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
                            var userId = long.Parse(context.Principal.Identity.Name ?? string.Empty);
                            var user = await userService.GetUserById(userId).ConfigureAwait(false);
                            if (user == null)
                            {
                                // return unauthorized if user no longer exists
                                context.Fail("Unauthorized");
                            }
                        },
                    };
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            return services;
        }
    }
}