using System;
using System.Collections.Generic;
using System.Linq;
using NServiceBus;
using StructureMap;
using StructureMap.Pipeline;
using IContainer = NServiceBus.ObjectBuilder.Common.IContainer;

class StructureMapObjectBuilder : IContainer
{
    public StructureMapObjectBuilder()
    {
        container = new Container();
    }

    public StructureMapObjectBuilder(StructureMap.IContainer container)
    {
        this.container = container;
    }

    public void Dispose()
    {
        //Injected at compile time
    }

    public IContainer BuildChildContainer()
    {
        return new StructureMapObjectBuilder(container.GetNestedContainer());
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
        }

        throw new ArgumentException("Unhandled lifecycle - " + dependencyLifecycle);
    }

    static IEnumerable<Type> GetAllInterfacesImplementedBy(Type t)
    {
        return t.GetInterfaces().Where(x => !x.FullName.StartsWith("System."));
    }

    StructureMap.IContainer container;
    Dictionary<Type, Instance> configuredInstances = new Dictionary<Type, Instance>();
}