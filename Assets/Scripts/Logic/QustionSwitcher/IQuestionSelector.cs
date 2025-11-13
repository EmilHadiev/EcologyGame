public interface IQuestionSelector
{
    int CurrentAnswerNumber { get; }
    int MaxQuestions { get; }

    Question GetAnswer();
    void PrepareNextQuestion();
}