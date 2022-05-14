namespace BTCWebWallet.RPCClient;

public class BalancesRequest : RPCWalletRequest
{
    private const string method_name = "getbalances";

    public BalancesRequest(string id, string walletName) 
        : base(method_name, id, walletName)
    {
        
    }
}