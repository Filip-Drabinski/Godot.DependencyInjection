using Godot.DependencyInjection.Attributes;
using Godot.DependencyInjection.Scanning.Models;
using Godot.DependencyInjection.Scanning.Models.Member;
using Godot.DependencyInjection.Scanning.Models.MethodParameter;
using Godot.DependencyInjection.Scanning.Models.MethodParameterMetadata;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Godot.DependencyInjection.Scanning;

internal static class InjectionScanner
{
    public static Dictionary<Type, InjectionMetadata> CollectMetadata(Assembly[] assemblies)
    {
        var metadataDictionary = new Dictionary<Type, InjectionMetadata>();
        for (int assemblyIndex = 0; assemblyIndex < assemblies.Length; assemblyIndex++)
        {
            var allTypes = assemblies[assemblyIndex].GetTypes();
            for (int typeIndex = 0; typeIndex < allTypes.Length; typeIndex++)
            {
                var metadata = ProcessType(allTypes[typeIndex]);
                if (!metadata.HasValue)
                {
                    continue;
                }
                metadataDictionary.Add(allTypes[typeIndex], metadata.Value);
            }
        }
        return metadataDictionary;
    }

    private static InjectionMetadata? ProcessType(Type type)
    {
        var methods = type.GetMethods(BindingFlags.Instance  | BindingFlags.Public | BindingFlags.NonPublic);
        var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        var fields = type.GetFields(BindingFlags.Instance  | BindingFlags.Public | BindingFlags.NonPublic);

        var methodsMetadata = GetMethodsMetadata(methods);
        var (membersMetadata, nestedInjections) = GetMembersMetadata(properties, fields);

        if (
            methodsMetadata.Length == 0
            && membersMetadata.Length == 0
            && nestedInjections.Length == 0
            )
        {
            return null;
        }
        var result = new InjectionMetadata(membersMetadata, methodsMetadata, nestedInjections);

        return result;
    }

    #region Methods
    private static MethodMetadata[] GetMethodsMetadata(MethodInfo[] methods)
    {
        if (methods.Length == 0)
        {
            return Array.Empty<MethodMetadata>();
        }
        var methodsMetadata = new List<MethodMetadata>(methods.Length);
        Type requiredAttributeType = typeof(RequiredAttribute);
        for (int i = 0; i < methods.Length; i++)
        {
            if (methods[i].GetCustomAttribute<InjectAttribute>() is null)
            {
                continue;
            }

            var metadata = ProcessMethod(methods[i], requiredAttributeType);
            methodsMetadata.Add(metadata);
        }
        return methodsMetadata.ToArray();
    }

    private static MethodMetadata ProcessMethod(MethodInfo method, Type requiredAttributeType)
    {
        var parameters = method.GetParameters();
        var methodParametersMetadata = new IMethodParameterMetadata[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            var isRequired = parameters[i].CustomAttributes.Any(x => x.AttributeType == requiredAttributeType);
            methodParametersMetadata[i] = MethodParameterMetadataFactory.CreateMethodParameterMetadata(isRequired, parameters[i].ParameterType);
        }

        var metadata = new MethodMetadata(method, methodParametersMetadata);
        return metadata;
    }
    #endregion
    
    #region Members
    private static (IMemberMetadata[], NestedInjectionMetadata[]) GetMembersMetadata(PropertyInfo[] properties, FieldInfo[] fields)
    {

        if (properties.Length == 0 && fields.Length == 0)
        {
            return (Array.Empty<IMemberMetadata>(), Array.Empty<NestedInjectionMetadata>());
        }
        else if (properties.Length != 0 && fields.Length == 0)
        {
            var propertiesResult = ProcessPropertiesOnly(properties);
            return propertiesResult;
        }
        if (properties.Length == 0 && fields.Length != 0)
        {
            var fieldsResult = ProcessFieldsOnly(fields);
            return fieldsResult;
        }
        var result = ProcessPropertiesAndFields(properties, fields);
        return result;
    }

