@Regression
Feature: Filter user stories by tag or name

  @Filter @Search @UI
  Scenario: Filter stories using the search bar
    Given I am logged into Taiga
    And I have a Kanban project open
    And multiple stories exist on board
    When I enter "UI" in the search field and click search
    Then only stories matching the tag "UI" should be displayed on the board
