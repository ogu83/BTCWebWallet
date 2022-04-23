namespace BTCWebWallet.RPCClient;

public class ListTransactionsRequest : RPCWalletRequest
{
    private const string method_name = "listtransactions";

    public ListTransactionsRequest(string id, string walletName, string label = "*", int count = short.MaxValue, int skip = 0, bool include_watchonly = true)
        : base(method_name, walletName)
        {
            Label = label;
            Count = count;
            Skip = skip;
            Include_watchonly = include_watchonly;
        }

    public string Label { get; set; }
    public int Count { get; set; }
    public int Skip { get; set; }
    public bool Include_watchonly { get; set; }

        public override List<object> Params
    {
        get 
        {
            var retval = new object[] 
            {
                Label, 
                Count, 
                Skip, 
                Include_watchonly         
            };

            return retval.ToList();
        }
    }
}
