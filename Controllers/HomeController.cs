using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BTCWebWallet.Models;
using BTCWebWallet.RPCClient;
using Microsoft.Extensions.Localization;

namespace BTCWebWallet.Controllers;

public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRPCClient _rpcClient;
    private readonly IBitcoinNode _bitcoinNode;
    private readonly IHostApplicationLifetime _appLifeTime;
    private readonly IStringLocalizer<HomeController> _localizer;

    public HomeController(
        ILogger<HomeController> logger,
        IRPCClient rpcClient,
        IBitcoinNode bitcoinNode,
        IHostApplicationLifetime appLifeTime,
        IStringLocalizer<HomeController> localizer,
        IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        _logger = logger;
        _rpcClient = rpcClient;
        _bitcoinNode = bitcoinNode;
        _appLifeTime = appLifeTime;
        _localizer = localizer;
    }

    public async Task<IActionResult> Index()
    {        
        while(!_bitcoinNode.IsReady())
        {
            Thread.Sleep(100);
        }
        
        var  model = new DashboardViewModel 
        {
            IsSuccess = true
        };

        var networkInfoResponse = await _rpcClient.GetNetworkInfo(new NetworkInfoRequest(rpc_id));
        if (!networkInfoResponse.HasError) 
        {
            model.NetworkInfo = networkInfoResponse.Result;
        }
        else 
        {
            _logger.LogError($"RPC Error {networkInfoResponse.Error}");
            AddPageError(RPCErrorToErrorViewModel(networkInfoResponse.Error));
        }

        var blockChainInfoResponse = await _rpcClient.GetBlockChainInfo(new BlockChainInfoRequest(rpc_id));
        if (!blockChainInfoResponse.HasError) 
        {
            model.BlockChainInfo = blockChainInfoResponse.Result;
        }
        else 
        {
            _logger.LogError($"RPC Error {blockChainInfoResponse.Error}");
            AddPageError(RPCErrorToErrorViewModel(blockChainInfoResponse.Error));
        }
        
        return View(model);
    }

    public IActionResult Shutdown() 
    {        
        _bitcoinNode.Terminate();
        _appLifeTime.StopApplication();                     
        return new JsonResult(new ShutDownViewModel());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost]    
    public async Task<IActionResult> VerifyChain(VerifyChainModel model) 
    {
        if (model == null) 
        {
            throw new ArgumentNullException("VerifyChain, model required");
        }

        var verifyChainResponse = await _rpcClient.GetVerifyChainInfo(new VerifyChainRequest(rpc_id, model.CheckLevel, model.NBlocks));
        if (verifyChainResponse != null) 
        {
            if (!verifyChainResponse.HasError) 
            {
                model.Result = verifyChainResponse.Result != null ? verifyChainResponse.Result.Value : false;
            }
            else 
            {
                _logger.LogError($"RPC Error {verifyChainResponse.Error}");
                AddPageError(RPCErrorToErrorViewModel(verifyChainResponse.Error));
            }
        }

        return new JsonResult(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}