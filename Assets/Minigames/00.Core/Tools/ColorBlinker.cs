using System.Collections;
using UnityEngine;
namespace Essentials
{
    public class ColorBlinker : MonoBehaviour
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
            startColor = GetComponentInChildren<Renderer>().material.color;
        }

        public void StartBlink(GameObject obj)
        {
            if (!isBlinking && gameObject.activeInHierarchy)
            {
                // Start the coroutine
                StartCoroutine(BlinkCoroutine(obj));
            }
        }
        private void OnDisable()
        {
            GetComponentInChildren<Renderer>().material.color = startColor;
            isBlinking = false;
        }
        private IEnumerator BlinkCoroutine(GameObject obj)
        {
            renderers = obj.GetComponentsInChildren<Renderer>();
            startColor = obj.GetComponentInChildren<Renderer>().material.color;
            isBlinking = true;
            Color originalColor = renderers[0].material.color;

            for (int i = 0; i < blinkTimes; i++)
            {
                // Blink on
                foreach (Renderer renderer in renderers)
                {
                    if (renderer)
                        renderer.material.color = blinkColor;
                }

                yield return new WaitForSeconds(blinkDuration);

                // Blink off
                foreach (Renderer renderer in renderers)
                {
                    if (renderer)
                        renderer.material.color = originalColor;
                }

                yield return new WaitForSeconds(blinkDuration);
            }

            isBlinking = false;
        }
    }
}