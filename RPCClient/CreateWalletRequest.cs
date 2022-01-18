using Newtonsoft.Json;

namespace BTCWebWallet.RPCClient;

public class CreateWalletRequest: RPCRequest
{
    private const string method_name = "createwallet";

    public CreateWalletRequest(
        string id,
        string walletname, 
        string passphrase,
        bool disablePrivateKeys = false,
        bool blank = false,
        bool avoidReuse = false,
        bool descriptors = false,
        bool loadOnStartup = true)
        : base(method_name, id)
    {
        WalletName = walletname;
        Passphrase = passphrase;
        DisablePrivateKeys = disablePrivateKeys;
        Blank = blank;
        Blank = blank;
        AvoidReuse = avoidReuse;
        Descriptors = descriptors;
        LoadOnStartup = loadOnStartup;
    }

    public CreateWalletRequest(
        string walletname,
        string passphrase,
        bool disablePrivateKeys = false,
        bool blank = false,
        bool avoidReuse = false,
        bool descriptors = false,
        bool loadOnStartup = true)
        : base(method_name) 
    {
        WalletName = walletname;
        Passphrase = passphrase;
        DisablePrivateKeys = disablePrivateKeys;
        Blank = blank;
        AvoidReuse = avoidReuse;
        Descriptors = descriptors;
        LoadOnStartup = loadOnStartup;
    }

    /// <summary>
    /// The name for the new wallet. If this is a path, the wallet will be created at the path location.
    /// </summary>
    [JsonIgnore]
    public string WalletName { get; set; }

    /// <summary>
    /// Encrypt the wallet with this passphrase.
    /// </summary>
    [JsonIgnore]
    public string Passphrase { get; set; }

    /// <summary>
    /// Disable the possibility of private keys (only watchonlys are possible in this mode).
    /// </summary>
    [JsonIgnore]
    public bool DisablePrivateKeys { get; set; }

    /// <summary>
    /// Create a blank wallet. A blank wallet has no keys or HD seed. One can be set using sethdseed.
    /// </summary>
    [JsonIgnore]
    public bool Blank { get; set; }

    /// <summary>
    /// Keep track of coin reuse, and treat dirty and clean coins differently with privacy considerations in mind.
    /// </summary>
    [JsonIgnore]
    public bool AvoidReuse { get;  set; }

    /// <summary>
    /// Create a native descriptor wallet. The wallet will use descriptors internally to handle address creation
    /// </summary>
    [JsonIgnore]
    public bool Descriptors { get; set; }

    /// <summary>
    /// Save wallet name to persistent settings and load on startup. True to add wallet to startup list, false to remove, null to leave unchanged.
    /// </summary>
    [JsonIgnore]
    public bool LoadOnStartup { get; set; }

    public override List<object> Params 
    { 
        get 
        {
            var retval = new object[] 
            {
                WalletName,
                DisablePrivateKeys,
                Blank,
                Passphrase,
                AvoidReuse,
                Descriptors,
                LoadOnStartup,                
            };

            return retval.ToList();
        } 
    }
}