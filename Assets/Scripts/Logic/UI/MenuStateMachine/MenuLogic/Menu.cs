using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    public void Exit() => gameObject.SetActive(false);
    public virtual void Enter() => gameObject.SetActive(true);
}