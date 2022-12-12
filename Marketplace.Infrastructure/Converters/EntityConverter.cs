using AutoMapper;
using AutoMapper.Internal;
using Marketplace.Contracts;
using System;
using System.Linq;
using System.Reflection;

namespace Marketplace.Infrastructure.Converters
{
    /// <summary>
    /// Represents class for entities' conversions.
    /// </summary>
    public class EntityConverter : IEntityConverter
    {
        public static Action<IMapperConfigurationExpression> GetDefaultMapperConfiguration(string[] mappingContainerAssemblyNames)
        {
            var assemblies = mappingContainerAssemblyNames.Select(assemblyName => Assembly.Load(assemblyName)).ToArray();
            return GetDefaultMapperConfiguration(assemblies);
        }

        public static Action<IMapperConfigurationExpression> GetDefaultMapperConfiguration(Assembly[] mappingContainerAssemblies)
        {
            return cfg => cfg.AddMaps(mappingContainerAssemblies);
        }

        private readonly IMapper _defaultMapper;
        private readonly IMapper _mergeMapper;
        private readonly MapperConfiguration _defaultCofiguration;

        public EntityConverter(Action<IMapperConfigurationExpression> configExpression, Func<Type, object> serviceCtor, bool skipMappingsValidation = false)
        {
            _defaultCofiguration = new MapperConfiguration(configExpression);
            if (!skipMappingsValidation)
            {
                _defaultCofiguration.AssertConfigurationIsValid();
            }

            _defaultMapper = _defaultCofiguration.CreateMapper(serviceCtor);
            _mergeMapper = CreateMergeMapper(configExpression, serviceCtor);
        }

        internal IMapper CreateMergeMapper(Action<IMapperConfigurationExpression> configExpression, Func<Type, object> serviceCtor)
        {
            var ignoreDefaultValuesConfiguration = new MapperConfiguration((cfg) =>
            {
                configExpression(cfg);
                cfg.Internal().ForAllPropertyMaps(pm => true, (pm, opt) =>
                {
                    //if value is null or value is not nullable type (e.g. value type)
                    if (pm.SourceMember == null ||
                       pm.SourceMember.MemberType == MemberTypes.Property
                       && (pm.SourceMember as PropertyInfo).PropertyType.IsValueType
                       && Nullable.GetUnderlyingType((pm.SourceMember as PropertyInfo).PropertyType) == null)
                    {
                        opt.Ignore();
                    }
                    else
                    {
                        opt.MapFrom(new IgnoreNullResolver(), pm.SourceMember.Name);
                    }
                });
            });

            return ignoreDefaultValuesConfiguration.CreateMapper(serviceCtor);
        }

        class IgnoreNullResolver : IMemberValueResolver<object, object, object, object>
        {
            public object Resolve(object source, object destination, object sourceMember, object destMember, ResolutionContext context)
            {
                return sourceMember ?? destMember;
            }
        }

        /// <summary>
        /// Assigns properties of one object to another by registered mapping configuration.
        /// </summary>
        /// <typeparam name="TIn">Type of object to be assigned from.</typeparam>
        /// <typeparam name="TOut">Type of object to be assigned to.</typeparam>
        /// <param name="source">Object to be assigned from.</param>
        /// <param name="destination">Object to be assigned to.</param>
        public void AssignTo<TIn, TOut>(TIn source, ref TOut destination)
        {
            destination = _defaultMapper.Map(source, destination);
        }

        /// <summary>
        /// Converts passed object to the specified type.
        /// </summary>
        /// <typeparam name="TIn">Type of object to be converted from.</typeparam>
        /// <typeparam name="TOut">Type of the output object.</typeparam>
        /// <param name="source">Object to be converted from.</param>
        /// <returns>Converted object.</returns>
        public TOut ConvertTo<TIn, TOut>(TIn source)
        {
            return _defaultMapper.Map<TOut>(source);
        }

        public void Merge<TIn, TOut>(TIn source, TOut destination)
        {
            _defaultMapper.Map(source, destination);
        }

        public T Copy<T>(T source)
        {
            return _defaultMapper.Map<T>(source);
        }
    }
}
