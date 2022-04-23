namespace BTCWebWallet.Models;

public class WalletViewModel : BaseViewModel
{
    public decimal Balance { get; set; }

    public RPCClient.WalletInfoResult? WalletInfo { get; set; }

    public RPCClient.BalancesResult? Balances { get; set; }

    public List<AddressViewModel>? Addresses { get; set;}

    public List<TransactionViewModel>? Transactions { get; set; }
}