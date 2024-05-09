using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseView : MonoBehaviour
{
    [SerializeField] private Button _continue;
    [SerializeField] private Button _restart;
    [SerializeField] private Button _menu;

    public void SetActive(bool value)
    {
        if(value)
            AudioManager.Instance.PlayClickButton();

        gameObject.SetActive(value);
    }

    private void Start()
    {
        _continue.onClick.AddListener(OnContinue);
        _restart.onClick.AddListener(OnRestart);
        _menu.onClick.AddListener(OnMenu);
    }

    private void OnContinue()
    {
        AudioManager.Instance.PlayClickButton();
        SetActive(false);
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
