using Godot.DependencyInjection.Services;
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

        public static object? GetServicesArray(this IServiceProvider serviceProvider, Type parameterType)
        {
            var services = serviceProvider.GetServices(parameterType);

            var castMethod = genericCastMethod.MakeGenericMethod(parameterType);
            var castedEnumerable = castMethod.Invoke(null, new object[] { services })!;

            var toArrayMethod = genericToArrayMethod.MakeGenericMethod(parameterType);
            var result = toArrayMethod.Invoke(null, new object[] { castedEnumerable });
            return result;
        }
        public static object? GetServicesEnumerable(this IServiceProvider serviceProvider, Type parameterType)
        {
            var services = serviceProvider.GetServices(parameterType);

            var castMethod = genericCastMethod.MakeGenericMethod(parameterType);
            var result = castMethod.Invoke(null, new object[] { services })!;

            return result;
        }
        public static object? GetProvidedArray(this IServiceProvider serviceProvider, Type parameterType)
        {
            var provided = serviceProvider.GetAllProvided(parameterType);

            var castMethod = genericCastMethod.MakeGenericMethod(parameterType);
            var castedEnumerable = castMethod.Invoke(null, new object[] { provided })!;

            var toArrayMethod = genericToArrayMethod.MakeGenericMethod(parameterType);
            var result = toArrayMethod.Invoke(null, new object[] { castedEnumerable });
            return result;
        }

        public static object? GetProvidedEnumerable(this IServiceProvider serviceProvider, Type parameterType)
        {
            var provided = serviceProvider.GetAllProvided(parameterType);

            var castMethod = genericCastMethod.MakeGenericMethod(parameterType);
            var result = castMethod.Invoke(null, new object[] { provided })!;

            return result;
        }

        public static object? GetProvided(this IServiceProvider serviceProvider, Type parameterType)
        {
            var allProvided = serviceProvider.GetAllProvided(parameterType);
            var provided = allProvided.FirstOrDefault();
            return provided;
        }
        public static object GetRequiredProvided(this IServiceProvider serviceProvider, Type parameterType)
        {
            var allProvided = serviceProvider.GetAllProvided(parameterType);
            var provided = allProvided.FirstOrDefault();
            if (provided is null)
            {
                throw new Exception($"Required provided value `{parameterType.FullName}` not found");
            }
            return provided;
        }

        private static IEnumerable<object> GetAllProvided(this IServiceProvider serviceProvider, Type parameterType)
        {
            var providerType = typeof(INodeProvider<>).MakeGenericType(parameterType);
            var managerType = typeof(IProviderManager<,>).MakeGenericType(providerType, parameterType);
            var service = serviceProvider.GetService(managerType)!;
            var providerManager = (IProviderManager)service;
            var provided = providerManager.ProvideAsObjects();
            return provided;
        }
    }
}
