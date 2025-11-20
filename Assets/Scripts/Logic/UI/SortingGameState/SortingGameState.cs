using UnityEngine;

[RequireComponent(typeof(SortingGameRender))]
public class SortingGameState : Menu
{
    [SerializeField] private SortingGameRender _render;

    private void OnValidate()
    {
        _render ??= GetComponent<SortingGameRender>();
    }

    public override void Enter()
    {
        base.Enter();
    }
}