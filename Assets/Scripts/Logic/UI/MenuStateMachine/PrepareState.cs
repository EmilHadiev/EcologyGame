using Cysharp.Threading.Tasks;
using System.Threading;
using Zenject;

public class PrepareState : Menu
{
    private const int Delay = 1500;

    [Inject] private IMenuStateMachine _menuStateMachine;

    private CancellationTokenSource _cts;

    public override void Show()
    {
        base.Show();
        HideAfterDelay().Forget();
    }

    private async UniTaskVoid HideAfterDelay()
    {
        _cts?.Cancel();
        _cts = new CancellationTokenSource();
        await UniTask.Delay(Delay, cancellationToken: _cts.Token);
        OpenMenu();
    }

    private void OpenMenu()
    {
        _menuStateMachine.SwitchState<MainMenu>();
    }

    private void OnDestroy()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }
}