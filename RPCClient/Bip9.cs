namespace BTCWebWallet.RPCClient;

public class Bip9 
{
    public enum StatusEnum { Defined, Started, LockedIn, Active, Failed }

    /// <summary>
    /// one of "defined", "started", "locked_in", "active", "failed"
    /// </summary>
    public string? Status { get; set; }

    public StatusEnum StatusAsEnum
    {
        get 
        {
            switch (Status)
            {
                default:
                case "defined":                 
                    return StatusEnum.Defined;
                case "started":
                    return StatusEnum.Started;
                case "locked_in":
                    return StatusEnum.LockedIn;
                case "active":
                    return StatusEnum.Active;
                case "failed":
                    return StatusEnum.Failed;
            }
        }
    }

    /// <summary>
    /// the bit (0-28) in the block version field used to signal this softfork (only for "started" status)
    /// </summary>
    public byte Bit { get; set; }

    public bool BitVisible { get {  return StatusAsEnum == StatusEnum.Started; } }

    /// <summary>
    /// the minimum median time past of a block at which the bit gains its meaning
    /// </summary>
    public long Start_time { get; set; }

    /// <summary>
    /// the median time past of a block at which the deployment is considered failed if not yet locked in
    /// </summary>
    public long Timeout { get; set; }

    /// <summary>
    /// height of the first block to which the status applies
    /// </summary>
    public long Since { get; set; }    

    /// <summary>
    /// numeric statistics about BIP9 signalling for a softfork (only for "started" status)
    /// </summary>
    public Statistics? Statistics { get; set; }

    public bool StatisticsVisible { get {  return StatusAsEnum == StatusEnum.Started; } }
}
