using System.Runtime.InteropServices;
using System.Text;

public class IniFile
{

    [DllImport("kernel32.dll")]
    private static extern int GetPrivateProfileString(
        string lpApplicationName,
        string lpKeyName,
        string lpDefault,
        StringBuilder lpReturnedstring,
        int nSize,
        string lpFileName);

    [DllImport("kernel32.dll")]
    private static extern int WritePrivateProfileString(
        string lpApplicationName,
        string lpKeyName,
        string lpstring,
        string lpFileName);

    private string filePath;

    public IniFile(string filePath)
    {
        this.filePath = filePath;
    }

    public string this[string section, string key]
    {
        set
        {
            WritePrivateProfileString(section, key, value, filePath);
        }
        get
        {
            StringBuilder sb = new StringBuilder(256);
            GetPrivateProfileString(section, key, string.Empty, sb, sb.Capacity, filePath);
            return sb.ToString();
        }
    }

    public string GetValue(string section, string key, string defaultvalue)
    {
        StringBuilder sb = new StringBuilder(256);
        GetPrivateProfileString(section, key, defaultvalue, sb, sb.Capacity, filePath);
        return sb.ToString();
    }

    public int GetValue(string section, string key, int defaultvalue)
    {
        StringBuilder sb = new StringBuilder(256);
        GetPrivateProfileString(section, key, defaultvalue.ToString(), sb, sb.Capacity, filePath);
        return int.Parse(sb.ToString());
    }

}