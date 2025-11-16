using Zenject;
using UnityEngine;

public class CorrectAnswerState : AnswerResultState
{
    [SerializeField] private AnswerResultView _view;

    [Inject] readonly private IPointsContainer _pointsContainer;

    public override void Enter()
    {
        base.Enter();
        AddPoints();
    }

    private void AddPoints() => _pointsContainer.AddPoints();

    protected override void PlaySound() => SoundContainer.Play(SoundsName.CorrectAnswer);

    protected override void ShowView() => _view.SetData(GetQuestion());
}