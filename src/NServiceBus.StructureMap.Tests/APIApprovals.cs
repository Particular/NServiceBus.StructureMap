using NServiceBus.StructureMap;
using NUnit.Framework;
using Particular.Approvals;
using PublicApiGenerator;

[TestFixture]
public class APIApprovals
{
    [Test]
    public void Approve()
    {
        var publicApi = ApiGenerator.GeneratePublicApi(typeof(StructureMapObjectBuilder).Assembly, excludeAttributes: new[] { "System.Runtime.Versioning.TargetFrameworkAttribute" });
        Approver.Verify(publicApi);
    }
}
