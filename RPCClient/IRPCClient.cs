namespace BTCWebWallet.RPCClient;

public interface IRPCClient
{
    Task<NetworkInfoResponse> GetNetworkInfo(NetworkInfoRequest request);
    Task<BlockChainInfoResponse> GetBlockChainInfo(BlockChainInfoRequest request);
    Task<ListWalletsResponse> GetListWallets(ListWalletsRequest request);
    Task<CreateWalletResponse> GetCreateWallet(CreateWalletRequest request);   
    Task<VerifyChainResponse> GetVerifyChainInfo(VerifyChainRequest request);
    Task<WalletInfoResponse> GetWalletInfo(WalletInfoRequest request);
    Task<RPCResponse> GetWalletPassphrase(PassphraseRequest request);
    Task<BalancesResponse> GetBalances(BalancesRequest request);
    Task<AddressesByLabelResponse> GetAddressesByLabel(AddressesByLabelRequest request);
    Task<GetNewAddressResponse> GetNewAddress(GetNewAddressRequest request);
    Task<ListLabelsResponse> GetListLabels(ListLabelsRequest request);
    Task<ListTransactionsResponse> GetListTransactions(ListTransactionsRequest request);
    Task<SendToAddressResponse> SendToAddress(SendToAddressRequest request);
    Task<DumpPrivKeyResponse> DumpPrivKey(DumpPrivKeyRequest request);
    Task<ImportPrivKeyResponse> ImportPrivKey(ImportPrivKeyRequest request);
    Task<DumpWalletResponse> DumpWallet(DumpWalletRequest request);
}