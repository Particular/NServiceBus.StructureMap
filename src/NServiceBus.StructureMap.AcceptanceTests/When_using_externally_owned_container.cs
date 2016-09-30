namespace NServiceBus.AcceptanceTests
{
    using System.Threading.Tasks;
    using AcceptanceTesting;
    using global::StructureMap;
    using NServiceBus.AcceptanceTests;
    using NServiceBus.AcceptanceTests.EndpointTemplates;
    using NUnit.Framework;

    public class When_using_externally_owned_container : NServiceBusAcceptanceTest
    {
        [Test]
        public void Should_shutdown_properly()
        {
            Scenario.Define<Context>()
                .WithEndpoint<Endpoint>()
                .Done(c => c.EndpointsStarted)
                .Run();

            Assert.IsFalse(Endpoint.Context.Decorator.Disposed);
            Assert.DoesNotThrow(() => Endpoint.Context.Container.Dispose());
        }

        class Context : ScenarioContext
        {
            public ContainerDecorator Decorator { get; set; }
            public IContainer Container { get; set; }
        }

        class Endpoint : EndpointConfigurationBuilder
        {
            public static Context Context { get; set; }
            public Endpoint()
            {
                EndpointSetup<DefaultServer>(config =>
                {
                    Context = new Context();

                    var container = new Container();
                    var scopeDecorator = new ContainerDecorator(container);

                    Context.Decorator = scopeDecorator;
                    Context.Container = container;

                    config.UseContainer<StructureMapBuilder>(c => c.ExistingContainer(scopeDecorator));
                });
            }
        }
    }
}