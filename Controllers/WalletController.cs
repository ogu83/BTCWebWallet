using Microsoft.AspNetCore.Mvc;
using BTCWebWallet.Models;
using BTCWebWallet.RPCClient;
using Microsoft.Extensions.Localization;
using BTCWebWallet.Helpers;

namespace BTCWebWallet.Controllers;

public class WalletController : BaseController
{
    private readonly ILogger<WalletController> _logger;
    private readonly IRPCClient _rpcClient;
    private readonly IStringLocalizer<WalletController> _localizer;
    private readonly IConfiguration _configuration;
    
    public WalletController(
        ILogger<WalletController> logger,
        IRPCClient rpcClient,
        IStringLocalizer<WalletController> localizer,
        IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration) : base(httpContextAccessor)
    {
        _logger = logger;
        _rpcClient = rpcClient;
        _localizer = localizer;
        _configuration = configuration;
    }

    public async Task<IActionResult> Index()
    {
        var model = new WalletsViewModel
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
            AddPageError(RPCErrorToErrorViewModel(walletListResponse.Error));
        }

        return View(model);
    }

    public async Task<IActionResult> Info(string name)
    {
        var model = new WalletViewModel
        {
            IsSuccess = true
        };

        var walletResponse = await _rpcClient.GetWalletInfo(new WalletInfoRequest(rpc_id, name));
        if (!walletResponse.HasError)
        {
            model.IsSuccess = true;
            model.WalletInfo = walletResponse.Result;
        }
        else
        {
            _logger.LogError($"RPC Error {walletResponse.Error}");
            AddPageError(RPCErrorToErrorViewModel(walletResponse.Error));
        }

        var balancesResponse = await _rpcClient.GetBalances(new BalancesRequest(rpc_id, name));
        if (!balancesResponse.HasError)
        {
            model.IsSuccess = model.IsSuccess && true;
            model.Balances = balancesResponse.Result;
        }
        else
        {
            _logger.LogError($"RPC Error {balancesResponse.Error}");
            AddPageError(RPCErrorToErrorViewModel(balancesResponse.Error));
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Unlock(string name, string passphrase)
    {
        var timeout = 600;
        try
        {
            timeout = int.Parse(_configuration.GetSection("WalletSettings").GetSection("UnlockTimeout").Value);
        }
        catch
        {

        }

        var response = await _rpcClient.GetWalletPassphrase(new PassphraseRequest(rpc_id, name, passphrase, timeout));
        if (response.HasError)
        {
            _logger.LogError($"RPC Error {response.Error}");
            AddPageError(RPCErrorToErrorViewModel(response.Error));
        }

        return Json(true);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateWalletViewModel model)
    {
        var validations = model.Validate();
        if (validations != null && validations.Count > 0)
        {
            AddPageError(validations);
            return RedirectToAction(nameof(Index));
        }

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
        }
        else
        {
            _logger.LogError($"RPC Error {createWalletResponse.Error}");
            AddPageError(RPCErrorToErrorViewModel(createWalletResponse.Error));
        }

        return RedirectToAction(nameof(Index));
    }
}