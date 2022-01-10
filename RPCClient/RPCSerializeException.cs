namespace BTCWebWallet.RPCClient;

public class RPCSerializeException : Exception
{    
    public RPCSerializeException(string json, string oType, Exception ex) 
        : base("RPC Serialize JSON Failed", ex)    
    {
        Json = json;
        OType = oType;
    }

    public string Json { get; set; }

    public string OType { get; set;}
}