using System.Collections.Generic;
using UnityEngine.EventSystems;

public static class EventSystemExtensions
{
    public static T GetFirstComponentUnderPointer<T>(this EventSystem system, PointerEventData eventData) where T : class
    {
        var result = new List<RaycastResult>();
        system.RaycastAll(eventData, result);

        foreach (var raycast in result)
        {
            if (raycast.gameObject.TryGetComponent<T>(out T component))
            {
                return component;
            }

            T parent = raycast.gameObject.GetComponentInParent<T>();
            if (parent != null)
                return parent;

            T child = raycast.gameObject.GetComponentInChildren<T>();
            if (child != null)
                return child;
        }

        return null;
    }

}