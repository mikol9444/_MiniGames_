using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Minigames._01JumpingJack
{
    public abstract class Interactable01 : MonoBehaviour
    {
        //Call this function if from other Ontrigger / OnCollision script
        public abstract void OnCollide();
        public virtual void Awake() { }
    }
}