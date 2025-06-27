using TechTalk.SpecFlow;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Pages.DashboardPage;

namespace StepDefinitions.FilterStorySteps
{
    [Binding]
    public class FilterStorySteps
    {
        private readonly IPage page = null!;
        private readonly DashboardPage dashboardPage = null!;

        public FilterStorySteps(ScenarioContext scenarioContext)
        {
            page = (IPage)scenarioContext["Page"];
            dashboardPage = new DashboardPage(page);
        }

        [Given("multiple stories exist on board")]
        public async Task MultipleStoriesExistOnBoard()
        {
            Assert.That(await dashboardPage.GetStoriesCount(), Is.GreaterThan((int) 0));
        }

        [When(@"I enter ""(.*)"" in the search field and click search")]
        public async Task WhenIEnterInTheSearchField(string keyword)
        {
            await dashboardPage.EnterKeywordAndSearch(keyword);
        }

        [Then(@"only stories matching the tag ""(.*)"" should be displayed on the board")]
        public async Task ThenOnlyStoriesMatchingTheTagShouldBeDisplayedOnTheBoard(string tag)
        {
            await dashboardPage.DisplayFilteredStories();
        }
    }
}
