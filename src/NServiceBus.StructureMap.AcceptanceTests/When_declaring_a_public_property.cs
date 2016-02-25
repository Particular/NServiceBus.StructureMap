namespace NServiceBus.AcceptanceTests.Basic
{
    using System.Threading.Tasks;
    using AcceptanceTesting;
    using EndpointTemplates;
    using NUnit.Framework;

    public class When_declaring_a_public_property : NServiceBusAcceptanceTest
    {
        [Test]
        public async Task Should_be_set_via_prop_injection()
        {
            var context = await Scenario.Define<Context>()
                    .WithEndpoint<Endpoint>(b => b.When((bus, c) => bus.SendLocal(new MyMessage())))
                    .Done(c => c.WasCalled)
                    .Run();

            Assert.IsTrue(context.PropertyWasInjected);
        }

        public class Context : ScenarioContext
        {
            public bool WasCalled { get; set; }
            public bool PropertyWasInjected { get; set; }
        }

        public class Endpoint : EndpointConfigurationBuilder
        {
            public Endpoint()
            {
                EndpointSetup<DefaultServer>(config => config.RegisterComponents(c => c.ConfigureComponent<MyPropDependency>(DependencyLifecycle.SingleInstance)));
            }

            public class MyMessageHandler : IHandleMessages<MyMessage>
            {
                public Context Context { get; set; }

                public MyPropDependency MyPropDependency { get; set; }

                public Task Handle(MyMessage message, IMessageHandlerContext context)
                {
                    Context.PropertyWasInjected = MyPropDependency != null;
                    Context.WasCalled = true;
                    return Task.FromResult(0);
                }
            }
            public class MyPropDependency
            {
            }
        }
        
        public class MyMessage : ICommand
        {
        }
    }


}
