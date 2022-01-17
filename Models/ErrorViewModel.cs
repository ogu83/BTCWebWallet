namespace BTCWebWallet.Models;

public class ErrorViewModel
{
    public enum ErrorType { PageError, RCPError }

    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    public int Code { get; set; }

    public string? Message { get; set; }

    public ErrorType Type { get; set; }
}