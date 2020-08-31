namespace NServiceBus
{
    using ObjectBuilder.Common;

    // An internal type referenced by the API approvals test as it can't reference obsoleted types.
    class StructureMapInternalType
    {
    }

    /// <summary>
    /// StructureMap Container
    /// </summary>
    [ObsoleteEx(
        Message = "StructureMap is no longer supported via the NServiceBus.StructureMap adapter. NServiceBus directly supports all containers compatible with Microsoft.Extensions.DependencyInjection.Abstractions via the externally managed container mode.",
        RemoveInVersion = "9.0.0",
        TreatAsErrorFromVersion = "8.0.0")]
    public class StructureMapBuilder
    {
    }

    /// <summary>
    /// Extension to pass an existing StructureMap <see cref="IContainer"/> instance.
    /// </summary>
    [ObsoleteEx(
        Message = "StructureMap is no longer supported via the NServiceBus.StructureMap adapter. NServiceBus directly supports all containers compatible with Microsoft.Extensions.DependencyInjection.Abstractions via the externally managed container mode.",
        RemoveInVersion = "9.0.0",
        TreatAsErrorFromVersion = "8.0.0")]
    public static class StructureMapExtensions
    {
    }
}