namespace BTCWebWallet.RPCClient;

public interface IRPCClient
{
    Task<NetworkInfoResponse> GetNetworkInfo(NetworkInfoRequest request);
    Task<ListWalletsResponse> GetListWallets(ListWalletsRequest request);
    Task<CreateWalletResponse> GetCreateWallet(CreateWalletRequest request);
}
