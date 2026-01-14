using Microsoft.Playwright;
using NUnit.Framework;
using static System.Net.Mime.MediaTypeNames;

namespace Personal_Portfolio_test
{
    public class FooterTests
    {
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IPage _page;

        [SetUp]
        public async Task Setup()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new()
            {
                Headless = false
            });

            _page = await _browser.NewPageAsync();

        }

        [Test]

        public async Task Verify_Copy_right_Content()
        {
            await _page.GotoAsync("https://georgia-antoniou.github.io/");
            var footerText = _page.GetByText("© 2026 Georgia E Antoniou. All Rights Reserved.");
            string? actualText = await footerText.TextContentAsync();
            Assert.That(actualText, Is.EqualTo("© 2026 Georgia E Antoniou. All Rights Reserved."));
        }

        [Test]

        public async Task Verify_Logo_Visible()
        {
            await _page.GotoAsync("https://georgia-antoniou.github.io/");
            await _page.WaitForTimeoutAsync(3000);
            var logo = _page.Locator("img[src*='logo.png']");
            var isVisible = await logo.IsVisibleAsync();
            Assert.That(isVisible, Is.True, "The logo was not visible on the page.");
        }

        [Test]
        public async Task Verify_LinkedIn_Link()
        {
            await _page.GotoAsync("https://georgia-antoniou.github.io/");
            var waitForPageTask = _page.Context.WaitForPageAsync();
            await _page.WaitForTimeoutAsync(3000);
            await _page.GetByRole(AriaRole.Link, new() { Name = "LinkedIn" }).ClickAsync();
            IPage newTabPage = await waitForPageTask;
            await newTabPage.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            string expectedUrl = "https://www.linkedin.com/in/georgia-antoniou-2629a819a/";
            Assert.That(newTabPage.Url, Is.EqualTo(expectedUrl));
        }

        [Test]
        public async Task Verify_gitHub_Link()
        {
            await _page.GotoAsync("https://georgia-antoniou.github.io/");
            var waitForPageTask = _page.Context.WaitForPageAsync();
            await _page.WaitForTimeoutAsync(3000);
            await _page.GetByRole(AriaRole.Link, new() { Name = "GitHub" }).ClickAsync();
            IPage TabPage2 = await waitForPageTask;
            await TabPage2.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            string expectedUrl = "https://github.com/GeorgiaAntoniou";
            Assert.That(TabPage2.Url, Is.EqualTo(expectedUrl));
        }

    }
}