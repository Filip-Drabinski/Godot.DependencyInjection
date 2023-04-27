using Godot.DependencyInjection.Scanning.Models.MethodParameterMetadata;

namespace Godot.DependencyInjection.Scanning.Models.MethodParameter
{
    internal static class MethodParameterMetadataFactory
    {
        public static IMethodParameterMetadata CreateMethodParameterMetadata(bool isRequired, Type parameterType)
        {
            if (parameterType.IsArray)
            {
                var elementType = parameterType.GetElementType()!;

                return new MethodArrayParameterMetadata(elementType);
            }
            if (parameterType.IsGenericType && parameterType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                var elementType = parameterType.GetGenericArguments()[0];

                return new MethodEnumerableParameterMetadata(elementType);
            }

            return new MethodParameterMetadata(isRequired, parameterType);
        }
    }
}
