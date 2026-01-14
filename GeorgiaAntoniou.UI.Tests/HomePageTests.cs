using Microsoft.Playwright;

namespace Personal_Portfolio_test
{
    public class HomePageTests
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
        public async Task Navigate_To_Page()
        {
            await _page.GotoAsync("https://georgia-antoniou.github.io/");
            if (_page.Url != "https://georgia-antoniou.github.io/")
            {
                await _page.WaitForTimeoutAsync(3000);
                await _page.ScreenshotAsync(new()
                {
                    Path = @$"C:\Users\Georgia\source\repos\Personal_Portfolio_test\Personal_Portfolio_test\Screenshots\error-{nameof(Navigate_To_Page)}.png"

                });
                Console.WriteLine("URL mismatch detected. Screenshot saved.");
            }
            Assert.That(_page.Url, Is.EqualTo("https://georgia-antoniou.github.io/"));
        }

        [Test]

        public async Task Verify_Header_Text()
        {
            await _page.GotoAsync("https://georgia-antoniou.github.io/");
            var headerLocator = _page.GetByRole(AriaRole.Heading, new() { Name = "Georgia E Antoniou" });
            string? actualText = await headerLocator.TextContentAsync();
            Assert.That(actualText, Is.EqualTo("Georgia E Antoniou"));
        }

        [Test]

        public async Task Verify_Title()
        {
            await _page.GotoAsync("https://georgia-antoniou.github.io/");
            var header4Locator = _page.GetByRole(AriaRole.Heading, new() { Name = "Quality Focused Software Tester" });
            string? actualText = await header4Locator.TextContentAsync();
            Assert.That(actualText, Is.EqualTo("Quality Focused Software Tester"));
        }

        [Test]

        public async Task Verify_The_Content_In_Home_Page()
        {
            await _page.GotoAsync("https://georgia-antoniou.github.io/");
            var homePageContent = _page.GetByRole(AriaRole.Heading, new() { Name = "Certified Software Quality Specialist" });
            string? actualText = await homePageContent.TextContentAsync();
            Assert.That(actualText, Is.EqualTo("Certified Software Quality Specialist"));

            var homePageContent2 = _page.GetByRole(AriaRole.Heading, new() { Name = "From Deep Exploratory Testing to Foundations in C# Automation. Dedicated to Defect Prevention." });
            actualText = await homePageContent2.TextContentAsync();
            Assert.That(actualText, Is.EqualTo("From Deep Exploratory Testing to Foundations in C# Automation. Dedicated to Defect Prevention."));

            var homePageContent3 = _page.GetByText("Focused, detail-oriented tester with hands-on experience in manual methodologies and foundational Playwright automation. Ready to deliver flawless products.");
            actualText = await homePageContent3.TextContentAsync();
            Assert.That(actualText, Is.EqualTo("Focused, detail-oriented tester with hands-on experience in manual methodologies and foundational Playwright automation. Ready to deliver flawless products."));
        }


        [Test]

        public async Task Verify_View_My_Projects_Button()
        {
            await _page.GotoAsync("https://georgia-antoniou.github.io/");
            await _page.GetByRole(AriaRole.Link, new() { Name = "View My Projects" }).ClickAsync();
        }


        [Test]

        public async Task Check_Nav_Bar_Links()
        {
            await _page.GotoAsync("https://georgia-antoniou.github.io/");
            await _page.GetByRole(AriaRole.Link, new() { Name = "Portfolio" }).ClickAsync();
            Assert.That(_page.Url, Is.EqualTo("https://georgia-antoniou.github.io/portfolio"));
            Console.WriteLine("Portfolio");

            await _page.GetByRole(AriaRole.Link, new() { Name = "About" }).ClickAsync();
            Assert.That(_page.Url, Is.EqualTo("https://georgia-antoniou.github.io/about"));
            Console.WriteLine("About");

            await _page.GetByRole(AriaRole.Link, new() { Name = "Blog" }).ClickAsync();
            Assert.That(_page.Url, Is.EqualTo("https://georgia-antoniou.github.io/blog"));
            Console.WriteLine("Blog");

            await _page.GetByRole(AriaRole.Link, new() { Name = "Contact" }).ClickAsync();
            Assert.That(_page.Url, Is.EqualTo("https://georgia-antoniou.github.io/contact"));
            Console.WriteLine("Contact");
        }


        [Test]

        public async Task Validate_Back_To_Top_Button()
        {
            await _page.GotoAsync("https://georgia-antoniou.github.io/about");
            var certCard = _page.GetByRole(AriaRole.Heading, new() { Name = "Learn C# Course" });
            await certCard.ScrollIntoViewIfNeededAsync();
            await _page.WaitForTimeoutAsync(2000);
            await _page.GetByLabel("Back to Top").ClickAsync();

            var scrollY = await _page.EvaluateAsync<double>("window.scrollY");
            Assert.That(scrollY, Is.EqualTo(0), "The page did not scroll back to the top!");
            await _page.WaitForTimeoutAsync(2000);
        }

    }
}
