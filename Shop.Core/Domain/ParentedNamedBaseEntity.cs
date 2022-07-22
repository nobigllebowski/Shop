using System.Collections.Generic;

namespace Shop.Core.Domain
{
    /// <summary>
    /// Базовый класс для связи с Parent
    /// </summary>
    public abstract class ParentedNamedBaseEntity : NamedBaseEntity
    {

    }

    /// <summary>
    /// Базовый класс для связи с Parent
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ParentedNamedBaseEntity<T> : ParentedNamedBaseEntity
        where T : ParentedNamedBaseEntity<T>
    {
#nullable enable
        public int? ParentId { get; set; }
        public virtual T Parent { get; set; }
        public virtual IEnumerable<T> Childs { get; set; }
    }
}