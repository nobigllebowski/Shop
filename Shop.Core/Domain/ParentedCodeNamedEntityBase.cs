using System.Collections.Generic;

namespace Shop.Core.Domain
{
    /// <summary>
    /// Базовый класс для связи с Parent
    /// </summary>
    public abstract class ParentedCodeNamedEntityBase : CodeNamedBaseEntity
    {
        
    }

    /// <summary>
    /// Базовый класс для связи с Parent
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ParentedCodeNamedEntityBase<T> : ParentedCodeNamedEntityBase
        where T : ParentedCodeNamedEntityBase<T> 
    {
#nullable enable
        public int? ParentId { get; set; }
        public virtual T Parent { get; set; }
        public virtual IEnumerable<T> Childs { get; set; }
    }
}