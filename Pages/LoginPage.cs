
using Microsoft.Playwright;

namespace Pages.LoginPage
{
    public class LoginPage
    {
        private readonly IPage page = null!;
        public LoginPage(IPage page)
        {
            this.page = page;
        }
        private ILocator Username_field => page.Locator("//input[@placeholder='Username or email (case sensitive)']");
        private ILocator Password_field => page.Locator("//input[@placeholder='Password (case sensitive)']");
        private ILocator Login_button => page.Locator("//button[@title='Login']");
        public async Task NavigateToUrl(string url)
        {
            await page.GotoAsync(url);
        }
        public async Task FillLoginDetails(string username , string password)
        {
            await Username_field.FillAsync(username);
            await Password_field.FillAsync(password);
            await Login_button.ClickAsync();
        }
        public async Task PerformLogin(string url , string username , string password)
        {
            await NavigateToUrl(url);
            await FillLoginDetails(username, password);
        }
    }
}