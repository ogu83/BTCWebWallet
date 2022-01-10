namespace BTCWebWallet.RPCClient;

public class Network 
{
    public string? Name { get; set; }
    public bool Limited { get; set; }
    public bool Reachable { get; set; }
    public string? Proxy { get; set; }
    public bool Proxy_randomize_credentials { get; set; }
}