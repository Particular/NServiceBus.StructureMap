[assembly: System.CLSCompliantAttribute(true)]
[assembly: System.Runtime.CompilerServices.InternalsVisibleToAttribute("NServiceBus.StructureMap.Tests")]
[assembly: System.Runtime.InteropServices.ComVisibleAttribute(false)]
[assembly: System.Runtime.Versioning.TargetFrameworkAttribute(".NETFramework,Version=v4.5.2", FrameworkDisplayName=".NET Framework 4.5.2")]

namespace NServiceBus
{
    
    public class StructureMapBuilder : NServiceBus.Container.ContainerDefinition
    {
        public StructureMapBuilder() { }
        public override NServiceBus.ObjectBuilder.Common.IContainer CreateContainer(NServiceBus.Settings.ReadOnlySettings settings) { }
    }
    public class static StructureMapExtensions
    {
        public static void ExistingContainer(this NServiceBus.Container.ContainerCustomizations customizations, StructureMap.IContainer container) { }
    }
}