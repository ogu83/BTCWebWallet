using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace BTCWebWallet.Controllers;

public class BaseController : Controller 
{
    protected readonly string rpc_id = $"BTCWebWallet_{Guid.NewGuid().ToString()}";

    public IActionResult SetCulture(string id="en")
    {
        string culture = id;
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
        );

        //ViewData["Message"] = "Culture set to " + culture;

        return View();
    }

    [HttpPost]
    public IActionResult SetLanguage(string culture, string returnUrl)
    {
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
        );

        return LocalRedirect(returnUrl);
    }
}