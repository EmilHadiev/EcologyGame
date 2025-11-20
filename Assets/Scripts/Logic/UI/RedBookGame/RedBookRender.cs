using Cysharp.Threading.Tasks;
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
    }

    private void OnDisable()
    {
        _checkButton.onClick.RemoveListener(CheckResult);
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
        NextState().Forget();
    }

    private bool IsCorrect()
    {
        int countCorrect = _views.Sum(c => Convert.ToInt32(c.IsCorrect));
        int selected = 0;

        for (int i = 0; i < _views.Count; i++)
        {
            if (_views[i].IsRightChoice)
                selected++;
        }

        return countCorrect == selected; ;
    }

    private void TryShowCorrectAnswer()
    {
        for (int i = 0; i < _views.Count; i++)
            _views[i].TryShowCorrectView();
    }

    private async UniTaskVoid NextState()
    {
        await UniTask.Delay(2500);
        _menuStateMachine.SwitchState<PrepareState>();
    }
}