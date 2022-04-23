using Newtonsoft.Json;

namespace BTCWebWallet.RPCClient;

public class ListTransactionResult
{
    /// <summary>
    /// Only returns true if imported addresses were involved in transaction.
    /// </summary>
    public bool InvolvesWatchonly { get; set; }

    /// <summary>
    /// The bitcoin address of the transaction.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// The transaction category.
    /// "send" Transactions sent.
    /// "receive" Non-coinbase transactions received.
    /// "generate" Coinbase transactions received with more than 100 confirmations.
    /// "immature" Coinbase transactions received with 100 or fewer confirmations.
    /// "orphan" Orphaned coinbase transactions received.
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// The amount in BTC. This is negative for the 'send' category, and is positive for all other categories
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// A comment for the address/transaction, if any
    /// </summary>
    public string? Label { get; set; }

    /// <summary>
    /// The vout value
    /// </summary>
    public decimal Vout { get; set; }

    /// <summary>
    /// The amount of the fee in BTC. This is negative and only available for the 'send' category of transactions.
    /// </summary>
    public decimal Fee { get; set; }

    /// <summary>
    /// The number of confirmations for the transaction. Negative confirmations means the transaction conflicted that many blocks ago.
    /// </summary>
    public int Confirmations { get; set; }

    /// <summary>
    /// Only present if transaction only input is a coinbase one.
    /// </summary>
    public bool Generated { get; set; }

    /// <summary>
    /// Only present if we consider transaction to be trusted and so safe to spend from.
    /// </summary>
    public bool Trusted { get; set; }

    /// <summary>
    /// The block hash containing the transaction.
    /// </summary>
    public string? Blockhash { get; set; } 

    /// <summary>
    /// The block height containing the transaction.
    /// </summary>
    public int Blockheight { get; set; }
    
    /// <summary>
    /// The index of the transaction in the block that includes it.
    /// </summary>
    public int Blockindex { get; set; }

    /// <summary>
    /// The block time expressed in UNIX epoch time.
    /// </summary>
    public long Blocktime { get; set; }

    /// <summary>
    /// The transaction id.
    /// </summary>
    public string? Txid { get; set; }

    /// <summary>
    /// Conflicting transaction ids
    /// </summary>
    public List<string>? Walletconflicts { get; set; }

    /// <summary>
    /// The transaction time expressed in UNIX epoch time.
    /// </summary>
    public long Time { get; set; }

    /// <summary>
    /// The time received expressed in UNIX epoch time.
    /// </summary>
    public long Timereceived { get; set; }

    /// <summary>
    /// If a comment is associated with the transaction, only present if not empty.
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>   
    /// ("yes|no|unknown") Whether this transaction could be replaced due to BIP125 (replace-by-fee); may be unknown for unconfirmed transactions not in the mempool
    /// </summary>
    [JsonProperty("bip125-replaceable")]
    public string? Bip125Replaceable { get; set; }

    /// <summary>  
    /// 'true' if the transaction has been abandoned (inputs are respendable). Only available for the 'send' category of transactions.
    /// </summary>
    public bool Abandoned { get; set; }
}