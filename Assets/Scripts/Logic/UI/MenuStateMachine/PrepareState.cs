using Cysharp.Threading.Tasks;
using System.Threading;
using Zenject;
using UnityEngine;
using TMPro;

public class PrepareState : Menu
{
    [SerializeField] private TMP_Text _prepareText;

    private const int Delay = 1500;

    [Inject] private IMenuStateMachine _menuStateMachine;
    [Inject] private IQuestionSelector _selector;

    private CancellationTokenSource _cts;

    public override void Show()
    {
        base.Show();
        ShowAnswerNumber();
        HideAfterDelay().Forget();
    }

    private void ShowAnswerNumber()
    {
        string text = _prepareText.text;
        text += $" {_selector.CurrentAnswerNumber}";
        _prepareText.text = text;
    }

    private async UniTaskVoid HideAfterDelay()
    {
        _cts?.Cancel();
        _cts = new CancellationTokenSource();
        await UniTask.Delay(Delay, cancellationToken: _cts.Token);
        OpenSelectAnswer();
    }

    private void OpenSelectAnswer()
    {
        _menuStateMachine.SwitchState<SelectAnswerState>();
    }

    private void OnDestroy()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }
}