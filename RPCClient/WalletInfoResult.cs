using BTCWebWallet.Helpers;
using Newtonsoft.Json;

namespace BTCWebWallet.RPCClient;

public class WalletInfoResult
{
    /// <sumamry>
    /// the wallet name
    /// </summary>
    public string? Walletname { get; set; }

    /// <summary>
    /// the wallet version
    /// </summary>
    public decimal Walletversion { get; set; }

    /// <summary>
    /// the database format (bdb or sqlite)
    /// </summary>
    public string? Format { get; set; }

    /// <summary>
    /// the total number of transactions in the wallet
    /// </summary>
    public int Txcount { get; set; }

    /// <summary>
    /// the UNIX epoch time of the oldest pre-generated key in the key pool. Legacy wallets only.
    /// </summary>
    public int Keypoololdest { get; set; }

    /// <summary>
    /// how many new keys are pre-generated (only counts external keys)
    /// </summary>
    public int Keypoolsize { get; set; }

    /// <summary>
    /// how many new keys are pre-generated for internal use (used for change outputs, only appears if the wallet is using this feature, otherwise external keys are used)
    /// </summary>
    public int Keypoolsize_hd_internal { get; set; }

    /// <summary>
    /// the UNIX epoch time until which the wallet is unlocked for transfers, or 0 if the wallet is locked (only present for passphrase-encrypted wallets)
    /// </summary>
    public int Unlocked_until { get; set; }

    /// <summary>
    /// the transaction fee configuration, set in BTC/kvB
    /// </summary>
    public decimal Paytxfee { get; set; }

    /// <summary>
    /// the Hash160 of the HD seed (only present when HD is enabled)
    /// </summary>
    public string? Hdseedid { get; set; }

    /// <summary>
    /// false if privatekeys are disabled for this wallet (enforced watch-only wallet)
    /// </summary>
    public bool Private_keys_enabled { get; set; }

    /// <summary>
    /// whether this wallet tracks clean/dirty coins in terms of reuse
    /// </summary>
    public bool Avoid_reuse { get; set; }

    /// <summary>
    /// current scanning details, or false if no scan is in progress
    /// </summary>
    [JsonConverter(typeof(BoolObjectConverter))]
    public Scanning? Scanning { get; set; }

    /// <summary>
    /// whether this wallet uses descriptors for scriptPubKey management
    /// </summary>
    public bool Descriptors { get; set; }
}
