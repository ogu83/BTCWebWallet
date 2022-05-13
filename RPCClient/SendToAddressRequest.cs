namespace BTCWebWallet.RPCClient;

public class SendToAddressRequest : RPCWalletRequest
{
    public const string ESTIMATE_MODE_UNSET = "unset";
    public const string ESTIMATE_MODE_ECONOMICAL = "economical";
    public const string ESTIMATE_MODE_CONSERVATIVE = "conservative";

    private const string method_name = "sendtoaddress";

    /// <summary>
    /// sendtoaddress "address" amount ( "comment" "comment_to" subtractfeefromamount replaceable conf_target "estimate_mode" avoid_reuse fee_rate verbose )
    /// Send an amount to a given address.
    /// Requires wallet passphrase to be set with walletpassphrase call if wallet is encrypted.
    /// <param name="address">The bitcoin address to send to.</param>
    /// <param name="amount">The amount in BTC to send. eg 0.1</param>
    /// <param name="comment">A comment used to store what the transaction is for. This is not part of the transaction, just kept in your wallet.</param>
    /// <param name="comment_to">A comment to store the name of the person or organization to which you’re sending the transaction. This is not part of the transaction, just kept in your wallet.</param>
    /// <param name="subtractfeefromamount">The fee will be deducted from the amount being sent. The recipient will receive less bitcoins than you enter in the amount field.</param>
    /// <param name="replaceable">Allow this transaction to be replaced by a transaction with higher fees via BIP 125</param>
    /// <param name="conf_target">Confirmation target in blocks</param>
    /// <param name="estimate_mode">The fee estimate mode, must be one of (case insensitive): “unset” “economical” “conservative”</param>
    /// <param name="avoid_reuse">(only available if avoid_reuse wallet flag is set) Avoid spending from dirty addresses; addresses are considered dirty if they have previously been used in a transaction.</param>
    /// </summary>
    public SendToAddressRequest(string id, string walletName, 
        string address, decimal amount, 
        string comment, string comment_to, 
        bool subtractfeefromamount, bool replaceable,
        int conf_target, string estimate_mode, bool avoid_reuse)
        : base(method_name, walletName)
    {
        Address = address;
        Amount = amount;
        Comment = comment;
        Comment_to = comment_to;
        Subtractfeefromamount = subtractfeefromamount;
        Replaceable = replaceable;
        Conf_target = conf_target;
        Estimate_mode = estimate_mode;
        Avoid_reuse = avoid_reuse;
    }

    public string Address { get; set; }
    public decimal Amount { get; set; }
    public string Comment { get; set; }
    public string Comment_to { get; set; }
    public bool Subtractfeefromamount { get; set; }
    public bool Replaceable { get; set; }
    public int Conf_target { get; set; }
    public string Estimate_mode { get; set; }
    public bool Avoid_reuse { get; private set; }

    public override List<object> Params
    {
        get
        {
            var retval = new object[]
            {
                Address, Amount, Comment, Comment_to, Subtractfeefromamount, Replaceable, Conf_target, Estimate_mode, Avoid_reuse
            };

            return retval.ToList();
        }
    }
}
