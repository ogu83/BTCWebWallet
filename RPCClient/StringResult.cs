namespace BTCWebWallet.RPCClient;

public class StringResult
{
    public string Value { get; set; }
    public StringResult(string value)
    {
        this.Value = value;
    }

    public static implicit operator StringResult(string val)
    {
        return new StringResult(val);
    }

    public override string ToString()
    {
        return Value;
    }

    public override bool Equals(object? obj)
    {
        return this.ToString() == obj?.ToString();
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}
