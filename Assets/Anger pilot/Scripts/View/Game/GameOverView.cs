using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour
{
    [SerializeField] private Button _restart;
    [SerializeField] private Button _menu;
    [SerializeField] private Text _scoreView;
    [SerializeField] private Text _crystalView;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void Start()
    {
        _restart.onClick.AddListener(OnRestart);
        _menu.onClick.AddListener(OnMenu);
        _scoreView.text = "Score: " + ContainerSaveerPlayerPrefs.Instance.SaveerData.Coins.ToString();
        _crystalView.text = ContainerSaveerPlayerPrefs.Instance.SaveerData.GameCrystal.ToString();
    }

    private void OnRestart()
    {
        AudioManager.Instance.PlayClickButton();
        ManagerScenes.Instance.LoadAsyncFromCoroutine("Game");
    }

    private void OnMenu()
    {
        AudioManager.Instance.PlayClickButton();
        ManagerScenes.Instance.LoadAsyncFromCoroutine("Menu");
    }
}
