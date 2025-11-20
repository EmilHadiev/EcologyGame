using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ResultState : Menu
{
    [SerializeField] private TMP_Text _countAnswersText;
    [SerializeField] private Button _restartButton;
    [SerializeField] private TMP_Text _answerResultText;

    private const int CountOfMiniGames = 3;

    private IQuestionSelector _selector;
    private IPointsContainer _pointsContainer;
    private IMenuStateMachine _menuStateMachine;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(StartNextQuestions);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(StartNextQuestions);
    }

    [Inject]
    private void Constructor(IQuestionSelector selector, IPointsContainer pointsContainer, IMenuStateMachine menuStateMachine)
    {
        _selector = selector;
        _pointsContainer = pointsContainer;
        _menuStateMachine = menuStateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        ShowResult();
        ResetQuestions();
    }

    private void ResetQuestions()
    {
        _selector.Reset();
    }

    private void ShowResult()
    {
        _countAnswersText.text = $"{GetCorrectPoints()}/{_selector.MaxQuestions}";
        _answerResultText.text = GetResultText();
    }

    private int GetCorrectPoints()
    {
        int additionalPoint = 1;
        return _pointsContainer.Points + additionalPoint;
    }

    private string GetResultText()
    {
        int points = GetCorrectPoints();
        int maxPoints = _selector.MaxQuestions + CountOfMiniGames;

        if (points >= maxPoints)
            return "Отлично!";
        else if (points >= maxPoints - 3)
            return "Молодец!";
        else if (points >= maxPoints - 6)
            return "Хорошо!";
        else if (points >= maxPoints - 9)
            return "Неплохо!";
        else
            return "Старайся лучше!";
    }

    private void StartNextQuestions()
    {
        _menuStateMachine.SwitchState<PrepareState>();
    }
}