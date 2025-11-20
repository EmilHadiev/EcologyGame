using UnityEngine;

public interface IElementPickable
{
    public Transform Transform { get; }

    void AddElement(SortingElementView view);
    void RemoveElement(SortingElementView view);
}