using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RedBookRender : MonoBehaviour
{
    [SerializeField] private RedBookView _template;
    [SerializeField] private Transform _container;
    [SerializeField] private Button _checkButton;
    [SerializeField] private Button _continueButton;

    private const int Delay = 1;

    private RedBookContainer _redBook;
    private IPointsContainer _points;
    private ISoundContainer _sound;
    private IMenuStateMachine _menuStateMachine;

    private List<RedBookView> _views;

    private void Awake()
    {
        CreateTemplates();
    }

    private void OnEnable()
    {
        _checkButton.onClick.AddListener(CheckResult);
        _continueButton.onClick.AddListener(SwitchState);
    }

    private void OnDisable()
    {
        _checkButton.onClick.RemoveListener(CheckResult);
        _continueButton.onClick.RemoveListener(SwitchState);
    }

    [Inject]
    private void Constructor(RedBookContainer redBook, IPointsContainer points, ISoundContainer soundContainer, IMenuStateMachine menuState)
    {
        _redBook = redBook;
        _points = points;
        _sound = soundContainer;
        _menuStateMachine = menuState;
    }

    public void CreateTemplates()
    {
        ArrayShuffler shuffler = new ArrayShuffler();
        var data = shuffler.Shuffle(_redBook.GetInfo());
        _views = new List<RedBookView>(data.Count());

        foreach (var info in data)
        {
            var prefab = Instantiate(_template, _container);
            prefab.Show(info);
            _views.Add(prefab);
            prefab.gameObject.SetActive(true);
        }
    }

    public void Show()
    {
        for (int i = 0; i < _views.Count; i++)
        {
            _views[i].gameObject.SetActive(true);
        }
    }

    private void CheckResult()
    {
        if (IsCorrect())
        {
            _sound.Play(SoundsName.CorrectAnswer);
            _points.AddPoints();
        }
        else
        {
            _sound.Play(SoundsName.WrongAnswer);
        }

        TryShowCorrectAnswer();

        _checkButton.gameObject.SetActive(false);
        PerformButtonAnimation();
    }

    private bool IsCorrect()
    {
        int countCorrect = _views.Sum(c => Convert.ToInt32(c.IsCorrect));
        int selected = 0;

        for (int i = 0; i < _views.Count; i++)
        {
            if (_views[i].IsRightChoice)
                selected++;

            if (_views[i].IsSelected && _views[i].IsRightChoice == false)
                return false;
        }

        return countCorrect == selected;
    }

    private void TryShowCorrectAnswer()
    {
        for (int i = 0; i < _views.Count; i++)
            _views[i].TryShowCorrectView();
    }

    private void SwitchState()
    {
        _menuStateMachine.SwitchState<PrepareState>();
    }

    private void PerformButtonAnimation()
    {
        var scale = _continueButton.transform.localScale;
        float multiplier = 1.25f;
        _continueButton.transform.DOScale(scale * multiplier, Delay).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
}