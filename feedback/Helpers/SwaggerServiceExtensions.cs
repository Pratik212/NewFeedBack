using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace FoodDelivery.Helpers
{
#pragma warning disable CS1591

    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
            // services.AddSwaggerExamples();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "FeedBack API",
                    Description = "This Api will be responsible for overall data distribution and authorization.",
                    Contact = new OpenApiContact
                    {
                        Name = "Ghanshyam Digital LLP",
                        Email = "digitalghanshyam@gmail.com",
                        Url = new Uri("https://ghanshyamdigital.in/"),
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
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
                // // Set the comments path for the Swagger JSON and UI.
                // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                // c.IncludeXmlComments(xmlPath);

                // [SwaggerRequestExample] & [SwaggerResponseExample]
                // version < 3.0 like this: c.OperationFilter<ExamplesOperationFilter>(); 
                // version 3.0 like this: c.AddSwaggerExamples(services.BuildServiceProvider());
                // version > 4.0 like this:
                c.ExampleFilters();

                c.OperationFilter<AddResponseHeadersFilter>(); // [SwaggerResponseHeader]

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter
                >(); // Adds "(Auth)" to the summary so that you can see which endpoints have Authorization
                // or use the generic method, e.g. c.OperationFilter<AppendAuthorizeToSummaryOperationFilter<MyCustomAttribute>>();

                // add Security information to each operation for OAuth2
                // c.OperationFilter<SecurityRequirementsOperationFilter>();
                // or use the generic method, e.g. c.OperationFilter<SecurityRequirementsOperationFilter<MyCustomAttribute>>();
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "FeedBack API";
                c.DefaultModelExpandDepth(2);
                c.DefaultModelRendering(ModelRendering.Model);
                c.DefaultModelsExpandDepth(-1);
                // c.DisplayOperationId();
                c.DisplayRequestDuration();
                c.DocExpansion(DocExpansion.None);
                c.EnableDeepLinking();
                c.EnableFilter();
                // c.MaxDisplayedTags(5);
                c.ShowExtensions();
                // c.ShowCommonExtensions();
                c.EnableValidator();
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FeedBack API V1");
                c.RoutePrefix = string.Empty;
                // c.DocExpansion(DocExpansion.Full);
                c.InjectStylesheet("/swagger/ui/custom.css");
                c.ShowCommonExtensions();
            });

            return app;
        }
    }

#pragma warning restore CS1591
}