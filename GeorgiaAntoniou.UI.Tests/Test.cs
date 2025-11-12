using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace GeorgiaAntoniou.UI.Tests
{
    public class Test
    {
        [Fact]
        public async Task HomePage()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
            });
            var context = await browser.NewContextAsync();

            var page = await context.NewPageAsync();
            await Task.Delay(5000);
            await page.GotoAsync("https://localhost:7170/");
           

        }

        [Fact]
        public async Task Portfolio()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
            });
            var context = await browser.NewContextAsync();

            var page = await context.NewPageAsync();
            await Task.Delay(5000);
            await page.GotoAsync("https://localhost:7170/");
            await Task.Delay(5000);
            await page.GetByRole(AriaRole.Link,new() { Name = "Portfolio" }).ClickAsync();
            

        }

        [Fact]

        public async Task Projects()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
            });
            var context = await browser.NewContextAsync();

            var page = await context.NewPageAsync();
            await page.GotoAsync("https://localhost:7170/");
            await page.GetByRole(AriaRole.Link, new() { Name = " Portfolio" }).ClickAsync();
            await page.GetByRole(AriaRole.Article).Filter(new() { HasText = "Agile Travel View Details" }).GetByRole(AriaRole.Link).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "← Back to All Projects" }).ClickAsync();
        }


        [Fact]

        public async Task Project_Socra()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
            });
            var context = await browser.NewContextAsync();

            var page = await context.NewPageAsync();
            await page.GotoAsync("https://localhost:7170/");
            await page.GetByRole(AriaRole.Link, new() { Name = " Portfolio" }).ClickAsync();
            await page.GetByRole(AriaRole.Article).Filter(new() { HasText = "socra.com View Details" }).GetByRole(AriaRole.Link).ClickAsync();
            var page1 = await page.RunAndWaitForPopupAsync(async () =>
            {
                await page.GetByRole(AriaRole.Link, new() { Name = "  Automation Tests" }).ClickAsync();
            });
            await page.GetByRole(AriaRole.Link, new() { Name = "← Back to All Projects" }).ClickAsync();
        }


    }
}
