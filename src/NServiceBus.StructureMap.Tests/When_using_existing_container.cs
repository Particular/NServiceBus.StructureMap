namespace NServiceBus.StructureMap.Tests
{
    using System;
    using System.Threading.Tasks;
    using NUnit.Framework;

    [TestFixture]
    public class When_using_existing_container
    {
        [Test]
        public void Properties_should_not_be_injected_on_user_types()
        {
            var container = new global::StructureMap.Container(_ =>
            {
                _.For<IUserService>().Use<UserService>();
                _.For<ISomeInterface>().Use<SomeClass>();
            });

            var builder = new StructureMapObjectBuilder(container);

            //container.Configure(.RegisterType<IUserService, UserService>();
            //container.RegisterType<ISomeInterface, SomeClass>();

            //var builder = new UnityObjectBuilder(container);
            var result = (UserService)builder.Build(typeof(IUserService));

            Assert.IsNull(result.Dependency);
        }


        class AbstractClass
        {
        }

        class SomeClass : AbstractClass, ISomeInterface
        {
        }

        class PropertyInjectionHandler : IHandleMessages<object>
        {
            ISomeInterface dependency;

            public ISomeInterface Dependency
            {
                get
                {
                    return dependency;
                }
                // ReSharper disable once UnusedMember.Local
                set
                {
                    if (dependency != null)
                    {
                        throw new Exception("Dependency has already a value");
                    }

                    dependency = value;
                }
            }

            public Task Handle(object message, IMessageHandlerContext context)
            {
                return Task.FromResult(0);
            }

        }

        class ConstructorInjectionHandler : IHandleMessages<object>
        {
            public ConstructorInjectionHandler(ISomeInterface dependency)
            {
                Dependency = dependency;
            }

            public ISomeInterface Dependency { get; }

            public Task Handle(object message, IMessageHandlerContext context)
            {
                return Task.FromResult(0);
            }
        }

        interface ISomeInterface
        {
        }

        interface IUserService
        {
        }

        class NamedService1 : ISomeInterface
        {
        }

        class NamedService2 : ISomeInterface
        {
        }

        class NamedService3 : ISomeInterface
        {
        }

        class UserService : IUserService
        {
            public ISomeInterface Dependency { get; set; }
        }
    }
}