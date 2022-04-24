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

        var labelsResponse = await _rpcClient.GetListLabels(new ListLabelsRequest(rpc_id, name, "receive"));
        if (!labelsResponse.HasError)
        {
            model.IsSuccess = model.IsSuccess && true;
            model.Addresses = new List<AddressViewModel>();
            foreach (var label in labelsResponse.Result ?? new List<StringResult>())
            {
                var str_label = label == null ? string.Empty : label.Value;
                var addressesResponse = await _rpcClient.GetAddressesByLabel(new AddressesByLabelRequest(rpc_id, name, str_label));
                if (!addressesResponse.HasError)
                {
                    model.IsSuccess = model.IsSuccess && true;
                    if (addressesResponse.Result != null)
                    {
                        model.Addresses
                             .AddRange(addressesResponse.Result
                                                        .Select(x => new AddressViewModel
                                                        {
                                                            Key = x.Key,
                                                            Purpose = x.Value.Purpose,
                                                            Label = str_label
                                                        }));
                    }
                }
                else
                {
                    _logger.LogError($"RPC Error {addressesResponse.Error}");
                    AddPageError(RPCErrorToErrorViewModel(addressesResponse.Error));
                }
            }
        }
        else
        {
            _logger.LogError($"RPC Error {labelsResponse.Error}");
            AddPageError(RPCErrorToErrorViewModel(labelsResponse.Error));
        }

        var transactionsResponse = await _rpcClient.GetListTransactions(new ListTransactionsRequest(rpc_id, name));
        if (!transactionsResponse.HasError)
        {
            model.IsSuccess = model.IsSuccess && true;
            var transactionsModel = transactionsResponse.Result?.Select(t => new TransactionViewModel
            {
                Id = t.Txid,
                Time = t.Time.ToDateTime(),
                RecievedTime = t.Timereceived.ToDateTime(),
                Amount = t.Amount,
                Fee = t.Fee,
                Category = t.Category,
                Abandoned = t.Abandoned,
                Address = t.Address,
                Confirmations = t.Confirmations,
                Label = t.Label,
                Trusted = t.Trusted,
                Comment = t.Comment,
                Bip125Replaceable = t.Bip125Replaceable
            }).ToList();
            model.Transactions = transactionsModel;
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

    [HttpPost]
    public async Task<IActionResult> AddNewAddress(string name, string label, string addressType)
    {
        var response = await _rpcClient.GetNewAddress(new GetNewAddressRequest(rpc_id, name, label, addressType));
        if (response.HasError)
        {
            _logger.LogError($"RPC Error {response.Error}");
            AddPageError(RPCErrorToErrorViewModel(response.Error));
        }

        return Json(true);
    }

    public async Task<IActionResult> SendToAddress(
        string name, string address, decimal amount,
        string comment, string comment_to,
        bool subtractfeefromamount, bool replaceable, int conf_target, string estimate_mode, bool avoid_reuse)
    {
        var response = await _rpcClient.SendToAddress(new SendToAddressRequest(
            rpc_id, name,
            address, amount,
            comment, comment_to,
            subtractfeefromamount, replaceable, conf_target, estimate_mode, avoid_reuse));

        if (response.HasError)
        {
            _logger.LogError($"RPC Error {response.Error}");
            AddPageError(RPCErrorToErrorViewModel(response.Error));
        }

        return Json(response.Result?.Value);
    }
}