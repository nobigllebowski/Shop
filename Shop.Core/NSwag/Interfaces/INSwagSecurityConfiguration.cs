using NSwag.Generation.AspNetCore;

namespace Shop.Core.NSwag.Interfaces
{
    /// <summary>
    /// Настройка авторизации в NSwag
    /// </summary>
    public interface INSwagSecurityConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        void ConfigureSecurity(AspNetCoreOpenApiDocumentGeneratorSettings configure, NSwagSecurityType securityType = NSwagSecurityType.None);

    }
}