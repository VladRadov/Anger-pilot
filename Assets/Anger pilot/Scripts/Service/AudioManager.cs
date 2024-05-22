using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSourceMusic;
    [SerializeField] private AudioSource _audioSourceSound;
    [SerializeField] private AudioSource _audioSourceSound2;
    [SerializeField] private AudioSource _audioSourceSound3;
    [SerializeField] private List<Sound> _audio;

    public static AudioManager Instance { get; private set; }

    public void ChangeStateMusic()
    {
        if (ContainerSaveerPlayerPrefs.Instance.SaveerData.IsMusicOn == 1)
            _audioSourceMusic.Play();
        else
            _audioSourceMusic.Stop();
    }

    public void ChangeStateSound()
    {
        if (ContainerSaveerPlayerPrefs.Instance.SaveerData.IsSoundOn == 1)
        {
            _audioSourceSound.Play();
            _audioSourceSound2.Play();
            _audioSourceSound3.Play();
        }
        else
        {
            _audioSourceSound.Stop();
            _audioSourceSound2.Stop();
            _audioSourceSound3.Stop();
        }
    }

    public bool IsAudioOn()
        => ContainerSaveerPlayerPrefs.Instance.SaveerData.IsMusicOn == 1 ? true : false;

    public bool IsSoundOn()
        => ContainerSaveerPlayerPrefs.Instance.SaveerData.IsSoundOn == 1 ? true : false;

    public void PlayClickButton() => PlaySound("ClickButton", _audioSourceSound);

    public void PlayMusicMenu() => PlayMusic("Menu", true);

    public void PlayMusicGame() => PlayMusic("Game", true);

    public void PlayWinner() => PlayMusic("Win", false);

    public void PlayGameOver() => PlayMusic("GameOver", false);

    public void PlayGetScore() => PlaySound("GetScore", _audioSourceSound);

    public void PlayGetHealth() => PlaySound("Health", _audioSourceSound);

    public void PlayGetCrystal() => PlaySound("Crystal", _audioSourceSound);

    public void PlayFire() => PlaySound("Fire", _audioSourceSound);

    public void PlayHitBullet() => PlaySound("HitBullet", _audioSourceSound);

    public void PlayGetSun() => PlaySound("GetSun", _audioSourceSound);

    public void PlayErrorEntry() => PlaySound("ErrorEntry", _audioSourceSound);

    public void PlayLaugh() => PlaySound("Laugh", _audioSourceSound2);

    public void PlayGrunt() => PlaySound("Grunt", _audioSourceSound2);

    public void PlayBat() => PlaySound("Bat", _audioSourceSound);

    public void PlayCrashPlane() => PlaySound("CrashPlane", _audioSourceSound2);

    public void PlayWolf() => PlaySound("Wolf", _audioSourceSound3);

    private void PlayMusic(string name, bool isLoop)
    {
        var audio = FindAudio(name);

        if (audio != null && IsAudioOn())
        {
            _audioSourceMusic.clip = audio.Music;
            _audioSourceMusic.Play();
            _audioSourceMusic.loop = isLoop;
        }
    }

    private void SetMusicSource(string name)
    {
        var audio = FindAudio(name);

        if (audio != null)
        {
            _audioSourceMusic.clip = audio.Music;

            if(IsAudioOn())
                _audioSourceMusic.Play();
        }
    }

    private void PlaySound(string name, AudioSource audioSource)
    {
        var audio = FindAudio(name);

        if (audio != null && IsSoundOn())
        {
            audioSource.clip = audio.Music;
            audioSource.Play();
        }
    }

    private Sound FindAudio(string name)
        => _audio.Find(audio => audio.Name == name);

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);

        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        //ContainerSaveerPlayerPrefs.Instance.SaveerData.AllCrystal = 1000;
        if (IsAudioOn() == false)
            _audioSourceMusic.Stop();

        if (IsSoundOn() == false)
        {
            _audioSourceSound.Stop();
            _audioSourceSound2.Stop();
            _audioSourceSound3.Stop();
        }

        SetMusicSource(ManagerScenes.Instance.NameActiveScene);
    }
}