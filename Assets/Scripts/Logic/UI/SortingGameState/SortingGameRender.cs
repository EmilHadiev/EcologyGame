using Cysharp.Threading.Tasks;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SortingGameRender : MonoBehaviour
{
    [SerializeField] private SortingElementView _template;
    [SerializeField] private Button _checkButton;
    [SerializeField] private SortingContainer[] _containers;
    [SerializeField] private SortingElementList _startContainer;

    private const int LongDelay = 15000;
    private const int ShortDelay = 3000;

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
    }

    private void OnDisable()
    {
        _checkButton.onClick.RemoveListener(CheckResult);
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

        Debug.Log(_isWrongAnswerPresent);

        if (_isWrongAnswerPresent)
        {
            TrySetCorrectColors();
            _soundContainer.Play(SoundsName.WrongAnswer);
            SwitchState(LongDelay).Forget();
        }
        else
        {
            _soundContainer.Play(SoundsName.CorrectAnswer);
            _pointsContainer.AddPoints();
            SwitchState(ShortDelay).Forget();
        }  
    }

    private async UniTaskVoid SwitchState(int delay)
    {
        _cts?.Cancel();
        _cts = new CancellationTokenSource();

        await UniTask.Delay(delay, cancellationToken: _cts.Token);
        _menuStateMachine.SwitchState<PrepareState>();
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