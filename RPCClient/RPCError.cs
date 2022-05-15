namespace BTCWebWallet.RPCClient;

public class RPCError
{
    public int Code { get; set; }
    public string? Message { get; set; }

    public override string ToString()
    {
        return $"{Code}: {Message}"; 
    }
}