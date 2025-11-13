using UnityEngine;

public class MenuStateMachine : MonoBehaviour, IMenuStateMachine
{
    [SerializeField] private Menu[] _menus;

    private void OnValidate()
    {
        if (_menus.Length == 0)
            _menus = FindObjectsOfType<Menu>();
    }

    private void Start()
    {
        SwitchState<MainMenu>();
    }

    public void SwitchState<T>() where T : Menu
    {
        foreach (var menu in _menus)
        {
            if (menu is T)
                menu.Enter();
            else
                menu.Exit();
        }
    }
}