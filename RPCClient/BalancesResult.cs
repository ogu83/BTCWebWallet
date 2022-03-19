namespace BTCWebWallet.RPCClient;

public class BalancesResult
{
    /// <summary>
    /// (json object) balances from outputs that the wallet can sign
    /// </summary>
    public MineBalanceResult? Mine { get; set; }

    /// <summary>
    /// watchonly balances (not present if wallet does not watch anything)
    /// </summary>
    public WatchOnlyBalanceResult? Watchonly { get; set; }
}
