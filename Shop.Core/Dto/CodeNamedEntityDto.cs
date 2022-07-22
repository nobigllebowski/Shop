using AutoMapper;
using Shop.Core.Domain;

namespace Shop.Core.Dto
{
    /// <summary>
    /// Dto с базовыми свойствами Код, Наименование, ИД
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    public abstract class CodeNamedEntityDto<TEntity> : IBaseEntityDto<TEntity> where TEntity : CodeNamedBaseEntity
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(TEntity), GetType()).ReverseMap();
        }
    }
}