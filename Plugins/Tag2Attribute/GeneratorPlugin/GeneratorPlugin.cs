using System.Reflection;
using Reqnroll.Generator.Plugins;
using Reqnroll.Generator.UnitTestConverter;
using Reqnroll.UnitTestProvider;

[assembly: Reqnroll.Infrastructure.GeneratorPlugin(typeof(ReqnrollExamples.Plugins.GeneratorPlugin.GeneratorPlugin))]

namespace ReqnrollExamples.Plugins.GeneratorPlugin
{
    public class GeneratorPlugin : IGeneratorPlugin
    {
        public void Initialize(
            GeneratorPluginEvents generatorPluginEvents,
            GeneratorPluginParameters generatorPluginParameters,
            UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            generatorPluginEvents.RegisterDependencies += (sender, eventArgs) =>
            {
                eventArgs.ObjectContainer.RegisterTypeAs<GeneratorPluginDecorator, ITestClassTagDecorator>("generatorPluginFeature");
                eventArgs.ObjectContainer.RegisterTypeAs<GeneratorPluginDecorator, ITestMethodTagDecorator>("generatorPluginScenario");
            };
        }
    }
}