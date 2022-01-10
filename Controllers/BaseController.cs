using Microsoft.AspNetCore.Mvc;

namespace BTCWebWallet.Controllers;

public class BaseController : Controller 
{
    protected readonly string rpc_id = $"BTCWebWallet_{Guid.NewGuid().ToString()}";
}
