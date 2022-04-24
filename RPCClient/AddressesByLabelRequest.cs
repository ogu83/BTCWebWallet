namespace BTCWebWallet.RPCClient;

public class AddressesByLabelRequest : RPCWalletRequest
{
    private const string method_name = "getaddressesbylabel";

    public AddressesByLabelRequest(string id, string walletName, string label)
        : base(method_name, walletName)
    {
        Label = label;
    }

    public string Label { get; set; }

    public override List<object> Params
    {
        get
        {
            var retval = new object[]
            {
                Label
            };

            return retval.ToList();
        }
    }
}