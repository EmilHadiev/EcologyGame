using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SortingGameRender : MonoBehaviour
{
    [SerializeField] private SortingElementView _template;
    [SerializeField] private Button _checkButton;
    [SerializeField] private SortingContainer[] _containers;
    [SerializeField] private SortingElementList _startContainer;

    private ISoundContainer _soundContainer;
    private IPointsContainer _pointsContainer;
    private IMenuStateMachine _menuStateMachine;
    private SortingElementContainer _container;

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
        for (int i = 0; i < _containers.Length; i++)
        {
            if (_containers[i].IsFilledCorrectly() == false)
            {
                _soundContainer.Play(SoundsName.WrongAnswer);
                return;
            }
        }

        _soundContainer.Play(SoundsName.CorrectAnswer);
        _pointsContainer.AddPoints();
        _menuStateMachine.SwitchState<PrepareState>();
    }
}