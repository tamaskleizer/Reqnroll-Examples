namespace ReqnrollExamples.Autofac.SeparatedFeatureTest;

using Reqnroll;
using Xunit.Abstractions;

public class TestOutputHelper(IReqnrollOutputHelper _outputHelper) : ITestOutputHelper
{
    public void WriteLine(string message)
    {
        _outputHelper.WriteLine(message);
    }

    public void WriteLine(string format, params object[] args)
    {
        _outputHelper.WriteLine(string.Format(format, args));
    }
}