@Regression
Feature: Move story in Kanban board

  @Move @UI @Smoke
  Scenario: Move a story from 'New' to 'In Progress'
    Given I am logged into Taiga
    And I have a Kanban project open
    And a story titled "Implement forgot password" exists in the "New" column
    When I drag the story titled "Implement forgot password" to "In Progress" column
    Then the story titled "Implement forgot password" should appear in the "In Progress" column