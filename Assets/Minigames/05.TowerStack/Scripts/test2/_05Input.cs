using UnityEngine;
using UnityEngine.EventSystems;

public class _05Input : MonoBehaviour, IPointerDownHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer Down Event Triggered!");
        FindObjectOfType<_05GameManager01>().OnFire();
    }
}
