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
        if (IsAnswersCompleted())
            return default;

        return _questions[_index];
    }

    public void PrepareNextQuestion() => _index++;

    public void Reset() => _index = 0;

    public bool IsAnswersCompleted() => _index + 1 >= _questions.Length;
}