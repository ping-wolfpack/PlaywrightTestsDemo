using System.Threading.Tasks;
using Microsoft.Playwright;


namespace PlaywrightTests.Pages;

public class SearchPage
{
  private readonly IPage _page;
  private readonly ILocator _searchTermInput;

  public SearchPage(IPage page)
  {
    _page = page;
    //_searchTermInput = page.Locator("input[name='q']");
    _searchTermInput = _page.GetByLabel("Search", new() { Exact = true });
  }

  public async Task GotoAsync(string text)
  {
    await _page.GotoAsync(text);
  }

  public async Task SearchAsync(string text)
  {
    await _searchTermInput.FillAsync(text);
    await _searchTermInput.PressAsync("Enter");
  }

  // You can provide this method to get the search results locator
  public ILocator GetSearchResultsLocator()
  {
    return _page.Locator("#rso");  // Selector for the search result container
  }

  public ILocator GetContactUsLocator()
  {
    return _page.Locator("a[href='https://www.prometheusgroup.com/contact-us']");
  }
}