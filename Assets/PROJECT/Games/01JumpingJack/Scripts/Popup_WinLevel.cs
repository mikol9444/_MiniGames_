using UnityEngine;
using TMPro;
public class Popup_WinLevel : MonoBehaviour
{
    public TextMeshProUGUI tm;
    public Animation anim;
    public string nextSceneName = "01JumpingJack_level01";
    private void Start()
    {
        Jack_Target.OnWinEvent += PopUP;
    }
    private void OnDisable()
    {
        Jack_Target.OnWinEvent -= PopUP;
    }
    public void PopUP()
    {
        tm.gameObject.SetActive(true);
        anim.Play();
    }
    public void NextLevel() => SceneManager_test.LoadSceneWithFade(nextSceneName);
}
