#pragma warning disable 1591
// ReSharper disable UnusedParameter.Global
namespace NServiceBus
{
    using System;
    using ObjectBuilder.Common;

    [ObsoleteEx(RemoveInVersion = "6.0", TreatAsErrorFromVersion = "5.0")]
    public static class ConfigureStructureMapBuilder
    {
        [ObsoleteEx(
            Message = "Use `configuration.UseContainer<StructureMapBuilder>()`, where `configuration` is an instance of type `BusConfiguration`.",
            TreatAsErrorFromVersion = "5.0",
            RemoveInVersion = "6.0")]
        public static Configure StructureMapBuilder(this Configure config)
        {
            throw new NotImplementedException();
        }

        [ObsoleteEx(
            Message = "Use `configuration.UseContainer<StructureMapBuilder>(b => b.ExistingContainer(container))`, where `configuration` is an instance of type `BusConfiguration`.",
            TreatAsErrorFromVersion = "5.0",
            RemoveInVersion = "6.0")]
        public static Configure StructureMapBuilder(this Configure config, IContainer container)
        {
            throw new NotImplementedException();

        }
    }
}