    private static (IMemberMetadata[], NestedInjectionMetadata[]) ProcessPropertiesAndFields(PropertyInfo[] properties, FieldInfo[] fields)
    {
        var membersMetadata = new List<IMemberMetadata>(properties.Length + fields.Length);
        var nestedInjectionMetadata = new List<NestedInjectionMetadata>(properties.Length + fields.Length);

        for (int i = 0; i < properties.Length; i++)
        {
            var memberAttribute = properties[i].GetCustomAttribute<InjectAttribute>();
            var nestedAttribute = properties[i].GetCustomAttribute<InjectMembersAttribute>();

            if (memberAttribute is not null)
            {
                var memberMetadata = MemberMetadataFactory.CreateMemberMetadata(properties[i].PropertyType, properties[i].SetValue, memberAttribute.IsRequired);
                membersMetadata.Add(memberMetadata);
            }


            if (nestedAttribute is not null)
            {
                var nestedMetadata = new NestedInjectionMetadata(properties[i].PropertyType, properties[i].GetValue);
                nestedInjectionMetadata.Add(nestedMetadata);
            }
        }

        for (int i = 0; i < fields.Length; i++)
        {
            var memberAttribute = fields[i].GetCustomAttribute<InjectAttribute>();
            var nestedAttribute = fields[i].GetCustomAttribute<InjectMembersAttribute>();

            if (memberAttribute is not null)
            {
                var memberMetadata = MemberMetadataFactory.CreateMemberMetadata(fields[i].FieldType, fields[i].SetValue, memberAttribute.IsRequired);
                membersMetadata.Add(memberMetadata);
            }
            if (nestedAttribute is not null)
            {
                var nestedMetadata = new NestedInjectionMetadata(fields[i].FieldType, fields[i].GetValue);
                nestedInjectionMetadata.Add(nestedMetadata);
            }
        }
        return (membersMetadata.ToArray(), nestedInjectionMetadata.ToArray());
    }

    private static (IMemberMetadata[], NestedInjectionMetadata[]) ProcessPropertiesOnly(PropertyInfo[] properties)
    {
        var membersMetadata = new List<IMemberMetadata>(properties.Length);
        var nestedInjectionMetadata = new List<NestedInjectionMetadata>(properties.Length);

        for (int i = 0; i < properties.Length; i++)
        {
            var memberAttribute = properties[i].GetCustomAttribute<InjectAttribute>();
            var nestedAttribute = properties[i].GetCustomAttribute<InjectMembersAttribute>();

            if (memberAttribute is not null)
            {
                var memberMetadata = MemberMetadataFactory.CreateMemberMetadata(properties[i].PropertyType, properties[i].SetValue, memberAttribute.IsRequired);
                membersMetadata.Add(memberMetadata);
            }
            if (nestedAttribute is not null)
            {
                var nestedMetadata = new NestedInjectionMetadata(properties[i].PropertyType, properties[i].GetValue);
                nestedInjectionMetadata.Add(nestedMetadata);
            }
        }

        return (membersMetadata.ToArray(), nestedInjectionMetadata.ToArray());
    }

    private static (IMemberMetadata[], NestedInjectionMetadata[]) ProcessFieldsOnly(FieldInfo[] fields)
    {
        var membersMetadata = new List<IMemberMetadata>(fields.Length);
        var nestedInjectionMetadata = new List<NestedInjectionMetadata>(fields.Length);

        for (int i = 0; i < fields.Length; i++)
        {
            var memberAttribute = fields[i].GetCustomAttribute<InjectAttribute>();
            var nestedAttribute = fields[i].GetCustomAttribute<InjectMembersAttribute>();

            if (memberAttribute is not null)
            {
                var memberMetadata = MemberMetadataFactory.CreateMemberMetadata(fields[i].FieldType, fields[i].SetValue, memberAttribute.IsRequired);
                membersMetadata.Add(memberMetadata);
            }
            if (nestedAttribute is not null)
            {
                var nestedMetadata = new NestedInjectionMetadata(fields[i].FieldType, fields[i].GetValue);
                nestedInjectionMetadata.Add(nestedMetadata);
            }
        }

        return (membersMetadata.ToArray(), nestedInjectionMetadata.ToArray());
    }

    #endregion
}
