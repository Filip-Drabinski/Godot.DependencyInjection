namespace Godot.DependencyInjection.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when multiple instances of DependencyInjectionObserver are created.
    /// </summary>
    [Serializable]
    public class DependencyInjectionObserverDuplicationException : Exception
    {
        /// <summary>
        /// Throws a <see cref="DependencyInjectionObserverDuplicationException"/> if the specified condition is true.
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        public static void ThrowIf(bool condition)
        {
            if (!condition)
            {
                return;
            }
            throw new DependencyInjectionObserverDuplicationException();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyInjectionObserverDuplicationException"/> class.
        /// </summary>
        public DependencyInjectionObserverDuplicationException() : base("Only one instance of DependencyInjectionObserver supported") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyInjectionObserverDuplicationException"/> class with a specified inner exception.
        /// </summary>
        /// <param name="inner">The inner exception.</param>
        public DependencyInjectionObserverDuplicationException(Exception inner) : base("Only one instance of DependencyInjectionObserver supported", inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyInjectionObserverDuplicationException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected DependencyInjectionObserverDuplicationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
