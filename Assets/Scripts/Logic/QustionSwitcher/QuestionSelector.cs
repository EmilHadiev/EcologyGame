using System.Linq;
using Zenject;

public class QuestionSelector : IInitializable, IQuestionSelector
{
    public int CurrentAnswerNumber => _index + 1;
    private int _index;
    private Question[] _questions;

    [Inject]
    private void Constructor(QuestionContainer questionContainer)
    {
        _questions = questionContainer.Questions.ToArray();
    }

    public void Initialize()
    {
        _index = 0;
    }

    public Question GetAnswer()
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