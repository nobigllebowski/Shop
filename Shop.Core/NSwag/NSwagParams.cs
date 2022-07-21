namespace Shop.Core.NSwag
{
    public class NSwagParams
    {
        /// <summary>
        ///     Класс, свойства которого описывают конфигурацию NSwag
        /// </summary>
        /// <param name="projectName">Имя проекта, отображаемое в сваггере.</param>
        /// <param name="securityType">Тип авторизации в сваггере</param>
        /// <param name="swaggerPath">Путь доступа к сваггеру, по умолчанию "/api".</param>
        /// <param name="routePrefix">
        ///     Путь к документации сваггера. Если проект корневой, оставить пустым, если нет - начать путь с
        ///     точки ".".
        /// </param>
        /// <param name="hideOnProd">Скрытие сваггера на проде, по умолчанию скрывается.</param>
        public NSwagParams(string? projectName = "Project based on PlatformCore",
            NSwagSecurityType securityType = NSwagSecurityType.None, string swaggerPath = "/api",
            string routePrefix = "", bool hideOnProd = true)
        {
            RoutePrefix = routePrefix;
            ProjectName = projectName;
            SwaggerPath = swaggerPath;
            SecurityType = securityType;
            HideOnProd = hideOnProd;
        }

        /// <summary>
        ///     Название проекта.
        /// </summary>
        public string? ProjectName { get; }

        /// <summary>
        ///     Путь доступа к сваггеру, по умолчанию "/api".
        /// </summary>
        public string SwaggerPath { get; }

        /// <summary>
        ///     Путь к документации сваггера.
        ///     Если проект корневой, оставить пустым, если нет - начать путь с точки ".".
        /// </summary>
        public string RoutePrefix { get; }

        /// <summary>
        ///     Тип авторизации
        /// </summary>
        public NSwagSecurityType SecurityType { get; }

        /// <summary>
        ///     Скрытие сваггера на проде, по умолчанию скрывается.
        /// </summary>
        public bool HideOnProd { get; }
    }
}