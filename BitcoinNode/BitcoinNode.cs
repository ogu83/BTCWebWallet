using System.Diagnostics;

public interface IBitcoinNode
{
    void Terminate();
}

public class BitcoinNode : IBitcoinNode, IDisposable
{
    private string _exePath;
    private string _configPath;
    private string _wltPath;
    private string _dataPath;

    private Process _process; 

    public BitcoinNode(string exePath, string configPath, string wltPath, string dataPath)
    {
        _exePath = exePath;
        _configPath = configPath;
        _wltPath = wltPath;
        _dataPath= dataPath;
        _process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                FileName = _exePath,
                Arguments = $" -conf={_configPath} -walletdir={_wltPath} -datadir={_dataPath}",
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