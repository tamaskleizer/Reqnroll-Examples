using System;
using System.CodeDom;
using System.Text.RegularExpressions;
using Reqnroll.Generator;
using Reqnroll.Generator.UnitTestConverter;

namespace ReqnrollExamples.Plugins.GeneratorPlugin
{
    public class GeneratorPluginDecorator : ITestClassTagDecorator, ITestMethodTagDecorator
    {
        public static readonly Regex TagNameRegex = new Regex(@"^generatorPlugin.*$", RegexOptions.IgnoreCase);
        public int Priority => 0;

        public bool RemoveProcessedTags => true;

        public bool ApplyOtherDecoratorsForProcessedTags => false;

        public bool CanDecorateFrom(string tagName, TestClassGenerationContext generationContext)
        {
            if (tagName.StartsWith("generatorPlugin", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return TagNameRegex.IsMatch(tagName);
        }

        public void DecorateFrom(string tagName, TestClassGenerationContext generationContext)
        {
            if (TagNameRegex.IsMatch(tagName))
            {
                var attribute = new CodeAttributeDeclaration(
                    "ReqnrollExamples.Plugins.GeneratorPluginTest.GeneratorPluginTrait",
                    new CodeAttributeArgument(
                        new CodeFieldReferenceExpression(
                            new CodeTypeReferenceExpression("ReqnrollExamples.Plugins.GeneratorPluginTest.GeneratorPluginType"),
                            "Feature")));

                generationContext.TestClass.CustomAttributes.Add(attribute);

                // To ignore the test class uncomment the following line
                // generationContext.CustomData.Add("IgnoreTestClass", true);
            }
        }

        public bool CanDecorateFrom(string tagName, TestClassGenerationContext generationContext, CodeMemberMethod testMethod)
        {
            if (tagName.StartsWith("generatorPlugin", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return TagNameRegex.IsMatch(tagName);
        }

        public void DecorateFrom(string tagName, TestClassGenerationContext generationContext, CodeMemberMethod testMethod)
        {
            if (TagNameRegex.IsMatch(tagName))
            {
                var attribute = new CodeAttributeDeclaration(
                    "ReqnrollExamples.Plugins.GeneratorPluginTest.GeneratorPluginTrait",
                    new CodeAttributeArgument(
                        new CodeFieldReferenceExpression(
                            new CodeTypeReferenceExpression("ReqnrollExamples.Plugins.GeneratorPluginTest.GeneratorPluginType"),
                            "Scenario")));

                testMethod.CustomAttributes.Add(attribute);
            }
        }
    }
}