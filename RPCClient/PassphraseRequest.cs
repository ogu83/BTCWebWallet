using Newtonsoft.Json;

namespace BTCWebWallet.RPCClient;

/// <summary>
// Stores the wallet decryption key in memory for ‘timeout’ seconds.
// This is needed prior to performing transactions related to private keys such as sending bitcoins Note:
// Issuing the walletpassphrase command while the wallet is already unlocked will set a new unlock time that overrides the old one.
/// </summary>
public class PassphraseRequest : RPCWalletRequest
{
    private const string method_name = "walletpassphrase";

    public PassphraseRequest(string id, string walletName, string passphrase, int timeout) 
        : base(method_name, id, walletName)
    {
        Passphrase = passphrase;
        Timeout = timeout;
    }

    /// <summary>
    /// The wallet passphrase
    /// </summary>
    [JsonIgnore]
    public string Passphrase { get; set; }

    /// <summary>
    /// The time to keep the decryption key in seconds; capped at 100000000 (~3 years).
    /// </summary>
    [JsonIgnore]
    public int Timeout { get; set; }

    public override List<object> Params 
    { 
        get 
        {
            var retval = new object[] 
            {
                Passphrase,
                Timeout
            };

            return retval.ToList();
        } 
    }
}
