using static AppSettings;

namespace PSX4AnyCommon
{

    public class CommonSettings
    {

        public static SettingsBase Enable_Sound_SETTINGS = new SettingsBase("EnableSound", "Sound");

        public static bool EnableSound { get; set; }

    }

}
