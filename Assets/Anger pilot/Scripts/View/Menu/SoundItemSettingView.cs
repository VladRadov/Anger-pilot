public class SoundItemSettingView : ItemSettingView
{
    protected override void InitializeStateItem()
    {
        var isSoundOn = ContainerSaveerPlayerPrefs.Instance.SaveerData.IsSoundOn;

        if (isSoundOn == 1)
            SettingOn();
        else
            SettingOff();
    }

    protected override void ChangeStateSetingItem()
    {
        var isSoundOn = ContainerSaveerPlayerPrefs.Instance.SaveerData.IsSoundOn;

        if (isSoundOn == 1)
        {
            ContainerSaveerPlayerPrefs.Instance.SaveerData.IsSoundOn = 0;
            SettingOff();
        }
        else
        {
            ContainerSaveerPlayerPrefs.Instance.SaveerData.IsSoundOn = 1;
            SettingOn();
        }

        AudioManager.Instance.ChangeStateSound();
        AudioManager.Instance.PlayClickButton();
    }
}
