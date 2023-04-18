using System.Collections;
using System.Collections.Generic;
using Minigames._01JumpingJack;
using UnityEngine;

namespace Minigames._01JumpingJack
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class Arrow01 : Impact01, ICollider
    {
        public float speed = 3f;
        public Vector3 dir = Vector3.left;
        private void OnEnable()
        {
            GetComponent<Rigidbody>().velocity = dir.normalized * speed;
            Destroy(gameObject, 5f);
        }
        public override void Impact(Transform other)
        {
            other.gameObject.SetActive(false);
            GameMaster01.Instance.StartRespawn();
            Destroy(gameObject);
        }
        public void OnCollide()
        {

        }
    }
}