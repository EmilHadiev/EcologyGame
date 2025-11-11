using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnswerSelector : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Text _answerDescription;

    public bool _isCorrect;
    private bool _isLock;

    public event Action<bool> Selected;

    public void SetQuestion(QuestVersion questVersion)
    {
        UnLock();
        _isCorrect = questVersion.IsCorrect;
        _answerDescription.text = questVersion.Description;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isLock)
            return;

        Selected?.Invoke(_isCorrect);
        
    }

    public void Lock() => _isLock = true;
    private void UnLock() => _isLock = false;
}