namespace NServiceBus.StructureMap
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::StructureMap.Pipeline;
    using ObjectBuilder.Common;
    using Container = global::StructureMap.Container;

    class StructureMapObjectBuilder : IContainer
    {
        public StructureMapObjectBuilder() : this(new Container(), true)
        {
        }

        public StructureMapObjectBuilder(global::StructureMap.IContainer container) : this(container, false)
        {
        }

        public StructureMapObjectBuilder(global::StructureMap.IContainer container, bool owned)
        {
            this.owned = owned;
            this.container = container;
        }

        public void Dispose()
        {
            //Injected at compile time
        }

#pragma warning disable IDE0051 // Remove unused private members
        void DisposeManaged()
#pragma warning restore IDE0051 // Remove unused private members
        {
            if (!owned)
            {
                return;
            }

            container?.Dispose();
        }

        public IContainer BuildChildContainer()
        {
            return new StructureMapObjectBuilder(container.GetNestedContainer(), true);
        }

        public object Build(Type typeToBuild)
        {
            if (container.Model.PluginTypes.Any(t => t.PluginType == typeToBuild))
            {
                return container.GetInstance(typeToBuild);
            }

            throw new ArgumentException(typeToBuild + " is not registered in the container");
        }

        public IEnumerable<object> BuildAll(Type typeToBuild)
        {
            return container.GetAllInstances(typeToBuild).Cast<object>();
        }

        public void Configure(Type component, DependencyLifecycle dependencyLifecycle)
        {
            lock (configuredInstances)
            {
                if (configuredInstances.ContainsKey(component))
                {
                    return;
                }
            }

            var lifecycle = GetLifecycleFrom(dependencyLifecycle);

            ConfiguredInstance configuredInstance = null;

            container.Configure(x =>
            {
                configuredInstance = x.For(component)
                    .LifecycleIs(lifecycle)
                    .Use(component);

                x.EnableSetterInjectionFor(component);

                var interfaces = GetAllInterfacesImplementedBy(component).ToList();

                foreach (var implementedInterface in interfaces)
                {
                    x.For(implementedInterface)
                        .LifecycleIs(lifecycle)
                        .Use(c => c.GetInstance(component));

                    x.EnableSetterInjectionFor(implementedInterface);
                }
            });

            lock (configuredInstances)
            {
                configuredInstances.Add(component, configuredInstance);
            }
        }

        public void Configure<T>(Func<T> componentFactory, DependencyLifecycle dependencyLifecycle)
        {
            var pluginType = typeof(T);

            lock (configuredInstances)
            {
                if (configuredInstances.ContainsKey(pluginType))
                {
                    return;
                }
            }

            var lifecycle = GetLifecycleFrom(dependencyLifecycle);
            LambdaInstance<T, T> lambdaInstance = null;

            container.Configure(x =>
            {
                lambdaInstance = x.For<T>()
                    .LifecycleIs(lifecycle)
                    .Use("Custom constructor func", componentFactory);

                x.EnableSetterInjectionFor(pluginType);

                foreach (var implementedInterface in GetAllInterfacesImplementedBy(pluginType))
                {
                    x.For(implementedInterface).Use(c => c.GetInstance<T>());

                    x.EnableSetterInjectionFor(implementedInterface);
                }
            }
                );

            lock (configuredInstances)
            {
                configuredInstances.Add(pluginType, lambdaInstance);
            }
        }

        public void RegisterSingleton(Type lookupType, object instance)
        {
            container.Configure(x =>
            {
                x.For(lookupType)
                    .Singleton()
                    .Use(instance);

                x.EnableSetterInjectionFor(lookupType);
            });
        }

        public bool HasComponent(Type componentType)
        {
            return container.Model.PluginTypes.Any(t => t.PluginType == componentType);
        }

        public void Release(object instance)
        {
        }

        static ILifecycle GetLifecycleFrom(DependencyLifecycle dependencyLifecycle)
        {
            switch (dependencyLifecycle)
            {
                case DependencyLifecycle.InstancePerCall:
                    return new UniquePerRequestLifecycle();
                case DependencyLifecycle.SingleInstance:
                    return new SingletonLifecycle();
                case DependencyLifecycle.InstancePerUnitOfWork:
                    return new ContainerLifecycle();
                default:
                    throw new ArgumentException("Unhandled lifecycle - " + dependencyLifecycle);
            }
        }

        static IEnumerable<Type> GetAllInterfacesImplementedBy(Type t)
        {
            return t.GetInterfaces().Where(x => !x.FullName.StartsWith("System."));
        }

        global::StructureMap.IContainer container;
        Dictionary<Type, Instance> configuredInstances = new Dictionary<Type, Instance>();
        bool owned;
    }
}