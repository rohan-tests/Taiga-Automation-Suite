@Regression
Feature: Edit an existing user story

  @Edit @UI
  Scenario: Update story title and description
    Given I am logged into Taiga
    And I have a Kanban project open
    And a story titled "Create User Profile Page" exists in the "New" column
    When I click on the 3 dot menu on story with title "Create User Profile Page"
    And I click Edit card
    And I change the title to "Create Client Profile Page" and description to "Build client page to display and edit user profile details."
    Then the story titled "Create Client Profile Page" should appear in the "New" column
  
  @Comment @UI
  Scenario: Comment on existing story
    Given I am logged into Taiga
    And I have a Kanban project open
    When I click on the story titled "Add Password Reset Functionality"
    And I enter "Looks good, ready to mark complete" as a comment
    And I click the save button
    Then the comment should be visible under the story
