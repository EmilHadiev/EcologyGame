using DG.Tweening;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SortingGameRender : MonoBehaviour
{
    [SerializeField] private SortingElementView _template;
    [SerializeField] private Button _checkButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private SortingContainer[] _containers;
    [SerializeField] private SortingElementList _startContainer;

    private const int Delay = 1;

    private ISoundContainer _soundContainer;
    private IPointsContainer _pointsContainer;
    private IMenuStateMachine _menuStateMachine;
    private SortingElementContainer _container;

    private bool _isChecked = false;

    private CancellationTokenSource _cts;

    private void OnValidate()
    {
        _checkButton ??= GetComponent<Button>();

        if (_containers.Length == 0)
            _containers = GetComponentsInChildren<SortingContainer>();
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

    private void Awake()
    {
        CreateTemplates();
    }

    private void OnDestroy()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }

    private void CreateTemplates()
    {
        var shuffler = new ArrayShuffler();

        var items = shuffler.Shuffle(_container.GetData()).ToArray();

        for (int i = 0; i < items.Length; i++)
        {
            var prefab = Instantiate(_template, _startContainer.transform);
            prefab.SetData(items[i]);
            _startContainer.AddElement(prefab);
        }
    }

    [Inject]
    private void Constructor(IMenuStateMachine menuStateMachine, ISoundContainer soundContainer, IPointsContainer points, SortingElementContainer container)
    {
        _menuStateMachine = menuStateMachine;
        _pointsContainer = points;
        _soundContainer = soundContainer;
        _container = container;
    }

    private void CheckResult()
    {
        if (_isChecked)
            return;

        _isChecked = true;

        bool _isWrongAnswerPresent = TryGetWrongAnswer();

        if (_isWrongAnswerPresent)
        {
            TrySetCorrectColors();
            _soundContainer.Play(SoundsName.WrongAnswer);
        }
        else
        {
            _soundContainer.Play(SoundsName.CorrectAnswer);
            _pointsContainer.AddPoints();
        }

        _checkButton.gameObject.SetActive(false);
        PerformButtonAnimation();
    }

    private void SwitchState()
    {
        _menuStateMachine.SwitchState<PrepareState>();
    }

    private void PerformButtonAnimation()
    {
        var scale = _continueButton.transform.localScale;
        float multiplier = 1.1f;
        _continueButton.transform.DOScale(scale * multiplier, Delay).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    private bool TryGetWrongAnswer()
    {
        for (int i = 0; i < _containers.Length; i++)
        {
            if (_containers[i].IsFilledCorrectly() == false)
            {
                return true;
            }
        }

        return false;
    }
    
    private void TrySetCorrectColors()
    {
        for (int i = 0; i < _containers.Length; i++)
        {
            _containers[i].TrySetCorrectColor();
        }
    }
}