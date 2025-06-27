using Microsoft.Playwright;
using Pages.CreateStoryPage;
using Pages.DashboardPage;
using Pages.LoginPage;
using TechTalk.SpecFlow;
using TestData;

namespace StepDefinitions.CommonSteps
{
    [Binding]
    public class CommonSteps
    {
        private readonly IPage page = null!;
        private readonly LoginPage loginPage = null!;
        private readonly DashboardPage dashboardPage = null!;

        public CommonSteps(ScenarioContext scenarioContext)
        {
            page = (IPage)scenarioContext["Page"];
            loginPage = new LoginPage(page);
            dashboardPage = new DashboardPage(page);
        }

        [Given(@"I am logged into Taiga")]
        public async Task GivenIAmLoggedIntoTaiga()
        {
            await loginPage.PerformLogin(Config.BaseUrl, Config.Username, Config.Password);
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }

        [Given(@"I have a Kanban project open")]
        public async Task GivenIHaveAKanbanProjectOpen()
        {
            await dashboardPage.NavigateToBoard();
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }

        [Then(@"the story titled ""(.*)"" should appear in the ""(.*)"" column")]
        public async Task ThenTheStoryShouldAppearInTheColumn(string storyTitle, string expectedColumn)
        {
            Assert.That(await dashboardPage.FetchLatestStoryTitleFromCol(storyTitle ,expectedColumn), Is.True);
            // await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.WaitForTimeoutAsync(500);
        }

    }
}
