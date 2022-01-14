namespace BTCWebWallet.RPCClient;

public class BlockChainInfoResult
{
    public enum ChainEnum { main, test, regtest }    

    /// <summary>
    /// current network name (main, test, regtest)
    /// </summary>
    public string? Chain { get; set; }

    public ChainEnum ChainAsEnum 
    {
        get 
        {
            switch (Chain)
            {                
                default:
                case "main":
                    return ChainEnum.main;
                case "test":
                    return ChainEnum.test;
                case "regtest":
                    return ChainEnum.regtest;
            }
        }
    }

    /// <summary>
    /// the height of the most-work fully-validated chain. The genesis block has height 0
    /// </summary>
    public int Blocks { get; set; }

    /// <summary>
    /// the current number of headers we have validated
    /// </summary>
    public int Headers { get; set; }

    /// <summary>
    /// the hash of the currently best block
    /// </summary>
    public string? Bestblockhash   { get; set; }

    /// <summary>
    /// the current difficulty
    /// </summary>
    public decimal Difficulty { get; set; }

    /// <summary>
    /// median time for the current best block
    /// </summary>
    public int Mediantime { get; set; }

    /// <summary>
    /// estimate of verification progress
    /// </summary>
    public decimal Verificationprogress { get; set; }

    public int Verificationprogress100 
    { 
        get 
        {
            return (int)(Verificationprogress * 100);
        }
    }

    /// <summary>
    /// estimate of whether this node is in Initial Block Download mode
    /// </summary>
    public bool Initialblockdownload { get; set; }

    /// <summary>
    /// total amount of work in active chain, in hexadecimal
    /// </summary>
    public string? Chainwork { get; set; }

    /// <summary>
    /// the estimated size of the block and undo files on disk
    /// </summary>
    public decimal Size_on_disk { get; set; }

    /// <summary>
    /// if the blocks are subject to pruning
    /// </summary>
    public bool Pruned { get; set; }

    /// <summary>
    /// lowest-height complete block stored (only present if pruning is enabled)
    /// </summary>    
    public decimal Pruneheight { get; set; }

    /// <summary>
    /// whether automatic pruning is enabled (only present if pruning is enabled)
    /// </summary>
    public bool Automatic_pruning { get; set; }

    /// <summary>
    /// the target size used by pruning (only present if automatic pruning is enabled)
    /// </summary>
    public decimal Prune_target_size { get; set; }

    /// <summary>
    /// status of softforks
    /// </summary>
    public Dictionary<string, SoftFork>? Softforks { get; set; }    

    /// <summary>
    /// any network and blockchain warnings
    /// </summary>
    public string? Warnings { get; set; }
}
