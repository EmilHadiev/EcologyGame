using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SortingElementView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [field: SerializeField] public SortingElementType Type { get; private set; }
    [SerializeField] private Image _elementImage;
    [SerializeField] private TMP_Text _elementName;
    [SerializeField] private Color[] _colors;

    private Transform _previousParent;
    private Canvas _parentCanvas;
    private CanvasGroup _canvasGroup;
    private int _previousSiblingIndex;

    private void Awake()
    {
        // Получаем корневой Canvas
        _parentCanvas = GetComponentInParent<Canvas>().rootCanvas;
        _canvasGroup = GetComponent<CanvasGroup>();
        if (_canvasGroup == null)
        {
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void SetData(SortingElementData data)
    {
        Type = data.Type;
        _elementImage.sprite = data.Sprite;
        _elementName.text = data.Name;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var currentContainer = GetContainer(eventData);

        _previousParent = transform.parent;
        _previousSiblingIndex = transform.GetSiblingIndex();

        currentContainer?.RemoveElement(this);

        // Перемещаем элемент на верхний уровень Canvas
        transform.SetParent(_parentCanvas.transform);
        transform.SetAsLastSibling(); // Делаем последним в иерархии - будет поверх всех

        // Делаем элемент полупрозрачным и отключаем блокировку лучей
        _canvasGroup.alpha = 0.6f;
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        Debug.Log(GetContainer(eventData));
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Восстанавливаем прозрачность и включаем блокировку лучей
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;

        var container = GetContainer(eventData);

        if (container != null)
        {
            transform.SetParent(container.Transform);
            container.AddElement(this);
        }
        else
        {
            // Возвращаем на предыдущее место с сохранением позиции в иерархии
            transform.SetParent(_previousParent);
            transform.SetSiblingIndex(_previousSiblingIndex);
        }
    }

    private IElementPickable GetContainer(PointerEventData eventData)
    {
        return EventSystem.current.GetFirstComponentUnderPointer<IElementPickable>(eventData);
    }

    public void TrySetCorrectColor()
    {
        _elementName.color = Color.red;
        _elementName.text = $"<S>{_elementName.text}";
    }
}