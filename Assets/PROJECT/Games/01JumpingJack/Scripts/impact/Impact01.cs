using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Minigames._01JumpingJack
{
    public class Impact01 : MonoBehaviour
    {
        public LayerMask layerMask;
        public virtual void OnTriggerEnter(Collider other)
        {
            if ((layerMask.value & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
            {
                Impact(other.transform);
            }
        }
        public virtual void Impact(Transform other) { }
    }
}