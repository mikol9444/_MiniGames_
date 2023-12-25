
using UnityEngine;
using UnityEngine.Events;
public class EventClass : MonoBehaviour
{
    public UnityEvent OnTrigger1;
    public UnityEvent OnTrigger2;

    public void Trigger1()
    {
        OnTrigger1.Invoke();
    }
    public void Trigger2()
    {
        OnTrigger2.Invoke();
    }
}
