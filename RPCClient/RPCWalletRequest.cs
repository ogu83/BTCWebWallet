using Newtonsoft.Json;

namespace BTCWebWallet.RPCClient;

public abstract class RPCWalletRequest : RPCRequest
{
    public RPCWalletRequest(string method, string id, string walletName) : base(method, id)
    {
        WalletName = walletName;
    }

    public RPCWalletRequest(string method, string walletName) : base(method)
    {
        WalletName = walletName;
    }

    [JsonIgnore]
    public readonly string WalletName;
}
