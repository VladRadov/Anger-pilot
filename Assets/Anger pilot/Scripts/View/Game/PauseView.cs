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
        => gameObject.SetActive(value);

    private void Start()
    {
        _continue.onClick.AddListener(OnContinue);
        _restart.onClick.AddListener(OnRestart);
        _menu.onClick.AddListener(OnMenu);
    }

    private void OnContinue()
    {
        SetActive(false);
    }

    private void OnRestart()
    {
        ManagerScenes.Instance.LoadAsyncFromCoroutine("Game");
    }

    private void OnMenu()
    {
        ManagerScenes.Instance.LoadAsyncFromCoroutine("Menu");
    }
}
