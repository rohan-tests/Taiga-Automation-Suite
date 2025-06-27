
using Microsoft.Playwright;

namespace Pages.DashboardPage
{
    public class DashboardPage
    {
        private readonly IPage page = null!;

        private static readonly Dictionary<string, string> ColumnIdMap = new()
        {
            ["New"] = "column-10306564",
            ["Ready"] = "column-10306565",
            ["In Progress"] = "column-10306566",
            ["Ready for Test"] = "column-10306567",
            ["Done"] = "column-10306568",
            ["Archived"] = "column-10306569"
        };

        private static readonly Dictionary<string, int> ColumnIndexMap = new()
        {
            ["New"] = 0,
            ["Ready"] = 1,
            ["In Progress"] = 2,
            ["Ready for Test"] = 3,
            ["Done"] = 4,
            ["Archived"] = 5
        };
        public DashboardPage(IPage page)
        {
            this.page = page;
        }
        private ILocator Project_title => page.Locator("h3.project-card-name");
        private ILocator New_story => page.Locator("button[title='Add new user story']");
        private ILocator DeleteButton => page.Locator("//button[text()='Delete card']");
        private ILocator ConfirmDelete => page.Locator("button.btn-confirm.js-confirm");
        private ILocator TotalStories => page.Locator("h2.card-title");
        private ILocator Search_field => page.Locator("//input[@placeholder='subject or reference']");
        private ILocator EditButton => page.Locator("//button[text()='Edit card']");

        public async Task NavigateToBoard()
        {
            await Project_title.HoverAsync();
            await Project_title.ClickAsync(new() { Force = true });
            await page.Locator("h1.project-name").WaitForAsync(new() { State = WaitForSelectorState.Visible });
            var shadowHost = page.Locator("tg-legacy-loader");
            var kanban_board = shadowHost.Locator("span.menu-option-text:has-text('Kanban')");
            await kanban_board.ClickAsync();
        }
        public async Task CreateNewUserStory(string columnName)
        {
            int colId = ColumnIndexMap[columnName];
            await New_story.Nth(colId).ClickAsync();
        }
        public async Task<bool> FetchLatestStoryTitleFromCol(string storyTitle, string colName)
        {
            string colId = ColumnIdMap[colName];
            var column = page.Locator($"div#{colId}");
            await column.GetByText(storyTitle).WaitForAsync();
            return await column.GetByText(storyTitle).IsVisibleAsync();
        }
        public async Task<bool> CheckIfStoryisVisible(string storyTitle, string colName)
        {
            string colId = ColumnIdMap[colName];
            var column = page.Locator($"div#{colId}");
            await page.WaitForTimeoutAsync(500);
            return await column.GetByText(storyTitle).IsVisibleAsync();
        }

        public async Task DragToColumn(string storyTitle, string targetColName)
        {
            string targetColId = ColumnIdMap[targetColName];
            var sourceCard = page.GetByText(storyTitle);
            var targetColumn = page.Locator($"div#{targetColId}");
            await sourceCard.DragToAsync(targetColumn);
        }
        public async Task ClickOnStoryMenu(string storyTitle)
        {
            var story_title = page.GetByText(storyTitle);
            var menu = story_title.Locator("xpath=./ancestor::h2/preceding-sibling::tg-card-actions//button[contains(@class, 'js-popup-button')]");
            await menu.ClickAsync();
        }
        public async Task ClickOnStoryByTitle(string storyTitle)
        {
            await page.GetByText(storyTitle).ClickAsync();
        }
        public async Task ClickAndConfirmDelete()
        {
            await DeleteButton.ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await ConfirmDelete.ClickAsync();
        }
        public async Task ClickEditButton()
        {
            await EditButton.ClickAsync();
        }
        public async Task<int> GetStoriesCount()
        {
            await TotalStories.First.WaitForAsync();
            return await TotalStories.CountAsync();
        }
        public async Task EnterKeywordAndSearch(string keyword)
        {
            await Search_field.FillAsync(keyword);
        }
        public async Task DisplayFilteredStories()
        {
            await page.WaitForTimeoutAsync(500);
            var filtered_stories = await page.QuerySelectorAllAsync("span.card-subject");
            foreach (var item in filtered_stories)
            {
                Console.WriteLine(await item.InnerTextAsync());
            }
        }
    }
}