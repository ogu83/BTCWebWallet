using System.Net;
using Newtonsoft.Json;
using System;

namespace BTCWebWallet.RPCClient;

public class RPCClient : IRPCClient
{
    public const string Jsonrpc = "1.0";

    private string _rpcbind;
    private string _rpcport;
    private string _rpcuser;
    private string _rpcpassword;

    public RPCClient(string rpcbind, string rpcport, string rpcuser, string rpcpassword)
    {
        _rpcbind = rpcbind;   
        _rpcport = rpcport;
        _rpcuser = rpcuser;
        _rpcpassword = rpcpassword;
    }

    private string uri() 
    {
        return $"http://{_rpcbind}:{_rpcport}/";
    }

    private NetworkCredential credential() 
    {
        return new NetworkCredential(_rpcuser, _rpcpassword);
    }

    private HttpClient client()
    {
        var retval = new HttpClient(new HttpClientHandler() { Credentials = credential() });
        return retval;
    }

    private async Task<T> GetResponse<T>(RPCRequest request) where T : RPCSerializable
    {
        using (var c = client()) 
        {
            var requestStr = request.ToString();
            var requestStrContent = new StringContent(requestStr);
            var response = await c.PostAsync(uri(), requestStrContent);
            var strResult = await response.Content.ReadAsStringAsync();
            try
            {
                var result = JsonConvert.DeserializeObject<T>(strResult);
                if (result == null)
                {
                    throw new ArgumentNullException("RPCClient Response Can't be null");
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new RPCSerializeException(strResult, typeof(T).ToString(), ex);                                   
            }                                    
        }        
    }

    private async Task<T> GetWalletResponse<T>(RPCWalletRequest request) where T : RPCSerializable
    {
        using (var c = client()) 
        {
            var requestStr = request.ToString();
            var requestStrContent = new StringContent(requestStr);
            var wallet_uri = $"{uri()}wallet/{request.WalletName}";
            var response = await c.PostAsync(wallet_uri, requestStrContent);
            var strResult = await response.Content.ReadAsStringAsync();
            try
            {
                var result = JsonConvert.DeserializeObject<T>(strResult);
                if (result == null)
                {
                    throw new ArgumentNullException("RPCClient Response Can't be null");
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new RPCSerializeException(strResult, typeof(T).ToString(), ex);                                   
            }                                    
        }        
    }

    public async Task<NetworkInfoResponse> GetNetworkInfo(NetworkInfoRequest request)
    {   
        var response = await GetResponse<NetworkInfoResponse>(request);
        var castedResponse = response as NetworkInfoResponse;
        return castedResponse;
    }

    public async Task<ListWalletsResponse> GetListWallets(ListWalletsRequest request)
    {   
        var response = await GetResponse<ListWalletsResponse>(request);
        var castedResponse = response as ListWalletsResponse;
        return castedResponse;
    }

    public async Task<CreateWalletResponse> GetCreateWallet(CreateWalletRequest request)
    {
        var response = await GetResponse<CreateWalletResponse>(request);
        var castedResponse = response as CreateWalletResponse;
        return castedResponse;
    }

    public async Task<BlockChainInfoResponse> GetBlockChainInfo(BlockChainInfoRequest request)
    {
        var response = await GetResponse<BlockChainInfoResponse>(request);
        var castedResponse = response as BlockChainInfoResponse;
        return castedResponse;
    }

    public async Task<VerifyChainResponse> GetVerifyChainInfo(VerifyChainRequest request)
    {
        var response = await GetResponse<VerifyChainResponse>(request);
        var castedResponse = response as VerifyChainResponse;
        return castedResponse;
    }

    public async Task<WalletInfoResponse> GetWalletInfo(WalletInfoRequest request)
    {
        var response = await GetWalletResponse<WalletInfoResponse>(request);
        var castedResponse = response as WalletInfoResponse;
        return castedResponse;
    }

    public async Task<RPCResponse> GetWalletPassphrase(PassphraseRequest request)
    {
        var response = await GetWalletResponse<RPCResponse>(request);
        var castedResponse = response as RPCResponse;
        return castedResponse;
    }

    public async Task<BalancesResponse> GetBalances(BalancesRequest request)
    {
        var response = await GetWalletResponse<BalancesResponse>(request);
        var castedResponse = response as BalancesResponse;
        return castedResponse;
    }

    public async Task<AddressesByLabelResponse> GetAddressesByLabel(AddressesByLabelRequest request)
    {
        var response = await GetWalletResponse<AddressesByLabelResponse>(request);
        var castedResponse = response as AddressesByLabelResponse;
        return castedResponse;
    }

    public async Task<GetNewAddressResponse> GetNewAddress(GetNewAddressRequest request)
    {
        var response = await GetWalletResponse<GetNewAddressResponse>(request);
        var castedResponse = response as GetNewAddressResponse;
        return castedResponse;
    }

    public async Task<ListLabelsResponse> GetListLabels(ListLabelsRequest request)
    {
        var response = await GetWalletResponse<ListLabelsResponse>(request);
        var castedResponse = response as ListLabelsResponse;
        return castedResponse;
    }

    public async Task<ListTransactionsResponse> GetListTransactions(ListTransactionsRequest request)
    {
        var response = await GetWalletResponse<ListTransactionsResponse>(request);
        var castedResponse = response as ListTransactionsResponse;
        return castedResponse;
    }

    public async Task<SendToAddressResponse> SendToAddress(SendToAddressRequest request)
    {
        var response = await GetWalletResponse<SendToAddressResponse>(request);
        var castedResponse = response as SendToAddressResponse;
        return castedResponse;
    }

    public async Task<DumpPrivKeyResponse> DumpPrivKey(DumpPrivKeyRequest request)
    {
        var response = await GetWalletResponse<DumpPrivKeyResponse>(request);
        var castedResponse = response as DumpPrivKeyResponse;
        return castedResponse;
    }

    public async Task<ImportPrivKeyResponse> ImportPrivKey(ImportPrivKeyRequest request)
    {
                var response = await GetWalletResponse<ImportPrivKeyResponse>(request);
        var castedResponse = response as ImportPrivKeyResponse;
        return castedResponse;
    }

    public async Task<DumpWalletResponse> DumpWallet(DumpWalletRequest request)
    {
        var response = await GetWalletResponse<DumpWalletResponse>(request);
        var castedResponse = response as DumpWalletResponse;
        return castedResponse;
    }
}