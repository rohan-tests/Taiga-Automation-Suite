using TechTalk.SpecFlow;
using System.Threading.Tasks;
using NUnit.Framework;
using Pages.DashboardPage;
using Microsoft.Playwright;

namespace StepDefinitions.DeleteStorySteps
{
    [Binding]
    public class DeleteStorySteps
    {
        private readonly IPage page = null!;
        private readonly DashboardPage dashboardPage = null!;
        public DeleteStorySteps(ScenarioContext scenarioContext)
        {
            page = (IPage)scenarioContext["Page"];
            dashboardPage = new DashboardPage(page);
        }

        [When(@"I click on the 3 dot menu on story with title ""(.*)""")]
        public async Task WhenIClickOnThe3DotMenuOnStory(string storyTitle)
        {
            await dashboardPage.ClickOnStoryMenu(storyTitle);
        }

        [When(@"I click the delete option and confirm delete")]
        public async Task WhenIClickTheDeleteOption()
        {
            await dashboardPage.ClickAndConfirmDelete();
        }

        [Then(@"the story titled ""(.*)"" should be removed from ""(.*)"" column")]
        public async Task ThenTheStoryShouldBeRemovedFromTheBoard(string storyTitle, string colName)
        {    
            Assert.That(await dashboardPage.CheckIfStoryisVisible(storyTitle, colName), Is.False);
        }

    }
}
