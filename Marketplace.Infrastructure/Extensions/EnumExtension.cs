using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Marketplace.Infrastructure.Extensions
{
    public static class EnumExtension
    {
        public static string GetEnumMemberValue<T>(T value) where T : struct, IConvertible
        {
            return typeof(T)
                .GetTypeInfo()
                .DeclaredMembers
                .SingleOrDefault(x => x.Name == value.ToString())
                ?.GetCustomAttribute<EnumMemberAttribute>(false)
                ?.Value;
        }
    }
}
