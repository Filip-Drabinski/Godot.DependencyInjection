namespace Godot.DependencyInjection.Exceptions;

[Serializable]
public class DependencyInjectionObserverDuplicationException : Exception
{
    public static void ThrowIf(bool condition)
    {
        if (!condition)
        {
            return;
        }
        throw new DependencyInjectionObserverDuplicationException();
    }
    public DependencyInjectionObserverDuplicationException() : base("Only one instance of DependencyInjectionObserer supported") { }
    public DependencyInjectionObserverDuplicationException(Exception inner) : base("Only one instance of DependencyInjectionObserer supported", inner) { }
    protected DependencyInjectionObserverDuplicationException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
