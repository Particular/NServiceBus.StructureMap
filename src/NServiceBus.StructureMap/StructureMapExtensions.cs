namespace NServiceBus
{
    using global::StructureMap;
    using NServiceBus.Container;

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
            customizations.Settings.Set("ExistingContainer", container);
        }
    }
}