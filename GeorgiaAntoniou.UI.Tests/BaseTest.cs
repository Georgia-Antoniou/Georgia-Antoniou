using Microsoft.Playwright;

namespace Personal_Portfolio_test
{
    /// <summary>
    /// Shared Playwright lifecycle for all UI tests: launches a browser per test and
    /// disposes it on teardown so no Chromium processes leak. Runs headless by default;
    /// set the HEADED=1 environment variable to watch the tests run.
    /// </summary>
    public abstract class BaseTest
    {
        protected const string BaseUrl = "https://georgia-antoniou.github.io/";

        protected IPlaywright _playwright = null!;
        protected IBrowser _browser = null!;
        protected IPage _page = null!;

        [SetUp]
        public async Task Setup()
        {
            _playwright = await Playwright.CreateAsync();
            var headed = Environment.GetEnvironmentVariable("HEADED") == "1";
            _browser = await _playwright.Chromium.LaunchAsync(new()
            {
                Headless = !headed
            });
            _page = await _browser.NewPageAsync();
        }

        [TearDown]
        public async Task TearDown()
        {
            if (_page is not null)
            {
                await _page.CloseAsync();
            }

            if (_browser is not null)
            {
                await _browser.DisposeAsync();
            }

            _playwright?.Dispose();
        }

        /// <summary>
        /// Resolves a path under the project's Screenshots folder, portable across machines.
        /// </summary>
        protected static string ScreenshotPath(string fileName)
        {
            var dir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Screenshots");
            Directory.CreateDirectory(dir);
            return Path.Combine(dir, fileName);
        }

        // Auto-retrying web-first assertions. Essential for a Blazor WASM SPA where content
        // renders asynchronously after navigation, and for PDF tabs that never reach the
        // "Load" state (so ToHaveURLAsync polling is more reliable than WaitForLoadStateAsync).
        protected static ILocatorAssertions Expect(ILocator locator) =>
            Assertions.Expect(locator);

        protected static IPageAssertions Expect(IPage page) =>
            Assertions.Expect(page);
    }
}
