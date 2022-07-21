using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NJsonSchema.Generation;
using NSwag.AspNetCore;
using Shop.Core.NSwag.Interfaces;
using Shop.Core.NSwag.Services;
using Shop.Core.StartupModule;
using System.Linq;

namespace Shop.Core.NSwag.Module
{
    /// <summary>
    ///     Модуль добавляет в проект NSwag, который использует спецификацию OpenAPI/Swagger для описания веб-API RESTful.
    /// </summary>
    public class NSwagStartupModule : IStartupModule
    {
        private IApiVersionDescriptionProvider? _apiVersionDescriptionProvider = null!;
        private readonly NSwagParams _params = new NSwagParams();
        private INSwagSecurityConfiguration? _securityConfiguration;

        /// <summary>
        /// Использование параметров NSwag по умолчанию
        /// </summary>
        public NSwagStartupModule()
        {
        }

        /// <summary>
        /// Настройка параметров NSwag
        /// </summary>
        /// <param name="nSwagParams">Класс <see cref="NSwagParams"/>, содержит поля для настройки <seealso cref="NSwag"/></param>
        public NSwagStartupModule(NSwagParams nSwagParams)
        {
            _params = nSwagParams;
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<INSwagSecurityConfiguration, NSwagSecurityConfiguration>();

            _securityConfiguration = services.BuildServiceProvider().GetService<INSwagSecurityConfiguration>();

            services.AddApiVersioning(o =>
            {
                o.ApiVersionReader = new UrlSegmentApiVersionReader();
                // NOTE: def. version won't wotk with url segments (at least not out of the box)
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.SubstituteApiVersionInUrl = true;
            });

            // NSwag (build an intermediate service provider & resolve IApiVersionDescriptionProvider)
            _apiVersionDescriptionProvider =
                services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();

            // Создание конечных точек сваггера для каждой обнаруженной версии API.
            if (_apiVersionDescriptionProvider != null)
                foreach (var description in _apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    var versionGroup = description.GroupName;

                    // registers a OpenAPI v3.0 document
                    // https://github.com/RicoSuter/NSwag/wiki/AspNetCore-Middleware#enable-jwt-authentication
                    services.AddOpenApiDocument(configure =>
                    {
                        // DocumentName is used in the top-right corner of the UI, it's also part of the swagger route
                        configure.DocumentName = versionGroup;

                        // ApiGroupNames is used to filter versions (ApiVersionAttribute) that are includes in this swagger document - NOTE: use v1 for 1.0 v1.1 for 1.1 and so on...
                        configure.ApiGroupNames = new[] { versionGroup };

                        // Json serialization
                        configure.SerializerSettings = new JsonSerializerSettings
                        { ContractResolver = new CamelCasePropertyNamesContractResolver() };

                        configure.Title = string.IsNullOrEmpty(_params.ProjectName)
                            ? "WebAPI (OpenApi)"
                            : $"{_params.ProjectName} WebAPI (OpenApi)";
                        configure.Description = string.IsNullOrEmpty(_params.ProjectName)
                            ? "ASP.NET Core Web API"
                            : $"ASP.NET Core Web API for {_params.ProjectName}";

                        configure.DefaultReferenceTypeNullHandling = ReferenceTypeNullHandling.Null;

                        _securityConfiguration?.ConfigureSecurity(configure, _params.SecurityType);
                    });
                }
        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Регистрация документов OpenAPI/Swagger.
            app.UseOpenApi();

            if (env.IsProduction() && _params.HideOnProd)
            {
                return;
            }

            // Веб-интерфейс SwaggerUI 3 для просмотра документов OpenAPI/Swagger.
            app.UseSwaggerUi3(settings =>
            {
                settings.TransformToExternalPath = (internalUiRoute, request) =>
                {
                    // Заголовок X-External - Path устанавливается в файле nginx.conf.
                    var externalPath = request.Headers.ContainsKey("X-External-Path")
                        ? request.Headers["X-External-Path"].First()
                        : "";
                    return externalPath + internalUiRoute;
                };

                // Путь доступа к сваггеру, по умолчанию "/api".
                settings.Path = _params.SwaggerPath;

                // Построение конечных точек сваггера для каждой обнаруженной версии API.
                foreach (var description in _apiVersionDescriptionProvider!.ApiVersionDescriptions)
                {
                    settings.SwaggerRoutes.Add(new SwaggerUi3Route($"{description.GroupName}",
                        $"{_params.RoutePrefix}/swagger/{description.GroupName}/swagger.json"));
                }
            });
        }
    }
}