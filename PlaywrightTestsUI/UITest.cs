using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PlaywrightTests.Pages;
using static Microsoft.Playwright.Assertions;


namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class UITest
{
    private IPlaywright _playwright;
    private IBrowser _browser;
    private IPage _page;
    private SearchPage _searchPage;
    private ContactUsPage _contactPage;

    [SetUp]
    public async Task SetUp()
    {
        _playwright = await Playwright.CreateAsync();  // Initialize Playwright

        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false  // Set to false to see the browser, change to true for headless mode
        });
        
        _page = await _browser.NewPageAsync();  // Create a new browser page/tab
        
        _searchPage = new SearchPage(_page);
        
        _contactPage = new ContactUsPage(_page);
    }
    [Test]
    public async Task TestSearchWithText()
    {
        await _page.GotoAsync("http://www.google.com");

        await _searchPage.SearchAsync("Prometheus Group");

        var searchResults = _searchPage.GetSearchResultsLocator();

        await Expect(searchResults).ToContainTextAsync("Prometheus Group");
     
        await _searchPage.GetContactUsLocator().ClickAsync();

        await _contactPage.SubmitAsync("Ping", "Wu");

        await Expect(_contactPage.GetEmailAddressErrorLocator()).ToContainTextAsync("Please complete this required field.");

        await Expect(_contactPage.GetPhoneNumberErrorLocator()).ToContainTextAsync("Please complete this required field.");

        await Expect(_contactPage.GetGlobalRegionErrorLocator()).ToContainTextAsync("Please complete this required field.");

        await Expect(_contactPage.GetProductInterestErrorLocator()).ToContainTextAsync("Please complete this required field.");
    }

    [TearDown]
    public async Task TearDown()
    {
        await _browser.CloseAsync();

        _playwright.Dispose();
    }

    
}