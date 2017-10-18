namespace NServiceBus.Features
{
    /// <summary>
    /// Adds Diagnostics information
    /// </summary>
    public class StructureMapDiagnostics : Feature
    {
        /// <summary>
        /// Constructor for diagnostics feature
        /// </summary>
        public StructureMapDiagnostics()
        {
            EnableByDefault();
        }

        /// <summary>
        /// Sets up diagnostics
        /// </summary>
        protected override void Setup(FeatureConfigurationContext context)
        {
            context.Settings.AddStartupDiagnosticsSection("NServiceBus.StructureMap", new
            {
                UsingExistingContainer = context.Settings.HasSetting<StructureMapBuilder.ContainerHolder>()
            });
        }
    }
}
