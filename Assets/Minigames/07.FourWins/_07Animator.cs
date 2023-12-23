using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _07Animator : MonoBehaviour
{    
    [SerializeField] [Range(0.1f, 25f)] protected float timeTillDestination = 1.5f;
       [SerializeField] private Vector3 destination = Vector3.zero;
    
    public void StartMovement(Vector3 dest){
        destination = dest;
        StartCoroutine(MoveToRoutine());
    }
    private IEnumerator MoveToRoutine()
    {
        Vector3 a = transform.position;
        float timeElapsed = 0f;
        while (timeElapsed < timeTillDestination)
        {
            timeElapsed += Time.deltaTime;
            float t = Mathf.Clamp01(timeElapsed / timeTillDestination);
            transform.position = Vector3.Lerp(a, destination, t);
            yield return null;
        }
    }
}
