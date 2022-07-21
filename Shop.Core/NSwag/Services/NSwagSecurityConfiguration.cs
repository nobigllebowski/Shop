using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.Generation.AspNetCore;
using NSwag.Generation.Processors.Security;
using Shop.Core.NSwag.Interfaces;
using System.Linq;

namespace Shop.Core.NSwag.Services
{
    /// <summary>
    /// Настройка авторизации в NSwag
    /// </summary>
    public class NSwagSecurityConfiguration : INSwagSecurityConfiguration
    {

        private IConfiguration _configuration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public NSwagSecurityConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Настройка авторизации 
        /// </summary>
        public void ConfigureSecurity(AspNetCoreOpenApiDocumentGeneratorSettings configure, NSwagSecurityType securityType = NSwagSecurityType.None)
        {
            switch (securityType)
            {
                case NSwagSecurityType.ApiKey:
                    ConfigureApiKeySecurity(configure);
                    break;

                case NSwagSecurityType.OAuth2:
                    ConfigureOAuth2Security(configure);
                    break;

                case NSwagSecurityType.None:
                    break;
            }
        }

        /// <summary>
        /// Настройка OAuth2 авторизации
        /// </summary>
        private void ConfigureOAuth2Security(AspNetCoreOpenApiDocumentGeneratorSettings configure)
        {
            configure.AddSecurity("bearer", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.OAuth2,
                Description = "OAuth2",
                Flow = OpenApiOAuth2Flow.Undefined,
                Flows = new OpenApiOAuthFlows()
                {
                    ClientCredentials = new OpenApiOAuthFlow
                    {
                        TokenUrl = _configuration.GetValue<string>("NSwagOAuth2Security:TokenUrl"),
                        AuthorizationUrl = _configuration.GetValue<string>("NSwagOAuth2Security:AuthorizationUrl")
                    }
                }
            });

            configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("bearer"));
        }

        /// <summary>
        /// Настройка ApiKey авторизации
        /// </summary>
        private void ConfigureApiKeySecurity(AspNetCoreOpenApiDocumentGeneratorSettings configure)
        {
            configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.ApiKey,
                Name = "Authorization",
                In = OpenApiSecurityApiKeyLocation.Header,
                Description = "Type into the textbox: Bearer {your JWT token}."
            });
            configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
        }
    }
}