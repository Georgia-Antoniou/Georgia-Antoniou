using Microsoft.Playwright;
using System.Text.RegularExpressions;

namespace Personal_Portfolio_test
{
    public class PortfolioTests : BaseTest
    {
        [Test]

        public async Task Correct_Url()
        {
            await _page.GotoAsync("https://georgia-antoniou.github.io/portfolio");
            Assert.That(_page.Url, Is.EqualTo("https://georgia-antoniou.github.io/portfolio"));

        }

        [Test]
        public async Task Validate_Project_cards()
        {
            await _page.GotoAsync("https://georgia-antoniou.github.io/portfolio");
            await _page.GetByRole(AriaRole.Link, new() { Name = "View Details" }).First.ClickAsync();
            Assert.That(_page.Url, Is.EqualTo("https://georgia-antoniou.github.io/portfolio/agile-travel/details"));
            await _page.GoBackAsync();
            await _page.WaitForTimeoutAsync(2000);
            await _page.GetByRole(AriaRole.Link, new() { Name = "View Details" }).Nth(4).ClickAsync();
            Assert.That(_page.Url, Is.EqualTo("https://georgia-antoniou.github.io/portfolio/socra-dot-com/details"));
            await _page.WaitForTimeoutAsync(2000);
        }

        [Test]

        public async Task Validate_Key_Artifacts()
        {
            await _page.GotoAsync("https://georgia-antoniou.github.io/portfolio/georgia-e-antoniou-portfolio/details");
            // The Test Plan opens a PDF in a new tab. Headless Chromium treats PDF links as
            // downloads and never navigates the tab, so we assert the rendered href instead.
            var testPlan = _page.GetByRole(AriaRole.Link, new() { Name = "Test Plan" });
            await Expect(testPlan).ToHaveAttributeAsync("href", new Regex(@"assets/pdfs/Personal-portfolio/Portfolio_Test_plan\.pdf$"));
        }

        [Test]

        public async Task Validate_Key_Artifacts2()
        {
            await _page.GotoAsync("https://georgia-antoniou.github.io/portfolio/socra-dot-com/details");
            var waitForPageTask = _page.Context.WaitForPageAsync();
            await _page.GetByRole(AriaRole.Link, new() { Name = "The Website" }).ClickAsync();
            IPage newTabPage = await waitForPageTask;
            await newTabPage.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            string expectedUrl = "https://socra.com/";
            Assert.That(newTabPage.Url, Is.EqualTo(expectedUrl));

            await newTabPage.CloseAsync();
            var waitForPageTask2 = _page.Context.WaitForPageAsync();
            await _page.GetByRole(AriaRole.Link, new() { Name = "Automation Tests" }).ClickAsync();

            IPage tab2 = await waitForPageTask2;
            await tab2.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            Assert.That(tab2.Url, Is.EqualTo("https://github.com/Georgia-Antoniou/Socra.UI.Tests/tree/master"));

        }
    }
}
