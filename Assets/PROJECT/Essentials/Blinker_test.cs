using System.Collections;
using UnityEngine;

public class Blinker_test : MonoBehaviour
{
    public Color blinkColor = Color.white;
    public float blinkDuration = 0.1f;
    public int blinkTimes = 3;

    private bool isBlinking = false;
    private Renderer[] renderers;
    private Color startColor;
    private void Awake()
    {
        // Get all the renderers in the object and its children
        renderers = GetComponentsInChildren<Renderer>();
        startColor = GetComponent<Renderer>().material.color;
    }

    public void StartBlink()
    {
        if (!isBlinking && gameObject.activeInHierarchy)
        {
            // Start the coroutine
            StartCoroutine(BlinkCoroutine());
        }
    }
    private void OnDisable()
    {
        GetComponent<Renderer>().material.color = startColor;
        isBlinking = false;
    }
    private IEnumerator BlinkCoroutine()
    {
        isBlinking = true;
        Color originalColor = renderers[0].material.color;

        for (int i = 0; i < blinkTimes; i++)
        {
            // Blink on
            foreach (Renderer renderer in renderers)
            {
                renderer.material.color = blinkColor;
            }

            yield return new WaitForSeconds(blinkDuration);

            // Blink off
            foreach (Renderer renderer in renderers)
            {
                renderer.material.color = originalColor;
            }

            yield return new WaitForSeconds(blinkDuration);
        }

        isBlinking = false;
    }
}
