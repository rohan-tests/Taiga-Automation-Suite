@Regression
Feature: Delete story in Kanban board

  @Delete @Smoke @UI #@NeedSeedStory
  Scenario: Delete a story from the board
    Given I am logged into Taiga
    And I have a Kanban project open
    And a story titled "Add Dashboard Page" exists in the "Ready" column
    When I click on the 3 dot menu on story with title "Add Dashboard Page"
    And I click the delete option and confirm delete
    Then the story titled "Add Dashboard Page" should be removed from "Ready" column