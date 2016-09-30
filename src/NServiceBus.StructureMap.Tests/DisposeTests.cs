namespace NServiceBus.StructureMap.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using global::StructureMap;
    using global::StructureMap.Pipeline;
    using global::StructureMap.Query;
    using NUnit.Framework;

    [TestFixture]
    public class DisposalTests
    {
        [Test]
        public void Owned_container_should_be_disposed()
        {
            var fakeScope = new FakeContainer();

            var container = new StructureMapObjectBuilder(fakeScope, true);
            container.Dispose();

            Assert.True(fakeScope.Disposed);
        }

        [Test]
        public void Externally_owned_container_should_not_be_disposed()
        {
            var fakeContainer = new FakeContainer();

            var container = new StructureMapObjectBuilder(fakeContainer, false);
            container.Dispose();

            Assert.False(fakeContainer.Disposed);
        }

        sealed class FakeContainer : IContainer
        {
            public bool Disposed { get; private set; }


            public void Dispose()
            {
                Disposed = true;
            }

            public T GetInstance<T>()
            {
                return default(T);
            }

            public T GetInstance<T>(string instanceKey)
            {
                return default(T);
            }

            public T GetInstance<T>(Instance instance)
            {
                return default(T);
            }

            public object GetInstance(Type pluginType)
            {
                return null;
            }

            public object GetInstance(Type pluginType, string instanceKey)
            {
                return null;
            }

            public object GetInstance(Type pluginType, Instance instance)
            {
                return null;
            }

            public IEnumerable<T> GetAllInstances<T>()
            {
                yield break;
            }

            public IEnumerable GetAllInstances(Type pluginType)
            {
                yield break;
            }

            public T TryGetInstance<T>()
            {
                return default(T);
            }

            public T TryGetInstance<T>(string instanceKey)
            {
                return default(T);
            }

            public object TryGetInstance(Type pluginType)
            {
                return null;
            }

            public object TryGetInstance(Type pluginType, string instanceKey)
            {
                return null;
            }

            public IEnumerable<T> GetAllInstances<T>(ExplicitArguments args)
            {
                yield break;
            }

            public IEnumerable GetAllInstances(Type pluginType, ExplicitArguments args)
            {
                yield break;
            }

            public T GetInstance<T>(ExplicitArguments args)
            {
                return default(T);
            }

            public T GetInstance<T>(ExplicitArguments args, string instanceKey)
            {
                return default(T);
            }

            public object GetInstance(Type pluginType, ExplicitArguments args)
            {
                return null;
            }

            public object GetInstance(Type pluginType, ExplicitArguments args, string instanceKey)
            {
                return null;
            }

            public T TryGetInstance<T>(ExplicitArguments args)
            {
                return default(T);
            }

            public T TryGetInstance<T>(ExplicitArguments args, string instanceKey)
            {
                return default(T);
            }

            public object TryGetInstance(Type pluginType, ExplicitArguments args)
            {
                return null;
            }

            public object TryGetInstance(Type pluginType, ExplicitArguments args, string instanceKey)
            {
                return null;
            }

            public void EjectAllInstancesOf<T>()
            {
            }

            public void BuildUp(object target)
            {
            }

            public Container.OpenGenericTypeExpression ForGenericType(Type templateType)
            {
                return null;
            }

            public ExplicitArgsExpression With(Type pluginType, object arg)
            {
                return null;
            }

            public ExplicitArgsExpression With(Action<IExplicitArgsExpression> action)
            {
                return null;
            }

            public ExplicitArgsExpression With<T>(T arg)
            {
                return null;
            }

            public IExplicitProperty With(string argName)
            {
                return null;
            }

            public CloseGenericTypeExpression ForObject(object subject)
            {
                return null;
            }

            public void Configure(Action<ConfigurationExpression> configure)
            {
            }

            public void Inject<T>(T instance) where T : class
            {
            }

            public void Inject(Type pluginType, object instance)
            {
            }

            public IContainer GetProfile(string profileName)
            {
                return null;
            }

            public string WhatDoIHave(Type pluginType = null, Assembly assembly = null, string @namespace = null, string typeName = null)
            {
                return null;
            }

            public string WhatDidIScan()
            {
                return null;
            }

            public void AssertConfigurationIsValid()
            {
            }

            public IContainer GetNestedContainer()
            {
                return null;
            }

            public IContainer GetNestedContainer(string profileName)
            {
                return null;
            }

            public IContainer CreateChildContainer()
            {
                return null;
            }

            public void Release(object @object)
            {
            }


            public IModel Model { get; }
            public string Name { get; set; }
            public ContainerRole Role { get; }
            public string ProfileName { get; }
            public ITransientTracking TransientTracking { get; }
        }
    }
}