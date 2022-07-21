using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shop.Core.StartupModule
{
    /// <summary>
    ///     Настраиваемый модуль, содержащий несколько сервисов.
    /// </summary>
    /// 
    public interface IStartupModule
    {
        /// <summary>
        ///     Метод настройки модуля сервиса.
        /// </summary>
        void ConfigureServices(IServiceCollection services, IConfiguration configuration);

        /// <summary>
        ///     Метод для конфигурации сервисов модуля.
        /// </summary>
        void Configure(IApplicationBuilder app, IWebHostEnvironment env);
    }
}