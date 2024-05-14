using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class TimerRunningView : MonoBehaviour
{
    private int _currentTime;
    private bool _isStopped;

    [SerializeField] private int _duration;
    [SerializeField] private Text _timerView;

    public ReactiveCommand OnEndTimerCommand = new();

    public async void StarTimer()
    {
        _isStopped = false;
        gameObject.SetActive(true);
        _currentTime = _duration;
        UpdateTimer();
        while (_currentTime != 0)
        {
            await Task.Delay(1000);
            if (_isStopped == false)
            {
                _currentTime -= 1;
                UpdateTimer();
            }
        }

        OnEndTimerCommand?.Execute();
        gameObject.SetActive(false);
    }

    public void StopTimer()
        => _isStopped = true;

    public void ContinueTimer()
        => _isStopped = false;

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
