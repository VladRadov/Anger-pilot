using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text _crystalCount;
    [SerializeField] private Text _scoreCount;

    public void AddCrystal()
        => ContainerSaveerPlayerPrefs.Instance.SaveerData.GameCrystal += 1;

    public void AddScore()
        => ContainerSaveerPlayerPrefs.Instance.SaveerData.Coins += 1;

    public void UpdateScrystal()
        => _crystalCount.text = ContainerSaveerPlayerPrefs.Instance.SaveerData.GameCrystal.ToString();

    public void UpdateScore()
        => _scoreCount.text = "Score: " + ContainerSaveerPlayerPrefs.Instance.SaveerData.Coins.ToString();

    public void SaveCrystalsOfGame()
    {
        ContainerSaveerPlayerPrefs.Instance.SaveerData.AllCrystal += ContainerSaveerPlayerPrefs.Instance.SaveerData.GameCrystal;
    }

    private void Start()
    {
        ContainerSaveerPlayerPrefs.Instance.SaveerData.GameCrystal = 0;
        ContainerSaveerPlayerPrefs.Instance.SaveerData.Coins = 0;
        UpdateScrystal();
        UpdateScore();
    }
}
