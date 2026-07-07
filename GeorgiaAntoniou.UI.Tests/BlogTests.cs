using Microsoft.Playwright;
using System.Text.RegularExpressions;

namespace Personal_Portfolio_test
{
    public class BlogTests : BaseTest
    {
        [Test]

        public async Task Verify_SearchBar()
        {
            await _page.GotoAsync("https://georgia-antoniou.github.io/blog");
            var searchInput = _page.GetByPlaceholder("               Search...");
            await searchInput.FillAsync("principles");

            var PrincipleArticle = _page.GetByRole(AriaRole.Link, new() { Name = "Principles of Testing" });
            await Assert.ThatAsync(async () => await PrincipleArticle.IsVisibleAsync(), Is.True);

            var PrioritizationArticle = _page.GetByRole(AriaRole.Link, new() { Name = "Test Case Prioritization" });
            await Assert.ThatAsync(async () => await PrioritizationArticle.IsHiddenAsync(), Is.True);
            await _page.WaitForTimeoutAsync(2000);

            await searchInput.FillAsync("");

            await searchInput.FillAsync("myth");
            var mythBuster1 = _page.GetByRole(AriaRole.Link, new() { Name = "The QA Mythbuster Series Episode 1" });
            await Assert.ThatAsync(async () => await mythBuster1.IsVisibleAsync(), Is.True);
            var mythBuster2 = _page.GetByRole(AriaRole.Link, new() { Name = "The QA Mythbuster Series Episode 2" });
            await Assert.ThatAsync(async () => await mythBuster2.IsVisibleAsync(), Is.True);
            var mythBuster3 = _page.GetByRole(AriaRole.Link, new() { Name = "The QA Mythbuster Series Episode 3" });
            await Assert.ThatAsync(async () => await mythBuster3.IsVisibleAsync(), Is.True);
            await searchInput.FillAsync("");

            
        }

        [Test]

        public async Task Verify_Article_Links()
        {
            await _page.GotoAsync("https://georgia-antoniou.github.io/blog");
            // Article links open PDFs in a new tab. Headless Chromium treats PDF links as
            // downloads and never navigates the tab, so we assert the rendered href instead.
            var stateTransition = _page.GetByRole(AriaRole.Link, new() { Name = "State Transition Testing" });
            await Expect(stateTransition).ToHaveAttributeAsync("href", new Regex(@"assets/pdfs/Articles/State_Transition_Testing\.pdf$"));

            var errorGuessing = _page.GetByRole(AriaRole.Link, new() { Name = "Error Guessing VS Checklist Based Testing" });
            await Expect(errorGuessing).ToHaveAttributeAsync("href", new Regex(@"assets/pdfs/Articles/Error_Guessing_VS_Checklist-Based_Testing\.pdf$"));
        }
    }
}