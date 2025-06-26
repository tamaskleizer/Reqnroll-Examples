namespace ReqnrollExamples.Plugins.TestMethodAttribute.TestMethodGeneratorPluginTest;

using Xunit.Abstractions;
using Xunit.Sdk;

public class CustomFactDiscoverer : FactDiscoverer
{
    public CustomFactDiscoverer(IMessageSink diagnosticMessageSink) : base(diagnosticMessageSink)
    {
    }
}
