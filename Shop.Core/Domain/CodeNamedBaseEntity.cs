namespace Shop.Core.Domain
{
    /// <summary>
    /// Базовая сущность, содержащая свойства "Наименование", "Код"
    /// </summary>
    public abstract class CodeNamedBaseEntity : NamedBaseEntity
    {
        #nullable enable 
        public string? Code { get; set; }
    }
}