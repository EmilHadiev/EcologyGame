using UnityEngine;

public class WrongAnswerState : AnswerResultState
{
    [SerializeField] private AnswerResultView _correctView;
    [SerializeField] private AnswerResultView _wrongView;

    protected override void PlaySound()
    {
        SoundContainer.Play(SoundsName.WrongAnswer);
    }

    protected override void ShowView()
    {
        _correctView.SetData(GetQuestion());
        _wrongView.SetData(GetWrongAnswer());
    }
}