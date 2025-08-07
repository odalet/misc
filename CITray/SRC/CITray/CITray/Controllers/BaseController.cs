using System;

namespace CITray.Controllers
{
    internal abstract class BaseController : IServiceProvider
    {
        private IServiceProvider services = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public BaseController(IServiceProvider serviceProvider)
        {
            services = serviceProvider ?? This.Services;
        }

        /// <summary>
        /// Gets the services container.
        /// </summary>
        /// <value>The services.</value>
        protected IServiceProvider Services
        {
            get { return services; }
        }

        #region IServiceProvider Members

        /// <summary>
        /// Gets the service object of the specified type.
        /// </summary>
        /// <param name="serviceType">An object that specifies the type of service object to get.</param>
        /// <returns>
        /// A service object of type <paramref name="serviceType"/>.
        /// -or-
        /// null if there is no service object of type <paramref name="serviceType"/>.
        /// </returns>
        public object GetService(Type serviceType)
        {
            return services.GetService(serviceType);
        }

        #endregion

        protected T GetService<T>() where T : class
        {
            return services.GetService<T>();
        }

        protected T GetService<T>(bool mandatory) where T : class
        {
            return services.GetService<T>(mandatory);
        }
    }
}
