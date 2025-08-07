using System;
using System.Globalization;
using System.Security.Permissions;
using System.Runtime.Serialization;

namespace CITray
{
    /// <summary>
    /// This exception is thrown by the <see cref="CITray.ServiceExtensions.GetService&lt;T&gt;(System.IServiceProvider)"/>
    /// method, when specifying the <b>mandatory</b> parameter and when the requested service is not found.
    /// </summary>
    [Serializable]
    public class ServiceNotFoundException : Exception
    {
        /// <summary>Service type that wasn't found.</summary>
        private Type serviceType;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceNotFoundException"/> class.
        /// </summary>
        public ServiceNotFoundException() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ServiceNotFoundException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ServiceNotFoundException(string message, Exception innerException) : 
            base(message, innerException) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceNotFoundException&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="type">The type of the service that couldn't be found.</param>
        public ServiceNotFoundException(Type type) :
            base(BuildMessage(type, string.Empty)) { serviceType = type; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceNotFoundException&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="type">The type of the service that couldn't be found.</param>
        /// <param name="innerException">The inner exception.</param>
        public ServiceNotFoundException(Type type, Exception innerException) :
            base(BuildMessage(type, string.Empty), innerException) { serviceType = type; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceNotFoundException&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="type">The type of the service that couldn't be found.</param>
        /// <param name="message">The message.</param>
        public ServiceNotFoundException(Type type, string message) :
            base(BuildMessage(type, message)) { serviceType = type; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceNotFoundException&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="type">The type of the service that couldn't be found.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ServiceNotFoundException(Type type, string message, Exception innerException) :
            base(BuildMessage(type, message), innerException) { serviceType = type; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceNotFoundException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is null. </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). </exception>
        protected ServiceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        
        /// <summary>Gets or sets the type of the service that couldn't be found.</summary>        
        /// <value>The type of the service.</value>
        public Type ServiceType 
        { 
            get { return serviceType; }
            set { serviceType = value; }
        }

        /// <summary>
        /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="info"/> parameter is a null reference (Nothing in Visual Basic).
        /// </exception>
        /// <PermissionSet>
        /// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*"/>
        /// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter"/>
        /// </PermissionSet>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException("info");
            info.AddValue("ServiceType", serviceType);
            base.GetObjectData(info, context);
        }

        /// <summary>
        /// Builds the exception message based on the specified service type and error message.
        /// </summary>
        /// <param name="type">The service type.</param>
        /// <param name="message">The error message.</param>
        /// <returns>The exception message.</returns>
        private static string BuildMessage(Type type, string message)
        {
            return string.IsNullOrEmpty(message) ?
                string.Format(CultureInfo.CurrentCulture, SR.ServiceNotFoundDefaultMessage, type) :
                string.Format(CultureInfo.CurrentCulture, SR.ServiceNotFoundDefaultMessage2, type, message);
        }
    }

    /// <summary>
    /// Generic version of <see cref="ServiceNotFoundException"/>.
    /// </summary>
    /// <typeparam name="T">Type du service non trouvé.</typeparam>
    [Serializable]
    public sealed class ServiceNotFoundException<T> : ServiceNotFoundException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceNotFoundException&lt;T&gt;"/> class.
        /// </summary>
        public ServiceNotFoundException() : base(typeof(T)) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceNotFoundException&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        public ServiceNotFoundException(Exception innerException) : base(typeof(T), innerException) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ServiceNotFoundException(string message) : base(typeof(T), message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ServiceNotFoundException(string message, Exception innerException) :
            base(typeof(T), message, innerException) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceNotFoundException&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is null. </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). </exception>
        private ServiceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
