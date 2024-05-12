using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveerDataInPlayerPrefs : SaveerData
{
    private readonly string KEY_COUNT_COIN = "Coins";
    private readonly string KEY_COUNT_ALL_CRYSTAL = "AllCrystal";
    private readonly string KEY_COUNT_GAME_CRYSTAL = "GameCrystal";
    private readonly string KEY_IS_MUSIC_ON = "IsMusicOn";
    private readonly string KEY_IS_SOUND_ON = "IsSoundOn";
    private readonly string KEY_IS_VIBRATION_ON = "IsVibrationOn";
    private readonly string KEY_CURRENT_SKINS = "CurrentSkins";
    private readonly string KEY_OPEN_SKINS = "OpenSkins";
    private readonly string KEY_CURRENT_GROUNDS = "CurrentGrounds";
    private readonly string KEY_OPEN_GROUNDS = "OpenGrounds";
    private readonly string KEY_EDUCATION = "IsEducation";

    public int Coins { get { return Load<int>(KEY_COUNT_COIN, 0); } set { Save<int>(KEY_COUNT_COIN, value); } }
    public int AllCrystal { get { return Load<int>(KEY_COUNT_ALL_CRYSTAL, 0); } set { Save<int>(KEY_COUNT_ALL_CRYSTAL, value); } }
    public int GameCrystal { get { return Load<int>(KEY_COUNT_GAME_CRYSTAL, 0); } set { Save<int>(KEY_COUNT_GAME_CRYSTAL, value); } }
    public int IsMusicOn { get { return Load<int>(KEY_IS_MUSIC_ON, 1); } set { Save<int>(KEY_IS_MUSIC_ON, value); } }
    public int IsSoundOn { get { return Load<int>(KEY_IS_SOUND_ON, 1); } set { Save<int>(KEY_IS_SOUND_ON, value); } }
    public int IsVibrationOn { get { return Load<int>(KEY_IS_VIBRATION_ON, 1); } set { Save<int>(KEY_IS_VIBRATION_ON, value); } }
    public string CurrentSkin { get { return Load<string>(KEY_CURRENT_SKINS, "1"); } set { Save<string>(KEY_CURRENT_SKINS, value); } }
    public string OpenSkins { get { return Load<string>(KEY_OPEN_SKINS, "1"); } set { Save<string>(KEY_OPEN_SKINS, value); } }
    public string CurrentGround { get { return Load<string>(KEY_CURRENT_GROUNDS, "1"); } set { Save<string>(KEY_CURRENT_GROUNDS, value); } }
    public string OpenGrounds { get { return Load<string>(KEY_OPEN_GROUNDS, "1"); } set { Save<string>(KEY_OPEN_GROUNDS, value); } }
    public int IsEducation { get { return Load<int>(KEY_EDUCATION, 0); } set { Save<int>(KEY_EDUCATION, value); } }

    public override T Load<T>(string nameParameter, T defaultValue)
    {
        if (PlayerPrefs.HasKey(nameParameter) == false)
            return defaultValue;

        Type inType = typeof(T);

        if (inType == typeof(int))
            return (T)(object)PlayerPrefs.GetInt(nameParameter);
        else if (inType == typeof(float))
            return (T)(object)PlayerPrefs.GetFloat(nameParameter);
        else
            return (T)(object)PlayerPrefs.GetString(nameParameter);
    }

    public override void Save<T>(string nameParameter, T value)
    {
        Type inType = typeof(T);

        if (inType == typeof(int))
            PlayerPrefs.SetInt(nameParameter, int.Parse(value.ToString()));
        else if (inType == typeof(float))
            PlayerPrefs.SetFloat(nameParameter, float.Parse(value.ToString()));
        else if (inType == typeof(string))
            PlayerPrefs.SetString(nameParameter, value.ToString());
    }
}
