﻿using FeedBack;
 using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KashIn.Helpers
{
#pragma warning disable CS1591

    public static class AppSettingServiceExtensions
    {
        public static IServiceCollection AddAppSettingsService(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<Settings>(options =>
            {
                options.Environment = config.GetSection("AppSettings:Environment").Value;
                options.Secret = config.GetSection("AppSettings:Secret").Value;
            });
            return services;
        }
    }

#pragma warning restore CS1591
}