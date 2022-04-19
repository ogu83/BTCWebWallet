namespace BTCWebWallet.RPCClient;

public class ListLabelsRequest : RPCWalletRequest
{
    private const string method_name = "listlabels";
    public const string SEND = "send";
    public const string RECIEVE = "recieve";

    public ListLabelsRequest(string id, string walletName, string purpose)
        : base(method_name, walletName)
    {
        Purpose = purpose;
    }

    public string Purpose { get; set; }

    public override List<object> Params
    {
        get 
        {
            var retval = new object[] 
            {
                Purpose         
            };

            return retval.ToList();
        }
    }
}
