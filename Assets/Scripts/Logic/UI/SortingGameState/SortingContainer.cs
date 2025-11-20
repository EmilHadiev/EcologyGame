using System.Collections.Generic;
using UnityEngine;

public class SortingContainer : MonoBehaviour, IElementPickable
{
    [SerializeField] private SortingElementType _type;

    private const int MaxSize = 3;

    private readonly List<SortingElementView> _elements = new List<SortingElementView>(MaxSize);

    public Transform Transform => transform;

    public bool IsFilled => _elements.Count >= MaxSize;

    public void AddElement(SortingElementView view)
    {
        if (IsFilled == false)
            _elements.Add(view);
    }

    public void RemoveElement(SortingElementView view)
    {
        _elements.Remove(view);
    }

    public bool IsFilledCorrectly()
    {
        for (int i = 0; i < _elements.Count; i++)
            if (_elements[i].Type != _type)
                return false;

        return true;
    }
}