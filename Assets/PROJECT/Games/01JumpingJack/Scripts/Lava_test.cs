using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava_test : MonoBehaviour
{
    public float playerRespawnTimer = 1.5f;
    public LayerMask layerMask;

    private void OnTriggerEnter(Collider other)
    {


        if ((layerMask.value & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
        {
            if (AudioManager_Test.instance)
            {
                AudioManager_Test.instance.PlaySound("Lava");
            }
            other.gameObject.SetActive(false);
            GetComponentInParent<Blinker_test>().StartBlink();
            Debug.LogWarning($"{other.name} Felt into Lava!");
            StartCoroutine(Respawn(playerRespawnTimer, other.transform));
        }
    }
    IEnumerator Respawn(float timer, Transform playerTransform)
    {
        yield return new WaitForSeconds(timer);
        playerTransform.position = new Vector3(0f, 0f, 0f);
        playerTransform.gameObject.SetActive(true);
    }
}
