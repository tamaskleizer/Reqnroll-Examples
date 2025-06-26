namespace ReqnrollExamples.Plugins.TestMethodAttribute.TestMethodGeneratorPluginTest;

using FluentAssertions;
using Reqnroll;
using System.Reflection;

[Binding]
public class TestMethodGeneratorPluginTestSteps
{
    private bool? _isMarked;

    [Given(@"I have a scenario with a mark")]
    [Given(@"I have a feature with a mark")]
    public void GivenIHaveAScenarioWithAMark()
    {
        _isMarked = null;
    }

    [Given(@"I have a scenario without a mark")]
    public void GivenIHaveAScenarioWithoutAMark()
    {
        _isMarked = null;
    }

    [When(@"^I run the generator plugin for ([_a-zA-Z][_a-zA-Z0-9]*) method$")]
    public void WhenIRunTheGeneratorPlugin(string methodName)
    {
        var types = GetAllGeneratedReqnrollTypes()
            .Should().NotBeEmpty("There should be at least one generated type from the Reqnroll generator plugin.").And.Subject;

        MethodInfo method = types
            .Select(t => t.GetMethod(methodName))
            .FirstOrDefault(m => m is not null)
            .Should().NotBeNull("The method '{0}' should exist in one of the generated types.", methodName).And.Subject;

        _isMarked = method.GetCustomAttributes()
            .OfType<CustomFactAttribute>()
            .SingleOrDefault()?.IsMarked;
    }

    [Then(@"the generated code should include the mark")]
    public void ThenTheGeneratedCodeShouldIncludeTheMark()
    {
        _isMarked.Should().BeTrue();
    }

    [Then(@"the generated code should not set the mark")]
    public void ThenTheGeneratedCodeShouldNotSetTheMark()
    {
        _isMarked.Should().BeNull();
    }

    [Then(@"the generated code should not include the mark")]
    public void ThenTheGeneratedCodeShouldNotIncludeTheMark()
    {
        _isMarked.Should().BeNull();
    }

    private static IEnumerable<Type> GetAllGeneratedReqnrollTypes()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();

        return assembly.GetTypes()
            .Where(IsReqnrollGeneratedType);

        bool IsReqnrollGeneratedType(Type type) =>
            type.GetCustomAttributes()
                .OfType<System.CodeDom.Compiler.GeneratedCodeAttribute>()
                .Any(attr => attr.Tool == "Reqnroll");
    }
}