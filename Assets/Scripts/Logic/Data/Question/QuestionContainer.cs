using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "QuestionsContainer", fileName = "Questions")]
public class QuestionContainer : ScriptableObject
{
    [SerializeField] private Question[] _questions;

    public IReadOnlyCollection<Question> GetQuestions() => _questions;
}