using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlower01 : MonoBehaviour
{
    public float slowdownFactor = 0.01f;
    public float timeDuration = 1f; // New variable to specify the duration of the time scale change
    public static TimeSlower01 Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void setTimeScale(float timeScale) => Time.timeScale = timeScale;
    public void StopTime(float targetTimeScale, Image img)
    {
        StartCoroutine(LerpTimeScale(targetTimeScale, img));
    }
    IEnumerator LerpTimeScale(float targetTimeScale, Image img)
    {
        float startTimeScale = Time.timeScale;
        float elapsedTime = 0f;

        while (elapsedTime < timeDuration)
        {
            img.color = new Color(0f, 0f, 0f, Mathf.Lerp(0, 0.85f, elapsedTime / timeDuration));
            Time.timeScale = Mathf.Lerp(Time.timeScale, targetTimeScale, elapsedTime / timeDuration);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = targetTimeScale;
    }
}
