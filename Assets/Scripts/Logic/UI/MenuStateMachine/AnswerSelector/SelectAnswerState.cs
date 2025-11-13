using UnityEngine;

[RequireComponent(typeof(SelectAnswerRender))]
public class SelectAnswerState : Menu
{
    [SerializeField] private SelectAnswerRender _render;

    private void OnValidate()
    {
        _render ??= GetComponent<SelectAnswerRender>();
    }

    public override void Enter()
    {
        base.Enter();
        _render.Show();
    }
}