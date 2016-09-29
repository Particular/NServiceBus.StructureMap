namespace NServiceBus.StructureMap.AcceptanceTests
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
        public async Task Should_shutdown_properly()
        {
            var context = await Scenario.Define<Context>()
                .WithEndpoint<Endpoint>()
                .Done(c => c.EndpointsStarted)
                .Run();

            Assert.IsFalse(context.Decorator.Disposed);
            Assert.DoesNotThrow(() => context.Container.Dispose());
        }

        class Context : ScenarioContext
        {
            public ContainerDecorator Decorator { get; set; }
            public IContainer Container { get; set; }
        }

        class Endpoint : EndpointConfigurationBuilder
        {
            public Endpoint()
            {
                EndpointSetup<DefaultServer>((config, desc) =>
                {
                    var container = new Container();
                    var scopeDecorator = new ContainerDecorator(container);

                    config.UseContainer<StructureMapBuilder>(c => c.ExistingContainer(scopeDecorator));

                    var context = (Context)desc.ScenarioContext;
                    context.Decorator = scopeDecorator;
                    context.Container = container;
                });
            }
        }
    }
}