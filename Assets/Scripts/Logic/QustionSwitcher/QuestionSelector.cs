using System.Linq;
using Zenject;

public class QuestionSelector : IInitializable, IQuestionSelector
{
    public int CurrentAnswerNumber => _index + 1;
    public int MaxQuestions => _questions.Length;

    public QuestVersion SelectedWrongAnswer { get; set; }

    private int _index;
    private Question[] _questions;

    [Inject]
    private void Constructor(QuestionContainer questionContainer)
    {
        _questions = questionContainer.GetQuestions().ToArray();
    }

    public void Initialize()
    {
        _index = 0;
    }

    public Question GetQuestion()
    {
        if (_index > _questions.Length - 1)
            _index = 0;

        return _questions[_index];
    }

    public void PrepareNextQuestion()
    {
        _index++;
    }
}