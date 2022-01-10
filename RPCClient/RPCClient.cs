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
}