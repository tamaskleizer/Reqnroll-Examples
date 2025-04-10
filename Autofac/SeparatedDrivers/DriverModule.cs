namespace ReqnrollExamples.Autofac.SeparatedDrivers;

using global::Autofac;
using ReqnrollExamples.Autofac.SeparatedDriversDef;

public class DriverModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        _ = builder
            .RegisterType<SeparatedDriver>()
            .As<ISeparatedDriver>()
            .InstancePerLifetimeScope();
    }
}