using Microsoft.Playwright;

namespace Personal_Portfolio_test
{
    public class HomePageTests : BaseTest
    {
        [Test]
        public async Task Navigate_To_Page()
        {
            await _page.GotoAsync(BaseUrl);
            if (_page.Url != BaseUrl)
            {
                await _page.WaitForTimeoutAsync(3000);
                await _page.ScreenshotAsync(new()
                {
                    Path = ScreenshotPath($"error-{nameof(Navigate_To_Page)}.png")
                });
                Console.WriteLine("URL mismatch detected. Screenshot saved.");
            }
            Assert.That(_page.Url, Is.EqualTo(BaseUrl));
        }

        [Test]
        public async Task Verify_Header_Text()
        {
            await _page.GotoAsync(BaseUrl);
            var headerLocator = _page.GetByRole(AriaRole.Heading, new() { Name = "Georgia E Antoniou" });
            string? actualText = await headerLocator.TextContentAsync();
            Assert.That(actualText, Is.EqualTo("Georgia E Antoniou"));
        }

        [Test]
        public async Task Verify_Title()
        {
            await _page.GotoAsync(BaseUrl);
            var header4Locator = _page.GetByRole(AriaRole.Heading, new() { Name = "Quality Focused Software Tester" });
            string? actualText = await header4Locator.TextContentAsync();
            Assert.That(actualText, Is.EqualTo("Quality Focused Software Tester"));
        }

        [Test]
        public async Task Verify_The_Content_In_Home_Page()
        {
            await _page.GotoAsync(BaseUrl);
            var homePageContent = _page.GetByRole(AriaRole.Heading, new() { Name = "AI-Augmented Software Quality Specialist" });
            string? actualText = await homePageContent.TextContentAsync();
            Assert.That(actualText, Is.EqualTo("AI-Augmented Software Quality Specialist"));

            var homePageContent2 = _page.GetByRole(AriaRole.Heading, new() { Name = "From Deep Exploratory Testing to Foundations in C# Automation and Generative-AI-Driven QA. Dedicated to Defect Prevention." });
            actualText = await homePageContent2.TextContentAsync();
            Assert.That(actualText, Is.EqualTo("From Deep Exploratory Testing to Foundations in C# Automation and Generative-AI-Driven QA. Dedicated to Defect Prevention."));

            var homePageContent3 = _page.GetByText("Focused, detail-oriented tester with hands-on experience in manual methodologies and foundational Playwright automation, now applying Generative AI and agentic tools like GitHub Copilot to test smarter. Ready to deliver flawless products.");
            actualText = await homePageContent3.TextContentAsync();
            Assert.That(actualText, Is.EqualTo("Focused, detail-oriented tester with hands-on experience in manual methodologies and foundational Playwright automation, now applying Generative AI and agentic tools like GitHub Copilot to test smarter. Ready to deliver flawless products."));
        }

        [Test]
        public async Task Verify_View_My_Projects_Button()
        {
            await _page.GotoAsync(BaseUrl);
            await _page.GetByRole(AriaRole.Link, new() { Name = "View My Projects" }).ClickAsync();
            Assert.That(_page.Url, Is.EqualTo("https://georgia-antoniou.github.io/portfolio"));
        }

        [Test]
        public async Task Check_Nav_Bar_Links()
        {
            await _page.GotoAsync(BaseUrl);
            await _page.GetByRole(AriaRole.Link, new() { Name = "Portfolio" }).ClickAsync();
            Assert.That(_page.Url, Is.EqualTo("https://georgia-antoniou.github.io/portfolio"));

            await _page.GetByRole(AriaRole.Link, new() { Name = "Journey" }).ClickAsync();
            Assert.That(_page.Url, Is.EqualTo("https://georgia-antoniou.github.io/journey"));

            await _page.GetByRole(AriaRole.Link, new() { Name = "About" }).ClickAsync();
            Assert.That(_page.Url, Is.EqualTo("https://georgia-antoniou.github.io/about"));

            await _page.GetByRole(AriaRole.Link, new() { Name = "Blog" }).ClickAsync();
            Assert.That(_page.Url, Is.EqualTo("https://georgia-antoniou.github.io/blog"));

            await _page.GetByRole(AriaRole.Link, new() { Name = "Contact" }).ClickAsync();
            Assert.That(_page.Url, Is.EqualTo("https://georgia-antoniou.github.io/contact"));
        }
    }
}
