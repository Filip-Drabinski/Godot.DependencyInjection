using static Godot.DependencyInjection.Scanning.Models.Member.MemberMetadata;

namespace Godot.DependencyInjection.Scanning.Models.Member;

internal static class MemberMetadataFactory
{
    /// <summary>
    /// creates member metadata
    /// </summary>
    /// <param name="serviceType"></param>
    /// <param name="memberSetter"></param>
    /// <param name="isRequired"></param>
    /// <param name="isProvided"></param>
    /// <returns></returns>
    public static IMemberMetadata CreateMemberMetadata(Type serviceType, MemberSetter memberSetter, bool isRequired, bool isProvided)
    {
        if (serviceType.IsArray)
        {
            var elementType = serviceType.GetElementType()!;

            return new MemberArrayMetadata(elementType, memberSetter,isProvided);
        }
        else if (serviceType.IsGenericType && serviceType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
        {
            var elementType = serviceType.GetGenericArguments()[0];

            return new MemberEnumerableMetadata(elementType, memberSetter, isProvided);
        }

        return new MemberMetadata(serviceType, memberSetter, isRequired, isProvided);
    }
}
