namespace BTCWebWallet.RPCClient;

public class ListWalletsResult : List<string>
{

}

public class CreateWalletResult 
{
    public string Name { get; set; }
    
    public string Warning { get; set; }
}
