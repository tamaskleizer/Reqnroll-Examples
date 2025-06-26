namespace ReqnrollExamples.Plugins.TestMethodAttribute.TestMethodGeneratorPlugin
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using Reqnroll.Generator;
    using Reqnroll.Generator.CodeDom;
    using Reqnroll.Generator.UnitTestProvider;

    public class CustomTestGeneratorProvider : XUnit2TestGeneratorProvider
    {
        public CustomTestGeneratorProvider(
            CodeDomHelper codeDomHelper)
            : base(codeDomHelper)
        {
        }

        public override void SetRowTest(TestClassGenerationContext generationContext, CodeMemberMethod testMethod, string scenarioTitle)
        {
            // Override for scenario-outline tests if row testing is enabled
            // This is needed if the feature is tagged

            // Call the base method to ensure that the test method is set up correctly
            base.SetRowTest(generationContext, testMethod, scenarioTitle);

            SetTestMethodInternal(testMethod, generationContext.Feature.Tags.Select(tag => tag.Name));
        }

        public override void SetTestMethod(TestClassGenerationContext generationContext, CodeMemberMethod testMethod, string friendlyTestName)
        {
            // Override to set a custom test method attribute
            // This is needed if the feature is tagged

            // Call the base method to ensure that the test method is set up correctly
            base.SetTestMethod(generationContext, testMethod, friendlyTestName);

            SetTestMethodInternal(testMethod, generationContext.Feature.Tags);
        }

        public override void SetTestMethodCategories(TestClassGenerationContext generationContext, CodeMemberMethod testMethod, IEnumerable<string> scenarioCategories)
        {
            SetTestMethodInternal(testMethod, scenarioCategories);

            // Call the base method to ensure that the categories are set correctly
            // Exclude the tag we already handle
            base.SetTestMethodCategories(
                    generationContext,
                    testMethod,
                    scenarioCategories.Where(tag => !IsTagMarked(tag)));
        }

        private const string MARKED_TAG = @"marked";
        private const string MARKED_PROPERTY_NAME = @"IsMarked";
        private const string CUSTOM_FACT_ATTRIBUTE = "ReqnrollExamples.Plugins.TestMethodAttribute.TestMethodGeneratorPluginTest.CustomFactAttribute";
        private const string CUSTOM_THEORY_ATTRIBUTE = "ReqnrollExamples.Plugins.TestMethodAttribute.TestMethodGeneratorPluginTest.CustomTheoryAttribute";

        private static void SetTestMethodInternal(
            CodeMemberMethod testMethod,
            IEnumerable<Gherkin.Ast.Tag> tags) =>
                SetTestMethodInternal(testMethod, tags.Select(tag => tag.Name));

        private static void SetTestMethodInternal(
            CodeMemberMethod testMethod,
            IEnumerable<string> tags)
        {
            if (tags.Any(IsTagMarked))
            {
                SetMarkedAttribute(testMethod, true);
            }
            else
            {
                // Here you can decide if you:
                // - leave the original xunit attribute as is (do nothing)
                // - add the custom attribute with IsMarked = false
                // - add the custom attribute with an implicit IsMarked = false (default value)
            }
        }

        private static bool IsTagMarked(string tag)
        {
            return tag.TrimStart('@').Equals(MARKED_TAG, StringComparison.OrdinalIgnoreCase);
        }

        private static void SetMarkedAttribute(
            CodeMemberMethod testMethod,
            bool isMarked)
        {
            var attributes = testMethod.CustomAttributes
                .OfType<CodeAttributeDeclaration>()
                .ToArray();

            var originalAttribute = attributes
                .FirstOrDefault(IsCustomTestAttribute);

            CodeAttributeArgument isMarkedArgument = null;

            if (originalAttribute != null)
            {
                isMarkedArgument = originalAttribute.Arguments
                    .OfType<CodeAttributeArgument>()
                    .FirstOrDefault(arg => string.Equals(arg.Name, MARKED_PROPERTY_NAME, StringComparison.OrdinalIgnoreCase));

                if (isMarkedArgument is null)
                {
                    isMarkedArgument = new CodeAttributeArgument(MARKED_PROPERTY_NAME, new CodePrimitiveExpression(isMarked));
                    originalAttribute.Arguments.Add(isMarkedArgument);
                }
                else
                {
                    isMarkedArgument.Value = new CodePrimitiveExpression(isMarked);
                }

                return;
            }

            var originalFactAttribute = attributes
                .FirstOrDefault(IsXunitFactAttribute);

            if (originalFactAttribute != null)
            {
                isMarkedArgument = new CodeAttributeArgument(MARKED_PROPERTY_NAME, new CodePrimitiveExpression(isMarked));
                testMethod.CustomAttributes.Remove(originalFactAttribute);

                testMethod.CustomAttributes.Add(new CodeAttributeDeclaration(
                    CUSTOM_FACT_ATTRIBUTE,
                    AddArgumentTo(
                        originalFactAttribute.Arguments,
                        isMarkedArgument)));
                return;
            }

            var originalTheoryAttribute = attributes
                .FirstOrDefault(IsXunitTheoryAttribute);

            if (originalTheoryAttribute != null)
            {
                isMarkedArgument = new CodeAttributeArgument(MARKED_PROPERTY_NAME, new CodePrimitiveExpression(isMarked));
                testMethod.CustomAttributes.Remove(originalTheoryAttribute);

                testMethod.CustomAttributes.Add(new CodeAttributeDeclaration(
                    CUSTOM_THEORY_ATTRIBUTE,
                    AddArgumentTo(
                        originalTheoryAttribute.Arguments,
                        isMarkedArgument)));
                return;
            }
        }

        private static bool IsXunitFactAttribute(CodeAttributeDeclaration attribute)
        {
            return string.Equals(attribute.Name, FACT_ATTRIBUTE, StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsXunitTheoryAttribute(CodeAttributeDeclaration attribute)
        {
            return string.Equals(attribute.Name, THEORY_ATTRIBUTE, StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsCustomTestAttribute(CodeAttributeDeclaration attribute)
        {
            return string.Equals(attribute.Name, CUSTOM_FACT_ATTRIBUTE, StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(attribute.Name, CUSTOM_THEORY_ATTRIBUTE, StringComparison.OrdinalIgnoreCase);
        }

        private static CodeAttributeArgument[] AddArgumentTo(
            CodeAttributeArgumentCollection argumentCollection,
            CodeAttributeArgument argument)
        {
            var asList = argumentCollection.OfType<CodeAttributeArgument>()
                .ToList();

            asList.Add(argument);

            return asList.ToArray();
        }
    }
}
