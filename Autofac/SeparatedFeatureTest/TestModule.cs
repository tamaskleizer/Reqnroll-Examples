namespace ReqnrollExamples.Autofac.SeparatedFeatureTest;

using global::Autofac;
using Xunit.Abstractions;

public class TestModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        _ = builder
            .RegisterType<TestOutputHelper>()
            .As<ITestOutputHelper>()
            .InstancePerLifetimeScope();
    }
}