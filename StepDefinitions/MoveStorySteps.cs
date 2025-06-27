using Microsoft.Playwright;
using Pages.CreateStoryPage;
using Pages.DashboardPage;
using Pages.LoginPage;
using TechTalk.SpecFlow;

namespace StepDefinitions.FilterStorySteps
{
    [Binding]
    public class MoveStorySteps
    {
        private readonly IPage page = null!;
        private readonly DashboardPage dashboardPage = null!;

        public MoveStorySteps(ScenarioContext scenarioContext)
        {
            page = (IPage)scenarioContext["Page"];
            dashboardPage = new DashboardPage(page);
        }

        [Given(@"a story titled ""(.*)"" exists in the ""(.*)"" column")]
        public async Task GivenAStoryExistsInTheColumn(string storyTitle, string columnName)
        {
            Assert.That(await dashboardPage.FetchLatestStoryTitleFromCol(storyTitle, columnName), Is.True);
        }

        [When(@"I drag the story titled ""(.*)"" to ""(.*)"" column")]
        public async Task WhenIDragTheStoryToTheColumn(string storyTitle, string targetColumn)
        {
            await dashboardPage.DragToColumn(storyTitle, targetColumn);
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }
    }
}