using System;
using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace ReqnrollExamples.Plugins.GeneratorPluginTest;

[TraitDiscoverer("ReqnrollExamples.Plugins.GeneratorPlugin.GeneratorPluginTraitDiscoverer", "GeneratorPlugin")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class GeneratorPluginTraitAttribute : Attribute, ITraitAttribute
{
    public GeneratorPluginType Type { get; }

    public GeneratorPluginTraitAttribute(GeneratorPluginType type)
    {
        Type = type;
    }
}

public class GeneratorPluginTraitDiscoverer : ITraitDiscoverer
{
    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        var typeObject = traitAttribute.GetNamedArgument<GeneratorPluginType>("type");

        yield return new KeyValuePair<string, string>("GeneratorPluginType", typeObject.ToString());
    }
}
