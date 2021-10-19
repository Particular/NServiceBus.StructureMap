﻿namespace NServiceBus
{
    using Container;
    using global::StructureMap;
    using Settings;
    using StructureMap;

    /// <summary>
    /// StructureMap Container
    /// </summary>
    [ObsoleteEx(
        Message = "Support for external dependency injection containers is no longer provided by NServiceBus adapters for each container library. Instead, the NServiceBus.Extensions.DependencyInjection library provides the ability to use any container that conforms to the Microsoft.Extensions.DependencyInjection container abstraction.",
        RemoveInVersion = "9.0.0",
        TreatAsErrorFromVersion = "8.0.0")]
    public class StructureMapBuilder : ContainerDefinition
    {
        /// <summary>
        ///     Implementers need to new up a new container.
        /// </summary>
        /// <param name="settings">The settings to check if an existing container exists.</param>
        /// <returns>The new container wrapper.</returns>
        public override ObjectBuilder.Common.IContainer CreateContainer(ReadOnlySettings settings)
        {
            if (settings.TryGet(out ContainerHolder containerHolder))
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