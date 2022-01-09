using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BTCWebWallet.Models;
using BTCWebWallet.RPCClient;

namespace BTCWebWallet.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRPCClient _rpcClient;
    private readonly IBitcoinNode _bitcoinNode;

    public HomeController(
        ILogger<HomeController> logger, 
        IRPCClient rpcClient,
        IBitcoinNode bitcoinNode)
    {
        _logger = logger;
        _rpcClient = rpcClient;
        _bitcoinNode = bitcoinNode;
    }

    public async Task<IActionResult> Index()
    {
        var id = Guid.NewGuid();
        var networkInfo = await _rpcClient.GetNetworkInfo(new NetworkInfoRequest($"BTCWebWallet_{id}"));        
        var newWallet = await _rpcClient.GetCreateWallet(new CreateWalletRequest($"BTCWebWallet_{id}", "testwallet0", "passphrase0"));
        var wallets = await _rpcClient.GetListWallets(new ListWalletsRequest($"BTCWebWallet_{id}"));
        Console.WriteLine("Network Info");
        Console.WriteLine(networkInfo.ToString());
        Console.WriteLine("New Wallet");
        Console.WriteLine(newWallet.ToString());
        Console.WriteLine("Wallets");
        Console.WriteLine(wallets);
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
