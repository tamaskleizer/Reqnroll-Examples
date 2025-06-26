Feature: Mark scenarios

@marked
Scenario: Marked scenario
    Given I have a scenario with a mark
    When I run the generator plugin for MarkedScenario method
    Then the generated code should include the mark

Scenario: Unmarked scenario
    Given I have a scenario without a mark
    When I run the generator plugin for UnmarkedScenario method
    Then the generated code should not set the mark
