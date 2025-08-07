using System;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;

namespace Delta.Ioc
{
    public class ServiceRegistry : ServiceLocatorImplBase, IServiceRegistry
    {
        private class InstanceDescriptor
        {
            public InstanceDescriptor(Func<object> factory)
            {
                Instance = new Lazy<object>(factory);
            }

            Lazy<object> Instance { get; private set; }
        }

        private readonly Dictionary<Type, Type> typeMappings = new Dictionary<Type, Type>();
        private readonly Dictionary<Type, Dictionary<string, InstanceDescriptor>> registry = 
            new Dictionary<Type, Dictionary<string, InstanceDescriptor>>();

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            throw new NotImplementedException();
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            throw new NotImplementedException();
        }
    }
}
