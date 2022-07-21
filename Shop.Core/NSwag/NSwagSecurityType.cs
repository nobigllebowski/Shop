namespace Shop.Core.NSwag
{
    /// <summary>
    /// Типы авторизации в NSwag
    /// </summary>
    public enum NSwagSecurityType
    {
        /// <summary>
        /// Отсутствие авторизации 
        /// </summary>
        None,

        /// <summary>
        /// Авторизация с помощью JWT токена
        /// </summary>
        ApiKey,

        /// <summary>
        /// Авторизация с помощью client_id, client_secret
        /// </summary>
        OAuth2
    }
}