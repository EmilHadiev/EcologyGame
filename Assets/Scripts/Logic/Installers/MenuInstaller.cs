using UnityEngine;
using Zenject;

public class MenuInstaller : MonoInstaller
{
    [SerializeField] private MenuStateMachine _menuStateMachine;

    public override void InstallBindings()
    {
        BindMenuStateMachine();
    }

    private void BindMenuStateMachine()
    {
        Container.BindInterfacesTo<MenuStateMachine>().FromInstance(_menuStateMachine).AsSingle();
    }
}