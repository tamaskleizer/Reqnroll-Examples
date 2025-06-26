[assembly: Reqnroll.Infrastructure.GeneratorPlugin(typeof(ReqnrollExamples.Plugins.TestMethodAttribute.TestMethodGeneratorPlugin.GeneratorPlugin))]

namespace ReqnrollExamples.Plugins.TestMethodAttribute.TestMethodGeneratorPlugin
{
    using Reqnroll.Generator.Plugins;
    using Reqnroll.Generator.UnitTestProvider;
    using Reqnroll.UnitTestProvider;

    public class GeneratorPlugin : IGeneratorPlugin
    {
        public void Initialize(
            GeneratorPluginEvents generatorPluginEvents,
            GeneratorPluginParameters generatorPluginParameters,
            UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            generatorPluginEvents.CustomizeDependencies += (_, eventArgs) =>
            {
                // Register a custom test generator provider
                _ = eventArgs.ObjectContainer.RegisterTypeAs<CustomTestGeneratorProvider, IUnitTestGeneratorProvider>();

                // If needed, register additional dependencies here
            };
        }
    }
}
