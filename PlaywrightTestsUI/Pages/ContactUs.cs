using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PlaywrightTests.Pages;

public class ContactUsPage
{
  private readonly IPage _page;
  private ILocator _txtFirstName => _page.GetByLabel("First Name*");
  private ILocator _txtLastName => _page.GetByLabel("Last Name*");
  private ILocator _btnSubmit => _page.GetByRole(AriaRole.Button, new() { Name = "Submit" });

  public ContactUsPage(IPage page)
  {
    _page = page;
  }

  public async Task SubmitAsync(string First, string Last)
  {
    await _txtFirstName.FillAsync(First);
    await _txtLastName.FillAsync(Last);
    await _btnSubmit.ClickAsync();
  }

  public ILocator GetEmailAddressErrorLocator()
  {
    var errorContainer = _page.Locator(".hs_email");
    var errorMessage = errorContainer.Locator(".hs-error-msg");
    return errorMessage;

  }

  public ILocator GetPhoneNumberErrorLocator()
  {
    var errorContainer = _page.Locator(".hs_phone");
    var errorMessage = errorContainer.Locator(".hs-error-msg");
    return errorMessage;
  }

  public ILocator GetGlobalRegionErrorLocator()
  {
    var errorContainer = _page.Locator(".hs-global_region");
    var errorMessage = errorContainer.Locator(".hs-error-msg");
    return errorMessage;
  }

  public ILocator GetProductInterestErrorLocator()
  {
    var errorContainer = _page.Locator(".hs-product");
    var errorMessage = errorContainer.Locator(".hs-error-msg");
    return errorMessage;
  }

}