using System;
using StructureMap;

static class ConfigurationExpressionExtensions
{
    /// <summary>
    /// Tells StructureMap to do setter injection for the given type
    /// </summary>
    public static void EnableSetterInjectionFor(this ConfigurationExpression configuration, Type pluginType)
    {
        configuration.Policies.SetAllProperties(x => x.TypeMatches(t => t == pluginType));
    }
}