namespace BTCWebWallet.RPCClient;

public class NetworkInfoRequest : RPCRequest
{
    private const string method_name = "getnetworkinfo";

    public NetworkInfoRequest(string id) : base(method_name, id)
    {
        
    }

    public NetworkInfoRequest() : base(method_name) 
    {

    }
}