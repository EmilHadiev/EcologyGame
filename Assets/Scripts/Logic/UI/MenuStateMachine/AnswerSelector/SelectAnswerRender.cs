using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;

public class SelectAnswerRender : MonoBehaviour
{
    [SerializeField] private AnswerSelector _template;
    [SerializeField] private Transform _container;
    [SerializeField] private TMP_Text _questionTitleText;
    [SerializeField] private TMP_Text _questionsCountText;
    [SerializeField] private Timer _timer;

    private IQuestionSelector _questionSelector;
    private IMenuStateMachine _stateMachine;

    private int _countQuestions;

    [Inject]
    private void Constructor(IQuestionSelector selector, IMenuStateMachine stateMachine)
    {
        _questionSelector = selector;
        _stateMachine = stateMachine;
    }

    private const int MaxAnswers = 4;

    private List<AnswerSelector> _selectors;

    private void Awake() => CreateTemplates();

    private void OnEnable()
    {
        for (int i = 0; i < _selectors.Count; i++)
            _selectors[i].Selected += OnAnswerSelected;

        _timer.TimerEnded += OnTimerEnded;
    }

    private void OnDisable()
    {
        for (int i = 0; i < _selectors.Count; i++)
            _selectors[i].Selected -= OnAnswerSelected;

        _timer.TimerEnded -= OnTimerEnded;
    }

    public void Show()
    {
        _timer.StartTimer();

        _questionTitleText.text = _questionSelector.GetQuestion().Title;

        var shuffler = new ArrayShuffler();       
        var selector = shuffler.Shuffle(_questionSelector.GetQuestion().QuestVersions).ToArray();

        for (int i = 0; i < selector.Length; i++)
            _selectors[i].SetQuestion(selector[i], _questionSelector);

        ShowCountQuestions(_questionSelector.CurrentAnswerNumber, _questionSelector.MaxQuestions);
    }

    private void ShowCountQuestions(int currentQuestion, int maxQuestion)
    {
        _questionsCountText.text = $"{currentQuestion} / {maxQuestion}";
    }

    private void CreateTemplates()
    {
        _selectors = new List<AnswerSelector>(MaxAnswers);

        for (int i = 0; i < MaxAnswers; i++)
        {
            var prefab = Instantiate(_template, _container);
            _selectors.Add(prefab);
        }
    }

    private void OnTimerEnded() => OnAnswerSelected(false);

    private void OnAnswerSelected(bool isCorrect)
    {
        Debug.Log(isCorrect);
        LockAnswers();
        StopTimer();
        ShowResult(isCorrect);
    }

    private void LockAnswers()
    {
        for (int i = 0; i < _selectors.Count; i++)
            _selectors[i].Lock();
    }

    private void StopTimer()
    {
        _timer.StopTimer();
    }

    private void ShowResult(bool isCorrect)
    {
        if (isCorrect)
            _stateMachine.SwitchState<CorrectAnswerState>();
        else
            _stateMachine.SwitchState<WrongAnswerState>();
    }
}