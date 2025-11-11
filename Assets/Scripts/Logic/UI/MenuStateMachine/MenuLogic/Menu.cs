using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    public void Hide() => gameObject.SetActive(false);
    public virtual void Show() => gameObject.SetActive(true);
}