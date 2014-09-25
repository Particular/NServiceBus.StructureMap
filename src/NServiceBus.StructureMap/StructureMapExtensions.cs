using NServiceBus.Container;
using StructureMap;

static class StructureMapExtensions
{
    /// <summary>
    /// Tells StructureMap to do setter injection for the given type
    /// </summary>
    public static void ExistingContainer(this ContainerCustomizations customizations, IContainer container)
    {
        customizations.Settings.Set("ExistingContainer", container);
    }
}