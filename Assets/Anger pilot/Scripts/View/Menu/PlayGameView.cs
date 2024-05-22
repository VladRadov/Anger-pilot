using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGameView : MonoBehaviour
{
    [SerializeField] private Button _startGame;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        _startGame.onClick.AddListener(PlayAnimationCrash);
    }

    private void OnStartGame()
        => ManagerScenes.Instance.LoadAsyncFromCoroutine("Game");

    private void PlayAnimationCrash()
    {
        AudioManager.Instance.PlayClickButton();
        OnStartGame();
    }

    private void OnValidate()
    {
        if (_startGame == null)
            _startGame = GetComponent<Button>();
    }
}
