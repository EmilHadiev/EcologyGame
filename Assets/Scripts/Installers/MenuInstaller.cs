using System;
using UnityEngine;
using Zenject;

public class MenuInstaller : MonoInstaller
{
    [SerializeField] private MenuStateMachine _menuStateMachine;
    [SerializeField] private SoundContainer _soundContainer;

    public override void InstallBindings()
    {
        BindMenuStateMachine();
        BindQuestionSelector();
        BindPointsContainer();
        BindSoundContainer();
    }

    private void BindSoundContainer()
    {
        Container.BindInterfacesTo<SoundContainer>().FromComponentInNewPrefab(_soundContainer).AsSingle();
    }

    private void BindPointsContainer()
    {
        Container.BindInterfacesTo<PointsContainer>().AsSingle();
    }

    private void BindQuestionSelector()
    {
        Container.BindInterfacesTo<QuestionSelector>().AsSingle();
    }

    private void BindMenuStateMachine()
    {
        Container.BindInterfacesTo<MenuStateMachine>().FromInstance(_menuStateMachine).AsSingle();
    }
}