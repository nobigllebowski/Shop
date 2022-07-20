using Shop.Core.Domain;

namespace Shop.Domain.Products
{
    public class Product : CodeNamedBaseEntity
    {
        public string Description { get; set; }
    }
}
