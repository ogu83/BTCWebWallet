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

    private bool _disposed;

    private bool _isReady;

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

        var oldProcesses = Process.GetProcessesByName("bitcoind").Union(Process.GetProcessesByName("bitcoind.exe"));
        if (oldProcesses != null) 
        {
            foreach (var p in oldProcesses)
            {
                p.Kill(true);
                p.WaitForExit(10000);
            }
        }

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

        _process.OutputDataReceived += new DataReceivedEventHandler((sender, data) =>
        {
            Console.WriteLine($"bitcoind: {data.Data}");

            if (data == null)
            {
                return;                
            }

            if (data.Data == null)
            {
                return;
            }

            if (data.Data.ToLower().Contains("done loading"))
            {
                _isReady = true;
            }
        });

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
        if (!_disposed)
        {
            _disposed = true;
            _process.Kill(true);
            _process.WaitForExit(10000);
            _process.Dispose();
        }
    }

    public void Terminate()
    {
        Dispose();
    }

    public bool IsReady() 
    {
        return _isReady;    
    }
}