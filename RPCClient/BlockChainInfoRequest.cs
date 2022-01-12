namespace BTCWebWallet.RPCClient;

public class BlockChainInfoRequest : RPCRequest
{
    private const string method_name = "getblockchaininfo";

    public BlockChainInfoRequest(string id) : base(method_name, id)
    {
        
    }

    public BlockChainInfoRequest() : base(method_name) 
    {

    }
}