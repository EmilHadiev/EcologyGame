using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RedBookView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _background;
    [SerializeField] private Image _animalImage;
    [SerializeField] private TMP_Text _animalNameText;

    public bool IsSelected { get; private set; }

    public bool IsRightChoice => IsCorrect && IsSelected;

    public bool IsCorrect { get; private set; }

    private void Start()
    {
        _background.enabled = false;
        IsSelected = false;
    }

    public void Show(RebBookInfo info)
    {
        IsCorrect = info.IsCorrect;
        _animalImage.sprite = info.Sprite;
        _animalNameText.text = info.Name;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsSelected == false)
        {
            IsSelected = true;
            _background.enabled = true;
        }
        else
        {
            IsSelected = false;
            _background.enabled = false;
        }
    }

    public void TryShowCorrectView()
    {
        _background.enabled = true;

        if (IsCorrect == true && IsSelected == false)
        {
            _background.color = Color.white;
        }
        else if (IsCorrect == false && IsSelected == false)
        {
            _background.color = Color.red;
        }
        else if (IsRightChoice == false)
        {
            _background.color = Color.red;
        }
    }
}