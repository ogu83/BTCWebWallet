namespace BTCWebWallet.Models;

public class WalletsViewModel : BaseViewModel 
{    
    public RPCClient.ListWalletsResult? ListWallets { get; set; }
}