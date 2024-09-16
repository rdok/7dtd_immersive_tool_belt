using System;
using System.Collections.Generic;

namespace ImmersiveToolBelt.Harmony
{
    public static class ServiceRegistry
    {
        private static readonly Dictionary<Type, Delegate> _factories = new Dictionary<Type, Delegate>();

        // Register a service factory with zero or many constructor arguments
        public static void Add<TService>(Func<object[], TService> provider)
        {
            _factories[typeof(TService)] = provider;
        }

        // Resolve a service by passing zero or many arguments
        public static TService Resolve<TService>(params object[] args)
        {
            if (_factories.TryGetValue(typeof(TService), out var provider))
            {
                return ((Func<object[], TService>)provider)(args);
            }

            throw new Exception($"Service {typeof(TService)} not registered");
        }
    }
}