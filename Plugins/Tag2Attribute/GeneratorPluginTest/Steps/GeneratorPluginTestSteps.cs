namespace ReqnrollExamples.Plugins.GeneratorPluginTest.Steps;

using FluentAssertions;
using System.Reflection;
using Reqnroll;

[Binding]
public class GeneratorPluginTestSteps
{
    private GeneratorPluginTraitAttribute? _attribute;

    [Given(@"I have a scenario with an attribute")]
    public void GivenIHaveAScenarioWithAnAttribute()
    {
        var generatedClass = GetGeneratedClass();

        var scenarioMethod = generatedClass.GetMethod("AttributeOnScenario")
            .Should().NotBeNull("The generated scenario method should exist in the generated class.").And.Subject;

        _attribute = scenarioMethod.GetCustomAttribute<GeneratorPluginTraitAttribute>()
            .Should().NotBeNull("The generated feature class should have the GeneratorPluginTestFeatureAttribute.").And.Subject as GeneratorPluginTraitAttribute;
    }

    [Given(@"I have a feature with an attribute")]
    public void GivenIHaveAFeatureWithAnAttribute()
    {
        var generatedClass = GetGeneratedClass();

        _attribute = generatedClass.GetCustomAttribute<GeneratorPluginTraitAttribute>()
            .Should().NotBeNull("The generated feature class should have the GeneratorPluginTestFeatureAttribute.").And.Subject as GeneratorPluginTraitAttribute;
    }

    [When(@"I run the generator plugin")]
    public void WhenIRunTheScenario()
    {
        // This step is done during the build process.
    }

    [Then(@"the generated code should include the attribute with its type set to (.*)")]
    public void ThenTheGeneratedCodeShouldIncludeTheAttribute(string expectedType)
    {
        Enum.TryParse<GeneratorPluginType>(expectedType, out var type)
            .Should().BeTrue("The expected type should be a valid GeneratorPluginType enum value.");

        _attribute!.Type.Should().Be(type);
    }

    private static Type GetGeneratedClass()
    {
        var assembly = Assembly.GetExecutingAssembly();

        return assembly.GetTypes()
            .FirstOrDefault(t => t.Name == "GeneratorPluginTestFeature")
            .Should().NotBeNull("The generated feature class should exist in the assembly.").And.Subject;
    }
}
