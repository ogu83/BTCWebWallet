namespace BTCWebWallet.Models;

public class TransactionViewModel
{
    public string? Comment { get; set; }
    public string? Id { get; set; }
    public DateTime Time { get; set; }
    public DateTime RecievedTime { get; set; }
    public decimal Amount { get; set; }
    public decimal Fee { get; set; }
    public string? Category { get; set; }
    public bool Abandoned { get; set; }
    public string? Address { get; set; }
    public int Confirmations { get; set; }
    public string? Label { get; set; }
    public bool Trusted { get; set; }
    public string? Bip125Replaceable { get; set; }
}