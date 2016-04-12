using NServiceBus.ContainerTests;
using NServiceBus.StructureMap;
using NUnit.Framework;

[SetUpFixture]
public class SetUpFixture
{
    [SetUp]
    public void Setup()
    {
        TestContainerBuilder.ConstructBuilder = () => new StructureMapObjectBuilder();
    }

}