using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SmoothPosition : MonoBehaviour
{
    public static SmoothPosition Instance;
    float timeTmp = 0;
    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }
    public void StartMoveTo(Transform transform, Vector3 targetPosition, float duration) => StartCoroutine(MoveTo(transform, targetPosition, duration));
    public void StartMoveBy(Transform transform, Vector3 offset, float duration) => StartCoroutine(MoveBy(transform, offset, duration));
    public void StartMoveInCircle(Transform transform, Vector3 center, float radius, float speed, float duration) => StartCoroutine(MoveInCircle(transform, center, radius, speed, duration));
    public void StartMoveInSpiral(Transform transform, Vector3 center, float radius, float spiralFactor, float speed, float duration) => StartCoroutine(MoveInSpiral(transform, center, radius, spiralFactor, speed, duration));
    public void StartMoveInEight(Transform transform, Vector3 center, float radius, float speed, float duration) => StartCoroutine(MoveInEight(transform, center, radius, speed, duration));
    public IEnumerator MoveTo(Transform transform, Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = transform.position;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        transform.position = targetPosition;
    }

    public IEnumerator MoveBy(Transform transform, Vector3 dir, float duration)
    {
        Vector3 targetPosition = transform.position + dir;
        yield return MoveTo(transform, targetPosition, duration);
    }

    public IEnumerator MoveInCircle(Transform transform, Vector3 dir, float radius, float speed, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float angle = time * speed;
            Vector3 offset = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * radius;
            transform.position += (offset + dir) * Time.deltaTime * speed;
            yield return null;
        }

    }

    public IEnumerator MoveInSpiral(Transform transform, Vector3 dir, float radius, float spiralFactor, float speed, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float angle = time * speed;
            float x = Mathf.Cos(angle) * (radius + angle * spiralFactor);
            float y = Mathf.Sin(angle) * (radius + angle * spiralFactor);
            float z = Mathf.Sin(angle) * (radius + angle * spiralFactor);
            Vector3 offset = new Vector3(x, y, z);
            transform.position += (offset + dir) * Time.deltaTime * speed;
            yield return null;
        }

    }

    public IEnumerator MoveInEight(Transform transform, Vector3 dir, float radius, float speed, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float angle = time * speed;
            Vector3 offset = new Vector3(Mathf.Sin(angle) * radius, Mathf.Cos(angle * 2) * radius, Mathf.Sin(angle * 3) * radius);
            transform.position += (offset + dir) * Time.deltaTime * speed;
            yield return null;
        }
    }

}
