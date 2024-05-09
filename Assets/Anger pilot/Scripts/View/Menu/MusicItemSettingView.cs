public class MusicItemSettingView : ItemSettingView
{
    protected override void InitializeStateItem()
    {
        var isSoundOn = ContainerSaveerPlayerPrefs.Instance.SaveerData.IsMusicOn;

        if (isSoundOn == 1)
            SettingOn();
        else
            SettingOff();
    }

    protected override void ChangeStateSetingItem()
    {
        var isSoundOn = ContainerSaveerPlayerPrefs.Instance.SaveerData.IsMusicOn;

        if (isSoundOn == 1)
        {
            ContainerSaveerPlayerPrefs.Instance.SaveerData.IsMusicOn = 0;
            SettingOff();
        }
        else
        {
            ContainerSaveerPlayerPrefs.Instance.SaveerData.IsMusicOn = 1;
            SettingOn();
        }

        AudioManager.Instance.ChangeStateMusic();
        AudioManager.Instance.PlayClickButton();
    }
}
