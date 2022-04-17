namespace BTCWebWallet.RPCClient;

public class BoolResult
{
    public bool Value { get; set; }
    public BoolResult(bool value)
    {
        this.Value = value;
    }

    public static implicit operator BoolResult(bool val)
    {
        return new BoolResult(val);
    }
    
    public static bool operator true(BoolResult val) => val.Value;
    
    public static bool operator false(BoolResult val) => !val.Value;
}