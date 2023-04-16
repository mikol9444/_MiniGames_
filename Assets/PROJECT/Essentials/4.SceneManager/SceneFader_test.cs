using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneFader_test : MonoBehaviour
{
    public Image fadeImage;
    public float fadeTime = 1f;
    public static SceneFader_test Instance;

    private void Start()
    {
        StartCoroutine(FadeIn());
        Instance = this;
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    private IEnumerator FadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        Color tempColor = fadeImage.color;
        tempColor.a = 1f;
        fadeImage.color = tempColor;

        while (tempColor.a > 0f)
        {
            tempColor.a -= Time.deltaTime / fadeTime;
            fadeImage.color = tempColor;
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
    }

    private IEnumerator FadeOut(string sceneName)
    {
        fadeImage.gameObject.SetActive(true);
        Color tempColor = fadeImage.color;
        tempColor.a = 0f;
        fadeImage.color = tempColor;

        while (tempColor.a < 1f)
        {
            tempColor.a += Time.deltaTime / fadeTime;
            fadeImage.color = tempColor;
            yield return null;
        }

        SceneManager_test.LoadScene(sceneName);
    }
}
