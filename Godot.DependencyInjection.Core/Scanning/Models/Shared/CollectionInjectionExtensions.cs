using Microsoft.Extensions.DependencyInjection;

namespace Godot.DependencyInjection.Scanning.Models.Shared
{
    internal static class CollectionInjectionExtensions
    {
        private static readonly System.Reflection.MethodInfo genericCastMethod;
        private static readonly System.Reflection.MethodInfo genericToArrayMethod;
        static CollectionInjectionExtensions()
        {
            genericCastMethod = typeof(Enumerable).GetMethod(nameof(Enumerable.Cast))!;
            genericToArrayMethod = typeof(Enumerable).GetMethod(nameof(Enumerable.ToArray))!;
        }

        public static object? GetServicesArray(this IServiceProvider serviceProvider,Type parameterType)
        {
            var services = serviceProvider.GetServices(parameterType);

            var castMethod = genericCastMethod.MakeGenericMethod(parameterType);
            var castedEnumerable = castMethod.Invoke(null, new object[] { services })!;

            var toArrayMethod = genericToArrayMethod.MakeGenericMethod(parameterType);
            var result = toArrayMethod.Invoke(null, new object[] { castedEnumerable });
            return result;
        }
        public static object? GetServicesEnumerable(this IServiceProvider serviceProvider,Type parameterType)
        {
            var services = serviceProvider.GetServices(parameterType);

            var castMethod = genericCastMethod.MakeGenericMethod(parameterType);
            var result = castMethod.Invoke(null, new object[] { services })!;

            return result;
        }
    }
}
