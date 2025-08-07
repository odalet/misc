using System;
using System.ComponentModel.Design;

namespace Delta.Ioc
{
    /// <summary>
    /// Extension method allowing to use generic forms of <c>GetService</c> on <see cref="IServiceProvider"/> instances,
    /// and generic forms of <c>AddService</c> and <c>RemoveService</c> on <see cref="IServiceContainer"/> instances.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Gets the service object of the specified type.
        /// </summary>
        /// <typeparam name="T">The type if the service to retrieve.</typeparam>
        /// <param name="serviceProvider">The service provider in which to search for the service instance.</param>
        /// <returns>
        /// A service object of type <typeparamref name="T"/>.
        /// </returns>
        public static T GetService<T>(this IServiceProvider serviceProvider)
        {
            return (T)serviceProvider.GetService(typeof(T));
        }

        public static void AddService<T>(this IServiceContainer serviceContainer, Func<T> serviceCreator) where T : class
        {
            serviceContainer.AddService(typeof(T), new ServiceCreatorCallback((c, t) => serviceCreator()));
        }

        public static void AddService<T>(this IServiceContainer serviceContainer, Func<T> serviceCreator, bool promote) where T : class
        {
            serviceContainer.AddService(typeof(T), new ServiceCreatorCallback((c, t) => serviceCreator()), promote);
        }

        public static void AddService<T>(this IServiceContainer serviceContainer, ServiceCreatorCallback callback) where T : class
        {
            serviceContainer.AddService(typeof(T), callback);
        }

        public static void AddService<T>(this IServiceContainer serviceContainer, ServiceCreatorCallback callback, bool promote) where T : class
        {
            serviceContainer.AddService(typeof(T), callback, promote);
        }

        public static void AddService<T>(this IServiceContainer serviceContainer, T serviceInstance) where T : class
        {
            serviceContainer.AddService(typeof(T), serviceInstance);
        }

        public static void AddService<T>(this IServiceContainer serviceContainer, T serviceInstance, bool promote) where T : class
        {
            serviceContainer.AddService(typeof(T), serviceInstance, promote);
        }

        public static void RemoveService<T>(this IServiceContainer serviceContainer, bool promote) where T : class
        {
            serviceContainer.RemoveService(typeof(T), promote);
        }

        public static void RemoveService<T>(this IServiceContainer serviceContainer) where T : class
        {
            serviceContainer.RemoveService(typeof(T));
        }
    }
}
