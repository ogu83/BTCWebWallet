namespace BTCWebWallet.Models;

public class DashboardViewModel : BaseViewModel 
{
    public RPCClient.NetworkInfoResult? NetworkInfo { get; set; }

    public RPCClient.BlockChainInfoResult? BlockChainInfo { get; set; }    
}