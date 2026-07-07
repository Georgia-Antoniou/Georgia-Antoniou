using Microsoft.Playwright;
using NUnit.Framework;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace Personal_Portfolio_test
{
    public class FooterTests : BaseTest
    {
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
            // LinkedIn redirects unauthenticated visitors to an authwall, so we verify the
            // rendered href the site controls rather than LinkedIn's post-redirect URL.
            var linkedIn = _page.GetByRole(AriaRole.Link, new() { Name = "LinkedIn" });
            await Expect(linkedIn).ToHaveAttributeAsync("href", new Regex(@"linkedin\.com/in/georgia-antoniou-2629a819a"));
        }

        [Test]
        public async Task Verify_gitHub_Link()
        {
            await _page.GotoAsync("https://georgia-antoniou.github.io/");
            var waitForPageTask = _page.Context.WaitForPageAsync();
            await _page.GetByRole(AriaRole.Link, new() { Name = "GitHub" }).ClickAsync();
            IPage TabPage2 = await waitForPageTask;
            await Expect(TabPage2).ToHaveURLAsync("https://github.com/GeorgiaAntoniou");
        }

    }
}