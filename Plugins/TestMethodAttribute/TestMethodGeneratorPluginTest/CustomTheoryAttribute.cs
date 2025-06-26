namespace ReqnrollExamples.Plugins.TestMethodAttribute.TestMethodGeneratorPluginTest;

using System;
using Xunit;

/// <summary>
/// Custom attribute to be used in test methods.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class CustomTheoryAttribute : TheoryAttribute
{
    /// <summary>
    /// Gets or sets a value indicating whether the test is marked.
    /// </summary>
    public bool IsMarked { get; set; }
}
