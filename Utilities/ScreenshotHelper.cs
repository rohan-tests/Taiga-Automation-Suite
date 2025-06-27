using Microsoft.Playwright;

namespace Utilities.ScreenshotHelper
{
    public static class ScreenshotHelper
    {
        public static async Task<string> CaptureScreenshotAsync(IPage page, string scenarioTitle)
        {
            var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            var screenshotsDir = Path.Combine(projectRoot, "Screenshots");
            Directory.CreateDirectory(screenshotsDir);

            var fileName = $"{scenarioTitle.Replace(" ", "_")}.png";
            var filePath = Path.Combine(screenshotsDir, fileName);

            await page.ScreenshotAsync(new PageScreenshotOptions { Path = filePath, FullPage = true });
            return filePath;
        }
    }
}
