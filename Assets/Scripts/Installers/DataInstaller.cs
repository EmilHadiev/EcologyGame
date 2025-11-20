using UnityEngine;
using Zenject;

public class DataInstaller : MonoInstaller
{
    [SerializeField] private QuestionContainer _questionContainer;
    [SerializeField] private RedBookContainer _redBook;
    [SerializeField] private SortingElementContainer _sortingElementContainer;

    public override void InstallBindings()
    {
        BindQuestions();
        BindRedBook();
        BindSorting();
    }

    private void BindSorting()
    {
        Container.Bind<SortingElementContainer>().FromNewScriptableObject(_sortingElementContainer).AsSingle();
    }

    private void BindRedBook()
    {
        Container.Bind<RedBookContainer>().FromNewScriptableObject(_redBook).AsSingle();
    }

    private void BindQuestions()
    {
        Container.Bind<QuestionContainer>().FromNewScriptableObject(_questionContainer).AsSingle();
    }
}