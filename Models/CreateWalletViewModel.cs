namespace BTCWebWallet.Models;

public class CreateWalletViewModel : BaseViewModel
{
    public string? Walletname { get; set; }
    public string? Passphrase { get; set; }
    public string? Passphrase1 { get; set; }
    public bool DisablePrivateKeys { get; set; }
    public bool Blank { get; set; }
    public bool AvoidReuse { get; set; }
    public bool Descriptors { get; set; }
    public bool LoadOnStartup { get; set; }

    public override List<ErrorViewModel> Validate()
    {
        if (!string.Equals(Passphrase, Passphrase1))
        {
            return new ErrorViewModel[]
            {
                new ErrorViewModel
                {
                    Code = Guid.NewGuid().GetHashCode(),
                    Message = "PassphraseNotConfirmed",
                    Type = ErrorViewModel.ErrorType.PageError
                }
            }.ToList();
        }
        else
        {
            return base.Validate();
        }
    }
}