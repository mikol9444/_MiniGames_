using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonWithEvent : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent onButtonDown;

    public void OnPointerDown(PointerEventData eventData)
    {
        onButtonDown?.Invoke();
    }
}
