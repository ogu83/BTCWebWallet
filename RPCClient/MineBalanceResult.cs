namespace BTCWebWallet.RPCClient;

public class MineBalanceResult : BalanceBaseResult
{
    /// <summary>
    /// (only present if avoid_reuse is set) balance from coins sent to addresses that were previously spent from (potentially privacy violating)
    /// </summary>
    public decimal Used { get; set; }
}
