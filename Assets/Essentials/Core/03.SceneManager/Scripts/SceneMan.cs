using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneMan : MonoBehaviour
{
    public static SceneMan Instance;
    private int buildIndex;
    public string sceneName;
    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;

        
        buildIndex = SceneManager.GetActiveScene().buildIndex;
    }
    public void ReloadScene() => SceneManager.LoadScene(buildIndex);
    public void NextScene() => SceneManager.LoadScene(buildIndex+1);
    public void PreviousScene() => SceneManager.LoadScene(buildIndex-1);
    public void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);


}
