using Cysharp.Threading.Tasks;
using Zenject;

public abstract class AnswerResultState : Menu
{
    private const int Delay = 4000;

    protected ISoundContainer SoundContainer;
    private IMenuStateMachine _stateMachine;
    private IQuestionSelector _selector;

    [Inject]
    private void Constructor(ISoundContainer soundContainer, IMenuStateMachine stateMachine, IQuestionSelector questionSelector)
    {
        SoundContainer = soundContainer;
        _stateMachine = stateMachine;
        _selector = questionSelector;
    }

    public override void Enter()
    {
        base.Enter();
        PlaySound();
        ShowView();
        PrepareNextQuestion();
        SwitchState().Forget();
    }

    protected abstract void PlaySound();
    protected abstract void ShowView();

    protected Question GetQuestion() => _selector.GetQuestion();
    protected QuestVersion GetWrongAnswer() => _selector.SelectedWrongAnswer;

    private async UniTaskVoid SwitchState()
    {
        await UniTask.Delay(Delay);
        _stateMachine.SwitchState<PrepareState>();
    }

    private void PrepareNextQuestion()
    {
        _selector.PrepareNextQuestion();
    }
}