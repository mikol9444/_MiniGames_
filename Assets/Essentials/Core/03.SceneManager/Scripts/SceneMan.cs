using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
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


        OnStart();
    }
    public void ReloadScene() => SceneManager.LoadScene(buildIndex);
    private void NextScene() => SceneManager.LoadScene(buildIndex + 1);
    private void PreviousScene() => SceneManager.LoadScene(buildIndex - 1);
    public void LoadScene(string sceneName)
    {
        this.sceneName = sceneName;
        OnEndScene();
    }

    public void SetSceneName(string nameofTheScene) => sceneName = nameofTheScene;
    private void SwitchScene() => SceneManager.LoadScene(sceneName);
    private void OnStart()
    {
        if (!faderPanel) { Debug.LogWarning("Faderpanel not set"); return; }
        faderPanel.gameObject?.SetActive(true);
        anim = GetComponentInChildren<Animator>();
        anim.SetTrigger("start");
    }
    public void OnEndScene() => anim.SetTrigger("end");
    public void StartReloadScene() => anim.SetTrigger("reload"); //
    public void ReturnToMenu() => LoadScene("startmenu");
}
