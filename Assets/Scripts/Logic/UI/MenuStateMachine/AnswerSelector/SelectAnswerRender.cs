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

    private QuestionSelector _questionSelector;
    private IMenuStateMachine _stateMachine;

    [Inject]
    private void Constructor(QuestionSelector selector, IMenuStateMachine menuStateMachine)
    {
        _questionSelector = selector;
        _stateMachine = menuStateMachine;
    }

    private const int MaxAnswers = 4;

    private List<AnswerSelector> _selectors;

    private void Awake() => CreateTemplates();

    private void OnEnable()
    {
        for (int i = 0; i < _selectors.Count; i++)
            _selectors[i].Selected += OnAnswerSelected;
    }

    private void OnDisable()
    {
        for (int i = 0; i < _selectors.Count; i++)
            _selectors[i].Selected -= OnAnswerSelected;
    }

    public void Show()
    {
        _questionTitleText.text = _questionSelector.GetAnswer().Title;

        var shuffler = new ArrayShuffler();       
        var selector = shuffler.Shuffle(_questionSelector.GetAnswer().QuestVersions).ToArray();

        for (int i = 0; i < selector.Length; i++)
            _selectors[i].SetQuestion(selector[i]);
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

    private void OnAnswerSelected(bool isCorrect)
    {
        Debug.Log(isCorrect);

        for (int i = 0; i < _selectors.Count; i++)
            _selectors[i].Lock();

        _questionSelector.PrepareNextQuestion();
        _stateMachine.SwitchState<PrepareState>();
    }
}