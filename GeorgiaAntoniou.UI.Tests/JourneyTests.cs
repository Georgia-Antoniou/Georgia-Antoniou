using Microsoft.Playwright;

namespace Personal_Portfolio_test
{
    public class JourneyTests : BaseTest
    {
        private const string JourneyUrl = "https://georgia-antoniou.github.io/journey";

        [Test]
        public async Task Verify_Journey_Heading()
        {
            await _page.GotoAsync(JourneyUrl);
            var heading = _page.GetByRole(AriaRole.Heading, new() { Name = "My Journey" });
            await Expect(heading).ToBeVisibleAsync();
        }

        [Test]
        public async Task Verify_Timeline_Cards_Present()
        {
            await _page.GotoAsync(JourneyUrl);

            var genAiCard = _page.GetByRole(AriaRole.Heading, new() { Name = "Generative AI in Software Testing" });
            await Expect(genAiCard).ToBeVisibleAsync();

            var istqbCard = _page.GetByRole(AriaRole.Heading, new() { Name = "ISTQB Foundation Level" });
            await Expect(istqbCard).ToBeVisibleAsync();

            var flipCards = _page.GetByRole(AriaRole.Button, new() { Name = "Flip card for details" });
            Assert.That(await flipCards.CountAsync(), Is.GreaterThan(1));
        }

        [Test]
        public async Task Validate_Back_To_Top_Button()
        {
            await _page.GotoAsync(JourneyUrl);
            var oldestCard = _page.GetByRole(AriaRole.Heading, new() { Name = "Learning HTML & CSS" });
            await oldestCard.ScrollIntoViewIfNeededAsync();
            await _page.WaitForTimeoutAsync(1000);
            await _page.GetByLabel("Back to Top").ClickAsync();

            var scrollY = await _page.EvaluateAsync<double>("window.scrollY");
            Assert.That(scrollY, Is.EqualTo(0), "The page did not scroll back to the top!");
        }
    }
}
