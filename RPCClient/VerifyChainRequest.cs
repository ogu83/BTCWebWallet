using Newtonsoft.Json;

namespace BTCWebWallet.RPCClient;

public class VerifyChainRequest : RPCRequest
{    
    private const string method_name = "verifychain";

    public VerifyChainRequest(string id, int checkLevel, int nblocks = 6) 
        : base(method_name, id)
    {
        CheckLevel = checkLevel;
        NBlocks = nblocks;
    }

    public VerifyChainRequest(int checkLevel, int nblocks = 6) 
        : base(method_name) 
    {
        CheckLevel = checkLevel;
        NBlocks = nblocks;
    }

    /// <summary>
    /// level 0 reads the blocks from disk
    /// level 1 verifies block validity
    /// level 2 verifies undo data
    /// level 3 checks disconnection of tip blocks
    /// level 4 tries to reconnect the blocks
    /// each level includes the checks of the previous levels
    /// </summary>
    [JsonIgnore]
    public int CheckLevel { get; set; }
    
    /// <summary>
    /// optional, default=6, 0=all
    /// </summary>
    [JsonIgnore]
    public int NBlocks { get; set; }

    public override List<object> Params 
    { 
        get 
        {
            var retval = new object[] 
            {
                CheckLevel,
                NBlocks
            };

            return retval.ToList();
        } 
    }
}