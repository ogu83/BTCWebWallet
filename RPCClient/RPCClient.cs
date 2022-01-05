using System.Net;
using Newtonsoft.Json;

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
            var result = JsonConvert.DeserializeObject<T>(strResult);
            return result;
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
}