namespace BTCWebWallet.Helpers;

public static class StringExtentions
{
    public static string AsDiskSizeString(this double value) 
    {
        string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        int order = 0;
        while (value >= 1024 && order < sizes.Length - 1) 
        {
            order++;
            value = value/1024;
        }

        // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
        // show a single decimal place, and no space.
        string result = String.Format("{0:0.##} {1}", value, sizes[order]);
        return result;
    }

    public static string AsDiskSizeString(this int value) 
    {
        return ((double)value).AsDiskSizeString();
    }

    public static string AsDiskSizeString(this long value) 
    {
        return ((double)value).AsDiskSizeString();
    }

        public static string AsDiskSizeString(this decimal value) 
    {
        return ((double)value).AsDiskSizeString();
    }
}
