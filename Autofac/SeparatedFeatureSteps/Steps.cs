namespace ReqnrollExamples.Autofac.SeparatedFeatureSteps;

using Reqnroll;
using ReqnrollExamples.Autofac.SeparatedDriversDef;
using Xunit;

[Binding]
public class Steps
{
    private readonly ISeparatedDriver _driver;

    public Steps(ISeparatedDriver driver)
    {
        _driver = driver;
    }

    [Given(@"I have steps implemented in another assembly")]
    public void GivenIStepsImplementedInAnotherAssembly()
    {
        _driver.DriverMethod("test input");
    }

    [When(@"I configure use the right configuration")]
    public void WhenIConfigureUseTheRightConfiguration()
    {
        // This step intentionally fails, so we can see the output from the previous step.
        Assert.Fail();
    }

    [Then(@"the steps are used correctly")]
    public void ThenTheStepsAreUsedCorrectly()
    {

    }
}
