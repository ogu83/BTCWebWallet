namespace BTCWebWallet.RPCClient;

public class WalletInfoRequest : RPCWalletRequest
{
    private const string method_name = "getwalletinfo";

    public WalletInfoRequest(string id, string walletName)
        : base(method_name, id, walletName)
    {

    }
}