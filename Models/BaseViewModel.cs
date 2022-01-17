namespace BTCWebWallet.Models;

public abstract class BaseViewModel
{
    public bool IsSuccess { get; set; }

    public virtual List<ErrorViewModel> Validate()
    {
        return new List<ErrorViewModel>();
    }
}