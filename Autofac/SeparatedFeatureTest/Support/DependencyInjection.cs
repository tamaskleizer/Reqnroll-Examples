namespace ReqnrollExamples.Autofac.SeparatedFeatureTest.Support;

using global::Autofac;
using Reqnroll.Autofac;
using Reqnroll.Autofac.ReqnrollPlugin;

public static class DependencyInjection
{
    [GlobalDependencies]
    public static void RegisterGlobalDependencies(ContainerBuilder builder)
    {
        _ = builder
            // Load dependencies from the current assembly
            .RegisterModule<TestModule>()
            // Load dependencies from the SeparatedDrivers.dll
            .RegisterModule<SeparatedDrivers.DriverModule>()
            // Register steps through the binding attribute
            .RegisterModule<SeparatedFeatureSteps.StepsModule>();
    }

    [ScenarioDependencies]
    public static void RegisterScenarioDependencies(ContainerBuilder builder)
    {
        _ = builder.AddReqnrollBindings(typeof(DependencyInjection));
    }
}