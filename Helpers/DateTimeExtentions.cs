namespace BTCWebWallet.Helpers;

public static class DateTimeExtentions 
{
    private static readonly DateTime _epoch = new DateTime(1970, 1, 1);

    public static int ConvertToUnixTime(DateTime value) 
    {
        TimeSpan t = DateTime.UtcNow - _epoch;
        int secondsSinceEpoch = (int)t.TotalSeconds;
        return secondsSinceEpoch;
    }

    public static int ToUnixTime(this DateTime value) 
    {
        return ConvertToUnixTime(value);
    }

    public static DateTime CreateFromUnixTime(int value) 
    {
        var sinceEpoch = _epoch.AddSeconds(value);
        return sinceEpoch;
    }

    public static DateTime ToDateTime(this int value) 
    {
        return CreateFromUnixTime(value);
    }

    public static DateTime ToDateTime(this long value) 
    {
        return CreateFromUnixTime((int)value);
    }
}
