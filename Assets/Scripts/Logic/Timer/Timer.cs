using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private int _time = 30;

    private const float AnimationDuration = 0.95f;

    private int _timeChache;
    private CancellationTokenSource _cts;

    public event Action TimerEnded;

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

        CountPerformer();
    }

    private void CountPerformer()
    {
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
        _timeChache -= 1;
        var endValue = Convert.ToSingle(_timeChache) / _time;
        _slider.DOValue(endValue, AnimationDuration);
    }

    private void CloseToken() => _cts?.Cancel();
}