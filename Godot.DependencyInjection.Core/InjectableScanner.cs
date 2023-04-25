using Godot.DependencyInjection.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Xml.Linq;
using static Godot.HttpClient;

namespace Godot.DependencyInjection;


internal static class InjectableScanner
{
    public static Dictionary<Type, InjectionMetadata> CollectMetadata()
    {
        var existingTypes = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(x => x.GetTypes());

        var relevantTypes = existingTypes.Where(
            x => x.GetMethods().Any(y => y.GetCustomAttribute<InjectAttribute>() is not null)
            || x.GetProperties().Any(y => y.GetCustomAttribute<InjectAttribute>() is not null)
            || x.GetFields().Any(y => y.GetCustomAttribute<InjectAttribute>() is not null)
        );
        var metadataDictionary = new Dictionary<Type, InjectionMetadata>();
        foreach (var type in relevantTypes)
        {
            var metadata = ProcessType(type);
            metadataDictionary.Add(type, metadata);
        }
        return metadataDictionary;
    }

    private static InjectionMetadata ProcessType(Type type)
    {
        var methodsMetadata = GetMethodsMetadata(type);
        var membersMetadata = GetMembersMetadata(type);

        var result = new InjectionMetadata(membersMetadata, methodsMetadata);

        return result;
    }

    #region Methods

    private static MethodMetadata[] GetMethodsMetadata(Type type)
    {
        var methods = type.GetMethods();
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
        var methodParametersMetadata = new MethodParameterMetadata[parameters.Length];
        for (int i = 0; i < parameters.Length; i++)
        {
            var isRequired = parameters[i].CustomAttributes.Any(x => x.AttributeType == requiredAttributeType);
            methodParametersMetadata[i] = new MethodParameterMetadata(isRequired, parameters[i].ParameterType);
        }
        var metadata = new MethodMetadata(method, methodParametersMetadata);
        return metadata;
    }
    #endregion
    #region Members


    private static MemberMetadata[] GetMembersMetadata(Type type)
    {
        var properties = type.GetProperties();
        var fields = type.GetFields();

        if (properties.Length == 0 && fields.Length == 0)
        {
            return Array.Empty<MemberMetadata>();
        }
        else if (properties.Length != 0 && fields.Length == 0)
        {
            var propertiesMetadata = ProcessPropertiesOnly(properties);
            return propertiesMetadata;
        }
        if (properties.Length == 0 && fields.Length != 0)
        {
            var fieldsMetadata = ProcessFieldsOnly(properties, fields);
            return fieldsMetadata;
        }
        var result = ProcessPropertiesAndFields(properties, fields);
        return result;
    }

    private static MemberMetadata[] ProcessPropertiesAndFields(PropertyInfo[] properties, FieldInfo[] fields)
    {
        var membersMetadata = new List<MemberMetadata>(properties.Length + fields.Length);

        for (int i = 0; i < properties.Length; i++)
        {
            var attribute = properties[i].GetCustomAttribute<InjectAttribute>();
            if (attribute is null)
            {
                continue;
            }
            var memberMetadata = new MemberMetadata(properties[i].PropertyType, properties[i].SetValue, attribute.IsRequired);
            membersMetadata.Add(memberMetadata);
        }

        for (int i = 0; i < fields.Length; i++)
        {
            var attribute = properties[i].GetCustomAttribute<InjectAttribute>();
            if (attribute is null)
            {
                continue;
            }
            var memberMetadata = new MemberMetadata(fields[i].FieldType, fields[i].SetValue, attribute.IsRequired);
            membersMetadata.Add(memberMetadata);
        }
        return membersMetadata.ToArray();
    }

    private static MemberMetadata[] ProcessPropertiesOnly(PropertyInfo[] properties)
    {
        var result = new MemberMetadata[properties.Length];
        for (int i = 0; i < properties.Length; i++)
        {
            var attribute = properties[i].GetCustomAttribute<InjectAttribute>();
            if (attribute is null)
            {
                continue;
            }
            var memberMetadata = new MemberMetadata(properties[i].PropertyType, properties[i].SetValue, attribute.IsRequired);
            result[i] = memberMetadata;
        }

        return result;
    }

    private static MemberMetadata[] ProcessFieldsOnly(PropertyInfo[] properties, FieldInfo[] fields)
    {
        var result = new MemberMetadata[fields.Length];
        for (int i = 0; i < fields.Length; i++)
        {
            var attribute = properties[i].GetCustomAttribute<InjectAttribute>();
            if (attribute is null)
            {
                continue;
            }
            var memberMetadata = new MemberMetadata(fields[i].FieldType, fields[i].SetValue, attribute.IsRequired);
            result[i] = memberMetadata;
        }

        return result;
    }
    #endregion

    internal struct InjectionMetadata
    {
        public MemberMetadata[] members;
        public MethodMetadata[] methods;
        // TODO: implement nested injection cache
        //public NestedInjectionMetadata[] nestedInjections;


        public InjectionMetadata(MemberMetadata[] members, MethodMetadata[] methods)
        {
            this.members = members;
            this.methods = methods;
        }

        public void Inject(IServiceProvider serviceProvider, object instance)
        {
            for (int i = 0; i < members.Length; i++)
            {
                members[i].Inject(serviceProvider, instance);
            }
            for (int i = 0; i < methods.Length; i++)
            {
                methods[i].Inject(serviceProvider, instance);
            }
        }
    }
    // TODO: implement nested injection cache
    //internal struct NestedInjectionMetadata
    //{
    //    public delegate object? MemberGetter(object? instance);
    //    public Type nestedType;
    //    public MemberGetter memberGetter;

    //    public NestedInjectionMetadata(Type nestedType, MemberGetter memberGetter)
    //    {
    //        this.nestedType = nestedType;
    //        this.memberGetter = memberGetter;
    //    }
    //}
    internal struct MemberMetadata
    {
        public delegate void MemberSetter(object? instance, object? value);
        public Type serviceType;
        public MemberSetter memberSetter;
        public bool isRequired;

        public MemberMetadata(Type serviceType, MemberSetter memberSetter, bool isRequired)
        {
            this.serviceType = serviceType;
            this.memberSetter = memberSetter;
            this.isRequired = isRequired;
        }

        internal void Inject(IServiceProvider serviceProvider, object instance)
        {
            var service = isRequired
                ? serviceProvider.GetRequiredService(serviceType)
                : serviceProvider.GetService(serviceType);
            memberSetter.Invoke(instance, service);
        }
    }
    internal struct MethodMetadata
    {
        public MethodInfo methodInfo;
        public MethodParameterMetadata[] parameters;

        public MethodMetadata(MethodInfo methodInfo, MethodParameterMetadata[] parameters)
        {
            this.methodInfo = methodInfo;
            this.parameters = parameters;
        }

        internal void Inject(IServiceProvider serviceProvider, object instance)
        {
            object?[] parametersValue = new object?[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                parametersValue[i] = parameters[i].GetService(serviceProvider);
            }
            methodInfo.Invoke(instance, parametersValue);
        }
    }
    internal struct MethodParameterMetadata
    {
        public bool isRequired;
        public Type parameterType;

        public MethodParameterMetadata(bool isRequired, Type parameterType)
        {
            this.isRequired = isRequired;
            this.parameterType = parameterType;
        }
        public object? GetService(IServiceProvider serviceProvider)
        {
            var service = isRequired
                ? serviceProvider.GetRequiredService(parameterType)
                : serviceProvider.GetService(parameterType);

            return service;
        }
    }
}
