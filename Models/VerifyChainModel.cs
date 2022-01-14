namespace BTCWebWallet.Models;

public class VerifyChainModel
{
        /// <summary>
    /// level 0 reads the blocks from disk
    /// level 1 verifies block validity
    /// level 2 verifies undo data
    /// level 3 checks disconnection of tip blocks
    /// level 4 tries to reconnect the blocks
    /// each level includes the checks of the previous levels
    /// </summary>
    public int CheckLevel { get; set; } = 3;
    
    /// <summary>
    /// optional, default=6, 0=all
    /// </summary>
    public int NBlocks { get; set; } = 6;

    public bool Result { get; set; }
}