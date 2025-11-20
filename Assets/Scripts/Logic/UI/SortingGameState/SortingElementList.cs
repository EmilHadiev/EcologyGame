using System.Collections.Generic;
using UnityEngine;

public class SortingElementList : MonoBehaviour, IElementPickable
{
    private readonly List<SortingElementView> _views = new List<SortingElementView>();

    public Transform Transform => transform;

    public void AddElement(SortingElementView view)
    {
        _views.Add(view);
    }

    public void RemoveElement(SortingElementView view)
    {
        _views.Remove(view);
    }
}