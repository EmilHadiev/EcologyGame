using Cysharp.Threading.Tasks;
using System.Threading;
using Zenject;
using UnityEngine;
using TMPro;
using System;

public class PrepareState : Menu
{
    [SerializeField] private TMP_Text _prepareText;

    private const int Delay = 1500;
    private const int RedBookGameIndex = 5;

    [Inject] private IMenuStateMachine _menuStateMachine;
    [Inject] private IQuestionSelector _selector;

    private CancellationTokenSource _cts;
    private const string Question = "Вопрос";
    private const string MiniGame = "Мини-игра";

    private bool _isRedBookGameComplete = false;

    private int CurrentAnswerNumber => _selector.CurrentAnswerNumber;

    public override void Enter()
    {
        base.Enter();
        TryShowAnswerNumber();
        HideAfterDelay().Forget();
    }

    private void TryShowAnswerNumber()
    {
        if (IsMiniGameState(RedBookGameIndex, _isRedBookGameComplete))
            _prepareText.text = MiniGame;
        else
            _prepareText.text = $"{Question} {CurrentAnswerNumber}";
    }

    private async UniTaskVoid HideAfterDelay()
    {
        _cts?.Cancel();
        _cts = new CancellationTokenSource();
        await UniTask.Delay(Delay, cancellationToken: _cts.Token);
        TryOpenSelectAnswer();
    }

    private void TryOpenSelectAnswer()
    {
        if (IsMiniGameState(RedBookGameIndex,
            () => _menuStateMachine.SwitchState<RedBookGameState>(),
            ref _isRedBookGameComplete))
            return;
            
        if (_selector.IsAnswersCompleted() == false)
            _menuStateMachine.SwitchState<SelectAnswerState>();
        else
            _menuStateMachine.SwitchState<ResultState>();
    }

    private void OnDestroy()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }

    private bool IsMiniGameState(int miniGameIndex, Action switchState, ref bool isGameComplete)
    {
        if (CurrentAnswerNumber == miniGameIndex && isGameComplete == false)
        {            
            switchState?.Invoke();
            isGameComplete = true;
            return true;
        }

        return false;
    }

    private bool IsMiniGameState(int miniGameIndex, bool isGameComplete)
    {
        return CurrentAnswerNumber == miniGameIndex && isGameComplete == false;
    }
}