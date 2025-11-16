using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnswerSelector : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Text _answerDescription;

    private const float Duration = 0.2f;
    private const float ScaleMultiplier = 1.1f;

    private Vector3 _scale;
    private IQuestionSelector _selector;
    private QuestVersion _answer;

    private bool _isCorrect;
    private bool _isLock;

    public event Action<bool> Selected;

    private void Awake()
    {
        _scale = transform.localScale;
    }

    public void SetQuestion(QuestVersion questVersion, IQuestionSelector selector)
    {
        UnLock();

        _answer = questVersion;
        _isCorrect = _answer.IsCorrect;
        _selector = selector;
        _answerDescription.text = _answer.Description;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isLock)
            return;

        TrySetWrongAnswer();
        PerformAnimation();               
    }

    public void Lock() => _isLock = true;
    private void UnLock() => _isLock = false;

    private void PerformAnimation()
    {
        transform.DOScale(_scale * ScaleMultiplier, Duration).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo).OnComplete(() => OnAnimationCompleted());
    }

    private void OnAnimationCompleted()
    {
        Selected?.Invoke(_isCorrect);
    }

    private void TrySetWrongAnswer()
    {
        if (_isCorrect == false)
            _selector.SelectedWrongAnswer = _answer;
    }
}