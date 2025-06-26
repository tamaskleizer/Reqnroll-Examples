namespace ReqnrollExamples.Plugins.TestMethodAttribute.TestMethodGeneratorPluginTest;

using Xunit.Abstractions;
using Xunit.Sdk;

public class CustomTheoryDiscoverer : TheoryDiscoverer
{
    public CustomTheoryDiscoverer(IMessageSink diagnosticMessageSink) : base(diagnosticMessageSink)
    {
    }
}
