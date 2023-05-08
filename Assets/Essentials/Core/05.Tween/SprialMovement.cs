using System.Collections;
using UnityEngine;

public class SpiralMovement : MonoBehaviour
{
    public float radius = 2f;
    public float speed = 1f;
    public float height = 1f;
    public int numTurns = 2;
    public Vector3 startPosition;

    private Vector3 _center;
    private float _angle = 0f;

    private void Awake()
    {
        _center = startPosition;
    }

    private void Update()
    {
        StartCoroutine(SpiralMove());
    }

    private IEnumerator SpiralMove()
    {
        for (float t = 0; t < numTurns * 2 * Mathf.PI; t += Time.deltaTime * speed)
        {
            float x = Mathf.Cos(t) * radius;
            float y = Mathf.Sin(t) * radius;
            float z = t / (2 * Mathf.PI) * height;
            Vector3 pos = new Vector3(x, y, z) + _center;
            transform.position = pos;
            _angle += Time.deltaTime * speed;
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        _center = startPosition;
        Gizmos.color = Color.yellow;
        for (float t = 0; t < numTurns * 2 * Mathf.PI; t += 0.1f)
        {
            float x = Mathf.Cos(t) * radius;
            float y = Mathf.Sin(t) * radius;
            float z = t / (2 * Mathf.PI) * height;
            Vector3 pos = new Vector3(x, y, z) + _center;
            Gizmos.DrawSphere(pos, 0.1f);
        }
    }
}
