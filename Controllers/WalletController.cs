using Microsoft.AspNetCore.Mvc;
using BTCWebWallet.Models;
using BTCWebWallet.RPCClient;
using Microsoft.Extensions.Localization;

namespace BTCWebWallet.Controllers;

public class WalletController : BaseController
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRPCClient _rpcClient;
    private readonly IStringLocalizer<WalletController> _localizer;

    public WalletController(
        ILogger<HomeController> logger, 
        IRPCClient rpcClient, 
        IStringLocalizer<WalletController> localizer)
    {
        _logger = logger;
        _rpcClient = rpcClient;
        _localizer = localizer;
    }

    public async Task<IActionResult> Index()
    {
        var  model = new WalletsViewModel 
        {
            IsSuccess = true
        };

        var walletListResponse = await _rpcClient.GetListWallets(new ListWalletsRequest(rpc_id));
        if (!walletListResponse.HasError) 
        {
            model.ListWallets = walletListResponse.Result;
        }
        else 
        {
            _logger.LogError($"RPC Error {walletListResponse.Error}");
            //TODO: Show RPC Error Some Where maybe a view page for that. Custom RPC Error Page
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateWalletViewModel model)
    {
        var createWalletResponse = await _rpcClient.GetCreateWallet(new CreateWalletRequest(
            rpc_id, 
            model.Walletname ?? string.Empty, 
            model.Passphrase ?? string.Empty, 
            model.DisablePrivateKeys, 
            model.Blank, 
            model.AvoidReuse, 
            model.Descriptors, 
            model.LoadOnStartup));

        if (!createWalletResponse.HasError) 
        {
            model.IsSuccess = true;
            return RedirectToAction(nameof(Index));
        }
        else 
        {
            _logger.LogError($"RPC Error {createWalletResponse.Error}");
            //TODO: Show RPC Error Some Where maybe a view page for that. Custom RPC Error Page
            throw new Exception($"RPC Error {createWalletResponse.Error}");
        }
    }
}
