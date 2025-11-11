using UnityEngine;
using Zenject;

public class DataInstaller : MonoInstaller
{
    [SerializeField] private QuestionContainer _questionContainer;

    public override void InstallBindings()
    {
        BindQuestions();
    }

    private void BindQuestions()
    {
        Container.Bind<QuestionContainer>().FromNewScriptableObject(_questionContainer).AsSingle();
    }
}