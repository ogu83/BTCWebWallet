using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BTCWebWallet.Models;
using BTCWebWallet.RPCClient;

namespace BTCWebWallet.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRPCClient _rpcClient;

    public HomeController(
        ILogger<HomeController> logger, 
        IRPCClient rpcClient)
    {
        _logger = logger;
        _rpcClient = rpcClient;
    }

    public async Task<IActionResult> Index()
    {
        var id = Guid.NewGuid();
        var networkInfo = await _rpcClient.GetNetworkInfo(new NetworkInfoRequest($"BTCWebWallet_{id}"));        
        var newWallet = await _rpcClient.GetCreateWallet(new CreateWalletRequest($"BTCWebWallet_{id}", "testwallet3", "passphrase3"));
        var wallets = await _rpcClient.GetListWallets(new ListWalletsRequest($"BTCWebWallet_{id}"));
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
