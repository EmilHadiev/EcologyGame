using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MiniGame/RedBook", fileName = "RedBookContainer")]
public class RedBookContainer : ScriptableObject
{
    [SerializeField] private RebBookInfo[] _redBooks;

    public IReadOnlyCollection<RebBookInfo> GetInfo() => _redBooks;
}