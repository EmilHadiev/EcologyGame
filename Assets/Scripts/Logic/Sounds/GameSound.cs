using System;
using UnityEngine;

[Serializable]
public struct GameSound
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public AudioClip Clip { get; private set; }
}