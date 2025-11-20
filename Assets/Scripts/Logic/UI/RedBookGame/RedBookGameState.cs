using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(RedBookRender))]
public class RedBookGameState : Menu
{
    [SerializeField] private RedBookRender _render;

    private void OnValidate()
    {
        _render ??= GetComponent<RedBookRender>();
    }

    public override void Enter()
    {
        base.Enter();
        Show();
    }

    private void Show()
    {
        _render.Show();
    }
}