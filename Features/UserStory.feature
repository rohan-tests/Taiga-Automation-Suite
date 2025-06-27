@Regression
Feature: Create a new story in Taiga Kanban board
  As a logged-in user 
  I want to create a new story
  So that I can track tasks visually on the board

  @Smoke @UI @Create #@StoryAlreadyExists
  Scenario: Successfully create a new story
    Given I am logged into Taiga
    And I have a Kanban project open
    When I click on new issue in "New" column
    And I enter the title "Setup Project Dashboard" and description "Create a dashboard to show project metrics and recent activity."
    And I click the Create button
    Then the story titled "Setup Project Dashboard" should appear in the "New" column
  
  @Negative @Invalid @UI
  Scenario: Invalid story creation
    Given I am logged into Taiga
    And I have a Kanban project open
    When I click on new issue in "New" column
    And I leave the title field blank and submit
    Then the error message should be thrown
