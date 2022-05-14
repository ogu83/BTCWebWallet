namespace BTCWebWallet.RPCClient;

public class DumpPrivKeyRequest : RPCWalletRequest
{
    private const string method_name = "dumpprivkey";

    public DumpPrivKeyRequest(string id, string walletName, string address)
        : base(method_name, id, walletName)
    {
        Address = address;
    }

    public string Address { get; set; }

    public override List<object> Params
    {
        get
        {
            var retval = new object[]
            {
               Address
            };
            return retval.ToList();
        }
    }
}
