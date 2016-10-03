namespace NServiceBus
{
    using Container;
    using StructureMap;

    /// <summary>
    /// Extension to pass an existing StructureMap <see cref="IContainer"/> instance.
    /// </summary>
    public static class StructureMapExtensions
    {
        /// <summary>
        /// Use the a pre-configured <see cref="IContainer"/>.
        /// </summary>
        public static void ExistingContainer(this ContainerCustomizations customizations, IContainer container)
        {
            customizations.Settings.Set<StructureMapBuilder.ContainerHolder>(new StructureMapBuilder.ContainerHolder(container));
        }
    }
}