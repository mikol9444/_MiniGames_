using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_test : MonoBehaviour
{


    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }

    public static void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
    public static void LoadSceneWithFade(string sceneName)
    {
        SceneFader_test.Instance.FadeToScene(sceneName);
    }
    public static void SwitchScene(string sceneName)
    {
        UnloadScene(SceneManager.GetActiveScene().name);
        LoadScene(sceneName);
    }

    public static void AdditiveLoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    public static void AdditiveUnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public static Scene GetActiveScene()
    {
        return SceneManager.GetActiveScene();
    }

    public static Scene GetSceneByName(string sceneName)
    {
        return SceneManager.GetSceneByName(sceneName);
    }

    public static Scene GetSceneByIndex(int index)
    {
        return SceneManager.GetSceneByBuildIndex(index);
    }
}
