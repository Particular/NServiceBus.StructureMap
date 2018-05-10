namespace NServiceBus
{
    using Container;
    using global::StructureMap;
    using Settings;
    using StructureMap;

    /// <summary>
    /// StructureMap Container
    /// </summary>
    public class StructureMapBuilder : ContainerDefinition
    {
        /// <summary>
        ///     Implementers need to new up a new container.
        /// </summary>
        /// <param name="settings">The settings to check if an existing container exists.</param>
        /// <returns>The new container wrapper.</returns>
        public override ObjectBuilder.Common.IContainer CreateContainer(ReadOnlySettings settings)
        {
            ContainerHolder containerHolder;
            if (settings.TryGet(out containerHolder))
            {
                settings.AddStartupDiagnosticsSection("NServiceBus.StructureMap", new
                {
                    UsingExistingContainer = true
                });

                return new StructureMapObjectBuilder(containerHolder.ExistingContainer);
            }

            settings.AddStartupDiagnosticsSection("NServiceBus.StructureMap", new
            {
                UsingExistingContainer = false
            });

            return new StructureMapObjectBuilder();
        }

        internal class ContainerHolder
        {
            public ContainerHolder(IContainer container)
            {
                ExistingContainer = container;
            }

            public IContainer ExistingContainer { get; }
        }
    }
}