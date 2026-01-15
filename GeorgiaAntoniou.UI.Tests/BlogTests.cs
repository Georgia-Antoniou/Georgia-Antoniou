using Microsoft.Playwright;

namespace Personal_Portfolio_test
{
    public class BlogTests
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
            var waitForPageTask = _page.Context.WaitForPageAsync();
            await _page.GetByRole(AriaRole.Link, new() { Name = "State Transition Testing" }).ClickAsync();
            IPage TabPage1 = await waitForPageTask;
            await TabPage1.WaitForLoadStateAsync();
            string expectedUrl = "https://georgia-antoniou.github.io/assets/pdfs/Articles/State_Transition_Testing.pdf";
            Assert.That(TabPage1.Url, Is.EqualTo(expectedUrl));

            waitForPageTask = _page.Context.WaitForPageAsync();
            await _page.GetByRole(AriaRole.Link, new() { Name = "Error Guessing VS Checklist Based Testing" }).ClickAsync();
            IPage TabPage2 = await waitForPageTask;
            await TabPage2.WaitForLoadStateAsync();
            expectedUrl = "https://georgia-antoniou.github.io/assets/pdfs/Articles/Error_Guessing_VS_Checklist-Based_Testing.pdf";
            Assert.That(TabPage2.Url, Is.EqualTo(expectedUrl));     
        }
    }
}