namespace Bolsover.DataBrowser;

public class Coordinator
{
    public static string FormatFileSize(long size)
    {
        int[] limits = {1024 * 1024 * 1024, 1024 * 1024, 1024};
        string[] units = {"GB", "MB", "KB"};

        for (var i = 0; i < limits.Length; i++)
            if (size >= limits[i])
                return string.Format("{0:#,##0.##} " + units[i], (double) size / limits[i]);

        return string.Format("{0} bytes", size);
    }
}