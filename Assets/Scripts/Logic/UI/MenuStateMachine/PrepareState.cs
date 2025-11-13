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
    private const string Question = "Вопрос";

    public override void Enter()
    {
        base.Enter();
        ShowAnswerNumber();
        HideAfterDelay().Forget();
    }

    private void ShowAnswerNumber()
    {
        _prepareText.text = $"{Question} {_selector.CurrentAnswerNumber}";
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