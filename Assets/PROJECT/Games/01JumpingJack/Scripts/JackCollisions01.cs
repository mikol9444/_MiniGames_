using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Minigames._01JumpingJack
{
    public interface ICollider
    {
        void OnCollide();
    }
    public class JackCollisions01 : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Interactable"))
            {
                collision.collider.GetComponent<ICollider>().OnCollide();
            }
        }

    }
}