using Shop.Core.Domain;
using Shop.Core.Mappings;

namespace Shop.Core.Dto
{
    /// <summary>
    /// Базовый интерфейс для Dto с маппингом
    /// </summary>
    /// <typeparam name="TEntity"> Тип сущности </typeparam>
    public interface IBaseEntityDto<TEntity> : IBaseEntityDto, IMapFrom<TEntity>
        where TEntity : class, IBaseEntity
    {
    }
}