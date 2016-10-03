namespace NServiceBus.AcceptanceTests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using StructureMap;
    using StructureMap.Pipeline;
    using StructureMap.Query;

    class ContainerDecorator : IContainer
    {
        public ContainerDecorator(IContainer decorated)
        {
            this.decorated = decorated;
        }

        public bool Disposed { get; private set; }

        public void Dispose()
        {
            decorated.Dispose();
            Disposed = true;
        }

        public T GetInstance<T>()
        {
            return decorated.GetInstance<T>();
        }

        public T GetInstance<T>(string instanceKey)
        {
            return decorated.GetInstance<T>(instanceKey);
        }

        public T GetInstance<T>(Instance instance)
        {
            return decorated.GetInstance<T>(instance);
        }

        public object GetInstance(Type pluginType)
        {
            return decorated.GetInstance(pluginType);
        }

        public object GetInstance(Type pluginType, string instanceKey)
        {
            return decorated.GetInstance(pluginType, instanceKey);
        }

        public object GetInstance(Type pluginType, Instance instance)
        {
            return decorated.GetInstance(pluginType, instance);
        }

        public IEnumerable<T> GetAllInstances<T>()
        {
            return decorated.GetAllInstances<T>();
        }

        public IEnumerable GetAllInstances(Type pluginType)
        {
            return decorated.GetAllInstances(pluginType);
        }

        public T TryGetInstance<T>()
        {
            return decorated.TryGetInstance<T>();
        }

        public T TryGetInstance<T>(string instanceKey)
        {
            return decorated.TryGetInstance<T>(instanceKey);
        }

        public object TryGetInstance(Type pluginType)
        {
            return decorated.TryGetInstance(pluginType);
        }

        public object TryGetInstance(Type pluginType, string instanceKey)
        {
            return decorated.TryGetInstance(pluginType, instanceKey);
        }

        public IEnumerable<T> GetAllInstances<T>(ExplicitArguments args)
        {
            return decorated.GetAllInstances<T>(args);
        }

        public IEnumerable GetAllInstances(Type pluginType, ExplicitArguments args)
        {
            return decorated.GetAllInstances(pluginType, args);
        }

        public T GetInstance<T>(ExplicitArguments args)
        {
            return decorated.GetInstance<T>(args);
        }

        public T GetInstance<T>(ExplicitArguments args, string instanceKey)
        {
            return decorated.GetInstance<T>(args, instanceKey);
        }

        public object GetInstance(Type pluginType, ExplicitArguments args)
        {
            return decorated.GetInstance(pluginType, args);
        }

        public object GetInstance(Type pluginType, ExplicitArguments args, string instanceKey)
        {
            return decorated.GetInstance(pluginType, args, instanceKey);
        }

        public T TryGetInstance<T>(ExplicitArguments args)
        {
            return decorated.TryGetInstance<T>(args);
        }

        public T TryGetInstance<T>(ExplicitArguments args, string instanceKey)
        {
            return decorated.TryGetInstance<T>(args, instanceKey);
        }

        public object TryGetInstance(Type pluginType, ExplicitArguments args)
        {
            return decorated.TryGetInstance(pluginType, args);
        }

        public object TryGetInstance(Type pluginType, ExplicitArguments args, string instanceKey)
        {
            return decorated.TryGetInstance(pluginType, args, instanceKey);
        }

        public void EjectAllInstancesOf<T>()
        {
            decorated.EjectAllInstancesOf<T>();
        }

        public void BuildUp(object target)
        {
            decorated.BuildUp(target);
        }

        public Container.OpenGenericTypeExpression ForGenericType(Type templateType)
        {
            return decorated.ForGenericType(templateType);
        }

        public ExplicitArgsExpression With(Type pluginType, object arg)
        {
            return decorated.With(pluginType, arg);
        }

        public ExplicitArgsExpression With(Action<IExplicitArgsExpression> action)
        {
            return decorated.With(action);
        }

        public ExplicitArgsExpression With<T>(T arg)
        {
            return decorated.With(arg);
        }

        public IExplicitProperty With(string argName)
        {
            return decorated.With(argName);
        }

        public CloseGenericTypeExpression ForObject(object subject)
        {
            return decorated.ForObject(subject);
        }

        public void Configure(Action<ConfigurationExpression> configure)
        {
            decorated.Configure(configure);
        }

        public void Inject<T>(T instance) where T : class
        {
            decorated.Inject(instance);
        }

        public void Inject(Type pluginType, object instance)
        {
            decorated.Inject(pluginType, instance);
        }

        public IContainer GetProfile(string profileName)
        {
            return decorated.GetProfile(profileName);
        }

        public string WhatDoIHave(Type pluginType = null, Assembly assembly = null, string @namespace = null, string typeName = null)
        {
            return decorated.WhatDoIHave(pluginType, assembly, @namespace, typeName);
        }

        public string WhatDidIScan()
        {
            return decorated.WhatDidIScan();
        }

        public void AssertConfigurationIsValid()
        {
            decorated.AssertConfigurationIsValid();
        }

        public IContainer GetNestedContainer()
        {
            return decorated.GetNestedContainer();
        }

        public IContainer GetNestedContainer(string profileName)
        {
            return decorated.GetNestedContainer(profileName);
        }

        public IContainer CreateChildContainer()
        {
            return decorated.CreateChildContainer();
        }

        public void Release(object @object)
        {
            decorated.Release(@object);
        }

        public IContainer GetNestedContainer(TypeArguments arguments)
        {
            return decorated.GetNestedContainer(arguments);
        }

        public IModel Model => decorated.Model;

        public string Name
        {
            get { return decorated.Name; }
            set { decorated.Name = value; }
        }

        public ContainerRole Role => decorated.Role;
        public string ProfileName => decorated.ProfileName;
        public ITransientTracking TransientTracking => decorated.TransientTracking;

        public DisposalLock DisposalLock
        {
            get { return decorated.DisposalLock; }
            set { decorated.DisposalLock = value; }
        }

        IContainer decorated;
    }
}