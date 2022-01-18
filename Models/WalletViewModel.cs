namespace BTCWebWallet.Models;

public class WalletViewModel : BaseViewModel
{
    public decimal Balance { get; set; }

    public RPCClient.WalletInfoResult? WalletInfo { get; set; }
}