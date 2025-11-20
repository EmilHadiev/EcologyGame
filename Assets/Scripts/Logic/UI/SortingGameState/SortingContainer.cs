using System.Collections.Generic;
using UnityEngine;

public class SortingContainer : MonoBehaviour, IElementPickable
{
    [SerializeField] private SortingElementType _type;

    private readonly List<SortingElementView> _elements = new List<SortingElementView>();

    public Transform Transform => transform;

    public void AddElement(SortingElementView view)
    {
        _elements.Add(view);
    }

    public void RemoveElement(SortingElementView view)
    {
        _elements.Remove(view);
    }

    public void TrySetCorrectColor()
    {
        for (int i = 0; i < _elements.Count; i++)
        {
            var element = _elements[i];

            if (_type != element.Type)
                element.TrySetCorrectColor();
        }
    }

    public bool IsFilledCorrectly()
    {
        if (_elements.Count == 0)
            return false;

        for (int i = 0; i < _elements.Count; i++)
            if (_elements[i].Type != _type)
                return false;

        return true;
    }
}