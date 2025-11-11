using System;
using UnityEngine;

[Serializable]
public struct QuestVersion
{
    public bool IsCorrect;
    [TextArea(2, 2)] public string Description;
}