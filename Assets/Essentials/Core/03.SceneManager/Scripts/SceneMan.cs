using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneMan : MonoBehaviour
{
    public static SceneMan Instance;
    private Animator anim;
    public GameObject faderPanel;
    private int buildIndex;
    public string sceneName;
    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        buildIndex = SceneManager.GetActiveScene().buildIndex;
    }
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
       if(faderPanel) faderPanel.gameObject?.SetActive(true);
        OnStart();
    }
    public void ReloadScene() => SceneManager.LoadScene(buildIndex);
    public void NextScene() => SceneManager.LoadScene(buildIndex+1);
    public void PreviousScene() => SceneManager.LoadScene(buildIndex-1);
    public void LoadScene(string sceneName)
    {
        this.sceneName = sceneName;
        OnEndScene();
    }

    public void SetSceneName(string nameofTheScene) => sceneName = nameofTheScene;
    public void SwitchScene() => SceneManager.LoadScene(sceneName);
    private void OnStart() => anim.SetTrigger("start");
    public  void OnEndScene() => anim.SetTrigger("end");
    public void StartReloadScene() => anim.SetTrigger("reload");
}
