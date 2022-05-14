using System.Text;
using System.Web;
using BTCWebWallet.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BTCWebWallet.Controllers;

public class BaseController : Controller
{
    protected readonly string rpc_id = $"BTCWebWallet_{Guid.NewGuid().ToString()}";

    protected readonly ISession? _session;

    protected readonly IHttpContextAccessor _httpContextAccessor;

    public BaseController(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        if (_httpContextAccessor.HttpContext != null)
        {
            _session = _httpContextAccessor.HttpContext?.Session;
        }
    }

    public IActionResult SetCulture(string id = "en")
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

    protected void SetSession<T>(string key, T obj) where T : class
    {
        var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));
        HttpContext.Session.Set(key, bytes);
    }

    protected T? GetSession<T>(string key) where T : class
    {
        var bytes = HttpContext.Session.Get(key);
        if (bytes != null)
        {
            var json = Encoding.UTF8.GetString(bytes);
            T? obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }
        else
        {
            return null;
        }
    }

    protected ErrorViewModel? RPCErrorToErrorViewModel(RPCClient.RPCError? error)
    {
        if (error == null)
        {
            return null;
        }

        return new ErrorViewModel
        {
            Code = error.Code,
            Message = HttpUtility.HtmlEncode(error.Message),
            Type = ErrorViewModel.ErrorType.RCPError
        };
    }

    public void AddPageError(ErrorViewModel? model)
    {
        if (model != null)
        {
            var errors = GetSession<List<ErrorViewModel>>("Errors") ?? new List<ErrorViewModel>();
            errors.Add(model);
            SetSession<List<ErrorViewModel>>("Errors", errors);
        }
    }

    public void AddPageError(List<ErrorViewModel> model)
    {
        var errors = GetSession<List<ErrorViewModel>>("Errors") ?? new List<ErrorViewModel>();
        errors.AddRange(model);
        SetSession<List<ErrorViewModel>>("Errors", errors);
    }

    public void ClearPageError()
    {
        var errors = GetSession<List<ErrorViewModel>>("Errors") ?? new List<ErrorViewModel>();
        errors.Clear();
        SetSession<List<ErrorViewModel>>("Errors", errors);
    }

    [HttpPost]
    public IActionResult RemovePageError(int code)
    {
        var errors = GetSession<List<ErrorViewModel>>("Errors") ?? new List<ErrorViewModel>();
        var error = errors.FirstOrDefault(x => x.Code == code);
        if (error != null)
        {
            errors.Remove(error);
            SetSession<List<ErrorViewModel>>("Errors", errors);
        }

        return new JsonResult(true);
    }
}