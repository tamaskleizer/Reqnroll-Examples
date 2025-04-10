namespace ReqnrollExamples.Autofac.SeparatedFeatureSteps;

using global::Autofac;

public class StepsModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        _ = builder
            .RegisterType<Steps>()
            .AsSelf()
            .InstancePerLifetimeScope();
    }
}