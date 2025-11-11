public interface IMenuStateMachine
{
    void SwitchState<T>() where T : Menu;
}