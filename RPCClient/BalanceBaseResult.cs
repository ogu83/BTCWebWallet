namespace BTCWebWallet.RPCClient;

public abstract class BalanceBaseResult 
{
    /// <summary>
    /// trusted balance (outputs created by the wallet or confirmed outputs)
    /// </summary>
    public decimal Trusted { get; set; }

    /// <summary>
    /// untrusted pending balance (outputs created by others that are in the mempool)
    /// </summary>
    public decimal Untrusted_pending { get; set; }

    /// <summary>
    /// balance from immature coinbase outputs
    /// </summary>
    public decimal Immarute { get; set; }
}