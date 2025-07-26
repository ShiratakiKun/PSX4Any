using System.Diagnostics;

public class AppSettings
{

    private static IniFile ini;

    public static void SetIni(IniFile ini)
    {
        if (ini == null)
            return;
        AppSettings.ini = ini;
    }

    public static void SetValue(SettingsBase sBase, object value)
    {
        if (ini == null || AppSettings.IsNull(sBase))
            return;
        AppSettings.ini[sBase.category, sBase.name] = value.ToString();
        Debug.WriteLine(sBase.name + " set: " + value);
    }

    public static string GetValue(SettingsBase sBase)
    {
        if (AppSettings.IsNull(sBase))
            return null;
        return AppSettings.ini[sBase.category, sBase.name];
    }

    public static bool GetValueBool(SettingsBase sBase)
    {
        string value = AppSettings.GetValue(sBase);
        return bool.Parse(value);
    }

    public static double GetValueDouble(SettingsBase sBase)
    {
        string value = AppSettings.GetValue(sBase);
        return double.Parse(value);
    }

    public static float GetValueFloat(SettingsBase sBase)
    {
        string value = AppSettings.GetValue(sBase);
        return float.Parse(value);
    }

    public static int GetValueInt(SettingsBase sBase)
    {
        string value = AppSettings.GetValue(sBase);
        return int.Parse(value);
    }

    public static long GetValueLong(SettingsBase sBase)
    {
        string value = AppSettings.GetValue(sBase);
        return long.Parse(value);
    }

    private static bool IsNull(SettingsBase sBase)
    {
        return string.IsNullOrWhiteSpace(sBase.name) || string.IsNullOrWhiteSpace(sBase.category);
    }

    public class SettingsBase
    {

        public string name { get; set; }
        public string category { get; set; }

        public SettingsBase(string name, string category)
        {
            this.name = name;
            this.category = category;
        }

    }

}