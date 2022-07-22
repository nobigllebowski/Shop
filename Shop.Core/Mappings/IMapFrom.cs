using AutoMapper;

namespace Shop.Core.Mappings
{
    /// <summary>
    /// Создание двухстороннего маппинга с сущностью
    /// </summary>
    public interface IMapFrom<T1, T2, T3>
    {
        /// <summary>
        /// Метод создания конфигурации маппинга
        /// </summary>
        void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(T1), GetType()).ReverseMap();
            profile.CreateMap(typeof(T2), GetType()).ReverseMap();
            profile.CreateMap(typeof(T3), GetType()).ReverseMap();
        }
    }

    /// <summary>
    /// Создание двухстороннего маппинга с сущностью
    /// </summary>
    public interface IMapFrom<T1, T2>
    {
        /// <summary>
        /// Метод создания конфигурации маппинга
        /// </summary>
        void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(T1), GetType()).ReverseMap();
            profile.CreateMap(typeof(T2), GetType()).ReverseMap();
        }
    }

    /// <summary>
    /// Создание двухстороннего маппинга с сущностью
    /// </summary>
    /// <typeparam name="T"> Тип базовой сущности </typeparam>
    public interface IMapFrom<T>
    {
        /// <summary>
        /// Метод создания конфигурации маппинга
        /// </summary>
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType()).ReverseMap();
    }

    /// <summary>
    /// Создание маппинга с сущностью
    /// </summary>
    public interface IMapFrom
    {
        /// <summary>
        /// Метод создания конфигурации маппинга
        /// </summary>
        void Mapping(Profile profile);
    }
}