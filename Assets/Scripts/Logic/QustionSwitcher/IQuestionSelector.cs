public interface IQuestionSelector
{
    int CurrentAnswerNumber { get; }
    int MaxQuestions { get; }

    bool IsAnswersCompleted();

    QuestVersion SelectedWrongAnswer { get; set; }

    Question GetQuestion();

    void Reset();
    void PrepareNextQuestion();
}