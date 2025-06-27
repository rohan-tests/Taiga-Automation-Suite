using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Microsoft.Playwright;
using Pages.DashboardPage;
using Pages.CreateStoryPage;

namespace StepDefinitions.EditStorySteps
{
    [Binding]
    public class EditStorySteps
    {
        private readonly IPage page = null!;
        private readonly DashboardPage dashboardPage = null!;
        private readonly CreateStoryPage createStoryPage = null!;
        public EditStorySteps(ScenarioContext scenarioContext)
        {
            page = (IPage)scenarioContext["Page"];
            dashboardPage = new DashboardPage(page);
            createStoryPage = new CreateStoryPage(page);
        }

        [When(@"I click Edit card")]
        public async Task WhenIClickEditCard()
        {
            await dashboardPage.ClickEditButton();
        }

        [When(@"I change the title to ""(.*)"" and description to ""(.*)""")]
        public async Task WhenIChangeTheTitleToAndDescriptionTo(string newTitle, string newDescription)
        {
            await createStoryPage.FillStoryDetails(newTitle, newDescription);
            await createStoryPage.ClickSubmitButton();
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }

        [When(@"I click on the story titled ""(.*)""")]
        public async Task WhenIClickOnTheStory(string storyTitle)
        {
            await dashboardPage.ClickOnStoryByTitle(storyTitle);
        }

        [When(@"I enter ""(.*)"" as a comment")]
        public async Task WhenIEnterComment(string comment)
        {
            await createStoryPage.AddComment(comment);
        }

        [When("I click the save button")]
        public async Task WhenIClickTheAddButton()
        {
            await createStoryPage.ClickSaveButton();
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }

        [Then("the comment should be visible under the story")]
        public async Task ThenTheCommentShouldBeVisible()
        {
            Console.WriteLine("Comment added : " + await createStoryPage.PrintLastComment());
        }
    }
}
