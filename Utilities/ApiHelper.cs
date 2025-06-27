using Microsoft.Playwright;
using System.Text.Json;
using System.Threading.Tasks;
using TestData;

public class ApiHelper
{
    private IPlaywright playwright = null!;
    private IAPIRequestContext apiContext = null!;
    private string? token;

    public ApiHelper(IPlaywright playwright)
    {
        this.playwright = playwright;
    }

    public async Task LoginAsync()
    {
        // Create a temporary API context to log in
        var tempContext = await playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions
        {
            BaseURL = Config.BaseApiUrl
        });

        // Step 1: Login and get token
        var loginResponse = await tempContext.PostAsync("/api/v1/auth", new()
        {
            DataObject = new
            {
                type = "normal",
                username = Config.Username,
                password = Config.Password
            }
        });
        Assert.That(loginResponse.Ok, Is.True);
        var loginJson = await loginResponse.JsonAsync();
        token = loginJson.Value.GetProperty("auth_token").ToString();
        // Step 2: Create a new API context with the token
        apiContext = await playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions
        {
            BaseURL = "https://api.taiga.io",
            ExtraHTTPHeaders = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {token}" },
                { "Content-Type", "application/json" }
            }
        });

    }

    public async Task CreateStory()
    {
        var createStoryResponse = await apiContext.PostAsync("/api/v1/userstories", new()
        {
            DataObject = new
            {
                project = Config.ProjectId,
                subject = "Implement Login Page UI",
                description = "Add functionality to login with username and password",
                tags = new string[] {"UI"},
                points = new { },
                status = 10306564,
                is_archived = false,
                is_closed = false
            }
        });
        Assert.That(createStoryResponse.Ok, Is.True);
        var storyJson = await createStoryResponse.JsonAsync();
        var storyId = storyJson.Value.GetProperty("id").ToString();
        Console.WriteLine($"Story created with ID: {storyId}");
    }

    // [TestCase("Create new story")]
    public async Task DeleteStoryByTitle(string storyTitle)
    {
        var response = await apiContext.GetAsync($"/api/v1/userstories?project={Config.ProjectId}");
        Assert.That(response.Ok, Is.True, "Failed to fetch user stories.");
        var json = await response.JsonAsync();
        if (!json.HasValue) throw new Exception("No response data.");

        foreach (var story in json.Value.EnumerateArray())
        {
            if (story.GetProperty("subject").GetString() == storyTitle)
            {
                var storyId = story.GetProperty("id").ToString();
                var deleteResponse = await apiContext.DeleteAsync($"/api/v1/userstories/{storyId}");
                Assert.That(deleteResponse.Ok, Is.True, "Failed to delete");
                Console.WriteLine($"Story deleted with title {storyTitle}");
                return;
            }
            // Console.WriteLine("No story found");
        }
    }
    public async Task DeleteAllStoriesAsync()
    {
        var response = await apiContext.GetAsync($"/api/v1/userstories?project={Config.ProjectId}");
        Assert.That(response.Ok, Is.True, "Failed to fetch user stories.");
        var json = await response.JsonAsync();
        if (!json.HasValue) throw new Exception("No response data.");

        foreach (var story in json.Value.EnumerateArray())
        {
            var storyId = story.GetProperty("id").GetInt32();
            var deleteResponse = await apiContext.DeleteAsync($"/api/v1/userstories/{storyId}");
            Assert.That(deleteResponse.Ok, Is.True, $"Failed to delete story ID {storyId}");
            Console.WriteLine($"Deleted story ID: {storyId}");
        }
    }
}
