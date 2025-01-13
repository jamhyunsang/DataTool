public class User
{
    #region Instance
    private static Lazy<User> instance = new Lazy<User>(() =>
    {
        var instance = new User();
        instance.Init();
        return instance;
    });

    public static User Instance
    {
        get { return instance.Value; }
    }
    #endregion

    #region Member Property
    private Settings Settings = null;
    #endregion

    #region Member Method
    private void Init()
    {
        if (File.Exists(Def.SettingsPath))
        {
            var JsonData = File.ReadAllText(Def.SettingsPath);
            Settings = Util.ToObject<Settings>(JsonData);
        }
        else
        {
            Settings = new Settings();
        }
    }

    private void Save()
    {
        File.WriteAllText(Def.SettingsPath, Util.ToJson(Settings));
    }

    public void SaveSettingInfo(SettingInfo SettingInfo)
    {
        if(Settings.MySettings.ContainsKey(SettingInfo.Name))
        {
            Settings.MySettings[SettingInfo.Name] = SettingInfo;
        }
        else
        {
            Settings.MySettings.Add(SettingInfo.Name, SettingInfo);
        }

        Settings.CurrentSetting = SettingInfo.Name;

        Save();
    }

    public void ChangeCurrentSetting(string SettingName)
    {
        Settings.CurrentSetting = SettingName;

        Save();
    }

    public bool IsExist(string SettingName)
    {
        return Settings.MySettings.ContainsKey(SettingName);
    }

    public bool IsSettingEmpty()
    {
        return Settings.MySettings.Count == 0;
    }

    public SettingInfo GetCurrentSetting()
    {
        return Settings.MySettings[Settings.CurrentSetting];
    }

    public SettingInfo GetSetting(string SettingName)
    {
        return Settings.MySettings[SettingName];
    }

    public string[] GetMySettingsKeys()
    {
        return Settings.MySettings.Keys.ToArray();
    }
    #endregion
}
