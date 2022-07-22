using System;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace Shop.Core.Mappings
{
    /// <summary>
    /// Автоматическикое создание маппинга для наследников IMapFrom
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(AppDomain.CurrentDomain.GetAssemblies());
        }

        private void ApplyMappingsFromAssembly(Assembly[] assemblies)
        {
            var types = assemblies.Where(x => !x.IsDynamic).SelectMany(assembly => assembly.GetExportedTypes().Where(t =>
                t.GetInterfaces().Any(i => 
                    i.IsGenericType && 
                    (
                        i.GetGenericTypeDefinition() == typeof(IMapFrom<>) ||
                        i.GetGenericTypeDefinition() == typeof(IMapFrom<,>) ||
                        i.GetGenericTypeDefinition() == typeof(IMapFrom<,,>)
                    ) ||
                    i == typeof(IMapFrom)) &&
                t.IsClass && !t.IsAbstract).ToList());
            
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping")
                                 ?? type.GetInterface("IMapFrom`1")?.GetMethod("Mapping")
                                 ?? type.GetInterface("IMapFrom`2")?.GetMethod("Mapping")
                                 ?? type.GetInterface("IMapFrom`3")?.GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] {this});
            }
        }
    }
}