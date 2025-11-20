using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MiniGame/Sorting", fileName = "SortingContainer")]
public class SortingElementContainer : ScriptableObject
{
    [SerializeField] private SortingElementData[] _data;

    public IReadOnlyCollection<SortingElementData> GetData() => _data;
}