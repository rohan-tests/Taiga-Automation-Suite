
using Microsoft.Playwright;

namespace Pages.CreateStoryPage
{
    public class CreateStoryPage
    {
        private readonly IPage page = null!;
        
        public CreateStoryPage(IPage page)
        {
            this.page = page;
        }
        private ILocator Title_input => page.Locator("input[placeholder='Subject']");
        private ILocator Desc_input => page.Locator("textarea[name='description']");
        private ILocator Submit_button => page.Locator("button#submitButton");
        private ILocator Comment_field => page.Locator("p.ck-placeholder");
        private ILocator Save_button => page.Locator("a:text('Save')").Nth(1);
        private ILocator Error_msg =>  page.Locator("li.checksley-required");

        public async Task FillStoryDetails(string title, string description)
        {
            await Title_input.ClearAsync();
            await Title_input.FillAsync(title);
            await Desc_input.ClearAsync();
            await Desc_input.FillAsync(description);
        }
        public async Task ClickSubmitButton()
        {
            await page.EvaluateAsync("document.body.style.zoom = '90%'");
            await Submit_button.ClickAsync(new() { Force = true });
        }
        public async Task AddComment(string comment)
        {
            await page.EvaluateAsync("document.body.style.zoom = '80%'");
            await Comment_field.ScrollIntoViewIfNeededAsync();
            await Comment_field.ClickAsync();
            await Comment_field.FillAsync(comment);
        }
        public async Task ClickSaveButton()
        {
            await Save_button.ClickAsync(new() { Force = true });
        }
        public async Task<string> PrintLastComment()
        {
            var all_comments = page.Locator("div.comment-text");
            int count = await all_comments.CountAsync();
            await all_comments.Nth(count).WaitForAsync();
            return await all_comments.Nth(0).InnerTextAsync();
        }
        public async Task<bool> CheckErrorMsg()
        {
            return await Error_msg.IsVisibleAsync();
        }
    }
}