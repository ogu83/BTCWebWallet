namespace BTCWebWallet.RPCClient;

public class NetworkInfoResult
{
    public int Version { get; set; }
    public string? Subversion { get; set; }
    public string? Protocolversion { get; set; }
    public string[]? Localservicesnames { get; set; }
    public bool Localrelay { get; set; }
    public int Timeoffset { get; set; }
    public bool Networkactive { get; set; }
    public bool Connections { get; set; }
    public bool Connections_in { get; set; }
    public bool Connections_out { get; set; }
    public Network[]? Networks { get; set; }
    public double Relayfee { get; set; }
    public double Incrementalfee { get; set; }
    public LocalAddress[]? Localaddresses { get; set; }
    public string? Warnings { get; set; }
}