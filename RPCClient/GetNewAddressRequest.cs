namespace BTCWebWallet.RPCClient;

public class GetNewAddressRequest : RPCWalletRequest
{
    private const string method_name = "getnewaddress";

    public GetNewAddressRequest(string id, string walletName, string label, string address_type)
        : base(method_name, walletName)
    {
        Label = label;
        AddressType = address_type;
    }

    public string Label { get; set; }

    public string AddressType { get; set; }

    public override List<object> Params
    {
        get
        {
            var retval = new object[] 
            {
                Label,
                AddressType         
            };

            return retval.ToList();
        }
    }
}
