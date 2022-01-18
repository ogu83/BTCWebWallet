namespace BTCWebWallet.RPCClient;

public class Scanning
{
    /// <summary>
    /// elapsed seconds since scan start
    /// </summary>
    public int Duration { get; set; }

    public TimeSpan DurationAsTimeSpan { get { return TimeSpan.FromSeconds(Duration); } }

    /// <summary>
    /// scanning progress percentage [0.0, 1.0]
    /// </summary>
    public decimal Progress { get; set; }

    public int Progress100 { get { return (int)(Progress * 100); }}
}
