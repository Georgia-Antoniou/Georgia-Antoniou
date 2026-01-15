using Microsoft.Playwright;
using NUnit.Framework;
using static System.Net.Mime.MediaTypeNames;

namespace Personal_Portfolio_test
{
    public class AboutTests
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
        public async Task Verify_Cert_Credential()
        {
            await _page.GotoAsync("https://georgia-antoniou.github.io/about");
            var waitForPageTask = _page.Context.WaitForPageAsync();
            await _page.GetByRole(AriaRole.Link, new() { Name = "Verify Credential" }).First.ClickAsync();
            IPage TabPage = await waitForPageTask;
            await TabPage.WaitForLoadStateAsync();
            string expectedUrl = "https://georgia-antoniou.github.io/assets/pdfs/Certs/ISTQB-Cert.pdf";
            Assert.That(TabPage.Url, Is.EqualTo(expectedUrl));
            
            
            waitForPageTask = _page.Context.WaitForPageAsync();
            await _page.GetByRole(AriaRole.Link, new() { Name = "Verify Credential" }).Nth(7).ClickAsync();
            IPage TabPage2 = await waitForPageTask;
            await TabPage2.WaitForLoadStateAsync();
            expectedUrl = "https://georgia-antoniou.github.io/assets/pdfs/Certs/CSS-certification.pdf";
            Assert.That(TabPage2.Url, Is.EqualTo(expectedUrl));
            
            waitForPageTask = _page.Context.WaitForPageAsync();
            await _page.GetByRole(AriaRole.Link, new() { Name = "Verify Credential" }).Nth(5).ClickAsync();
            IPage TabPage3 = await waitForPageTask;
            await TabPage3.WaitForLoadStateAsync();
            expectedUrl = "https://georgia-antoniou.github.io/assets/pdfs/Certs/SQL-CERT.jpg";
            Assert.That(TabPage3.Url, Is.EqualTo(expectedUrl));
        }

        
        
    }
}  