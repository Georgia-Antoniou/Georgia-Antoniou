using Microsoft.Playwright;

namespace Personal_Portfolio_test
{
    public class ContactTests
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

        public async Task Verify_Contact_Heading() 
        {
            await _page.GotoAsync("https://georgia-antoniou.github.io/contact");
            var headerLocator = _page.GetByText("Contact Me");
            string? actualText = await headerLocator.TextContentAsync();
            Assert.That(actualText, Is.EqualTo("Contact Me"));
        }

        [Test]

        public async Task Verify_Contact_Email_Link() 
        {
            await _page.GotoAsync("https://georgia-antoniou.github.io/contact");
            var emailLink = _page.GetByRole(AriaRole.Link, new() { Name = "georgiaantoniou96@gmail.com" });
            string? hrefValue = await emailLink.GetAttributeAsync("href");
            Assert.That(hrefValue, Is.EqualTo("mailto:georgiaantoniou96@gmail.com"));

        }

    }
}