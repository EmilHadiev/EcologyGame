using UnityEngine;
using Zenject;

public class MenuInstaller : MonoInstaller
{
    [SerializeField] private MenuStateMachine _menuStateMachine;

    public override void InstallBindings()
    {
        BindMenuStateMachine();
        BindQuestionSelector();
    }

    private void BindQuestionSelector()
    {
        Container.BindInterfacesAndSelfTo<QuestionSelector>().AsSingle();
    }

    private void BindMenuStateMachine()
    {
        Container.BindInterfacesTo<MenuStateMachine>().FromInstance(_menuStateMachine).AsSingle();
    }
}