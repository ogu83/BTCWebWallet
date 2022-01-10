using System.Diagnostics;

public class BitcoinNode : IBitcoinNode, IDisposable
{
    private string _exePath;
    private string _configPath;
    private string _wltPath;
    private string _dataPath;
    private string _rpcbind;
    private string _rpcport;
    private string _rpcuser;
    private string _rpcpassword;
    private string _rpcallowip;

    private Process _process;

    public BitcoinNode(
        string exePath,
        string configPath,
        string wltPath,
        string dataPath, 
        string rpcbind, 
        string rpcport, 
        string rpcuser, 
        string rpcpassword,
        string rpcallowip)
    {
        _exePath = exePath;
        _configPath = configPath;
        _wltPath = wltPath;
        _dataPath = dataPath;

        _rpcbind = rpcbind;
        _rpcport = rpcport;
        _rpcuser = rpcuser;
        _rpcpassword = rpcpassword;
        _rpcallowip = rpcallowip;

        var args = $" -conf={_configPath} -walletdir={_wltPath} -datadir={_dataPath} -rpcbind={_rpcbind}:{_rpcport} -rpcuser={_rpcuser} -rpcpassword={_rpcpassword} -rpcallowip={_rpcallowip}";

        _process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                FileName = _exePath,
                Arguments = args,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            }
        };

        _process.OutputDataReceived += (sender, data) => Console.WriteLine($"bitcoind: {data.Data}");
        _process.ErrorDataReceived += (sender, data) => Console.WriteLine($"bitcoind: {data.Data}");

        if (!_process.Start())
        {
            throw new Exception("Bitcoind Not Started");
        }

        _process.BeginErrorReadLine();
        _process.BeginOutputReadLine();
    }

    public void Dispose()
    {
        if (_process != null)
        {
            _process.Kill(true);
            _process.WaitForExit(10000);
            _process.Dispose();
        }
    }

    public void Terminate()
    {
        Dispose();
    }
}