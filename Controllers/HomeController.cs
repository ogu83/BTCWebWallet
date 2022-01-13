﻿using System.Diagnostics;
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
        IStringLocalizer<HomeController> localizer)
    {
        _logger = logger;
        _rpcClient = rpcClient;
        _bitcoinNode = bitcoinNode;
        _appLifeTime = appLifeTime;
        _localizer = localizer;
    }

    public async Task<IActionResult> Index()
    {
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
            //TODO: Show RPC Error Some Where maybe a view page for that. Custom RPC Error Page
        }

        var blockChainInfoResponse = await _rpcClient.GetBlockChainInfo(new BlockChainInfoRequest(rpc_id));
        if (!blockChainInfoResponse.HasError) 
        {
            model.BlockChainInfo = blockChainInfoResponse.Result;
        }
        else 
        {
            _logger.LogError($"RPC Error {blockChainInfoResponse.Error}");
            //TODO: Show RPC Error Some Where maybe a view page for that. Custom RPC Error Page
        }
        
        // _logger.LogInformation()
        //var newWallet = await _rpcClient.GetCreateWallet(new CreateWalletRequest($"BTCWebWallet_{id}", "testwallet0", "passphrase0"));
        //var wallets = await _rpcClient.GetListWallets(new ListWalletsRequest($"BTCWebWallet_{id}"));
                           
        // Console.WriteLine("New Wallet");
        // Console.WriteLine(newWallet.ToString());
        
        // Console.WriteLine("Wallets");
        // Console.WriteLine(wallets);
        
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}