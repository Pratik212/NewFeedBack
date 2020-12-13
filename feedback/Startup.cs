using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FeedBack.Helpers;
using FeedBack.Interfaces;
using FeedBack.Providers;
using FoodDelivery.Helpers;
using KashIn.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace FeedBack
{
    public class Startup
    {
        private readonly string MyAllowSpecificOrigins = "CorsPolicy";
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddDbContext<FeedBackContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
            
            services.AddTransient<IUserFeedBackRepository, UserFeedBackRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            
            services.AddAutoMapper(typeof(Startup));
            
            services.AddSwaggerDocumentation();
            
            services.AddAppSettingsService(Configuration);
            
            services.AddTokenAuthentication(Configuration);    
            
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins(
                                "http://localhost:3000")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(MyAllowSpecificOrigins);
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwaggerDocumentation();
            
            app.ConfigureCustomExceptionMiddleware();
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}