using System;

class DateTimeHelper {

    public static DateTime UnixTimeStampToDateTime(long unixTimeStamp) {
        var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        try {
            return dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        } catch (Exception) {
            return DateTime.MinValue;
        }
    }

    public static long ToUnixTimestamp(DateTime dt) {
        dt = dt.ToUniversalTime();
        return (long)(dt.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
    }

    public static long CurrentUnixTimeStamp {
        get {
            return (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }

}