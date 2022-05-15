namespace BTCWebWallet.RPCClient;

public class ImportPrivKeyRequest : RPCWalletRequest
{
    private const string method_name = "importprivkey";

    public ImportPrivKeyRequest(string id, string walletName, string privKey, string label = "", bool rescan = true)
        : base(method_name, id, walletName)
    {
        PrivKey = privKey;
        Label = label;
        ReScan = rescan;
    }

    public string PrivKey { get; set; }

    public string Label { get; set; }

    public bool ReScan { get; set; }

    public override List<object> Params
    {
        get
        {
            var retval = new object[]
            {
               PrivKey,
               Label,
               ReScan
            };
            return retval.ToList();
        }
    }
}
