using Microsoft.Playwright;

namespace Personal_Portfolio_test
{
    public class ContactTests : BaseTest
    {
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

        [Test]
        public async Task Submitting_Valid_Form_Shows_Thank_You_Message()
        {
            await _page.GotoAsync("https://georgia-antoniou.github.io/contact");

            await _page.GetByRole(AriaRole.Textbox, new() { Name = "Name:" }).FillAsync("Test User");
            await _page.GetByRole(AriaRole.Textbox, new() { Name = "Email:" }).FillAsync("test@example.com");
            await _page.GetByRole(AriaRole.Textbox, new() { Name = "Message:" }).FillAsync("Automated exploratory test message.");
            await _page.GetByRole(AriaRole.Button, new() { Name = "Send Message" }).ClickAsync();

            var thankYou = _page.GetByText("Thank you for your message! I'll get back to you soon.");
            await Expect(thankYou).ToBeVisibleAsync();

            var sendAnother = _page.GetByRole(AriaRole.Button, new() { Name = "Send Another Message" });
            await Expect(sendAnother).ToBeVisibleAsync();
        }

        [Test]
        public async Task Submitting_Empty_Form_Does_Not_Show_Success()
        {
            await _page.GotoAsync("https://georgia-antoniou.github.io/contact");

            await _page.GetByRole(AriaRole.Button, new() { Name = "Send Message" }).ClickAsync();

            var thankYou = _page.GetByText("Thank you for your message! I'll get back to you soon.");
            await Assert.ThatAsync(async () => await thankYou.IsHiddenAsync(), Is.True,
                "Empty form submission should not display the success message.");

            var sendButton = _page.GetByRole(AriaRole.Button, new() { Name = "Send Message" });
            await Assert.ThatAsync(async () => await sendButton.IsVisibleAsync(), Is.True,
                "The contact form should still be visible after an empty submission.");
        }

    }
}