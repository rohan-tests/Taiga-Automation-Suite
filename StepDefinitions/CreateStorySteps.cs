using TechTalk.SpecFlow;
using Microsoft.Playwright;
using System.Threading.Tasks;
using NUnit.Framework;
using Pages.LoginPage;
using Pages.DashboardPage;
using Pages.CreateStoryPage;

namespace StepDefinitions.CreateStorySteps
{
    [Binding]
    public class CreateStorySteps
    {
        private readonly IPage page = null!;
        private readonly DashboardPage dashboardPage = null!;
        private readonly CreateStoryPage createStoryPage = null!;
        public CreateStorySteps(ScenarioContext scenarioContext)
        {
            page = (IPage)scenarioContext["Page"];
            dashboardPage = new DashboardPage(page);
            createStoryPage = new CreateStoryPage(page);
        }

        [When(@"I click on new issue in ""(.*)"" column")]
        public async Task WhenIClickOn(string columnName)
        {
            await dashboardPage.CreateNewUserStory(columnName);
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }

        [When(@"I enter the title ""(.*)"" and description ""(.*)""")]
        public async Task WhenIEnterTheTitle(string title, string description)
        {
            await createStoryPage.FillStoryDetails(title, description);
        }

        [When(@"I click the Create button")]
        public async Task WhenIClickTheButton()
        {      
            await createStoryPage.ClickSubmitButton();
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }
        [When(@"I leave the title field blank and submit")]
        public async Task ILeaveTheTitleBlank()
        {
            await createStoryPage.ClickSubmitButton();
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }
        [Then(@"the error message should be thrown")]
        public async Task TheErrorMessageShouldbeThrown()
        {
            Assert.That(await createStoryPage.CheckErrorMsg(), Is.True);
        }
    }
}
