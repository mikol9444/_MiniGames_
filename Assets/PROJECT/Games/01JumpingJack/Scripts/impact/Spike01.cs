using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Minigames._01JumpingJack
{
    public class Spike01 : Impact01
    {
        public override void Impact(Transform other)
        {
            if (AudioManager_Test.Instance)
            {
                AudioManager_Test.Instance.PlaySound("SpikeSound");
            }
            GetComponentInParent<Blinker_test>().StartBlink();
            Debug.LogWarning($"{other.name} Felt into Lava!");
            other.gameObject.SetActive(false);
            GameMaster01.Instance.StartRespawn();
        }

    }

}
