using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private int _time = 30;

    public event Action TimerEnded;

    private int _timeChache;

    private CancellationTokenSource _cts;

    private void OnValidate()
    {
        _slider ??= GetComponent<Slider>();
    }

    private void OnDestroy()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }

    public void StartTimer()
    {
        _slider.value = 1;
        _timeChache = _time;

        CloseToken();
        _cts = new CancellationTokenSource();
        StartCount(_cts).Forget();
    }

    public void StopTimer() => CloseToken();

    private async UniTaskVoid StartCount(CancellationTokenSource cts)
    {
        try
        {
            while (true)
            {
                await UniTask.Delay(1000, cancellationToken: _cts.Token);
                CalculateValue();

                if (_timeChache <= 0)
                {
                    TimerEnded?.Invoke();
                    CloseToken();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void CalculateValue()
    {
        //добавить сюла dotween!
        _timeChache -= 1;
        _slider.value = Convert.ToSingle(_timeChache) / _time;
    }

    private void CloseToken() => _cts?.Cancel();
}