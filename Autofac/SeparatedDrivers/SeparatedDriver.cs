namespace ReqnrollExamples.Autofac.SeparatedDrivers;

using ReqnrollExamples.Autofac.SeparatedDriversDef;
using Xunit.Abstractions;

public class SeparatedDriver(ITestOutputHelper _outputHelper) : ISeparatedDriver
{
    public void DriverMethod(string input)
    {
        _outputHelper.WriteLine($"DriverMethod called with input: {input}");
    }
}
