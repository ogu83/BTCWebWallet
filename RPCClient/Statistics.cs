namespace BTCWebWallet.RPCClient;

public class Statistics 
{
    /// <summary>
    /// the length in blocks of the BIP9 signalling period
    /// </summary>
    public int Period { get; set; }

    /// <summary>
    /// the number of blocks with the version bit set required to activate the feature
    /// </summary>
    public int Threshold { get; set; }

    /// <summary>
    /// the number of blocks elapsed since the beginning of the current period
    /// </summary>
    public int Elapsed { get; set; }

    /// <summary>
    /// the number of blocks with the version bit set in the current period
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// returns false if there are not enough blocks left in this period to pass activation threshold
    /// </summary>
    public bool Possible { get; set; }
}