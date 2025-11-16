public interface IQuestionSelector
{
    int CurrentAnswerNumber { get; }
    int MaxQuestions { get; }

    QuestVersion SelectedWrongAnswer { get; set; }

    Question GetQuestion();
    void PrepareNextQuestion();
}