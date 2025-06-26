@marked
Feature: Mark feature

Scenario: Marked feature
    Given I have a feature with a mark
    When I run the generator plugin for MarkedFeature method
    Then the generated code should include the mark
