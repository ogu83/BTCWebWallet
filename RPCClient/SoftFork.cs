namespace BTCWebWallet.RPCClient;

public class SoftFork
{
    public enum TypeEnum { Buried, Bib9 }

    /// <summary>
    /// one of "buried", "bip9"
    /// </summary>
    public string? Type { get; set; }

    public TypeEnum TypeAsEnum 
    { 
        get 
        {
            switch (Type)
            {                
                default:
                case "buried":
                    return TypeEnum.Buried;
                case "bip9":
                    return TypeEnum.Bib9;
            }
        }
    }

    /// <summary>
    /// status of bip9 softforks (only for "bip9" type)
    /// </summary>
    public Bip9? Bip9 { get; set; }

    public bool Bip9Visible { get { return TypeAsEnum == TypeEnum.Bib9; } }

    /// <summary>
    /// height of the first block which the rules are or will be enforced (only for "buried" type, or "bip9" type with "active" status)
    /// </summary>
    public int Height { get; set; }

    public bool HeightVisible { get { return TypeAsEnum == TypeEnum.Buried || (TypeAsEnum == TypeEnum.Bib9 && Active); }}

    /// <summary>
    /// true if the rules are enforced for the mempool and the next block    
    /// </summary>
    public bool Active { get; set; }
}
