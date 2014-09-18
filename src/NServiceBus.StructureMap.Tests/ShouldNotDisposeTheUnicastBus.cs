namespace NServiceBus.StructureMap.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class ShouldNotDisposeTheUnicastBus
    {
        [Test]
        public void MakeSure_()
        {
            var builder = new StructureMapObjectBuilder();


            builder.Configure(typeof(MyBus), DependencyLifecycle.SingleInstance);

            builder.Configure(typeof(DependsOnIBus), DependencyLifecycle.InstancePerCall);

            using (var childBuilder = builder.BuildChildContainer())
            {
                //this is the main use case
                childBuilder.Build(typeof(DependsOnIBus));

                //this one is ok
                childBuilder.Build(typeof(MyBus));

                //but this one fails
                childBuilder.Build(typeof(IBus2));
            }

            Assert.False(((MyBus)builder.Build(typeof(MyBus))).Disposed);
        }
    }

    public class DependsOnIBus
    {
        public IBus2 Bus2 { get; set; }
    }

    public class MyBus : IBus2
    {
        public bool Disposed;

        public void Dispose()
        {
            Disposed = true;
        }
    }

    public interface IBus2 : IDisposable
    {
    }
}