Feature: Separated Steps and Feature Test

Scenario: Steps from another assembly are used
    Given I have steps implemented in another assembly
    When I configure use the right configuration
    Then the steps are used correctly