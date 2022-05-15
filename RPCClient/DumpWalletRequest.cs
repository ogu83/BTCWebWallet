namespace BTCWebWallet.RPCClient;

public class DumpWalletRequest : RPCWalletRequest
{
    private const string method_name = "dumpwallet";

    public string Filename { get; set; }

    public DumpWalletRequest(string id, string walletName, string filename)
        : base(method_name, id, walletName)
    {
        Filename = filename;
    }

    public override List<object> Params
    {
        get
        {
            var retval = new object[]
            {
               Filename
            };
            return retval.ToList();
        }
    }
}
