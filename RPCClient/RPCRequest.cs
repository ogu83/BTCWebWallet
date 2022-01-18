namespace BTCWebWallet.RPCClient;

public abstract class RPCRequest : RPCSerializable
{
    public RPCRequest(string method, string id)
    {
        Method = method;
        Id = id;
        Jsonrpc = RPCClient.Jsonrpc;
    }

    public RPCRequest(string method)
    {
        Method = method;
        Id = Guid.NewGuid().ToString();
        Jsonrpc = RPCClient.Jsonrpc;
    }

    public string Method { get; set; }
    public string Id { get; set; }
    public string Jsonrpc { get; set; }
    public virtual List<object> Params
    {
        get
        {
            return new List<object>();
        }
    }
}