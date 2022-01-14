namespace BTCWebWallet.Models;

public class CreateWalletViewModel : BaseViewModel
{
    public string? Walletname { get; set; }
    public string? Passphrase { get; set; }
    public bool DisablePrivateKeys { get; set; }
    public bool Blank { get; set; }
    public bool AvoidReuse { get; set; }
    public bool Descriptors { get; set; }
    public bool LoadOnStartup { get; set; }
}