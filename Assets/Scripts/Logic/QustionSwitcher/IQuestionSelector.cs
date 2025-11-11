public interface IQuestionSelector
{
    int CurrentAnswerNumber { get; }

    Question GetAnswer();
    void PrepareNextQuestion();
}