using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class ButtonBegin : MonoBehaviour
{
    [SerializeField] private Button _button;

    [Inject] private readonly IMenuStateMachine _menuStateMachine;

    private void OnValidate()
    {
        _button ??= GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(HideMainMenu);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(HideMainMenu);
    }

    private void HideMainMenu() => _menuStateMachine.SwitchState<PrepareState>();
}