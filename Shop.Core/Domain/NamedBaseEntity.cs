namespace Shop.Core.Domain
{
    /// <summary>
    /// Базовая сущность, содержащая свойство "Наименование"
    /// </summary>
    public abstract class NamedBaseEntity : BaseEntity
    {
        #nullable enable
        public string? Name { get; set; }
    }
}