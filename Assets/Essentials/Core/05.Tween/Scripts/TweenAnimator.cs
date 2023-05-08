using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TweenAnimator : MonoBehaviour
{
    public enum MovementType { ToWorldPosition, ToLocalPosition, Circle, Spiral }
    public MovementType movementType = MovementType.ToLocalPosition;

    public bool pingPongActive = false;
    public bool pingAndPong = false;
    [SerializeField] [Range(0.1f, 50f)] private float movementSpeed = 1.5f;
    [SerializeField] [Range(0.1f, 25f)] protected float timeTillDestination = 1.5f;
    [SerializeField] private Vector3 destination = Vector3.up;
    [SerializeField] private Vector3 startPosition = Vector3.up;
    [SerializeField] float circleRadius = 1.5f;

    [SerializeField] private float spiralRadius = 2f;
    [SerializeField] private int spiralFrequency = 2;

    protected Action onStart;
    protected Action onComplete;
    private bool routineInProgress;





    private void Awake()
    {
        onStart = () =>
        {
            Debug.Log($"Tween started on {this.name}!");
            routineInProgress = true;
        };
        onComplete = () =>
        {
            Debug.Log($"Tween completed on {this.name}!");
            routineInProgress = false;
            pingAndPong = !pingAndPong;
            if (pingPongActive)
            {

                StartMovement();
            }
        };
        startPosition = transform.position;
        StartMovement();
    }
    public void SetDestination(Vector3 dest) => destination = dest;
    public void StartMovement()
    {
        if (routineInProgress)
        {
            Debug.LogWarning("Routine in Progress, wait till finished or Press Stop Button");
            return;
        }
        onStart();
        switch (movementType)
        {
            case MovementType.ToWorldPosition:
                StartCoroutine(nameof(MoveToRoutine));
                break;
            case MovementType.ToLocalPosition:
                StartCoroutine(nameof(MoveByRoutine));
                break;
            case MovementType.Circle:
                StartCoroutine(nameof(CircleRoutine));
                break;
            case MovementType.Spiral:
                StartCoroutine(nameof(SpiralCoroutine));
                break;
        }
    }
    public void StopAll()
    {
        StopAllCoroutines();
        onComplete();
    }
    public virtual void PingPong() => pingPongActive = !pingPongActive;
    private IEnumerator MoveToRoutine()
    {
        Vector3 a = transform.position;
        Vector3 b = pingAndPong ? startPosition : destination;
        float timeElapsed = 0f;
        while (timeElapsed < timeTillDestination)
        {
            timeElapsed += Time.deltaTime;
            float t = Mathf.Clamp01(timeElapsed / timeTillDestination);
            transform.position = Vector3.Lerp(a, b, t);
            yield return null;
        }
        onComplete();
    }

    protected virtual IEnumerator MoveByRoutine()
    {
        Vector3 a = transform.position;
        Vector3 b = pingAndPong ? startPosition : transform.position + destination;
        float timeElapsed = 0f;
        while (timeElapsed < timeTillDestination)
        {
            timeElapsed += Time.deltaTime;
            float t = Mathf.Clamp01(timeElapsed / timeTillDestination);
            transform.position = Vector3.Lerp(a, b, t);
            yield return null;
        }
        onComplete();
    }
    private IEnumerator CircleRoutine()
    {
        Vector3 startPosition = transform.position;
        float timeElapsed = 0f;
        float angle = 0f;
        while (pingPongActive || timeElapsed < timeTillDestination)
        {
            timeElapsed += Time.deltaTime;
            angle += Time.deltaTime * 360f / timeTillDestination * movementSpeed;
            float x = startPosition.x + circleRadius * Mathf.Sin(angle * Mathf.Deg2Rad);
            float z = startPosition.z + circleRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
            transform.position = new Vector3(x, transform.position.y, z);

            yield return null;
        }
        transform.position = startPosition;
        onComplete();
    }



    private IEnumerator SpiralCoroutine()
    {
        float timeElapsed = 0f;

        for (float t = 0; t < spiralFrequency * 2 * Mathf.PI; t += Time.deltaTime * movementSpeed)
        {
            timeElapsed += Time.deltaTime;
            float x = Mathf.Cos(t) * spiralRadius;
            float y = Mathf.Sin(t) * spiralRadius;
            float z = t / (2 * Mathf.PI) * circleRadius;
            Vector3 pos = new Vector3(x, y, z) + startPosition;
            transform.position = pos;
            yield return null;
        }
        onComplete();
        transform.position = startPosition;

    }


    private void OnDrawGizmosSelected()
    {
        Vector3 a = transform.position;
        Vector3 b = pingAndPong ? startPosition : destination;
        switch (movementType)
        {
            case MovementType.ToWorldPosition:

                Gizmos.color = Color.red;
                Gizmos.DrawLine(a, b);
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(b, 0.25f);
                break;
            case MovementType.ToLocalPosition:
                b = pingAndPong ? startPosition : startPosition + destination;
                Gizmos.color = Color.red;
                Gizmos.DrawLine(a, b);
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(b, 0.25f);
                break;
            case MovementType.Circle:
                // Draw a wire circle with the specified radius and center at the destination position
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireSphere(startPosition, circleRadius);
                break;
            case MovementType.Spiral:
                Vector3 center = startPosition;
                Gizmos.color = Color.cyan;
                float doublePi = 2 * Mathf.PI;
                float stepSize = 0.1f;
                int numSteps = Mathf.RoundToInt(spiralFrequency * doublePi / stepSize);
                Vector3 prevPos = Vector3.zero;
                for (int i = 0; i <= numSteps; i++)
                {
                    float t = i * stepSize;
                    float x = Mathf.Cos(t) * spiralRadius;
                    float y = Mathf.Sin(t) * spiralRadius;
                    float z = t / doublePi * circleRadius;
                    Vector3 pos = new Vector3(x, y, z) + startPosition;
                    if (i > 0)
                    {
                        Gizmos.DrawLine(prevPos, pos);
                    }
                    prevPos = pos;
                }
                break;

        }
    }
    private void OnValidate()
    {
        startPosition = transform.position;
    }
}
