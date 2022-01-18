namespace BTCWebWallet.RPCClient;

public abstract class RPCResponse<T> : RPCSerializable where T : class
{
    public T? Result { get; set; }
    public RPCError? Error { get; set; }
    public string? Id { get; set; }
    public bool HasError { get { return Error != null; } }
}

public class RPCResponse : RPCSerializable
{
    public RPCError? Error { get; set; }
    public string? Id { get; set; }
    public bool HasError { get { return Error != null; } }
}