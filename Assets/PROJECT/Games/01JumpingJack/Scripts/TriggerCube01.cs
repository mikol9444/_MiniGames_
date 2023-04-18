using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.Events;

namespace Minigames._01JumpingJack
{
    [System.Serializable]
    public class JumpEvent : UnityEvent<bool> { }
    public class TriggerCube01 : MonoBehaviour
    {

        public GameObject popupImage;
        public TextMeshProUGUI popupText;
        public string message;
        private bool isPopupActive = false;
        private GameObject playerObject;
        public Color gizmosColor = new Color(0f, 0.35f, 0f, 0.35f);

        void Start()
        {
            playerObject = GameObject.FindGameObjectWithTag("Player");
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && !isPopupActive)
            {
                StopTime();
            }
        }

        public void ResumeTime()
        {
            if (isPopupActive)
            {
                TimeSlower01.Instance.StopAllCoroutines();
                TimeSlower01.Instance.setTimeScale(1f);
                isPopupActive = false;
                popupImage.SetActive(false);
                gameObject.SetActive(false);
            }

        }
        public void StopTime()
        {
            popupText.text = message;
            popupImage.SetActive(true);
            isPopupActive = true;
            Image img = popupImage.GetComponent<Image>();
            TimeSlower01.Instance.StopTime(0f, img);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = gizmosColor;
            Gizmos.DrawCube(transform.position, transform.localScale);
        }

    }
}
