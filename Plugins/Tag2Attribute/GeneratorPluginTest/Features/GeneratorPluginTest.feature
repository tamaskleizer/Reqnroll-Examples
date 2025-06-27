@generatorPluginFeatureTest
Feature: Generator Plugin Test

@generatorPluginScenarioTest
Scenario: Attribute on Scenario
    Given I have a scenario with an attribute
    When I run the generator plugin
    Then the generated code should include the attribute with its type set to Scenario

Scenario: Attribute on Feature
    Given I have a feature with an attribute
    When I run the generator plugin
    Then the generated code should include the attribute with its type set to Feature
