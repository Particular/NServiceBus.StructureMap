using NServiceBus.ContainerTests;
using NServiceBus.StructureMap;
using NUnit.Framework;

[SetUpFixture]
public class SetUpFixture
{
    public SetUpFixture()
    {
        TestContainerBuilder.ConstructBuilder = () => new StructureMapObjectBuilder();
    }

}