namespace Shop.Core.Domain
{
    public class CodeNamedBaseEntity : NamedBaseEntity
    {
        #nullable enable
        public string? Code { get; set; }
    }
}
