public class VibroItemSettingView : ItemSettingView
{
    protected override void InitializeStateItem()
    {
        var isSoundOn = ContainerSaveerPlayerPrefs.Instance.SaveerData.IsVibrationOn;

        if (isSoundOn == 1)
            SettingOn();
        else
            SettingOff();
    }

    protected override void ChangeStateSetingItem()
    {
        var isSoundOn = ContainerSaveerPlayerPrefs.Instance.SaveerData.IsVibrationOn;

        if (isSoundOn == 1)
        {
            ContainerSaveerPlayerPrefs.Instance.SaveerData.IsVibrationOn = 0;
            SettingOff();
        }
        else
        {
            ContainerSaveerPlayerPrefs.Instance.SaveerData.IsVibrationOn = 1;
            SettingOn();
        }

        AudioManager.Instance.PlayClickButton();
    }
}
