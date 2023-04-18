using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//

namespace Minigames._01JumpingJack
{
    public class GameMaster01 : MonoBehaviour
    {
        [SerializeField] private Vector3 respawnPosition = Vector3.zero;
        [SerializeField] private float respawnTimer = 2f;

        public static GameMaster01 Instance;
        private Transform playerTransform;
        int currentLevel = 1;
        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
            DontDestroyOnLoad(this);
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            if (playerTransform == null) Debug.LogWarning($"PLAYER TRANSFORM IS NOT ATTACHEDTO {this.name}");
        }
        private void Start()
        {
            _01JumpingJack.FinishFlag01.OnWinEvent += GoalReached;
            InputManager.Instance._inputReader.EnterEvent += NextLevel;
        }
        private void OnDisable()
        {
            _01JumpingJack.FinishFlag01.OnWinEvent -= GoalReached;
            InputManager.Instance._inputReader.EnterEvent -= NextLevel;
        }

        public void StartRespawn()
        {
            StartCoroutine(Respawn(respawnTimer));
        }
        private IEnumerator Respawn(float timer)
        {
            yield return new WaitForSeconds(timer);
            playerTransform.position = respawnPosition;
            playerTransform.gameObject.SetActive(true);
        }
        private void OnDrawGizmosSelected()
        {
            float gizmoSpawnRadius = 0.5f;
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(respawnPosition, gizmoSpawnRadius);
        }

        public void GoalReached()
        {
            StartCoroutine(Goal());
        }
        private IEnumerator Goal()
        {
            InputManager.Instance.Deactivate();
            yield return new WaitForSeconds(1.5f);
            SceneFader_test.Instance.FadeOutPanel();
        }
        public void NextLevel(bool value)
        {
            if (value) SceneManager_test.LoadScene(GenerateJackString(++currentLevel));
        }
        string GenerateJackString(int number)
        {
            string numberString = number.ToString("D2"); // convert the number to a string with 2 digits (e.g. "01", "42")
            return numberString + "Jack";
        }

    }

}
