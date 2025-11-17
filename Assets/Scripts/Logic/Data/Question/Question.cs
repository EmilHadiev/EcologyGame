using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Question
{
    [TextArea(2,2)] public string Title;
    [SerializeField] private QuestVersion[] _questVersions;
    public IReadOnlyCollection<QuestVersion> QuestVersions => _questVersions;
}