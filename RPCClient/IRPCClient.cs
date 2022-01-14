namespace BTCWebWallet.RPCClient;

public interface IRPCClient
{
    Task<NetworkInfoResponse> GetNetworkInfo(NetworkInfoRequest request);
    Task<BlockChainInfoResponse> GetBlockChainInfo(BlockChainInfoRequest request);
    Task<ListWalletsResponse> GetListWallets(ListWalletsRequest request);
    Task<CreateWalletResponse> GetCreateWallet(CreateWalletRequest request);   
    Task<VerifyChainResponse> GetVerifyChainInfo(VerifyChainRequest request); 
}