using Microsoft.Playwright;
using System.Text.RegularExpressions;

namespace Personal_Portfolio_test
{
    public class AboutTests : BaseTest
    {
        private const string AboutUrl = "https://georgia-antoniou.github.io/about";

        [Test]
        public async Task Verify_About_Heading()
        {
            await _page.GotoAsync(AboutUrl);
            var heading = _page.GetByRole(AriaRole.Heading, new() { Name = "About me" });
            await Expect(heading).ToBeVisibleAsync();
        }

        [Test]
        public async Task Verify_AI_Augmented_Skills_Section()
        {
            await _page.GotoAsync(AboutUrl);
            var aiSkills = _page.GetByRole(AriaRole.Heading, new() { Name = "AI-Augmented QA" });
            await Expect(aiSkills).ToBeVisibleAsync();
        }

        // Verifies specific certification links point at the correct PDF. We assert the
        // rendered href (and that it opens in a new tab) rather than the opened PDF tab,
        // because headless Chromium treats PDF links as downloads and never navigates the tab.
        [Test]
        public async Task Verify_Cert_Credential()
        {
            await _page.GotoAsync(AboutUrl);

            await AssertCredentialLink("GenAI_AI_Agents_Cert.pdf");
            await AssertCredentialLink("ISTQB-Cert.pdf");
            await AssertCredentialLink("CSS-certification.pdf");
        }

        private async Task AssertCredentialLink(string pdfFileName)
        {
            var link = _page.Locator($"a[href$='{pdfFileName}']");
            await Expect(link).ToHaveAttributeAsync("href", new Regex($"/assets/pdfs/Certs/{Regex.Escape(pdfFileName)}$"));
            await Expect(link).ToHaveAttributeAsync("target", "_blank");
        }

        [Test]
        public async Task Validate_Back_To_Top_Button()
        {
            await _page.GotoAsync(AboutUrl);
            var lastCard = _page.GetByRole(AriaRole.Heading, new() { Name = "Learning CSS" });
            await lastCard.ScrollIntoViewIfNeededAsync();
            await _page.WaitForTimeoutAsync(1000);
            await _page.GetByLabel("Back to Top").ClickAsync();

            var scrollY = await _page.EvaluateAsync<double>("window.scrollY");
            Assert.That(scrollY, Is.EqualTo(0), "The page did not scroll back to the top!");
        }
    }
}
