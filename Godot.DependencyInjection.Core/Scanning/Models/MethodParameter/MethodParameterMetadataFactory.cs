using Godot.DependencyInjection.Scanning.Models.MethodParameterMetadata;

namespace Godot.DependencyInjection.Scanning.Models.MethodParameter
{
    internal static class MethodParameterMetadataFactory
    {
        public static IMethodParameterMetadata CreateMethodParameterMetadata(Type parameterType, bool isRequired, bool isProvided)
        {
            if (parameterType.IsArray)
            {
                var elementType = parameterType.GetElementType()!;

                return new MethodArrayParameterMetadata(elementType, isProvided);
            }
            if (parameterType.IsGenericType && parameterType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                var elementType = parameterType.GetGenericArguments()[0];

                return new MethodEnumerableParameterMetadata(elementType, isProvided);
            }

            return new MethodParameterMetadata(parameterType, isRequired, isProvided);
        }
    }
}
