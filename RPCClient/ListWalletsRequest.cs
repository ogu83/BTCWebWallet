namespace BTCWebWallet.RPCClient;

public class ListWalletsRequest : RPCRequest
{
    private const string method_name = "listwallets";

    public ListWalletsRequest(string id) : base(method_name, id)
    {
        
    }

    public ListWalletsRequest() : base(method_name) 
    {

    }
}