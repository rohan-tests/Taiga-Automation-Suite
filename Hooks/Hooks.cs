using Microsoft.Playwright;
using TechTalk.SpecFlow;
using AventStack.ExtentReports;
using Utilities.ScreenshotHelper;
using Utilities.ExtentReportManager;
using TestData;

[Binding]
public class Hooks
{
    private readonly ScenarioContext _scenarioContext;
    private static ExtentReports _extent = null!;

    private IPlaywright _playwright = null!;
    private IBrowser _browser = null!;
    private IBrowserContext _context = null!;
    private IPage _page = null!;
    private ExtentTest _test = null!;

    public Hooks(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        _extent = ExtentReportManager.GetInstance();
    }

    [BeforeScenario]
    public async Task BeforeScenario()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        _context = await _browser.NewContextAsync();
        _page = await _context.NewPageAsync();

        _scenarioContext["Page"] = _page;
        _scenarioContext["Playwright"] = _playwright;
        _scenarioContext["Browser"] = _browser;
        _scenarioContext["Context"] = _context;

        _test = _extent.CreateTest(_scenarioContext.ScenarioInfo.Title);
        _scenarioContext["ExtentTest"] = _test;

        var tags = _scenarioContext.ScenarioInfo.Tags;
        if (tags.Contains("NeedSeedStory") || tags.Contains("StoryAlreadyExists") || Config.BoardReset)
        {
            var apiHelper = new ApiHelper(_playwright);
            await apiHelper.LoginAsync();

            if (tags.Contains("StoryAlreadyExists"))
            {
                await apiHelper.DeleteStoryByTitle("Setup Project Dashboard");
                await apiHelper.CreateStory();
            }
            else if (tags.Contains("NeedSeedStory"))
            {
                await apiHelper.CreateStory();
            }

            if (Config.BoardReset)
            {
                await apiHelper.DeleteAllStoriesAsync();
            }
        }
    }

    [AfterStep]
    public async Task AfterStep()
    {
        var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
        var stepText = _scenarioContext.StepContext.StepInfo.Text;
        var test = (ExtentTest)_scenarioContext["ExtentTest"];
        var page = (IPage)_scenarioContext["Page"];

        if (_scenarioContext.TestError == null)
        {
            test.CreateNode(stepType, stepText).Pass("Step passed");
        }
        else
        {
            var screenshotPath = await ScreenshotHelper.CaptureScreenshotAsync(page, _scenarioContext.ScenarioInfo.Title);
            test.CreateNode(stepType, stepText)
                .Fail($"Step failed: {_scenarioContext.TestError.Message}")
                .AddScreenCaptureFromPath(screenshotPath);
        }
    }

    [AfterScenario]
    public async Task AfterScenario()
    {
        await _page.CloseAsync();
        await _context.CloseAsync();
        await _browser.CloseAsync();
        _playwright.Dispose();
    }

    [AfterTestRun]
    public static void AfterTestRun()
    {
        _extent.Flush();
    }
}
