using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Minigames._01JumpingJack
{
    public class Shooter01 : MonoBehaviour
    {
        public Arrow01 projectile;
        public float repeatRate = 1f;
        public float startShooting = 0f;
        private void Start()
        {
            InvokeRepeating(nameof(ShootArrow), startShooting, repeatRate);
        }


        public void ShootArrow()
        {
            if (projectile == null)
            {
                Debug.LogWarning($"projectile is null in {this}");
                CancelInvoke();
                return;
            }
            AudioManager_Test.Instance.PlaySound("Shoot");
            Instantiate(projectile, transform.position, Quaternion.identity);
        }

    }
}