using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class TimerRunningView : MonoBehaviour
{
    private int _currentTime;

    [SerializeField] private int _duration;
    [SerializeField] private Text _timerView;

    public ReactiveCommand OnEndTimerCommand = new();

    public async void StarTimer()
    {
        gameObject.SetActive(true);
        _currentTime = 0;
        UpdateTimer();
        while (_currentTime <= _duration)
        {
            await Task.Delay(1000);
            _currentTime += 1;
            UpdateTimer();
        }

        OnEndTimerCommand.Execute();
        gameObject.SetActive(false);
    }

    private void UpdateTimer()
    {
        if(_timerView != null)
            _timerView.text = _currentTime.ToString();
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(OnEndTimerCommand);
    }
}
