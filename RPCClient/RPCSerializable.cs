using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BTCWebWallet.RPCClient;

public abstract class RPCSerializable
{
    public override string ToString()
    {
        var retval = JsonConvert.SerializeObject(this, new JsonSerializerSettings
        {
            Formatting = Formatting.None,
            ContractResolver = new DefaultContractResolver 
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            }
        });
        return retval;
    }
}
