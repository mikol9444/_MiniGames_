using UnityEngine;
using TMPro;
namespace Minigames._01JumpingJack
{
    public class WinLevelPopup01 : MonoBehaviour
    {
        public GameObject winLevelPopup;
        public GameObject nextLevelButtonPopup;
        public GameObject startText;

        private void Start()
        {
            FinishFlag01.OnWinEvent += PopUP;
            //startText.SetActive(true);
        }
        private void OnDisable()
        {
            FinishFlag01.OnWinEvent -= PopUP;
        }
        public void PopUP()
        {
            winLevelPopup.gameObject.SetActive(true);
            nextLevelButtonPopup.gameObject.SetActive(true);

        }

    }
}